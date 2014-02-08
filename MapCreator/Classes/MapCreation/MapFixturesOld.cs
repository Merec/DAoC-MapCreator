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

namespace MapCreator.Deprecated
{
    enum Renderer
    {
        None,
        Shaded,
        Flat,
        Tree
    }

    class RendererConfiguration
    {
        private string m_id;

        public string Id
        {
            get { return m_id; }
            set { m_id = value; }
        }

        private Renderer m_renderer;

        public Renderer Renderer
        {
            get { return m_renderer; }
            set { m_renderer = value; }
        }

        private bool m_outline;

        public bool Outline
        {
            get { return m_outline; }
            set { m_outline = value; }
        }

        private bool m_isDefault;

        public bool IsDefault
        {
            get { return m_isDefault; }
            set { m_isDefault = value; }
        }

        public RendererConfiguration(string id, Renderer renderer, bool outline, bool isDefault)
        {
            m_id = id;
            m_renderer = renderer;
            m_outline = outline;
            m_isDefault = isDefault;
        }
    }

    class MapFixtures
    {
        private ZoneConfiguration zoneConfiguration;
        private List<RiverConfiguration> rivers;

        private MapNifs mapNifs = new MapNifs();

        #region Lightning

        private double zScale = 20.0;
        public double ZScale
        {
            get { return zScale; }
            set { zScale = value; }
        }

        private float m_lightMin = 0.7f;
        public float LightMin
        {
            get { return m_lightMin; }
            set { m_lightMin = value; }
        }

        private float m_lightMax = 1f;
        public float LightMax
        {
            get { return m_lightMax; }
            set { m_lightMax = value; }
        }

        private Vector3 lightVector = Vector3.Normalize(new Vector3(1f, 1f, -1f));
        public Vector3 LightVector
        {
            get { return lightVector; }
            set { lightVector = Vector3.Normalize(value); }
        }

        #endregion

        private Dictionary<string, List<NifModel>> m_models = new Dictionary<string, List<NifModel>>();

        /// <summary>
        /// Model Renderer Configurations
        /// </summary>
        private Dictionary<string, RendererConfiguration> modelRendererConfigurations = new Dictionary<string, RendererConfiguration>();

        /// <summary>
        /// The default Model Renderer Configuration
        /// </summary>
        private RendererConfiguration defaultModelRendererConfiguration;

        /// <summary>
        /// Holds the fixtures that are below the rivers
        /// </summary>
        private Dictionary<string, List<FixtureModel>> m_fixturesBelowRivers = new Dictionary<string, List<FixtureModel>>();

        /// <summary>
        /// Holds the fixtures that are above the rivers
        /// </summary>
        private Dictionary<string, List<FixtureModel>> m_fixturesAboveRivers = new Dictionary<string, List<FixtureModel>>();

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="zoneConfiguration"></param>
        public MapFixtures(ZoneConfiguration zoneConfiguration, List<RiverConfiguration> rivers)
        {
            this.zoneConfiguration = zoneConfiguration;
            this.rivers = rivers;

            // Parse CSV files
            FillNifTable();
            FillFixturesTable();

            // No NIFs found
            if (mapNifs.Nif.Rows.Count == 0)
            {
                MainForm.Log("No NIFs found! (nifs.csv not found or empty?)", MainForm.LogLevel.error);
                return;
            }

            // No Fixtures found
            if (mapNifs.Fixtures.Rows.Count == 0)
            {
                MainForm.Log("No fixtures found! (fixtures.csv not found or empty?)", MainForm.LogLevel.error);
                return;
            }

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
            foreach (MapNifs.NifRow nifRow in mapNifs.Nif.Rows)
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

            // Load Confiuration
            LoadModelConfiguration();

            // Load the models
            LoadPolys();
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

                //if (fields.Length == 23)
                //{
                    MapNifs.NifRow nifRow = mapNifs.Nif.NewNifRow();
                    int column = 0;
                    foreach (string fieldValue in fields)
                    {
                        nifRow[column] = Convert.ChangeType(fieldValue, mapNifs.Nif.Columns[column].DataType);
                        column++;
                    }
                    mapNifs.Nif.AddNifRow(nifRow);
                //}
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

                //if (fields.Length == 19)
                //{
                    MapNifs.FixturesRow fixtureRow = mapNifs.Fixtures.NewFixturesRow();
                    int column = 0;
                    foreach (string fieldValue in fields)
                    {
                        fixtureRow[column] = Convert.ChangeType(fieldValue, mapNifs.Fixtures.Columns[column].DataType, provider);
                        column++;
                    }
                    mapNifs.Fixtures.AddFixturesRow(fixtureRow);
                //}
            }
        }

        #endregion

        /// <summary>
        /// Load ModelCategories to ModelRendererConfiguration
        /// </summary>
        private void LoadModelConfiguration()
        {
            /*
            foreach (MapCreatorData.ModelCategoryRow row in DataWrapper.ModelsData.ModelCategory.Rows)
            {
                // Parse the textual representation of the renderer to its enum value
                Renderer renderer = Renderer.Shaded;
                try
                {
                    renderer = (Renderer)Enum.Parse(typeof(Renderer), row.renderer);
                }
                catch
                {
                    MainForm.Log(string.Format("The renderer ({1}) of the ModelCategory \"{0}\" is invalid!", row.renderer, row.renderer), MainForm.LogLevel.error);
                    continue;
                }

                RendererConfiguration conf = new RendererConfiguration(row.id, renderer, row.outline, row.is_default);
                modelRendererConfigurations.Add(row.id, conf);

                if (row.is_default)
                {
                    defaultModelRendererConfiguration = conf;
                }
            }
             * */
        }

        /// <summary>
        /// Load Polys
        /// </summary>
        private void LoadPolys()
        {
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
            foreach (MapNifs.NifRow nifRow in mapNifs.Nif.Rows)
            {
                // Skip if nif path is not found
                if (nifRow.IsArchive_PathNull()) continue;

                // The model polygons
                Polygon[] modelPolygons = null;

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
                            modelPolygons = nifParser.GetPolys();
                        }
                    }
                }
                else
                {
                    // .poly file for model is found, read it
                    modelPolygons = NifParser.ReadPoly(new StreamReader(new MemoryStream(polyMpk.GetFile(modelPolyFileName).Data)));
                }

                if (modelPolygons != null && modelPolygons.Length > 0)
                {
                    // Get the category of the model
                    string category = GetModelCategory(Path.GetFileNameWithoutExtension(nifRow.Filename));

                    // Add model to global dictionary
                    if (!m_models.ContainsKey(category))
                    {
                        m_models.Add(category, new List<NifModel>());
                    }

                    m_models[category].Add(new NifModel(nifRow, modelPolygons));
                }
            }

            // Save the mpk
            if (polyMpkModified)
            {
                polyMpk.Save(polysMpkFile);
            }

            // Delete polys directory
            Directory.Delete(polysDirectory.FullName, true);
        }

        /// <summary>
        /// Get the category of a model
        /// </summary>
        /// <param name="nifFilename"></param>
        /// <returns></returns>
        private string GetModelCategory(string nifFilename)
        {
            return "";
            /*
            foreach (MapCreatorData.ModelRow row in DataWrapper.ModelsData.Model.Rows)
            {
                Regex regex = new Regex(row.pattern.ToLower());
                if (regex.IsMatch(nifFilename.ToLower()))
                {
                    if (!row.IsrendererNull() && row.renderer.Trim() != "")
                    {
                        // Parse the textual representation of the renderer to its enum value
                        Renderer renderer = Renderer.Shaded;
                        try
                        {
                            renderer = (Renderer)Enum.Parse(typeof(Renderer), row.renderer);
                        }
                        catch
                        {
                            MainForm.Log(string.Format("The renderer ({1}) of the ModelCategory \"{0}\" is invalid!", row.renderer, row.renderer), MainForm.LogLevel.error);
                            continue;
                        }

                        RendererConfiguration modelConf = new RendererConfiguration(nifFilename, renderer, row.outline, false);
                        modelRendererConfigurations.Add(nifFilename, modelConf);
                        return nifFilename;
                    }
                    else
                    {
                        if (modelRendererConfigurations.ContainsKey(row.category))
                        {
                            return row.category;
                        }
                        else
                        {
                            MainForm.Log(string.Format("Unable to find the model category \"{0}\" of \"{1}\", using default.", row.category, row.pattern), MainForm.LogLevel.error);
                            return defaultModelRendererConfiguration.Id;
                        }
                    }
                }
            }

            // Return the default renderer
            return defaultModelRendererConfiguration.Id;
             * */
        }

        /// <summary>
        /// Calculate heights, drop to right list, parse polys and so on
        /// </summary>
        private void PrepareModels()
        {
            MainForm.ProgressReset();
            MainForm.Log("Preparing models...", MainForm.LogLevel.notice);
            MainForm.ProgressStart("Preparing models...");

            // Create paths out of the rivers
            Dictionary<RiverConfiguration, GraphicsPath> riverPaths = new Dictionary<RiverConfiguration, GraphicsPath>();
            foreach (RiverConfiguration rConf in rivers)
            {
                GraphicsPath riverPath = new GraphicsPath();
                PointF[] points = rConf.GetCoordinates().Select(c => new PointF((float)(c.X * zoneConfiguration.MapScale), (float)(c.Y * zoneConfiguration.MapScale))).ToArray();
                riverPath.AddPolygon(points);
                riverPaths.Add(rConf, riverPath);
            }
            
            /*
            foreach (KeyValuePair<RiverConfiguration, GraphicsPath> river in riverPaths)
            {
                if (river.Value.IsVisible(x, y)) return river.Key.Height;
            }
            */


            foreach (KeyValuePair<string, RendererConfiguration> renderer in modelRendererConfigurations)
            {
                // Check if there are models in this category
                if (m_models.ContainsKey(renderer.Key))
                {
                    List<NifModel> models = m_models[renderer.Key];
                    if (models.Count == 0 || renderer.Value.Renderer != Renderer.None) continue;

                    // Get fixtues with the Nif_Id of models
                    var fixtures = from fixture in mapNifs.Fixtures
                                   join model in models on fixture.Nif_Id equals model.Id
                                   select new { fixtureRow = fixture, nifModel = model };

                    foreach (var fixture in fixtures)
                    {
                        double x = zoneConfiguration.ZoneCoordinateToMapCoordinate(fixture.fixtureRow.X);
                        double y = zoneConfiguration.ZoneCoordinateToMapCoordinate(fixture.fixtureRow.Y);
                        // Parse the Z-Position of the fixture
                        double z = fixture.fixtureRow.Z;

                        // Check if "on_ground" is set
                        if (fixture.fixtureRow.On_Ground == 1)
                        {
                            z = zoneConfiguration.Heightmap.GetHeight(fixture.fixtureRow.X, fixture.fixtureRow.Y);
                        }

                        // Add the height of model (max-z) to z
                        var vectors = fixture.nifModel.Polys.SelectMany(p => p.Vectors);
                        z = vectors.Max(p => p.Z) + z;

                        int riverZ = GetRiverHeight((int)x, (int)y);

                        //Do not draw fixtures that are not on a river or are on a river and have a z higher than riverZ
                        if (riverZ == 0 || (riverZ != 0 && z > riverZ))
                        {
                            if (!m_fixturesAboveRivers.ContainsKey(renderer.Key))
                            {
                                m_fixturesAboveRivers.Add(renderer.Key, new List<FixtureModel>());
                            }

                            // Above water
                            //m_fixturesAboveRivers[renderer.Key].Add(new FixtureModel(fixture.nifModel.Polys, fixture.fixtureRow));
                        }

                        // Second pass, above water; Do not draw items that are on river and have a z lower than riverZ
                        if (riverZ != 0 && z < riverZ)
                        {
                            // Under water
                        }

                    }
                }
            }

            MainForm.ProgressReset();
        }

        /// <summary>
        /// Draw all models
        /// </summary>
        /// <param name="map"></param>
        /// <param name="underwater"></param>
        public void Draw(MagickImage map, bool underwater)
        {
            // Skip underwater if there are no rivers
            if (underwater && rivers.Count == 0) return;

            foreach (KeyValuePair<string, RendererConfiguration> renderer in modelRendererConfigurations)
            {
                // Check if there are models in this category
                if (m_models.ContainsKey(renderer.Key))
                {
                    List<NifModel> models = m_models[renderer.Key];
                    if (models.Count == 0) continue;

                    if (renderer.Value.Renderer != Renderer.None)
                    {
                        Render(map, models, renderer.Value.Renderer, renderer.Value, underwater);
                    }
                }
            }
        }

        /// <summary>
        /// Renders model using a specified configuration
        /// </summary>
        /// <param name="map"></param>
        /// <param name="models"></param>
        /// <param name="renderer"></param>
        /// <param name="conf"></param>
        /// <param name="underwater"></param>
        public void Render(MagickImage map, List<NifModel> models, Renderer renderer, RendererConfiguration conf, bool underwater)
        {
            MainForm.ProgressReset();
            MainForm.Log(string.Format("Drawing: {0} ...", renderer), MainForm.LogLevel.notice);
            MainForm.ProgressStart(string.Format("Drawing: {0} ...", renderer));
            int progressCounter = 0;

            // Get fixtues with the Nif_Id of models
            var fixtures = from fixture in mapNifs.Fixtures
                           join model in models on fixture.Nif_Id equals model.Id
                           select new { fixtureRow = fixture, nifModel = model };

            using (MagickImage modelsImage = new MagickImage(MagickColor.Transparent, map.Width, map.Height))
            {
                foreach (var fixture in fixtures)
                {
                    MapNifs.FixturesRow fixtureRow = fixture.fixtureRow;
                    NifModel model = fixture.nifModel;

                    double x = zoneConfiguration.ZoneCoordinateToMapCoordinate(fixtureRow.X);
                    double y = zoneConfiguration.ZoneCoordinateToMapCoordinate(fixtureRow.Y);

                    // Filter above and under water
                    double z = fixtureRow.Z;

                    if (fixtureRow.On_Ground == 1)
                    {
                        z = zoneConfiguration.Heightmap.GetHeight(fixtureRow.X, fixtureRow.Y);
                    }

                    var vectors = model.Polys.SelectMany(p => p.Vectors);
                    z = vectors.Max(p => p.Z) + z;

                    int riverZ = GetRiverHeight((int)x, (int)y);

                    if (underwater)
                    {
                        // Do not draw fixtures that are not on a river or are on a river and have a z higher than riverZ
                        if (riverZ == 0 || (riverZ != 0 && z > riverZ)) continue;
                    }
                    else
                    {
                        // Second pass, above water; Do not draw items that are on river and have a z lower than riverZ
                        if (riverZ != 0 && z < riverZ) continue;
                    }

                    MagickColor color = ColorTranslator.FromWin32(Convert.ToInt32(model.Row.Color));
                    float scale = (float)((fixtureRow.Scale / 100f) * zoneConfiguration.LocScale);
                    float angle = 360f - fixtureRow.A;

                    MainForm.Log(string.Format("Drawing {0}...", fixtureRow.Textual_Name), MainForm.LogLevel.notice);

                    MagickImage modelImage;
                    switch (renderer)
                    {
                        case Renderer.Shaded:
                        default:
                            modelImage = DrawShaded(model.Polys, System.Drawing.Color.White, scale, angle);
                            break;
                        case Renderer.Flat:
                            modelImage = DrawFlat(model.Polys, System.Drawing.Color.White, scale, angle);
                            break;
                        case Renderer.Tree:
                            modelImage = DrawTree(model.Polys, System.Drawing.Color.White, scale, angle);
                            break;
                    }

                    if (modelImage != null)
                    {
                        int positionX = Convert.ToInt32(x - modelImage.Width / 2);
                        int positionY = Convert.ToInt32(y - modelImage.Height / 2);
                        modelsImage.Composite(modelImage, positionX, positionY, CompositeOperator.SrcOver);
                        modelImage.Dispose();
                    }

                    MainForm.ProgressUpdate(100 * progressCounter / fixtures.Count());
                    progressCounter++;
                }

                // Add ouline if set
                if (conf.Outline)
                {
                    AddOutline(modelsImage);
                }

                MainForm.ProgressStartMarquee("Merging...");
                MainForm.Log("Merging models overlay...", MainForm.LogLevel.notice);
                map.Composite(modelsImage, 0, 0, CompositeOperator.SrcOver);
            }

            MainForm.Log(string.Format("Finished drawing {0} models!", renderer), MainForm.LogLevel.success);
            MainForm.ProgressReset();
        }

        private void AddOutline(MagickImage image)
        {
            using (MagickImage border = new MagickImage(image))
            {
                border.Alpha(AlphaOption.Deactivate);
                border.Colorize(System.Drawing.Color.FromArgb(0, 0, 0), new Percentage(60));
                border.Alpha(AlphaOption.Activate);
                //border.Blur(1, 0); // Too expensive....

                image.Composite(border, 0, 0, CompositeOperator.DstOver);

                image.Composite(border, 1, 1, CompositeOperator.DstOver);
                image.Composite(border, -1, -1, CompositeOperator.DstOver);

                image.Composite(border, 0, 1, CompositeOperator.DstOver);
                image.Composite(border, 0, -1, CompositeOperator.DstOver);

                image.Composite(border, 1, 0, CompositeOperator.DstOver);
                image.Composite(border, -1, 0, CompositeOperator.DstOver);
            }
        }
         
        /// <summary>
        /// Get the normals of a set of Vectors3 for lighning
        /// </summary>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        /// <param name="p3"></param>
        /// <returns></returns>
        private Vector3 GetNormal(Vector3 p1, Vector3 p2, Vector3 p3)
        {
            Vector3 v1 = new Vector3(p2[0] - p1[0], p2[1] - p1[1], p2[2] - p1[2]);
            Vector3 v2 = new Vector3(p3[0] - p2[0], p3[1] - p2[1], p3[2] - p2[2]);
            return Vector3.Cross(v1, v2);
        }

        /// <summary>
        /// Check wether a polygon is visible or not
        /// </summary>
        /// <param name="polygon"></param>
        /// <returns></returns>
        private bool IsVisible(Polygon polygon)
        {
            float polyArea = PolygonArea(polygon.Vectors);

            // Do not draw polygons that are have a area lower than 0.05 pixels
            if (polyArea < 0.01) return false;
            return true;
        }

        private Dictionary<RiverConfiguration, GraphicsPath> riverPaths = null;
        private int GetRiverHeight(int x, int y)
        {
            if (riverPaths == null)
            {
                riverPaths = new Dictionary<RiverConfiguration, GraphicsPath>();
                foreach (RiverConfiguration rConf in rivers)
                {
                    GraphicsPath riverPath = new GraphicsPath();
                    PointF[] points = rConf.GetCoordinates().Select(c => new PointF((float)(c.X * zoneConfiguration.MapScale), (float)(c.Y * zoneConfiguration.MapScale))).ToArray();
                    riverPath.AddPolygon(points);

                    riverPaths.Add(rConf, riverPath);
                }
            }

            foreach (KeyValuePair<RiverConfiguration, GraphicsPath> river in riverPaths)
            {
                if (river.Value.IsVisible(x, y)) return river.Key.Height;
            }

            return 0;
        }

        /// <summary>
        /// Gets the area of a polygon
        /// </summary>
        /// <param name="polygon"></param>
        /// <returns></returns>
        float PolygonArea(Vector3[] polygon)
        {
            int i, j;
            float area = 0;

            for (i = 0; i < polygon.Length; i++)
            {
                j = (i + 1) % polygon.Length;
                area += polygon[i].X * polygon[j].Y;
                area -= polygon[i].Y * polygon[j].X;
            }

            area /= 2f;
            return (area < 0 ? -area : area);
        }

        /// <summary>
        /// Draws a shaded model
        /// </summary>
        /// <param name="polys"></param>
        /// <param name="baseColor"></param>
        /// <param name="scale"></param>
        /// <param name="angle"></param>
        /// <param name="color"></param>
        /// <returns></returns>
        private MagickImage DrawShaded(Polygon[] polys, System.Drawing.Color color, float scale, float angle)
        {
            if (polys.Length == 0)
            {
                // Empty mesh
                return null;
            }

            // Scale and rotate polygons
            List<Polygon> polygons = TransformPolygons(polys, scale, angle);
            //MainForm.Log(string.Format("Removed {0} of {1} polygons!", polys.Length - polygons.Count, polys.Length));

            if (polygons.Count == 0)
            {
                // Empty mesh
                return null;
            }

            var vectors = polygons.SelectMany(p => p.Vectors);
            float minX = vectors.Min(p => p.X);
            float maxX = vectors.Max(p => p.X);
            float minY = vectors.Min(p => p.Y);
            float maxY = vectors.Max(p => p.Y);

            // Get the canvas size
            float minXProduct = (minX < 0) ? minX * -1 : minX;
            float maxXProduct = (maxX < 0) ? maxX * -1 : maxX;
            float minYProduct = (minY < 0) ? minY * -1 : minY;
            float maxYProduct = (maxY < 0) ? maxY * -1 : maxY;
            float canvasWidth = (minXProduct < maxXProduct) ? maxXProduct * 2 : minXProduct * 2;
            float canvasHeight = (minYProduct < maxYProduct) ? maxYProduct * 2 : minYProduct * 2;

            // The model image
            MagickImage model = new MagickImage(MagickColor.Transparent, Convert.ToInt32(canvasWidth), Convert.ToInt32(canvasHeight));

            // Contains all polygons
            List<DrawListEntry> drawlist = new List<DrawListEntry>();

            // Draw flatten image by irgnoring Z
            foreach (Polygon poly in polygons)
            {
                Vector3 n = Vector3.Normalize(GetNormal(poly.P1, poly.P2, poly.P3));

                // backface cull
                if (n[2] < 0) continue;

                // shade
                float ndotl = LightVector[0] * n[0] + LightVector[1] * n[1] + LightVector[2] * n[2];
                if (ndotl > 0) ndotl = 0;
                float lighting = LightMin - (LightMax - LightMin) * ndotl;

                List<Coordinate> coordinates = new List<Coordinate>();
                foreach (Vector3 vector in poly.Vectors)
                {
                    coordinates.Add(new Coordinate(canvasWidth / 2 + vector.X, canvasHeight / 2 - vector.Y));
                }

                // We want to draw the vectors in z-oder
                float maxZ = poly.Vectors.Max(p => p.Z);
                drawlist.Add(new DrawListEntry(maxZ, lighting, coordinates, color));
            }

            foreach (DrawListEntry entry in drawlist.OrderBy(o => o.Order))
            {
                using (DrawablePolygon polyDraw = new DrawablePolygon(entry.Coordinates))
                {
                    model.FillColor = new MagickColor(entry.Lightning * entry.Color.R, entry.Lightning * entry.Color.G, entry.Lightning * entry.Color.B);
                    model.Draw(polyDraw);
                }
            }

            return model;
        }

        /// <summary>
        /// Draws a flat model
        /// </summary>
        /// <param name="polys"></param>
        /// <param name="baseColor"></param>
        /// <param name="scale"></param>
        /// <param name="angle"></param>
        /// <param name="color"></param>
        /// <returns></returns>
        private MagickImage DrawFlat(Polygon[] polys, System.Drawing.Color color, float scale, float angle)
        {
            if (polys.Length == 0)
            {
                // Empty mesh
                return null;
            }

            // Scale and rotate polygons
            List<Polygon> polygons = TransformPolygons(polys, scale, angle);
            //MainForm.Log(string.Format("Removed {0} of {1} polygons!", polys.Length - polygons.Count, polys.Length));

            if (polygons.Count == 0)
            {
                // Empty mesh
                return null;
            }

            var vectors = polygons.SelectMany(p => p.Vectors);
            float minX = vectors.Min(p => p.X);
            float maxX = vectors.Max(p => p.X);
            float minY = vectors.Min(p => p.Y);
            float maxY = vectors.Max(p => p.Y);

            // Get the canvas size
            float minXProduct = (minX < 0) ? minX * -1 : minX;
            float maxXProduct = (maxX < 0) ? maxX * -1 : maxX;
            float minYProduct = (minY < 0) ? minY * -1 : minY;
            float maxYProduct = (maxY < 0) ? maxY * -1 : maxY;
            float canvasWidth = (minXProduct < maxXProduct) ? maxXProduct * 2 : minXProduct * 2;
            float canvasHeight = (minYProduct < maxYProduct) ? maxYProduct * 2 : minYProduct * 2;

            // The model image
            MagickImage model = new MagickImage(MagickColor.Transparent, Convert.ToInt32(canvasWidth), Convert.ToInt32(canvasHeight));

            // Draw flat with just white
            model.FillColor = System.Drawing.Color.White;

            // Contains all polygons
            List<DrawListEntry> drawlist = new List<DrawListEntry>();

            // Draw flatten image by irgnoring Z
            foreach (Polygon poly in polygons)
            {
                List<Coordinate> coordinates = new List<Coordinate>();
                foreach (Vector3 vector in poly.Vectors)
                {
                    coordinates.Add(new Coordinate(canvasWidth / 2 + vector.X, canvasHeight / 2 - vector.Y));
                }

                // We want to draw the vectors in z-oder
                float maxZ = poly.Vectors.Max(p => p.Z);
                drawlist.Add(new DrawListEntry(maxZ, 0, coordinates, color));
            }

            foreach (DrawListEntry entry in drawlist.OrderBy(o => o.Order))
            {
                using (DrawablePolygon polyDraw = new DrawablePolygon(entry.Coordinates))
                {
                    model.Draw(polyDraw);
                }
            }

            return model;
        }

        /// <summary>
        /// Draws a tree
        /// </summary>
        /// <param name="polys"></param>
        /// <param name="color"></param>
        /// <param name="scale"></param>
        /// <param name="angle"></param>
        /// <returns></returns>
        private MagickImage DrawTree(Polygon[] polys, System.Drawing.Color color, float scale, float angle)
        {
            return null;
        }

        /// <summary>
        /// Transforms polygons
        /// </summary>
        /// <param name="polys"></param>
        /// <param name="scale"></param>
        /// <param name="angle"></param>
        /// <returns></returns>
        private List<Polygon> TransformPolygons(IEnumerable<Polygon> polys, float scale, float angle)
        {
            SharpDX.Matrix rotation = SharpDX.Matrix.Identity;
            if (angle != 0)
            {
                rotation = SharpDX.Matrix.RotationZ((float)(angle * Math.PI / 180.0));
            }

            List<Polygon> polygons = new List<Polygon>();
            foreach (Polygon poly in polys)
            {
                Vector3 p1 = Vector3.TransformCoordinate(poly.P1, rotation);
                Vector3 p2 = Vector3.TransformCoordinate(poly.P2, rotation);
                Vector3 p3 = Vector3.TransformCoordinate(poly.P3, rotation);

                if (scale != 1)
                {
                    p1.X *= (float)scale;
                    p1.Y *= (float)scale;
                    p1.Z *= (float)scale;

                    p2.X *= (float)scale;
                    p2.Y *= (float)scale;
                    p2.Z *= (float)scale;

                    p3.X *= (float)scale;
                    p3.Y *= (float)scale;
                    p3.Z *= (float)scale;
                }

                // Check visibility of polygons
                Polygon newPolygon = new Polygon(p1, p2, p3);
                if (IsVisible(newPolygon))
                {
                    polygons.Add(newPolygon);
                }
            }

            return polygons;
        }

    }

    class DrawListEntry : IEnumerable
    {
        private float m_order;

        public float Order
        {
            get { return m_order; }
            set { m_order = value; }
        }

        private float m_lightning;

        public float Lightning
        {
            get { return m_lightning; }
            set { m_lightning = value; }
        }

        private IEnumerable<Coordinate> m_coordinates;

        internal IEnumerable<Coordinate> Coordinates
        {
            get { return m_coordinates; }
            set { m_coordinates = value; }
        }

        private MagickColor m_color;
        public MagickColor Color
        {
            get { return m_color; }
            set { m_color = value; }
        }

        public DrawListEntry(float order, float lightning, IEnumerable<Coordinate> coordinates, MagickColor color)
        {
            m_order = order;
            m_lightning = lightning;
            m_coordinates = coordinates;
            m_color = color;
        }

        public IEnumerator GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }

    class NifModel
    {
        private int m_id;
        public int Id
        {
            get { return m_id; }
        }

        private MapNifs.NifRow m_row;
        public MapNifs.NifRow Row
        {
            get { return m_row; }
        }

        private Polygon[] m_polys;
        public Polygon[] Polys
        {
            get { return m_polys; }
        }

        private MagickImage image;
        public MagickImage Image
        {
            get { return image; }
            set { image = value; }
        }

        public NifModel(MapNifs.NifRow row, Polygon[] polys)
        {
            m_id = row.Id;
            m_row = row;
            m_polys = polys;
        }
    }

}
