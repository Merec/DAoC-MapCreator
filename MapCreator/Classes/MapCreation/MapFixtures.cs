using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ImageMagick;
using MapCreator.Fixtures;
using System.Diagnostics;
using SharpDX;

namespace MapCreator
{
    class MapFixtures : IDisposable
    {
        private ZoneConfiguration zoneConfiguration;
        private List<WaterConfiguration> rivers;

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

        int m_treeTransparency = 20;
        public int TreeTransparency
        {
            get { return m_treeTransparency; }
            set { m_treeTransparency = value; }
        }

        #endregion

        public MapFixtures(ZoneConfiguration zoneConfiguration, List<WaterConfiguration> rivers)
        {
            this.zoneConfiguration = zoneConfiguration;
            this.rivers = rivers;

            // Load Renderer Configurations

            // Initialize the fixtures loader, loads CSV files and polygons
            FixturesLoader.Initialize(zoneConfiguration);

            // Prepare models
            m_fixtures = FixturesLoader.GetDrawableFixtures();
        }

        public void Start()
        {
            if (m_fixtures.Count == 0) return;
            MainForm.ProgressStartMarquee("Sorting fixtures ....");

            // Create paths out of the rivers
            Dictionary<WaterConfiguration, System.Drawing.Drawing2D.GraphicsPath> riverPaths = new Dictionary<WaterConfiguration, System.Drawing.Drawing2D.GraphicsPath>();
            foreach (WaterConfiguration rConf in rivers)
            {
                System.Drawing.Drawing2D.GraphicsPath riverPath = new System.Drawing.Drawing2D.GraphicsPath();
                System.Drawing.PointF[] points = rConf.GetCoordinates().Select(c => new System.Drawing.PointF(Convert.ToSingle(c.X * zoneConfiguration.MapScale), Convert.ToSingle(c.Y * zoneConfiguration.MapScale))).ToArray();
                riverPath.AddPolygon(points);
                riverPaths.Add(rConf, riverPath);
            }

            foreach (DrawableFixture model in m_fixtures)
            {
                // ignote the model if there are no polygons
                if(model.ProcessedPolygons.Count() == 0) continue;

                // UI options
                if (!DrawTrees && (model.IsTree || model.IsTreeCluster))
                {
                    continue;
                }

                if (!DrawFixtures && !(model.IsTree || model.IsTreeCluster))
                {
                    continue;
                }

                if (!DrawTreesAsImages && (model.IsTree || model.IsTreeCluster))
                {
                    model.RendererConf = FixtureRendererConfigurations.GetRendererById("TreeShaded");
                }

                double modelCenterX = zoneConfiguration.ZoneCoordinateToMapCoordinate(model.FixtureRow.X);
                double modelCenterY = zoneConfiguration.ZoneCoordinateToMapCoordinate(model.FixtureRow.Y);

                // Check if on river or not
                int riverHeight = 0;
                foreach (KeyValuePair<WaterConfiguration, System.Drawing.Drawing2D.GraphicsPath> river in riverPaths)
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

            MainForm.ProgressReset();
        }

        public void Draw(MagickImage map, bool underwater)
        {
            if (underwater)
            {
                Draw(map, m_fixturesUnderWater);
            }
            else
            {
                Draw(map, m_fixturesAboveWater);
            }
        }

        private void Draw(MagickImage map, List<DrawableFixture> fixtures)
        {
            MainForm.ProgressStart(string.Format("Drawing fixtures ({0}) ...", fixtures.Count));
            Stopwatch timer = Stopwatch.StartNew();

            using (MagickImage modelsOverlay = new MagickImage(MagickColor.Transparent, zoneConfiguration.TargetMapSize, zoneConfiguration.TargetMapSize))
            {
                using (MagickImage treeOverlay = new MagickImage(MagickColor.Transparent, zoneConfiguration.TargetMapSize, zoneConfiguration.TargetMapSize))
                {
                    int processCounter = 0;
                    foreach (DrawableFixture fixture in fixtures)
                    {
                        // Debug single models
                        /*
                        if (fixture.FixtureRow.NifId != 443 && fixture.FixtureRow.NifId != 433 && fixture.FixtureRow.NifId != 459)
                        {
                            continue;
                        }
                        else
                        {
                            DrawShaded((fixture.IsTree || fixture.IsTreeCluster) ? treeOverlay : modelsOverlay, fixture);
                        }
                        */

                        switch (fixture.RendererConf.Renderer)
                        {
                            case FixtureRenderererType.Shaded:
                                DrawShaded((fixture.IsTree || fixture.IsTreeCluster) ? treeOverlay : modelsOverlay, fixture);
                                break;
                            case FixtureRenderererType.Flat:
                                DrawFlat((fixture.IsTree || fixture.IsTreeCluster) ? treeOverlay : modelsOverlay, fixture);
                                break;
                            case FixtureRenderererType.Image:
                                //DrawShaded((fixture.IsTree || fixture.IsTreeCluster) ? treeOverlay : modelsOverlay, fixture);
                                DrawImage((fixture.IsTree || fixture.IsTreeCluster) ? treeOverlay : modelsOverlay, fixture);
                                break;
                        }

                        int percent = 100 * processCounter / fixtures.Count();
                        MainForm.ProgressUpdate(percent);
                        processCounter++;
                    }

                    MainForm.ProgressStartMarquee("Merging ...");

                    FixtureRendererConfiguration2 treeImagesRConf = FixtureRendererConfigurations.GetRendererById("TreeImage");
                    if (treeImagesRConf.HasShadow)
                    {
                        //treeOverlay.BorderColor = MagickColor.Transparent;
                        //treeOverlay.Border(1);
                        treeOverlay.Shadow(
                            treeImagesRConf.ShadowOffsetX,
                            treeImagesRConf.ShadowOffsetY,
                            treeImagesRConf.ShadowSize,
                            new Percentage(100 - treeImagesRConf.ShadowTransparency),
                            treeImagesRConf.ShadowColor
                        );
                    }

                    if (treeImagesRConf.Transparency != 0)
                    {
                        treeOverlay.Alpha(AlphaOption.Set);
                        double divideValue = 100.0 / (100.0 - TreeTransparency);
                        treeOverlay.Evaluate(Channels.Alpha, EvaluateOperator.Divide, divideValue);
                    }

                    map.Composite(modelsOverlay, 0, 0, CompositeOperator.SrcOver);
                    map.Composite(treeOverlay, 0, 0, CompositeOperator.SrcOver);
                }
            }

            timer.Stop();
            MainForm.Log(string.Format("Finished in {0} seconds.", timer.Elapsed.TotalSeconds), MainForm.LogLevel.success);
            MainForm.ProgressReset();
        }

        private void DrawShaded(MagickImage overlay, DrawableFixture fixture)
        {
            //MainForm.Log(string.Format("Shaded: {0} ({1}) ...", fixture.Name, fixture.NifName), MainForm.LogLevel.notice);

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
                                Convert.ToSingle(drawableElement.lightning * fixture.RendererConf.Color.R),
                                Convert.ToSingle(drawableElement.lightning * fixture.RendererConf.Color.G),
                                Convert.ToSingle(drawableElement.lightning * fixture.RendererConf.Color.B)
                            );
                        }
                        else
                        {
                            modelCanvas.FillColor = fixture.RendererConf.Color;
                        }

                        modelCanvas.Draw(polyDraw);
                    }
                }

                if (fixture.RendererConf.HasShadow)
                {
                    modelCanvas.BorderColor = MagickColor.Transparent;
                    modelCanvas.Border((int)fixture.RendererConf.ShadowSize);
                    modelCanvas.Shadow(
                        fixture.RendererConf.ShadowOffsetX, 
                        fixture.RendererConf.ShadowOffsetY, 
                        fixture.RendererConf.ShadowSize,
                        new Percentage(100 - fixture.RendererConf.ShadowTransparency),
                        fixture.RendererConf.ShadowColor
                    );

                    // Update the canvas position to match the new border
                    fixture.CanvasX -= fixture.RendererConf.ShadowSize;
                    fixture.CanvasY -= fixture.RendererConf.ShadowSize;
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
            //MainForm.Log(string.Format("Flat: {0} ({1}) ...", fixture.Name, fixture.NifName), MainForm.LogLevel.notice);

            using (MagickImage modelCanvas = new MagickImage(MagickColor.Transparent, fixture.CanvasWidth, fixture.CanvasHeight))
            {
                modelCanvas.FillColor = fixture.RendererConf.Color;

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

                    // Update the canvas position to match the new border
                    fixture.CanvasX -= fixture.RendererConf.ShadowSize;
                    fixture.CanvasY -= fixture.RendererConf.ShadowSize;
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
            if (fixture.IsTree)
            {
                DrawTree(overlay, fixture);
                return;
            }

            //MainForm.Log(string.Format("Image: {0} ({1}) ...", fixture.Name, fixture.NifName), MainForm.LogLevel.notice);
            string fileName = System.IO.Path.GetFileNameWithoutExtension(fixture.NifName);
            string defaultTree = "elm1";

            // Load default tree
            if (!m_modelImages.ContainsKey(defaultTree))
            {
                string defaultTreeImage = string.Format("{0}\\data\\prerendered\\trees\\{1}.png", System.Windows.Forms.Application.StartupPath, defaultTree);                     
                if (System.IO.File.Exists(defaultTreeImage))
                {
                    MagickImage treeImage = new MagickImage(defaultTreeImage);
                    treeImage.Blur();
                    m_modelImages.Add(defaultTree, treeImage);
                }
                else
                {
                    m_modelImages.Add(fileName, null);
                }
            }

            if (fixture.IsTreeCluster)
            {
                DrawTreeCluster(overlay, fixture);
                return;
            }

            // Load model image
            if (!m_modelImages.ContainsKey(fileName))
            {
                string objectImageFile = string.Format("{0}\\data\\prerendered\\objects\\{1}.png", System.Windows.Forms.Application.StartupPath, fileName);
                if (fixture.IsTree) objectImageFile = string.Format("{0}\\data\\prerendered\\trees\\{1}.png", System.Windows.Forms.Application.StartupPath, fileName);

                if (System.IO.File.Exists(objectImageFile))
                {
                    MagickImage objectImage = new MagickImage(objectImageFile);
                    if(fixture.IsTree) objectImage.Blur();
                    m_modelImages.Add(fileName, objectImage);
                }
                else
                {
                    if (fixture.IsTree)
                    {
                        MainForm.Log(string.Format("Can not find image for tree {0} ({1}), using default tree", fixture.Name, fixture.NifName), MainForm.LogLevel.warning);
                        m_modelImages.Add(fileName, m_modelImages[defaultTree]);
                    }
                    else m_modelImages.Add(fileName, null);
                }
            }

            // Draw the image
            if (m_modelImages.ContainsKey(fileName) && m_modelImages[fileName] != null)
            {
                NifRow orginalNif = FixturesLoader.NifRows.Where(n => n.NifId == fixture.FixtureRow.NifId).FirstOrDefault();
                if (orginalNif == null)
                {
                    MainForm.Log(string.Format("Error with imaged nif ({0})!", fixture.FixtureRow.TextualName), MainForm.LogLevel.warning);
                }

                System.Drawing.SizeF objectSize = orginalNif.GetSize(0, 0);

                // The final image
                using (MagickImage modelImage = new MagickImage(MagickColor.Transparent, fixture.CanvasWidth, fixture.CanvasHeight))
                {
                    // Place the replacing image
                    using (MagickImage newModelImage = m_modelImages[fileName].Clone())
                    {
                        newModelImage.BackgroundColor = MagickColor.Transparent;

                        double scaleWidthToTreeImage = objectSize.Width / newModelImage.Width;
                        double scaleHeightToTreeImage = objectSize.Height / newModelImage.Height;
                        int width = Convert.ToInt32(newModelImage.Width * scaleWidthToTreeImage * fixture.Scale);
                        int height = Convert.ToInt32(newModelImage.Height * scaleHeightToTreeImage * fixture.Scale);
                        
                        // Resize to new size
                        newModelImage.Resize(width, height);

                        // Rotate the image
                        //newModelImage.Rotate(fixture.FixtureRow.A * -1 * fixture.FixtureRow.AxisZ3D);
                        newModelImage.Rotate((360d * fixture.FixtureRow.AxisZ3D - fixture.FixtureRow.A) * -1);

                        // Place in center of modelImage
                        modelImage.Composite(newModelImage, Gravity.Center, CompositeOperator.SrcOver);
                    }

                    // Draw the shaped model if wanted
                    if (fixture.RendererConf.HasLight)
                    {
                        using (MagickImage modelShaped = new MagickImage(MagickColor.Transparent, fixture.CanvasWidth, fixture.CanvasHeight))
                        {
                            foreach (DrawableElement drawableElement in fixture.DrawableElements)
                            {
                                modelShaped.FillColor = new MagickColor(
                                    Convert.ToSingle(128 * 256 * drawableElement.lightning),
                                    Convert.ToSingle(128 * 256 * drawableElement.lightning),
                                    Convert.ToSingle(128 * 256 * drawableElement.lightning)
                                );

                                using (DrawablePolygon polyDraw = new DrawablePolygon(drawableElement.coordinates))
                                {
                                    modelShaped.Draw(polyDraw);
                                }
                            }

                            // Remove outstanding edges from the shape using the replacing image
                            modelShaped.Composite(modelImage, 0, 0, CompositeOperator.DstIn);
                            // Enlight the replacing image using the shape
                            modelImage.Composite(modelShaped, 0, 0, CompositeOperator.Overlay);
                        }
                    }

                    // Add the shadow if not a tree (tree shadow are substituted by a treeoverlay)
                    if (fixture.RendererConf.HasShadow && !fixture.IsTree)
                    {
                        modelImage.BorderColor = MagickColor.Transparent;
                        modelImage.Border((int)fixture.RendererConf.ShadowSize);
                        modelImage.Shadow(
                            fixture.RendererConf.ShadowOffsetX,
                            fixture.RendererConf.ShadowOffsetY,
                            fixture.RendererConf.ShadowSize,
                            new Percentage(100 - fixture.RendererConf.ShadowTransparency),
                            fixture.RendererConf.ShadowColor
                        );

                        // Update the canvas position to match the new border
                        fixture.CanvasX -= fixture.RendererConf.ShadowSize;
                        fixture.CanvasY -= fixture.RendererConf.ShadowSize;
                    }

                    // Set transprency if not a tree (see shadow)
                    if (fixture.RendererConf.Transparency != 0 && !fixture.IsTree)
                    {
                        double divideValue = 100.0 / (100.0 - fixture.RendererConf.Transparency);
                        modelImage.Evaluate(Channels.Alpha, EvaluateOperator.Divide, divideValue);
                    }

                    // Place the image on the right position
                    overlay.Composite(modelImage, Convert.ToInt32(fixture.CanvasX), Convert.ToInt32(fixture.CanvasY), CompositeOperator.SrcOver);
                }
            }
        }

        private void DrawTree(MagickImage overlay, DrawableFixture fixture)
        {
            System.Drawing.Color testColor = System.Drawing.ColorTranslator.FromHtml("#5e683a");

            using (MagickImage pattern = new MagickImage(MagickColor.Transparent, fixture.CanvasWidth, fixture.CanvasHeight))
            {
                using (MagickImage patternTexture = new MagickImage(string.Format("{0}\\data\\textures\\{1}.png", System.Windows.Forms.Application.StartupPath, "leaves_mask")))
                {
                    patternTexture.Resize(fixture.CanvasWidth / 2, fixture.CanvasHeight / 2);
                    pattern.Texture(patternTexture);

                    Random rnd = new Random();
                    pattern.Rotate(rnd.Next(0, 360));
                    
                    using (MagickImage modelCanvas = new MagickImage(MagickColor.Transparent, fixture.CanvasWidth, fixture.CanvasHeight))
                    {
                        foreach (DrawableElement drawableElement in fixture.DrawableElements)
                        {
                            using (DrawablePolygon polyDraw = new DrawablePolygon(drawableElement.coordinates))
                            {
                                // A Shaded model without lightning is not shaded... but just we add this just be flexible
                                if (fixture.RendererConf.HasLight)
                                {
                                    float r, g, b, light;

                                    light = (float)drawableElement.lightning * 2f;
                                    r = fixture.Tree.AverageColor.R * light;
                                    g = fixture.Tree.AverageColor.G * light;
                                    b = fixture.Tree.AverageColor.B * light;


                                    modelCanvas.FillColor = new MagickColor(
                                        r * 255,
                                        g * 255,
                                        b * 255
                                    );
                                }
                                else
                                {
                                    modelCanvas.FillColor = fixture.RendererConf.Color;
                                }

                                modelCanvas.Draw(polyDraw);
                            }
                        }

                        // Add leaves pattern
                        pattern.Composite(modelCanvas, Gravity.Center, CompositeOperator.DstIn);
                        modelCanvas.Composite(pattern, Gravity.Center, CompositeOperator.CopyOpacity);


                        if (fixture.RendererConf.HasShadow)
                        {
                            modelCanvas.BorderColor = MagickColor.Transparent;
                            modelCanvas.Border((int)fixture.RendererConf.ShadowSize);
                            modelCanvas.Shadow(
                                fixture.RendererConf.ShadowOffsetX,
                                fixture.RendererConf.ShadowOffsetY,
                                fixture.RendererConf.ShadowSize,
                                new Percentage(100 - fixture.RendererConf.ShadowTransparency),
                                fixture.RendererConf.ShadowColor
                            );

                            // Update the canvas position to match the new border
                            fixture.CanvasX -= fixture.RendererConf.ShadowSize;
                            fixture.CanvasY -= fixture.RendererConf.ShadowSize;
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
            }
        }

        private void DrawTreeCluster(MagickImage overlay, DrawableFixture fixture)
        {
            //MainForm.Log(string.Format("Image: {0} ({1}) ...", fixture.Name, fixture.TreeCluster.Tree), MainForm.LogLevel.notice);
            string fileName = System.IO.Path.GetFileNameWithoutExtension(fixture.TreeCluster.Tree);
            string defaultTree = "elm1";

            // Load model image
            if (!m_modelImages.ContainsKey(fileName))
            {
                string treeImageFile = string.Format("{0}\\data\\prerendered\\trees\\{1}.png", System.Windows.Forms.Application.StartupPath, fileName);
                if (System.IO.File.Exists(treeImageFile))
                {
                    MagickImage modelImage = new MagickImage(treeImageFile);
                    modelImage.Blur();
                    m_modelImages.Add(fileName, modelImage);
                }
                else
                {
                    MainForm.Log(string.Format("Can not find image for tree {0} ({1}), using default tree", fixture.TreeCluster.Tree, fixture.NifName), MainForm.LogLevel.warning);
                    m_modelImages.Add(fileName, m_modelImages[defaultTree]);
                }
            }

            if (m_modelImages.ContainsKey(fileName) && m_modelImages[fileName] != null)
            {
                // Get the width of the orginal tree shape
                NifRow tree = FixturesLoader.NifRows.Where(n => n.Filename.ToLower() == fixture.TreeCluster.Tree.ToLower()).FirstOrDefault();
                if (tree == null) return;

                System.Drawing.SizeF treeSize = tree.GetSize(0, 0);

                int dimensions = ((fixture.CanvasWidth > fixture.CanvasHeight) ? fixture.CanvasWidth : fixture.CanvasHeight) + 10;
                int extendedWidth = dimensions - fixture.CanvasWidth;
                int extendedHeight = dimensions - fixture.CanvasHeight;

                using (MagickImage treeCluster = new MagickImage(MagickColor.Transparent, dimensions, dimensions))
                {
                    double centerX = treeCluster.Width / 2d;
                    double centerY = treeCluster.Height / 2d;

                    foreach (SharpDX.Vector3 treeInstance in fixture.TreeCluster.TreeInstances)
                    {
                        using (MagickImage treeImage = m_modelImages[fileName].Clone())
                        {
                            double scaleWidthToTreeImage = treeSize.Width / treeImage.Width;
                            double scaleHeightToTreeImage = treeSize.Height / treeImage.Height;
                            int width = Convert.ToInt32(treeImage.Width * scaleWidthToTreeImage * fixture.Scale);
                            int height = Convert.ToInt32(treeImage.Height * scaleHeightToTreeImage * fixture.Scale);
                            treeImage.Resize(width, height);

                            int x = Convert.ToInt32(centerX - width / 2d - zoneConfiguration.ZoneCoordinateToMapCoordinate(treeInstance.X) * (fixture.FixtureRow.Scale / 100));
                            int y = Convert.ToInt32(centerY - height / 2d - zoneConfiguration.ZoneCoordinateToMapCoordinate(treeInstance.Y) * (fixture.FixtureRow.Scale / 100));
                            treeCluster.Composite(treeImage, x, y, CompositeOperator.SrcOver);
                        }
                    }

                    treeCluster.Rotate((360d * fixture.FixtureRow.AxisZ3D - fixture.FixtureRow.A) * -1);

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

                        modelCanvas.Composite(treeCluster, Gravity.Center, CompositeOperator.DstIn);
                        treeCluster.Composite(modelCanvas, Gravity.Center, CompositeOperator.Overlay);
                        //treeCluster.Composite(modelCanvas, Gravity.Center, CompositeOperator.SrcOver);
                    }

                    if (fixture.RendererConf.HasShadow)
                    {
                        treeCluster.BorderColor = MagickColor.Transparent;
                        treeCluster.Border(1);
                        treeCluster.Shadow(
                            fixture.RendererConf.ShadowOffsetX,
                            fixture.RendererConf.ShadowOffsetY,
                            fixture.RendererConf.ShadowSize,
                            new Percentage(100 - fixture.RendererConf.ShadowTransparency),
                            fixture.RendererConf.ShadowColor
                        );
                    }

                    if (fixture.RendererConf.Transparency != 0)
                    {
                        treeCluster.Alpha(AlphaOption.Set);

                        double divideValue = 100.0 / (100.0 - fixture.RendererConf.Transparency);
                        treeCluster.Evaluate(Channels.Alpha, EvaluateOperator.Divide, divideValue);
                    }

                    overlay.Composite(treeCluster, Convert.ToInt32(fixture.CanvasX - extendedWidth/2), Convert.ToInt32(fixture.CanvasY - extendedHeight/2), CompositeOperator.SrcOver);
                }
            }
        }

        public void Dispose()
        {
            m_modelImages.Select(i => i.Value).Where(i => i != null).ToList().ForEach(i => i.Dispose());
        }
    }
}
