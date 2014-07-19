using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using MPKLib;
using NifUtil;

namespace MapCreator.Fixtures
{
    static class FixturesLoader
    {
        private static ZoneConfiguration zoneConf;

        private static List<NifRow> nifRows = new List<NifRow>();
        private static List<FixtureRow> fixtureRows = new List<FixtureRow>();
        private static List<TreeRow> treeRows = new List<TreeRow>();
        private static List<TreeClusterRow> treeClusterRows = new List<TreeClusterRow>();

        // Location where to search for npk with nifs
        private static List<string> nifSearchPaths = new List<string>();

        private static List<string> nifObjectImages = new List<string>();

        internal static List<NifRow> NifRows
        {
            get { return FixturesLoader.nifRows; }
            set { FixturesLoader.nifRows = value; }
        }

        public static void Initialize(ZoneConfiguration zoneConfiguration)
        {
            FixturesLoader.zoneConf = zoneConfiguration;

            // Clear on call
            nifRows.Clear();
            fixtureRows.Clear();
            // Trees and TreeCluster are always the same, do not load on each progress
            //treeRows.Clear();
            //treeClusterRows.Clear();

            LoadCsvData();
            LoadPolygons();

            // Load the filenames in data/prerendered/objects
            string objectImageFileDirectory = string.Format("{0}\\data\\prerendered\\objects", System.Windows.Forms.Application.StartupPath);
            if (!Directory.Exists(objectImageFileDirectory)) Directory.CreateDirectory(objectImageFileDirectory);
            nifObjectImages = Directory.GetFiles(objectImageFileDirectory).Select(f => Path.GetFileNameWithoutExtension(f).ToLower()).ToList();
        }

        /// <summary>
        /// Load all required CSV data
        /// </summary>
        private static void LoadCsvData() {
            MainForm.ProgressStartMarquee("Loading fixture data ...");

            List<string> nifsCsvRows = DataWrapper.GetFileContent(zoneConf.CvsMpk, "nifs.csv");
            List<string> fixturesRows = DataWrapper.GetFileContent(zoneConf.CvsMpk, "fixtures.csv");

            // Create a NumberFormatInfo object for floats and set some of its properties.
            System.Globalization.NumberFormatInfo provider = new System.Globalization.NumberFormatInfo();
            provider.NumberDecimalSeparator = ".";
            provider.NumberGroupSeparator = "";
            provider.NumberGroupSizes = new int[] { 2 };

            foreach (string row in nifsCsvRows)
            {
                if (row.StartsWith("Grid") || row.StartsWith("NIF")) continue;

                string[] fields = row.Split(',');

                NifRow nifRow = new NifRow();
                nifRow.NifId = Convert.ToInt32(fields[0]);
                nifRow.TextualName = fields[1];
                nifRow.Filename = fields[2];
                nifRow.Color = Convert.ToInt32(fields[5]);
                nifRows.Add(nifRow);
            }

            // Read fixtures.csv
            foreach (string row in fixturesRows)
            {
                if (row.StartsWith("Fixtures") || row.StartsWith("ID")) continue;

                string[] fields = row.Split(',');
                FixtureRow fixtureRow = new FixtureRow();
                fixtureRow.Id = Convert.ToInt32(fields[0]);
                fixtureRow.NifId = Convert.ToInt32(fields[1]);
                fixtureRow.TextualName = fields[2];
                fixtureRow.X = Convert.ToDouble(fields[3], provider);
                fixtureRow.Y = Convert.ToDouble(fields[4], provider);
                fixtureRow.Z = Convert.ToDouble(fields[5], provider);
                fixtureRow.A = Convert.ToInt32(fields[6]);
                fixtureRow.Scale = Convert.ToInt32(fields[7]);
                fixtureRow.OnGround = (Convert.ToInt32(fields[11]) == 1) ? true : false;
                fixtureRow.Flip = (Convert.ToInt32(fields[12]) == 1) ? true : false;

                if (fields.Length > 15)
                {
                    fixtureRow.Angle3D = Convert.ToDouble(fields[15], provider);
                    fixtureRow.AxisX3D = Convert.ToDouble(fields[16], provider);
                    fixtureRow.AxisY3D = Convert.ToDouble(fields[17], provider);
                    fixtureRow.AxisZ3D = Convert.ToDouble(fields[18], provider);
                }

                fixtureRows.Add(fixtureRow);
            }

            // Only load on first init
            if (treeRows.Count == 0)
            {
                string treeMpk = string.Format("{0}\\zones\\trees\\treemap.mpk", Properties.Settings.Default.game_path);
                string treeClusterMpk = string.Format("{0}\\zones\\trees\\tree_clusters.mpk", Properties.Settings.Default.game_path);

                List<string> treesCsvRows = DataWrapper.GetFileContent(treeMpk, "Treemap.csv");
                List<string> treeClusterCsvRows = DataWrapper.GetFileContent(treeClusterMpk, "tree_clusters.csv");

                foreach (string row in treesCsvRows)
                {
                    if (row.StartsWith("NIF Name")) continue;

                    string[] fields = row.Split(',');
                    //if (fields[4] == "") continue;

                    TreeRow treeRow = new TreeRow();
                    treeRow.Name = fields[0];
                    treeRow.ZOffset = (string.IsNullOrEmpty(fields[4])) ? 0 : Convert.ToInt32(fields[4]);
                    treeRows.Add(treeRow);
                }

                foreach (string row in treeClusterCsvRows)
                {
                    if (row.StartsWith("name")) continue;
                    if (row == "") continue;

                    string[] fields = row.Split(',');
                    TreeClusterRow treeClusterRow = new TreeClusterRow();
                    treeClusterRow.Name = fields[0];
                    treeClusterRow.Tree = fields[1];
                    treeClusterRow.TreeInstances = new List<SharpDX.Vector3>();
                    for (int i = 2; i < fields.Length; i = i + 3)
                    {
                        if(fields[i] == "" || fields[i+1] == "" || fields[i+2] == "") break;

                        float x = Convert.ToSingle(fields[i], provider);
                        float y = Convert.ToSingle(fields[i + 1], provider);
                        float z = Convert.ToSingle(fields[i + 2], provider);
                        if (x == 0 && y == 0 && z == 0) break;

                        treeClusterRow.TreeInstances.Add(new SharpDX.Vector3(x, y, z));
                    }

                    treeClusterRows.Add(treeClusterRow);
                }
            }
            
            // Add the trees of the clusters to the cache
            for (int i = 0; i < nifRows.Count; i++ )
            {
                bool isTreeCluster = treeClusterRows.Any(tc => tc.Name.ToLower() == nifRows[i].Filename.ToLower());
                if (isTreeCluster)
                {
                    TreeClusterRow treeCluster = treeClusterRows.Where(tc => tc.Name.ToLower() == nifRows[i].Filename.ToLower()).FirstOrDefault();
                    if(treeCluster == null || nifRows.Where(n => n.Filename == treeCluster.Tree).Count() > 0) continue;

                    NifRow tree = new NifRow();
                    tree.NifId = 10000 + i;
                    tree.TextualName = treeCluster.Tree + " (cluster tree)";
                    tree.Filename = treeCluster.Tree;
                    nifRows.Add(tree);
                }
            }

            MainForm.ProgressReset();
        }

        /// <summary>
        /// Load the Polygons if the nifRows
        /// </summary>
        private static void LoadPolygons()
        {
            if (nifRows.Count() == 0 || fixtureRows.Count() == 0) return;

            // MainForm progress
            MainForm.Log("Loading polygons ...", MainForm.LogLevel.notice);
            MainForm.ProgressStart("Loading polygons ...");

            DirectoryInfo polysDirectory = new DirectoryInfo(string.Format("{0}\\data\\polys", System.Windows.Forms.Application.StartupPath));
            if (!polysDirectory.Exists) polysDirectory.Create();

            string polysMpkFile = string.Format("{0}\\data\\polys.mpk", System.Windows.Forms.Application.StartupPath);

            MPAK polyMpk = new MPAK();
            bool polyMpkModified = false;

            if (!File.Exists(polysMpkFile)) polyMpkModified = true; // Create a new poyls.mpk
            else polyMpk.Load(polysMpkFile); // Load existing polys.mpk

            // Loop all nifs from nifs.csv
            int progressCounter = 0;
            foreach (NifRow nifRow in nifRows)
            {
                // Check if this nif is a TreeCluster
                bool isTreeCluster = treeClusterRows.Any(tc => tc.Name.ToLower() == nifRow.Filename.ToLower());
                if(isTreeCluster) continue;

                // The poly filename
                string modelPolyFileName = Path.GetFileNameWithoutExtension(nifRow.Filename) + ".poly";
                string modelPolySavePath = string.Format("{0}\\{1}", polysDirectory, modelPolyFileName);

                // MPK handling, cache .poly file for models
                if (polyMpk.Files.Where(f => f.Name.ToLower() == modelPolyFileName.ToLower()).Count() == 0)
                {
                    string nifArchivePath = FindNifArchive(nifRow);
                    if(string.IsNullOrEmpty(nifArchivePath)) continue;

                    // open the archive
                    using (StreamReader nifFileFromNpk = MpkWrapper.GetFileFromMpk(nifArchivePath, nifRow.Filename))
                    {
                        if (nifFileFromNpk != null)
                        {
                            MainForm.Log(string.Format("Processing {0}...", nifRow.TextualName), MainForm.LogLevel.notice);

                            // Create a new poly and add to mpk
                            polyMpkModified = true;

                            NifParser nifParser = new NifParser();
                            nifParser.Load(nifFileFromNpk);
                            nifParser.Convert(ConvertType.Poly, modelPolySavePath);

                            // Add file to polys.mpk
                            polyMpk.AddFile(modelPolySavePath);

                            // Assign polys
                            nifRow.Polygons = nifParser.GetPolys();
                        }
                    }
                }
                else
                {
                    // .poly file for model is found, read it
                    nifRow.Polygons = NifParser.ReadPoly(new StreamReader(new MemoryStream(polyMpk.GetFile(modelPolyFileName).Data)));
                }

                int percent = 100 * progressCounter / nifRows.Count;
                MainForm.ProgressUpdate(percent);
                progressCounter++;
            }

            MainForm.ProgressStartMarquee("Saving polygons ...");

            // Save the mpk
            if (polyMpkModified)
            {
                polyMpk.Save(polysMpkFile);
            }

            // Delete polys directory
            Directory.Delete(polysDirectory.FullName, true);

            MainForm.Log("Polygons loaded!", MainForm.LogLevel.success);
            MainForm.ProgressReset();
        }

        public static List<DrawableFixture> GetDrawableFixtures()
        {
            List<DrawableFixture> drawables = new List<DrawableFixture>();

            // MainForm progress
            MainForm.Log("Preparing fixtures ...", MainForm.LogLevel.notice);
            MainForm.ProgressStart("Preparing fixtures ...");

            int progressCounter = 0;
            foreach (FixtureRow fixtureRow in fixtureRows)
            {
                NifRow nifRow = nifRows.Where(n => n.NifId == fixtureRow.NifId).FirstOrDefault();
                if (nifRow == null) continue;

                DrawableFixture fixture = new DrawableFixture();

                // Set default values
                fixture.Name = fixtureRow.TextualName;
                fixture.NifName = nifRow.Filename;
                fixture.FixtureRow = fixtureRow;
                fixture.ZoneConf = zoneConf;

                // Get renderer configuration
                FixtureRendererConfiguration2? rConf = FixtureRendererConfigurations.GetFixtureRendererConfiguration(nifRow.Filename);

                fixture.IsTree = treeRows.Any(t => t.Name.ToLower() == nifRow.Filename.ToLower());
                fixture.IsTreeCluster = treeClusterRows.Any(tc => tc.Name.ToLower() == nifRow.Filename.ToLower());

                if (rConf != null && (rConf.Value.Name == "TreeShaded" || rConf.Value.Name == "TreeImage"))
                {
                    fixture.IsTree = true;
                }

                if (fixture.IsTree)
                {
                    fixture.Tree = treeRows.Where(tc => tc.Name.ToLower() == nifRow.Filename.ToLower()).FirstOrDefault();
                    fixture.RawPolygons = nifRow.Polygons;

                    if (rConf == null) fixture.RendererConf = FixtureRendererConfigurations.GetRendererById("TreeImage");
                    else fixture.RendererConf = rConf.GetValueOrDefault();
                }
                else if (fixture.IsTreeCluster)
                {
                    fixture.TreeCluster = treeClusterRows.Where(tc => tc.Name.ToLower() == nifRow.Filename.ToLower()).FirstOrDefault();

                    // Get the polygons of the base nif
                    var treeNif = nifRows.Where(n => n.Filename.ToLower() == fixture.TreeCluster.Tree.ToLower()).FirstOrDefault();
                    if (treeNif == null) continue;
                    Polygon[] baseTreePolygons = treeNif.Polygons;

                    // Loop the instances and transform the polygons
                    List<Polygon> treeClusterPolygons = new List<Polygon>();
                    foreach (SharpDX.Vector3 tree in fixture.TreeCluster.TreeInstances)
                    {
                        foreach (Polygon treePolygon in baseTreePolygons)
                        {
                            Polygon newPolygon = new Polygon(treePolygon.P1, treePolygon.P2, treePolygon.P3);
                            for (int i = 0; i < newPolygon.Vectors.Length; i++)
                            {
                                newPolygon.Vectors[i].X -= tree.X;
                                newPolygon.Vectors[i].Y += tree.Y;
                                newPolygon.Vectors[i].Z += tree.Z;
                            }
                            treeClusterPolygons.Add(newPolygon);
                        }
                    }
                    fixture.RawPolygons = treeClusterPolygons;

                    if (rConf == null) fixture.RendererConf = FixtureRendererConfigurations.GetRendererById("TreeImage");
                    else fixture.RendererConf = rConf.GetValueOrDefault();
                }
                else
                {
                    fixture.RawPolygons = nifRow.Polygons;

                    if (rConf == null)
                    {
                        string nifFilenamWithoutExtension = Path.GetFileNameWithoutExtension(fixture.NifName);
                        if (nifObjectImages.Contains(nifFilenamWithoutExtension.ToLower()))
                        {
                            fixture.RendererConf = FixtureRendererConfigurations.GetRendererById("Prerendered");
                        }
                        else
                        {
                            fixture.RendererConf = FixtureRendererConfigurations.DefaultConfiguration;
                        }
                    }
                    else fixture.RendererConf = rConf.GetValueOrDefault();
                }

                // Calculate the final look of the model
                fixture.Calc();

                drawables.Add(fixture);

                progressCounter++;
                int percent = 100 * progressCounter / fixtureRows.Count;
                MainForm.ProgressUpdate(percent);
            }

            MainForm.Log("Fixtures prepared!", MainForm.LogLevel.success);
            MainForm.ProgressReset();

            return drawables;
        }

        private static string FindNifArchive(NifRow nifRow)
        {
            if (nifSearchPaths.Count == 0)
            {
                nifSearchPaths.Add(string.Format("{0}\\{1}", Properties.Settings.Default.game_path, "Newtowns\\zones\\Nifs")); // Newtows
                nifSearchPaths.Add(string.Format("{0}\\{1}", zoneConf.ZoneDirectory, "nifs")); // Current Zone Directory
                nifSearchPaths.Add(string.Format("{0}\\{1}", Properties.Settings.Default.game_path, "zones\\Nifs")); // Globals zones nif dir
                //nifPaths.Add(string.Format("{0}\\{1}", Properties.Settings.Default.game_path, "zones\\trees")); // Global trees nif dir: removed, theses nifs are CAD files
                nifSearchPaths.Add(string.Format("{0}\\{1}", Properties.Settings.Default.game_path, "frontiers\\NIFS")); // Frontiers
                nifSearchPaths.Add(string.Format("{0}\\{1}", Properties.Settings.Default.game_path, "phousing\\nifs")); // Housing
                nifSearchPaths.Add(string.Format("{0}\\{1}", Properties.Settings.Default.game_path, "Tutorial\\zones\\nifs")); // Tutorial
                nifSearchPaths.Add(string.Format("{0}\\{1}", Properties.Settings.Default.game_path, "pregame")); // Pregame?
                nifSearchPaths.Add(string.Format("{0}\\{1}", Properties.Settings.Default.game_path, "zones\\Dnifs")); // Guess Dungeon nifs
            }

            // Search NPKs
            string archiveName = Path.GetFileNameWithoutExtension(nifRow.Filename) + ".npk";
            //MainForm.Log(string.Format("Searching for {0}", archiveName), MainForm.LogLevel.notice);
            foreach (string dir in nifSearchPaths)
            {
                if (Directory.Exists(dir) && Directory.GetFiles(dir, archiveName).Length > 0)
                {
                    //MainForm.Log(string.Format("Found {0} in {1}!", archiveName, dir), MainForm.LogLevel.success);
                    return string.Format("{0}\\{1}", dir, archiveName);
                }
            }

            MainForm.Log(string.Format("Unable to find nif \"{0}\"!", nifRow.Filename), MainForm.LogLevel.warning);
            return null;
        }

    }
}
