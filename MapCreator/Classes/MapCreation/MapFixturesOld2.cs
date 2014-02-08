using System;
using System.Collections.Generic;
using System.Linq;
using MapCreator.data;
using System.IO;
using NifUtil;
using System.Windows.Forms;
using MPKLib;
using ImageMagick;
using System.Drawing;
using SharpDX;
using System.Collections;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Drawing.Drawing2D;
using System.Threading.Tasks;
using System.Diagnostics;

namespace MapCreator
{
    class MapFixturesOld2
    {
        private ZoneConfiguration zoneConfiguration;
        private List<RiverConfiguration> rivers;

        /// <summary>
        /// Holds all data from CSV files
        /// </summary>
        private MapNifs.FixturesDataTable m_fixturesTable = new MapNifs.FixturesDataTable();
        private MapNifs.NifDataTable m_nifsTable = new MapNifs.NifDataTable();

        /// <summary>
        /// NIF Polygons
        /// </summary>
        private Dictionary<int, Polygon[]> nifPolygons = new Dictionary<int, Polygon[]>();

        /// <summary>
        /// All fixtures that need to be drawn before river is drawn
        /// </summary>
        private List<FixtureModel> fixturesBelowWater = new List<FixtureModel>();
        
        /// <summary>
        /// All fixtures that need to be drawn after river is drawn
        /// </summary>
        private List<FixtureModel> fixturesOverWater = new List<FixtureModel>();

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="zoneConfiguration"></param>
        /// <param name="rivers"></param>
        public MapFixturesOld2(ZoneConfiguration zoneConfiguration, List<RiverConfiguration> rivers)
        {
            this.zoneConfiguration = zoneConfiguration;
            this.rivers = rivers;

            FillNifTable();
            FillFixturesTable();

            // No NIFs found
            if (m_nifsTable.Rows.Count == 0)
            {
                MainForm.Log("No NIFs found! (nifs.csv not found or empty?)", MainForm.LogLevel.error);
                return;
            }

            // No Fixtures found
            if (m_fixturesTable.Rows.Count == 0)
            {
                MainForm.Log("No fixtures found! (fixtures.csv not found or empty?)", MainForm.LogLevel.error);
                return;
            }

            FindNifArchives();
            LoadPolys();
            PrepareFixtures();
        }

        /// <summary>
        /// Finds all NPK archives by a nif file
        /// </summary>
        private void FindNifArchives()
        {
            // Nif paths
            List<string> searchLocations = new List<string>();
            searchLocations.Add(string.Format("{0}\\{1}", Properties.Settings.Default.game_path, "Newtowns\\zones\\Nifs")); // Newtows
            searchLocations.Add(string.Format("{0}\\{1}", zoneConfiguration.ZoneDirectory, "nifs")); // Current Zone Directory
            searchLocations.Add(string.Format("{0}\\{1}", Properties.Settings.Default.game_path, "zones\\Nifs")); // Globals zones nif dir
            searchLocations.Add(string.Format("{0}\\{1}", Properties.Settings.Default.game_path, "zone\\trees")); // Global trees nif dir
            searchLocations.Add(string.Format("{0}\\{1}", Properties.Settings.Default.game_path, "frontiers\\NIFS")); // Frontiers
            searchLocations.Add(string.Format("{0}\\{1}", Properties.Settings.Default.game_path, "phousing\\nifs")); // Housing
            searchLocations.Add(string.Format("{0}\\{1}", Properties.Settings.Default.game_path, "Tutorial\\zones\\nifs")); // Tutorial
            searchLocations.Add(string.Format("{0}\\{1}", Properties.Settings.Default.game_path, "pregame")); // Pregame?
            searchLocations.Add(string.Format("{0}\\{1}", Properties.Settings.Default.game_path, "zones\\Dnifs")); // Guess Dungeon nifs

            // Search NPKs
            foreach (MapNifs.NifRow nifRow in m_nifsTable.Rows)
            {
                string archiveName = Path.GetFileNameWithoutExtension(nifRow.Filename) + ".npk";
                //MainForm.Log(string.Format("Searching for {0}", archiveName), MainForm.LogLevel.notice);
                foreach (string dir in searchLocations)
                {
                    if (Directory.Exists(dir) && Directory.GetFiles(dir, archiveName).Length > 0)
                    {
                        //MainForm.Log(string.Format("Found {0} in {1}!", archiveName, dir), MainForm.LogLevel.success);
                        nifRow.Archive_Path = string.Format("{0}\\{1}", dir, archiveName);
                        break;
                    }
                }

                if (nifRow.IsArchive_PathNull())
                {
                    MainForm.Log(string.Format("Unable to find {0}!", archiveName), MainForm.LogLevel.warning);
                }
            }
        }

        /// <summary>
        /// Load Polys
        /// </summary>
        private void LoadPolys()
        {
            if (m_nifsTable.Count == 0) return;

            DirectoryInfo polysDirectory = new DirectoryInfo(string.Format("{0}\\data\\polys", Application.StartupPath));
            if (!polysDirectory.Exists) polysDirectory.Create();

            string polysMpkFile = string.Format("{0}\\data\\polys.mpk", Application.StartupPath);

            MPAK polyMpk = new MPAK();
            bool polyMpkModified = false;

            if (!File.Exists(polysMpkFile)) polyMpkModified = true; // Create a new poyls.mpk
            else polyMpk.Load(polysMpkFile); // Load existing polys.mpk

            // MainForm progress
            MainForm.ProgressReset();
            MainForm.Log("Loading polys ...", MainForm.LogLevel.notice);
            MainForm.ProgressStart("Loading polys ...");

            // Loop all nifs from nifs.csv
            int progressCounter = 0;
            foreach (MapNifs.NifRow nifRow in m_nifsTable.Rows)
            {
                // Skip if nif path is not found
                if (nifRow.IsArchive_PathNull()) continue;

                // The model polygons
                Polygon[] currentNifPolygons = null;

                // The poly filename
                string modelPolyFileName = Path.GetFileNameWithoutExtension(nifRow.Filename) + ".poly";
                string modelPolySavePath = string.Format("{0}\\{1}", polysDirectory, modelPolyFileName);

                // MPK handling, cache .poly file for models
                if (polyMpk.Files.Where(f => f.Name == modelPolyFileName).Count() == 0)
                {
                    // open the archive
                    using (StreamReader nifFileFromNpk = MpkWrapper.GetFileFromMpk(nifRow.Archive_Path, nifRow.Filename))
                    {
                        if (nifFileFromNpk != null)
                        {
                            //MainForm.Log(string.Format("Converting {0}...", nifRow.Textual_Name), MainForm.LogLevel.notice);

                            // Create a new poly and add to mpk
                            polyMpkModified = true;

                            NifParser nifParser = new NifParser();
                            nifParser.Load(nifFileFromNpk);
                            nifParser.Convert(ConvertType.Poly, modelPolySavePath);

                            // Add file to polys.mpk
                            polyMpk.AddFile(modelPolySavePath);

                            // Assign polys
                            currentNifPolygons = nifParser.GetPolys();
                        }
                    }
                }
                else
                {
                    // .poly file for model is found, read it
                    currentNifPolygons = NifParser.ReadPoly(new StreamReader(new MemoryStream(polyMpk.GetFile(modelPolyFileName).Data)));
                }

                if (currentNifPolygons != null && currentNifPolygons.Length > 0)
                {
                    nifPolygons.Add(nifRow.Id, currentNifPolygons);
                }

                int percent = 100 * progressCounter / m_nifsTable.Count;
                MainForm.ProgressUpdate(percent);
                progressCounter++;
            }

            MainForm.ProgressStartMarquee("Saving...");

            // Save the mpk
            if (polyMpkModified)
            {
                polyMpk.Save(polysMpkFile);
            }

            // Delete polys directory
            Directory.Delete(polysDirectory.FullName, true);

            MainForm.Log("Polys loaded", MainForm.LogLevel.success);
            MainForm.ProgressReset();
        }

        /// <summary>
        /// Prepare all fixtures to draw them
        /// </summary>
        private void PrepareFixtures()
        {
            if (nifPolygons.Count == 0) return;

            MainForm.ProgressReset();
            MainForm.Log("Preparing fixtures...", MainForm.LogLevel.notice);
            MainForm.ProgressStart("Preparing fixtures...");

            // Create paths out of the rivers
            Dictionary<RiverConfiguration, GraphicsPath> riverPaths = new Dictionary<RiverConfiguration, GraphicsPath>();
            foreach (RiverConfiguration rConf in rivers)
            {
                GraphicsPath riverPath = new GraphicsPath();
                PointF[] points = rConf.GetCoordinates().Select(c => new PointF(Convert.ToSingle(c.X * zoneConfiguration.MapScale), Convert.ToSingle(c.Y * zoneConfiguration.MapScale))).ToArray();
                riverPath.AddPolygon(points);
                riverPaths.Add(rConf, riverPath);
            }

            int progressCounter = 0;
            foreach (MapNifs.FixturesRow fixtureRow in m_fixturesTable)
            {
                if(!nifPolygons.ContainsKey(fixtureRow.Nif_Id)) continue;
                
                // Debug
                //if (fixtureRow.Nif_Id != 408 && fixtureRow.Nif_Id != 417) continue;

                // Hold some data for better visuality
                Polygon[] polys = nifPolygons[fixtureRow.Nif_Id];
                MapNifs.NifRow nifRow = m_nifsTable.FindById(fixtureRow.Nif_Id);

                FixtureModel model = new FixtureModel(zoneConfiguration, polys, nifRow, fixtureRow);
                if (model.Polygons.Count() == 0)
                {
                    MainForm.Log(model.Name + " no polygons left");
                    continue;
                }
                if (model.RendererConfiguration.Renderer == FixtureRendererTypeUnused.None) continue;

                // Check if on river or not
                int riverHeight = 0;
                foreach (KeyValuePair<RiverConfiguration, GraphicsPath> river in riverPaths)
                {
                    if (river.Value.IsVisible(Convert.ToSingle(model.X), Convert.ToSingle(model.Y)))
                    {
                        riverHeight = river.Key.Height;
                        break;
                    }
                }

                if (riverHeight == 0 || (riverHeight != 0 && model.Z > riverHeight))
                {
                    fixturesOverWater.Add(model);
                }
                else
                {
                    fixturesBelowWater.Add(model);
                }

                // Progress
                int progressPercent = 100 * progressCounter / m_fixturesTable.Count;
                MainForm.ProgressUpdate(progressPercent);
                progressCounter++;
            }

            MainForm.ProgressReset();
            MainForm.Log("Fixtures prepared.", MainForm.LogLevel.success);
        }

        /// <summary>
        /// Draw all models
        /// </summary>
        /// <param name="map"></param>
        /// <param name="underwater"></param>
        public void Draw(MagickImage map, bool underwater, bool debug)
        {
            if(debug) {
                MagickNET.SetLogEvents(LogEvents.All);
                MagickNET.Log += MagickNET_Log;
            }

            List<FixtureModel> models;
            if (underwater)
            {
                MainForm.Log("Drawing fixtures under water level...", MainForm.LogLevel.notice);
                models = fixturesBelowWater.OrderBy(o => o.Z).ToList();
            }
            else
            {
                MainForm.Log("Drawing fixtures above water level...", MainForm.LogLevel.notice);
                models = fixturesOverWater.OrderBy(o => o.Z).ToList();
            }

            MainForm.Log("Drawing models...", MainForm.LogLevel.notice);
            Stopwatch timer = Stopwatch.StartNew();

            using (MagickImage modelsOverlay = new MagickImage(MagickColor.Transparent, zoneConfiguration.TargetMapSize, zoneConfiguration.TargetMapSize))
            {
                // Test 1
                MainForm.ProgressStart("Drawing models ...");
                int processCounter = 0;

                foreach (FixtureModel model in models)
                {
                    switch (model.RendererConfiguration.Renderer)
                    {
                        case FixtureRendererTypeUnused.Shaded:
                        default:
                            DrawShaded(modelsOverlay, model);
                            break;
                        case FixtureRendererTypeUnused.Flat:
                            DrawFlat(modelsOverlay, model);
                            break;
                        case FixtureRendererTypeUnused.Tree:
                            DrawShaded(modelsOverlay, model);
                            break;
                        case FixtureRendererTypeUnused.TreeImage:
                            DrawTreeImage(modelsOverlay, model);
                            break;
                    }

                    int percent = 100 * processCounter / models.Count;
                    MainForm.ProgressUpdate(percent);
                    processCounter++;
                }
             
                map.Composite(modelsOverlay, 0, 0, CompositeOperator.SrcOver);
            }
            
            timer.Stop();
            MainForm.Log(string.Format("Finished models! Took {0} seconds.", timer.Elapsed.TotalSeconds), MainForm.LogLevel.success);

            /*
            // Test2
            Stopwatch timer = Stopwatch.StartNew();
            MainForm.ProgressStart("Drawing models (multithreaded) ...");
            int processCounter = 0;

            using (MagickImage modelsOverlay = new MagickImage(MagickColor.Transparent, zoneConfiguration.TargetMapSize, zoneConfiguration.TargetMapSize))
            {
                Parallel.ForEach(models, model =>
                {
                    switch (model.RendererConfiguration.Renderer)
                    {
                        case FixtureRendererType.Shaded:
                        default:
                            DrawShaded(modelsOverlay, model);
                            break;
                        case FixtureRendererType.Flat:
                            DrawFlat(modelsOverlay, model);
                            break;
                        case FixtureRendererType.Tree:
                            DrawTree(modelsOverlay, model);
                            break;
                    }

                    int percent = 100 * processCounter / models.Count;
                    MainForm.ProgressUpdate(percent);
                    processCounter++;
                });

                map.Composite(modelsOverlay, 0, 0, CompositeOperator.SrcOver);
            }

            timer.Stop();
            MainForm.Log(string.Format("Finished models (multithreaded)! Took {0} seconds.", timer.Elapsed.TotalSeconds), MainForm.LogLevel.success);
            */
        }

        private void DrawShaded(MagickImage overlay, FixtureModel model)
        {
            //MainForm.Log(model.Name, MainForm.LogLevel.notice);

            using (MagickImage modelCanvas = new MagickImage(MagickColor.Transparent, model.Canvas.Width, model.Canvas.Height))
            {
                foreach (DrawListEntry drawListEntry in model.Canvas.DrawListEntries)
                {
                    using (DrawablePolygon polyDraw = new DrawablePolygon(drawListEntry.coordinates))
                    {
                        modelCanvas.FillColor = new MagickColor(
                            Convert.ToSingle(drawListEntry.lightning * model.Canvas.Color.R * 255),
                            Convert.ToSingle(drawListEntry.lightning * model.Canvas.Color.G * 255),
                            Convert.ToSingle(drawListEntry.lightning * model.Canvas.Color.B * 255),
                            Convert.ToSingle(model.Canvas.Alpha)
                        );
                        modelCanvas.Draw(polyDraw);
                    }
                }

                if (model.RendererConfiguration.Outline)
                {
                    modelCanvas.BorderColor = MagickColor.Transparent;
                    modelCanvas.Border(1);
                    modelCanvas.Shadow(0, 0, 1, new Percentage(100), System.Drawing.Color.Black);
                }

                overlay.Composite(modelCanvas, model.Canvas.X, model.Canvas.Y, CompositeOperator.SrcOver);
            }
        }

        private void DrawFlat(MagickImage overlay, FixtureModel model)
        {
            //MainForm.Log(model.Name, MainForm.LogLevel.notice);

            using (MagickImage modelCanvas = new MagickImage(MagickColor.Transparent, model.Canvas.Width, model.Canvas.Height))
            {
                modelCanvas.FillColor = new MagickColor(
                    Convert.ToSingle(model.Canvas.Color.R * 255),
                    Convert.ToSingle(model.Canvas.Color.G * 255),
                    Convert.ToSingle(model.Canvas.Color.B * 255),
                    Convert.ToSingle(model.Canvas.Alpha)
                );

                foreach (DrawListEntry drawListEntry in model.Canvas.DrawListEntries)
                {
                    using (DrawablePolygon polyDraw = new DrawablePolygon(drawListEntry.coordinates))
                    {
                        modelCanvas.Draw(polyDraw);
                    }
                }

                if (model.RendererConfiguration.Outline)
                {
                    modelCanvas.BorderColor = MagickColor.Transparent;
                    modelCanvas.Border(1);
                    modelCanvas.Shadow(0, 0, 1, new Percentage(100), System.Drawing.Color.Black);
                }

                overlay.Composite(modelCanvas, model.Canvas.X, model.Canvas.Y, CompositeOperator.SrcOver);
            }
        }

        private Dictionary<string, MagickImage> treeImages = new Dictionary<string, MagickImage>();

        private void DrawTreeImage(MagickImage overlay, FixtureModel model)
        {
            if (!treeImages.ContainsKey(model.NifRow.Filename))
            {
                string treeImageFile = string.Format("{0}\\data\\prerendered\\{1}.png", Application.StartupPath, Path.GetFileNameWithoutExtension(model.NifRow.Filename));
                if (File.Exists(treeImageFile))
                {
                    MagickImage treeImage = new MagickImage(treeImageFile);
                    treeImage.Blur();

                    treeImages.Add(model.NifRow.Filename, treeImage);
                }
                else
                {
                    treeImages.Add(model.NifRow.Filename, null);
                }
            }

            if (treeImages.ContainsKey(model.NifRow.Filename) && treeImages[model.NifRow.Filename] != null)
            {
                using (MagickImage newTree = treeImages[model.NifRow.Filename].Clone())
                {
                    newTree.BackgroundColor = MagickColor.Transparent;
                    newTree.Rotate(model.FixtureRow.A);
                    newTree.Trim();
                    newTree.Resize(model.Canvas.Width, model.Canvas.Height);

                    using (MagickImage modelCanvas = new MagickImage(MagickColor.Transparent, model.Canvas.Width, model.Canvas.Height))
                    {
                        foreach (DrawListEntry drawListEntry in model.Canvas.DrawListEntries)
                        {
                            modelCanvas.FillColor = new MagickColor(
                                Convert.ToSingle(128 * 256 * drawListEntry.lightning),
                                Convert.ToSingle(128 * 256 * drawListEntry.lightning),
                                Convert.ToSingle(128 * 256 * drawListEntry.lightning)
                            );

                            using (DrawablePolygon polyDraw = new DrawablePolygon(drawListEntry.coordinates))
                            {
                                modelCanvas.Draw(polyDraw);
                            }
                        }

                        modelCanvas.Composite(newTree, 0, 0, CompositeOperator.DstIn);
                        newTree.Composite(modelCanvas, 0, 0, CompositeOperator.Overlay);
                    }

                    if (model.RendererConfiguration.Transparency != 0)
                    {
                        newTree.Alpha(AlphaOption.Set);

                        double divideValue = 100.0 / (100.0 - model.RendererConfiguration.Transparency);
                        newTree.Evaluate(Channels.Alpha, EvaluateOperator.Divide, divideValue);
                    }

                    overlay.Composite(newTree, model.Canvas.X, model.Canvas.Y, CompositeOperator.SrcOver);
                }
            }
        }

        private void AddOutline(MagickImage image)
        {
            using (MagickImage border = new MagickImage(image))
            {
                image.Shadow(0, 0, 1, new Percentage(100),System.Drawing.Color.Black);
            }
        }

        #region Load CSV files

        /// <summary>
        /// Loads nifs.csv
        /// </summary>
        private void FillNifTable()
        {
            List<string> nifs = DataWrapper.GetFileContent(zoneConfiguration.CvsMpk, "nifs.csv");
            // Read CSV
            foreach (string row in nifs)
            {
                if (row.StartsWith("Grid") || row.StartsWith("NIF")) continue;

                string[] fields = row.Split(',');

                MapNifs.NifRow nifRow = m_nifsTable.NewNifRow();
                int column = 0;
                foreach (string fieldValue in fields)
                {
                    nifRow[column] = Convert.ChangeType(fieldValue, m_nifsTable.Columns[column].DataType);
                    column++;
                }
                m_nifsTable.AddNifRow(nifRow);
            }
        }

        /// <summary>
        /// Loads fixtures.csv
        /// </summary>
        private void FillFixturesTable()
        {
            List<string> fixtures = DataWrapper.GetFileContent(zoneConfiguration.CvsMpk, "fixtures.csv");

            // Create a NumberFormatInfo object and set some of its properties.
            NumberFormatInfo provider = new NumberFormatInfo();
            provider.NumberDecimalSeparator = ".";
            provider.NumberGroupSeparator = "";
            provider.NumberGroupSizes = new int[] { 2 };

            // Read CSV
            foreach (string row in fixtures)
            {
                if (row.StartsWith("Fixtures") || row.StartsWith("ID")) continue;

                string[] fields = row.Split(',');

                MapNifs.FixturesRow fixtureRow = m_fixturesTable.NewFixturesRow();
                int column = 0;
                foreach (string fieldValue in fields)
                {
                    fixtureRow[column] = Convert.ChangeType(fieldValue, m_fixturesTable.Columns[column].DataType, provider);
                    column++;
                }
                m_fixturesTable.AddFixturesRow(fixtureRow);
            }
        }

        #endregion

        StreamWriter writer;

        public void MagickNET_Log(object sender, LogEventArgs arguments)
        {
            if (writer == null)
            {
                string date = DateTime.Now.Hour + "_" + DateTime.Now.Minute + "_" + DateTime.Now.Second;
                writer = new StreamWriter(string.Format("{0}\\magick_{1}.log", Application.StartupPath, date));
            }

            writer.WriteLine(arguments.Message);
        }

    }

}
