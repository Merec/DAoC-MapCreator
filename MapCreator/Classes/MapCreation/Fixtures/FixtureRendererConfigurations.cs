//
// MapCreator
// Copyright(C) 2015 Stefan Schäfer <merec@merec.org>
//
// This program is free software; you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation; either version 2 of the License, or
// (at your option) any later version.
//
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU General Public License along
// with this program; if not, write to the Free Software Foundation, Inc.,
// 51 Franklin Street, Fifth Floor, Boston, MA 02110-1301 USA.
//

using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace MapCreator
{
    enum FixtureRenderererType
    {
        None,
        Shaded,
        Flat,
        Image
    }

    class FixtureRendererConfigurations
    {
        private static XDocument fixturesXml;

        private static List<FixtureRendererConfiguration2> rendererCategories = new List<FixtureRendererConfiguration2>();

        private static Dictionary<string, FixtureRendererConfiguration2> configurations = new Dictionary<string, FixtureRendererConfiguration2>();
        private static FixtureRendererConfiguration2 defaultConfiguration;

        internal static FixtureRendererConfiguration2 DefaultConfiguration
        {
            get { return FixtureRendererConfigurations.defaultConfiguration; }
            set { FixtureRendererConfigurations.defaultConfiguration = value; }
        }

        /// <summary>
        /// Static constructor
        /// </summary>
        static FixtureRendererConfigurations()
        {
            // Read Zones
            fixturesXml = XDocument.Load(string.Format("{0}\\fixtures.xml", Application.StartupPath));
            ParseFixturesXml();
        }

        /// <summary>
        /// Get the renderer configuration of a nifname
        /// </summary>
        /// <param name="nifname"></param>
        /// <returns></returns>
        public static FixtureRendererConfiguration2? GetFixtureRendererConfiguration(string nifname)
        {
            foreach (KeyValuePair<string, FixtureRendererConfiguration2> renderer in configurations)
            {
                Regex regex = new Regex(renderer.Key.ToLower(), RegexOptions.IgnoreCase);
                if (regex.IsMatch(nifname))
                {
                    return renderer.Value;
                }
            }
            return null;
        }

        public static FixtureRendererConfiguration2 GetRendererById(string id)
        {
            var conf = rendererCategories.Where(c => c.Name == id);
            if (conf.Count() == 0) return defaultConfiguration;
            else return conf.FirstOrDefault();
        }

        /// <summary>
        /// Parses all filters to configurations
        /// </summary>
        public static void ParseFixturesXml()
        {
            var categories = fixturesXml.Descendants("FixtureCategory");
            //List<FixtureRendererConfiguration2> rendererConfigurations = new List<FixtureRendererConfiguration2>();
            foreach (XElement category in categories)
            {
                FixtureRendererConfiguration2? rendererConfiguration = ParseRendererConfigurationFromXmlNode(category);
                if (rendererConfiguration == null) continue;
                rendererCategories.Add(rendererConfiguration.GetValueOrDefault());
            }

            var fixtures = fixturesXml.Descendants("Fixture");
            foreach (XElement fixture in fixtures)
            {
                var patternNode = fixture.Descendants("pattern");
                if (patternNode.Count() == 0 || string.IsNullOrEmpty(patternNode.First().Value))
                {
                    MainForm.Log(string.Format("Fixtures: Error in fixtures.xml, no file pattern set."), MainForm.LogLevel.error);
                    continue;
                }

                // Check pattern
                try
                {
                    Regex regexTest = new Regex(patternNode.First().Value);
                }
                catch
                {
                    MainForm.Log(string.Format("Fixtures: Error in fixtures.xml, the pattern \"{0}\" is not valid.", patternNode.First().Value), MainForm.LogLevel.error);
                    continue;
                }

                var categoryNode = fixture.Descendants("category");
                if (patternNode.Count() == 0 || string.IsNullOrEmpty(patternNode.First().Value))
                {
                    MainForm.Log(string.Format("Fixtures: Error in fixtures.xml, no category set."), MainForm.LogLevel.error);
                    continue;
                }

                string pattern = patternNode.First().Value;
                string category = categoryNode.First().Value;

                // Get category
                var categoryResult = rendererCategories.Where(c => c.Name == category);
                if (categoryResult.Count() > 0)
                {
                    configurations.Add(pattern, categoryResult.First());
                }
            }
        }

        private static FixtureRendererConfiguration2? ParseRendererConfigurationFromXmlNode(XElement node)
        {
            // Create a NumberFormatInfo object for floats and set some of its properties.
            System.Globalization.NumberFormatInfo provider = new System.Globalization.NumberFormatInfo();
            provider.NumberDecimalSeparator = ".";
            provider.NumberGroupSeparator = "";
            provider.NumberGroupSizes = new int[] { 2 };

            try
            {
                FixtureRendererConfiguration2 conf = new FixtureRendererConfiguration2();

                if (node.Descendants("id").Count() > 0)
                {
                    conf.Name = node.Descendants("id").First().Value;
                }
                else if(node.Descendants("pattern").Count() > 0)
                {
                    conf.Name = node.Descendants("pattern").First().Value;
                }

                conf.Renderer = GetRendererType(node.Descendants("renderer").First().Value);
                conf.Color = new ImageMagick.MagickColor((node.Descendants("color").Count() > 0) ? node.Descendants("color").First().Value : "#FFF");
                conf.Transparency = Convert.ToInt32((node.Descendants("transparency").Count() > 0) ? node.Descendants("transparency").First().Value : "0");

                var lightElement = node.Descendants("light");
                if (lightElement.Count() > 0)
                {
                    var light = lightElement.First();
                    conf.HasLight = Convert.ToBoolean(light.Value);
                    conf.LightMin = Convert.ToDouble(!string.IsNullOrEmpty(light.Attribute("min").Value) ? light.Attribute("min").Value : "0.6", provider);
                    conf.LightMax = Convert.ToDouble(!string.IsNullOrEmpty(light.Attribute("max").Value) ? light.Attribute("max").Value : "1.0", provider);
                    conf.LightVector = GetLightVector(string.IsNullOrEmpty(light.Attribute("direction").Value) ? light.Attribute("direction").Value : "1,1,-1");
                }

                var shadowElement = node.Descendants("shadow");
                if (shadowElement.Count() > 0)
                {
                    var shadow = shadowElement.First();
                    conf.HasShadow = Convert.ToBoolean(shadow.Value);
                    conf.ShadowColor = new ImageMagick.MagickColor((!string.IsNullOrEmpty(shadow.Attribute("color").Value)) ? shadow.Attribute("color").Value : "#000");
                    conf.ShadowOffsetX = Convert.ToInt32(!string.IsNullOrEmpty(shadow.Attribute("offset_x").Value) ? shadow.Attribute("offset_x").Value : "0");
                    conf.ShadowOffsetY = Convert.ToInt32(!string.IsNullOrEmpty(shadow.Attribute("offset_y").Value) ? shadow.Attribute("offset_y").Value : "0");
                    conf.ShadowSize = Convert.ToDouble(!string.IsNullOrEmpty(shadow.Attribute("size").Value) ? shadow.Attribute("size").Value : "1", provider);
                    conf.ShadowTransparency = Convert.ToInt32(!string.IsNullOrEmpty(shadow.Attribute("size").Value) ? shadow.Attribute("size").Value : "75");
                }

                if (!string.IsNullOrEmpty(node.Attribute("is_default").Value) && Convert.ToBoolean(node.Attribute("is_default").Value) == true)
                {
                    defaultConfiguration = conf;
                }

                return conf;
            }
            catch(Exception ex)
            {
                MainForm.Log("Error in fixtures XML", MainForm.LogLevel.error);
                MainForm.Log(ex.Message, MainForm.LogLevel.error);
                return null;
            }
        }

        /// <summary>
        /// Gets the enum value out a text value
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        private static FixtureRenderererType GetRendererType(string name)
        {
            // Parse the textual representation of the renderer to its enum value
            FixtureRenderererType renderer = FixtureRenderererType.Shaded;
            try
            {
                renderer = (FixtureRenderererType)Enum.Parse(typeof(FixtureRenderererType), name);
            }
            catch
            {
                MainForm.Log(string.Format("The renderer \"{0}\" is invalid!", name), MainForm.LogLevel.error);
            }

            return renderer;
        }

        /// <summary>
        /// Gets the Vector out of a string
        /// </summary>
        /// <param name="lightVector"></param>
        /// <returns></returns>
        private static SharpDX.Vector3 GetLightVector(string lightVector)
        {
            if (lightVector == "") return new SharpDX.Vector3(1f, 1f, -1f);

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


    }

    struct FixtureRendererConfiguration2
    {
        public string Name;
        public FixtureRenderererType Renderer;
        public ImageMagick.MagickColor Color;
        public int Transparency;

        // Light
        public bool HasLight;
        public double LightMin;
        public double LightMax;
        public SharpDX.Vector3 LightVector;

        // Shadow
        public bool HasShadow;
        public ImageMagick.MagickColor ShadowColor;
        public int ShadowOffsetX;
        public int ShadowOffsetY;
        public double ShadowSize;
        public int ShadowTransparency;

        public FixtureRendererConfiguration2(string name)
        {
            Name = name;
            Renderer = FixtureRenderererType.Shaded;
            Color = new ImageMagick.MagickColor("#FFF");
            Transparency = 0;

            HasLight = true;
            LightMin = 0.6;
            LightMax = 1.0;
            LightVector = new SharpDX.Vector3(1f, 1f, -1f);

            HasShadow = true;
            ShadowColor = new ImageMagick.MagickColor("#000");
            ShadowOffsetX = 0;
            ShadowOffsetY = 0;
            ShadowSize = 1.0;
            ShadowTransparency = 75;
        }
    }
}
