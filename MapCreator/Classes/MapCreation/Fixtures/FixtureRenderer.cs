using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MapCreator.data;
using System.Windows.Forms;
using System.IO;
using System.Text.RegularExpressions;

namespace MapCreator
{
    enum FixtureRendererType
    {
        None,
        Shaded,
        Flat,
        Tree
    }

    class FixtureRenderer
    {

        private static MapCreatorData fixtureFilterData = new MapCreatorData();
        public static MapCreatorData FixtureFilterData
        {
            get { return FixtureRenderer.fixtureFilterData; }
        }

        /// <summary>
        /// Model Renderer Configurations
        /// </summary>
        private static Dictionary<string, FixtureRendererConfiguration> fixtureRendererConfigurations = new Dictionary<string, FixtureRendererConfiguration>();
        private static FixtureRendererConfiguration defaultFixtureRendererConfiguration;

        /// <summary>
        /// Initialization
        /// </summary>
        static FixtureRenderer()
        {
            string fixtureFilterXmlFile = string.Format("{0}\\fixture_renderer.xml", Application.StartupPath);
            File.Delete(fixtureFilterXmlFile);

            if (!File.Exists(fixtureFilterXmlFile))
            {
                GenerateDefaultModelValues();
                fixtureFilterData.WriteXml(fixtureFilterXmlFile);
            }
            else
            {
                fixtureFilterData.ReadXml(fixtureFilterXmlFile);
            }

            ParseRendererConfigurations();
        }

        /// <summary>
        /// Generates some default fixture filter
        /// </summary>
        private static void GenerateDefaultModelValues()
        {
            // Categories
            var noneCategory = fixtureFilterData.FixtureFilterCategory.AddFixtureFilterCategoryRow(
                "None", FixtureRendererType.None.ToString(), 0, 0, "1,1,-1", "#FFFFFF", 0, false, false
            );
            var structuresCategory = fixtureFilterData.FixtureFilterCategory.AddFixtureFilterCategoryRow(
                "Buildings", FixtureRendererType.Shaded.ToString(), 0.7, 1.0, "1,1,-1", "#FFFFFF", 0, true, true
            );
            var decorCategory = fixtureFilterData.FixtureFilterCategory.AddFixtureFilterCategoryRow(
                "Decor", FixtureRendererType.Flat.ToString(), 0, 0, "1,1,-1", "#FFFFFF", 0, true, false
            );
            var treesCategory = fixtureFilterData.FixtureFilterCategory.AddFixtureFilterCategoryRow(
                "Trees", FixtureRendererType.Tree.ToString(), 0.8, 1.0, "1,1,-1", "#003800", 75, false, false
            );

            // Knwon Non-Drawabls
            //modelsData.Model.AddModelRow("", noneCategory, "", "");

            //
            // Known Trees
            //
            List<string> trees = new List<string>()
            {
                // Albion
                "B_HTOAK[0-9]+", "Bmtntre1", "Bpinetree", "bpintre1", "BScryPine", "BSpanMoss", "bpinea", "bwillow",
                
                // Hibernia
                "Hbareskny", "BigHibTree", "HElm", "HElm1grey", "HBirchtree", "Hdeadtree", "HBirchSingle", 
                "HElm2", "HElm2tan", "Hlowtree", "HOaktree",
                
                // Midgard
                "Nbirchtree", "NPineA", "NPineA-s", "NPineDk", "Npinetree", "Npinetree-s", "npintre1", "NPintre-S",

                // other
                "ash", "beech.*", "BigPalm", "bigtree", "burnttree", "cedar", "chestnut", "creepywebPine", "hazel", "maple", 
                "mightyoak", "mightyoak-small", "tall_whitepine.*", "TALLOAK1", "yew", "hollytree", "elm[0-9]", "oak[0-9]+", 
                "appletree", "olivetree", "pintre[0-9]+", "crookedpalm", "SmallPalm", "reedclump1", "SkinnyPalm"

            };
            foreach (string tree in trees) fixtureFilterData.FixtureFilter.AddFixtureFilterRow(tree, treesCategory, "", 0, 0, "", "", 0, false);

            //
            // Known decor (rocks and other)
            //
            List<string> decors = new List<string>()
            {
                "b-fence[0-9]+", "stone[0-9]+", "NF_b-fence[0-9]+" // Alb
            };
            foreach (string decor in decors) fixtureFilterData.FixtureFilter.AddFixtureFilterRow(decor, decorCategory, "", 0, 0, "", "", 0, false);

            fixtureFilterData.FixtureFilter.AddFixtureFilterRow("example_custom_renderer_place_nifname_here", null, "Flat", 0.5, 2.0, "1,1,-1", "#000000", 25, true);
        }

        /// <summary>
        /// Parses all filters to configurations
        /// </summary>
        private static void ParseRendererConfigurations()
        {
            List<FixtureRendererConfiguration> renderer = new List<FixtureRendererConfiguration>();
            foreach (MapCreatorData.FixtureFilterCategoryRow filterCategoryRow in FixtureFilterData.FixtureFilterCategory)
            {
                FixtureRendererConfiguration rendererConf = new FixtureRendererConfiguration(filterCategoryRow.id);
                rendererConf.Renderer = GetRendererType(filterCategoryRow.renderer);
                rendererConf.LightMin = (float)filterCategoryRow.light_min;
                rendererConf.LightMax = (float)filterCategoryRow.light_max;
                rendererConf.LightVector = GetLightVector(filterCategoryRow.light_vector);
                rendererConf.Outline = filterCategoryRow.outline;
                rendererConf.Transparency = filterCategoryRow.transparency;
                try
                {
                    rendererConf.Color = System.Drawing.ColorTranslator.FromHtml(filterCategoryRow.color);
                }
                catch
                {
                    MainForm.Log(string.Format("The Color \"{0}\" is invalid.", filterCategoryRow.color), MainForm.LogLevel.warning);
                    rendererConf.Color = System.Drawing.Color.White;
                }
                
                
                renderer.Add(rendererConf);

                if (filterCategoryRow.is_default)
                {
                    defaultFixtureRendererConfiguration = rendererConf;
                }
            }

            foreach (MapCreatorData.FixtureFilterRow filterRow in fixtureFilterData.FixtureFilter)
            {
                FixtureRendererConfiguration rendererConf;
                if (filterRow.IscategoryNull() || filterRow.category == "")
                {
                    rendererConf = new FixtureRendererConfiguration(filterRow.pattern);
                    rendererConf.Renderer = GetRendererType(filterRow.renderer);
                    rendererConf.LightMin = (float)filterRow.light_min;
                    rendererConf.LightMax = (float)filterRow.light_max;
                    rendererConf.LightVector = GetLightVector(filterRow.light_vector);
                    rendererConf.Outline = filterRow.outline;
                    rendererConf.Transparency = filterRow.transparency;

                    try
                    {
                        rendererConf.Color = System.Drawing.ColorTranslator.FromHtml(filterRow.color);
                    }
                    catch
                    {
                        MainForm.Log(string.Format("The Color \"{0}\" is invalid.", filterRow.color), MainForm.LogLevel.warning);
                        rendererConf.Color = System.Drawing.Color.White;
                    }
                }
                else
                {
                    var result = renderer.Where(r => r.Name == filterRow.category);
                    if (result.Count() == 0)
                    {
                        MainForm.Log(string.Format("The FilterCategory \"{0}\" was not found, using default.", filterRow.category), MainForm.LogLevel.warning);
                        rendererConf = defaultFixtureRendererConfiguration;
                    }
                    else
                    {
                        rendererConf = result.First();
                    }
                }

                // Check if the pattern is valid
                try
                {
                    Regex regexTest = new Regex(filterRow.pattern);
                    fixtureRendererConfigurations.Add(filterRow.pattern, rendererConf);
                }
                catch
                {
                    MainForm.Log(string.Format("The pattern of the filter \"{0}\" is invalid.", filterRow.pattern), MainForm.LogLevel.warning);
                }
            }
        }

        /// <summary>
        /// Gets the enum value out a text value
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        private static FixtureRendererType GetRendererType(string name)
        {
            // Parse the textual representation of the renderer to its enum value
            FixtureRendererType renderer = FixtureRendererType.Shaded;
            try
            {
                renderer = (FixtureRendererType)Enum.Parse(typeof(FixtureRendererType), name);
            }
            catch
            {
                MainForm.Log(string.Format("The renderer \"{0}\" is invalid!", name), MainForm.LogLevel.error);
            }

            return renderer;
        }

        private static SharpDX.Vector3 GetLightVector(string lightVector)
        {
            if(lightVector == "") return new SharpDX.Vector3(1f, 1f, -1f);

            string[] parts = lightVector.Split(',');
            if (parts.Length != 3)
            {
                MainForm.Log(string.Format("LightVector \"{0}\" is inivalid.", lightVector), MainForm.LogLevel.warning);
                return new SharpDX.Vector3(1f, 1f, -1f);
            }

            try
            {
                return new SharpDX.Vector3(Convert.ToSingle(parts[0]), Convert.ToSingle(parts[1]), Convert.ToSingle(parts[2]));
            }
            catch
            {
                MainForm.Log(string.Format("LightVector \"{0}\" is inivalid.", lightVector), MainForm.LogLevel.warning);
                return new SharpDX.Vector3(1f, 1f, -1f);
            }
        }

        /// <summary>
        /// Gets the RendererConfiguration of the specifield nif name
        /// </summary>
        /// <param name="nifName"></param>
        /// <returns></returns>
        public static FixtureRendererConfiguration GetRendererConfiguration(string nifName)
        {
            foreach (KeyValuePair<string, FixtureRendererConfiguration> renderer in fixtureRendererConfigurations)
            {
                Regex regex = new Regex(renderer.Key.ToLower(), RegexOptions.IgnoreCase);
                if (regex.IsMatch(nifName))
                {
                    return renderer.Value;
                }
            }

            return defaultFixtureRendererConfiguration;
        }

    }

    struct FixtureRendererConfiguration
    {
        public string Name;
        public FixtureRendererType Renderer;
        public float LightMin;
        public float LightMax;
        public SharpDX.Vector3 LightVector;
        public System.Drawing.Color Color;
        public int Transparency;
        public bool Outline;

        public FixtureRendererConfiguration(string name)
        {
            Name = name;

            Renderer = FixtureRendererType.Shaded;
            LightMin = 0.5f;
            LightMax = 1.0f;
            LightVector = new SharpDX.Vector3(1f, 1f, -1f);
            Color = System.Drawing.Color.White;
            Transparency = 100;
            Outline = true;
        }
    }
}
