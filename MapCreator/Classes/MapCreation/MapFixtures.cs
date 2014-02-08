using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ImageMagick;
using MapCreator.Fixtures;
using System.Diagnostics;

namespace MapCreator
{
    class MapFixtures : IDisposable
    {
        private ZoneConfiguration zoneConfiguration;
        private List<RiverConfiguration> rivers;

        private List<DrawableFixture> m_fixtures = new List<DrawableFixture>();

        private List<DrawableFixture> m_fixturesUnderWater = new List<DrawableFixture>();
        private List<DrawableFixture> m_fixturesAboveWater = new List<DrawableFixture>();

        private Dictionary<string, MagickImage> m_modelImages = new Dictionary<string, MagickImage>();

        #region Settings

        bool m_drawFixtures = true;
        public bool DrawFixtures
        {
            get { return m_drawFixtures; }
            set { m_drawFixtures = value; }
        }

        bool m_drawTrees = true;
        public bool DrawTrees
        {
            get { return m_drawTrees; }
            set { m_drawTrees = value; }
        }

        bool m_drawTreesAsImages = true;
        public bool DrawTreesAsImages
        {
            get { return m_drawTreesAsImages; }
            set { m_drawTreesAsImages = value; }
        }

        #endregion

        public MapFixtures(ZoneConfiguration zoneConfiguration, List<RiverConfiguration> rivers)
        {
            this.zoneConfiguration = zoneConfiguration;
            this.rivers = rivers;

            // Load Renderer Configurations

            // Initialize the fixtures loader, loads CSV files and polygons
            FixturesLoader.Initialize(zoneConfiguration);

            // Prepare models
            m_fixtures = FixturesLoader.GetDrawableFixtures();

            // Sort Fixtures by under/above water
            SortFixtures();
        }

        public void SortFixtures()
        {
            if (m_fixtures.Count == 0) return;

            // Create paths out of the rivers
            Dictionary<RiverConfiguration, System.Drawing.Drawing2D.GraphicsPath> riverPaths = new Dictionary<RiverConfiguration, System.Drawing.Drawing2D.GraphicsPath>();
            foreach (RiverConfiguration rConf in rivers)
            {
                System.Drawing.Drawing2D.GraphicsPath riverPath = new System.Drawing.Drawing2D.GraphicsPath();
                System.Drawing.PointF[] points = rConf.GetCoordinates().Select(c => new System.Drawing.PointF(Convert.ToSingle(c.X * zoneConfiguration.MapScale), Convert.ToSingle(c.Y * zoneConfiguration.MapScale))).ToArray();
                riverPath.AddPolygon(points);
                riverPaths.Add(rConf, riverPath);
            }

            foreach (DrawableFixture model in m_fixtures)
            {
                double modelCenterX = zoneConfiguration.ZoneCoordinateToMapCoordinate(model.FixtureRow.X);
                double modelCenterY = zoneConfiguration.ZoneCoordinateToMapCoordinate(model.FixtureRow.Y);

                // Check if on river or not
                int riverHeight = 0;
                foreach (KeyValuePair<RiverConfiguration, System.Drawing.Drawing2D.GraphicsPath> river in riverPaths)
                {
                    if (river.Value.IsVisible(Convert.ToSingle(modelCenterX), Convert.ToSingle(modelCenterY)));
                    {
                        riverHeight = river.Key.Height;
                        break;
                    }
                }

                if (riverHeight == 0 || (riverHeight != 0 && model.FixtureRow.Z > riverHeight))
                {
                    m_fixturesAboveWater.Add(model);
                }
                else
                {
                    m_fixturesUnderWater.Add(model);
                }
            }

            m_fixturesAboveWater = m_fixturesAboveWater.OrderBy(f => f.CanvasZ).ToList();
            m_fixturesUnderWater = m_fixturesUnderWater.OrderBy(f => f.CanvasZ).ToList();

            // Dispose all paths
            riverPaths.Select(d => d.Value).ToList().ForEach(r => r.Dispose());
        }

        public void Draw(MagickImage map, bool underwater)
        {
            if (underwater)
            {
                MainForm.Log("Drawing fixtures under water level...", MainForm.LogLevel.notice);
                Draw(map, m_fixturesUnderWater);
            }
            else
            {
                MainForm.Log("Drawing fixtures above water level...", MainForm.LogLevel.notice);
                Draw(map, m_fixturesAboveWater);
            }
        }

        private void Draw(MagickImage map, List<DrawableFixture> fixtures)
        {
            MainForm.ProgressStart("Drawing models ...");
            Stopwatch timer = Stopwatch.StartNew();

            using (MagickImage modelsOverlay = new MagickImage(MagickColor.Transparent, zoneConfiguration.TargetMapSize, zoneConfiguration.TargetMapSize))
            {
                int processCounter = 0;
                foreach (DrawableFixture fixture in fixtures)
                {
                    switch (fixture.RendererConf.Renderer)
                    {
                        case FixtureRenderererType.Shaded:
                            DrawShaded(modelsOverlay, fixture);
                            break;
                        case FixtureRenderererType.Flat:
                            DrawFlat(modelsOverlay, fixture);
                            break;
                        case FixtureRenderererType.Image:
                            DrawImage(modelsOverlay, fixture);
                            break;
                    }

                    int percent = 100 * processCounter / fixtures.Count();
                    MainForm.ProgressUpdate(percent);
                    processCounter++;
                }

                MainForm.Log("Merging ....");
                MainForm.ProgressStartMarquee("Merging ...");
                map.Composite(modelsOverlay, 0, 0, CompositeOperator.SrcOver);
            }

            timer.Stop();
            MainForm.Log(string.Format("Finished drawing models in {0} seconds.", timer.Elapsed.TotalSeconds), MainForm.LogLevel.success);
        }

        private void DrawShaded(MagickImage overlay, DrawableFixture fixture)
        {
            MainForm.Log(string.Format("Shaded: {0} ({1}) ...", fixture.Name, fixture.NifName), MainForm.LogLevel.notice);

            using (MagickImage modelCanvas = new MagickImage(MagickColor.Transparent, fixture.CanvasWidth, fixture.CanvasHeight))
            {
                foreach (DrawableElement drawableElement in fixture.DrawableElements)
                {
                    using (DrawablePolygon polyDraw = new DrawablePolygon(drawableElement.coordinates))
                    {
                        // A Shaded model without lightning is not shaded... but just we add this just be flexible
                        if (fixture.RendererConf.HasLight)
                        {
                            modelCanvas.FillColor = new MagickColor(
                                Convert.ToSingle(drawableElement.lightning * fixture.ModelColor.R),
                                Convert.ToSingle(drawableElement.lightning * fixture.ModelColor.G),
                                Convert.ToSingle(drawableElement.lightning * fixture.ModelColor.B)
                            );
                        }
                        else
                        {
                            modelCanvas.FillColor = fixture.ModelColor;
                        }

                        modelCanvas.Draw(polyDraw);
                    }
                }

                if (fixture.RendererConf.HasShadow)
                {
                    modelCanvas.BorderColor = MagickColor.Transparent;
                    modelCanvas.Border(1);
                    modelCanvas.Shadow(
                        fixture.RendererConf.ShadowOffsetX, 
                        fixture.RendererConf.ShadowOffsetY, 
                        fixture.RendererConf.ShadowSize,
                        new Percentage(100 - fixture.RendererConf.ShadowTransparency),
                        fixture.RendererConf.ShadowColor
                    );
                }

                if (fixture.RendererConf.Transparency != 0)
                {
                    modelCanvas.Alpha(AlphaOption.Set);

                    double divideValue = 100.0 / (100.0 - fixture.RendererConf.Transparency);
                    modelCanvas.Evaluate(Channels.Alpha, EvaluateOperator.Divide, divideValue);
                }

                overlay.Composite(modelCanvas, Convert.ToInt32(fixture.CanvasX), Convert.ToInt32(fixture.CanvasY), CompositeOperator.SrcOver);
            }
        }

        private void DrawFlat(MagickImage overlay, DrawableFixture fixture)
        {
            MainForm.Log(string.Format("Flat: {0} ({1}) ...", fixture.Name, fixture.NifName), MainForm.LogLevel.notice);

            using (MagickImage modelCanvas = new MagickImage(MagickColor.Transparent, fixture.CanvasWidth, fixture.CanvasHeight))
            {
                modelCanvas.FillColor = fixture.ModelColor;

                foreach (DrawableElement drawableElement in fixture.DrawableElements)
                {
                    using (DrawablePolygon polyDraw = new DrawablePolygon(drawableElement.coordinates))
                    {
                        modelCanvas.Draw(polyDraw);
                    }
                }

                if (fixture.RendererConf.HasShadow)
                {
                    modelCanvas.BorderColor = MagickColor.Transparent;
                    modelCanvas.Border(1);
                    modelCanvas.Shadow(
                        fixture.RendererConf.ShadowOffsetX,
                        fixture.RendererConf.ShadowOffsetY,
                        fixture.RendererConf.ShadowSize,
                        new Percentage(100 - fixture.RendererConf.ShadowTransparency),
                        fixture.RendererConf.ShadowColor
                    );
                }

                if (fixture.RendererConf.Transparency != 0)
                {
                    modelCanvas.Alpha(AlphaOption.Set);

                    double divideValue = 100.0 / (100.0 - fixture.RendererConf.Transparency);
                    modelCanvas.Evaluate(Channels.Alpha, EvaluateOperator.Divide, divideValue);
                }

                overlay.Composite(modelCanvas, Convert.ToInt32(fixture.CanvasX), Convert.ToInt32(fixture.CanvasY), CompositeOperator.SrcOver);
            }
        }

        private void DrawImage(MagickImage overlay, DrawableFixture fixture)
        {
            //MainForm.Log(string.Format("Image: {0} ({1}) ...", fixture.Name, fixture.NifName), MainForm.LogLevel.notice);
            string fileName = System.IO.Path.GetFileNameWithoutExtension(fixture.NifName);

            if (!m_modelImages.ContainsKey(fileName))
            {
                string treeImageFile = string.Format("{0}\\data\\prerendered\\{1}.png", System.Windows.Forms.Application.StartupPath, fileName);
                if (System.IO.File.Exists(treeImageFile))
                {
                    MagickImage treeImage = new MagickImage(treeImageFile);

                    if (fixture.IsTree)
                    {
                        treeImage.Blur();
                    }

                    m_modelImages.Add(fileName, treeImage);
                }
                else
                {
                    m_modelImages.Add(fileName, null);
                }
            }

            if (m_modelImages.ContainsKey(fileName) && m_modelImages[fileName] != null)
            {
                using (MagickImage newTree = m_modelImages[fileName].Clone())
                {
                    newTree.BackgroundColor = MagickColor.Transparent;
                    newTree.Rotate(fixture.FixtureRow.A);
                    newTree.Trim();
                    newTree.Resize(fixture.CanvasWidth, fixture.CanvasHeight);

                    using (MagickImage modelCanvas = new MagickImage(MagickColor.Transparent, fixture.CanvasWidth, fixture.CanvasHeight))
                    {
                        foreach (DrawableElement drawableElement in fixture.DrawableElements)
                        {
                            modelCanvas.FillColor = new MagickColor(
                                Convert.ToSingle(128 * 256 * drawableElement.lightning),
                                Convert.ToSingle(128 * 256 * drawableElement.lightning),
                                Convert.ToSingle(128 * 256 * drawableElement.lightning)
                            );

                            using (DrawablePolygon polyDraw = new DrawablePolygon(drawableElement.coordinates))
                            {
                                modelCanvas.Draw(polyDraw);
                            }
                        }

                        modelCanvas.Composite(newTree, 0, 0, CompositeOperator.DstIn);
                        newTree.Composite(modelCanvas, 0, 0, CompositeOperator.Overlay);
                    }

                    if (fixture.RendererConf.HasShadow)
                    {
                        newTree.BorderColor = MagickColor.Transparent;
                        newTree.Border(1);
                        newTree.Shadow(
                            fixture.RendererConf.ShadowOffsetX,
                            fixture.RendererConf.ShadowOffsetY,
                            fixture.RendererConf.ShadowSize,
                            new Percentage(100 - fixture.RendererConf.ShadowTransparency),
                            fixture.RendererConf.ShadowColor
                        );
                    }

                    if (fixture.RendererConf.Transparency != 0)
                    {
                        newTree.Alpha(AlphaOption.Set);

                        double divideValue = 100.0 / (100.0 - fixture.RendererConf.Transparency);
                        newTree.Evaluate(Channels.Alpha, EvaluateOperator.Divide, divideValue);
                    }

                    overlay.Composite(newTree, Convert.ToInt32(fixture.CanvasX), Convert.ToInt32(fixture.CanvasY), CompositeOperator.SrcOver);
                }
            }
        }

        public void Dispose()
        {
            m_modelImages.Select(i => i.Value).Where(i => i != null).ToList().ForEach(i => i.Dispose());
        }
    }
}
