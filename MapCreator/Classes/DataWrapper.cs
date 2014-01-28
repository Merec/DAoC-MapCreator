using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Windows.Forms;
using System.IO;
using System.Text.RegularExpressions;
using MapCreator.data;

namespace MapCreator
{
    class DataWrapper
    {
        private static XDocument zonesXml = null;

        private static string presetsDataFile;

        private static MapCreatorData mapCreatorData = new MapCreatorData();
        public static MapCreatorData MapCreatorData
        {
            get { return DataWrapper.mapCreatorData; }
            set { DataWrapper.mapCreatorData = value; }
        }

        static DataWrapper()
        {
            // Read Zones
            zonesXml = XDocument.Load(string.Format("{0}\\data\\zones.xml", Application.StartupPath));

            // Create/Read data xml file
            presetsDataFile = string.Format("{0}\\presets.xml", Application.StartupPath);
            if (!File.Exists(presetsDataFile))
            {
                mapCreatorData.ZoneSelectionPresets.WriteXml(presetsDataFile);
            }
            else
            {
                mapCreatorData.ZoneSelectionPresets.ReadXml(presetsDataFile);
            }
        }

        #region MapCreatorData Presets

        public static void LoadPresets()
        {
            mapCreatorData.ZoneSelectionPresets.Clear();
            mapCreatorData.ZoneSelectionPresets.ReadXml(presetsDataFile);
        }

        public static void SavePresets()
        {
            mapCreatorData.ZoneSelectionPresets.WriteXml(presetsDataFile);
        }

        public static void AddPresetRow(string name, List<string> zoneIds)
        {
            MapCreatorData.ZoneSelectionPresetsRow row = mapCreatorData.ZoneSelectionPresets.NewZoneSelectionPresetsRow();
            row.Name = name;
            row.Zones = String.Join(",", zoneIds);
            mapCreatorData.ZoneSelectionPresets.AddZoneSelectionPresetsRow(row);
            SavePresets();
        }

        public static void RemovePreset(MapCreatorData.ZoneSelectionPresetsRow row)
        {
            mapCreatorData.ZoneSelectionPresets.RemoveZoneSelectionPresetsRow(row);
            SavePresets();
        }

        public static MapCreatorData.ZoneSelectionPresetsRow[] GetPresetRows()
        {
            return mapCreatorData.ZoneSelectionPresets.ToArray();
        }

        #endregion

        #region Zones.xml

        /// <summary>
        /// Gets all realms
        /// </summary>
        /// <returns></returns>
        public static List<string> GetRealms()
        {
            return zonesXml.Descendants("realm").Attributes("name").Select(x => x.Value).ToList();
        }

        /// <summary>
        /// Gets all Expansion of a realm
        /// </summary>
        /// <param name="realm"></param>
        /// <returns></returns>
        public static List<string> GetExpansionsByRealm(string realm)
        {
            List<string> expansions = zonesXml.Descendants("expansion")
                .Where(r => r.Parent.Attribute("name").Value == realm)
                .Select(e => e.Attribute("name").Value)
                .ToList();

            return expansions;
        }

        /// <summary>
        /// Gets all ZoneTypes of an Realms and Expansion
        /// </summary>
        /// <param name="realm"></param>
        /// <param name="expansion"></param>
        /// <returns></returns>
        public static List<string> GetZoneTypesByRealmAndExpansion(string realm, string expansion)
        {
            if (string.IsNullOrEmpty(realm)) return new List<string>();
            if (string.IsNullOrEmpty(expansion)) return new List<string>();

            var zoneTypes = zonesXml.Descendants("zone")
                .Where(r => r.Parent.Attribute("name").Value == expansion && r.Parent.Parent.Attribute("name").Value == realm)
                .OrderBy(e => e.Attribute("type").Value)
                .Select(e => e.Attribute("type").Value)
                .Distinct()
                .ToList();

            return zoneTypes;
        }

        /// <summary>
        /// Gets all zones by realm, expansion and zony type
        /// </summary>
        /// <param name="realm"></param>
        /// <param name="expansion"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static Dictionary<string, string> GetZonesByRealmAndExpansionAndType(string realm, string expansion, string type)
        {
            if (string.IsNullOrEmpty(realm)) return new Dictionary<string, string>();
            if (string.IsNullOrEmpty(expansion)) return new Dictionary<string, string>();
            if (string.IsNullOrEmpty(type)) return new Dictionary<string, string>();

            var zones = zonesXml.Descendants("zone")
                .Where(r => r.Parent.Attribute("name").Value == expansion && r.Parent.Parent.Attribute("name").Value == realm && r.Attribute("type").Value == type)
                .OrderBy(e => e.Attribute("id").Value)
                .ToDictionary(e => e.Attribute("id").Value, e => e.Value);

            return zones;
        }

        /// <summary>
        /// Gets an expansion of a zone
        /// </summary>
        /// <param name="zoneId"></param>
        /// <returns></returns>
        public static GameExpansion GetExpansionByZone(string zoneId)
        {
            var results = zonesXml.Descendants("zone").Where(z => z.Attribute("id").Value == zoneId).Select(e => e.Parent.Attribute("name").Value);

            if (results.Count() > 0)
            {
                return (GameExpansion)Enum.Parse(typeof(GameExpansion), results.First().Replace(" ", ""), true);
            }

            return GameExpansion.Unknown;
        }

        public static ZoneSelection GetZoneSelectionByZoneId(string zoneId)
        {
            var results = zonesXml.Descendants("zone").Where(z => z.Attribute("id").Value == zoneId).Select(e => e.Value);

            if (results.Count() > 0)
            {
                return new ZoneSelection(zoneId, results.First());
            }
            else
            {
                throw new Exception("Unable to resolve zone.");
            }
        }

        #endregion

        #region Values from Dat-Files

        /// <summary>
        /// Gets a value from a sector.dat in an MPK file
        /// </summary>
        /// <param name="mpkFile"></param>
        /// <param name="iniRegion"></param>
        /// <param name="iniProperty"></param>
        /// <returns></returns>
        public static string GetSectorDatValue(string mpkFile, string iniRegion, string iniProperty)
        {
            StreamReader sectorDatFile = MpkWrapper.GetFileFromMpk(mpkFile, "sector.dat");
            return GetDatFileProperty(sectorDatFile, iniRegion, iniProperty);
        }

        /// <summary>
        /// Get the value from a property of a .dat file
        /// </summary>
        /// <param name="datFile"></param>
        /// <param name="iniRegion"></param>
        /// <param name="iniProperty"></param>
        /// <returns></returns>
        public static string GetDatFileProperty(StreamReader datFile, string iniRegion, string iniProperty)
        {
            // Seek the stream
            datFile.BaseStream.Position = 0;

            Regex regionRegex = new Regex(string.Format(@"\[{0}\]", iniRegion), RegexOptions.IgnoreCase);
            Regex propertyRegex = new Regex(string.Format(@"{0}=(.*)", iniProperty), RegexOptions.IgnoreCase);

            string row;
            Match match;
            bool recording = false;

            while ((row = datFile.ReadLine()) != null)
            {
                if (row.Trim().StartsWith(";")) continue;

                if (regionRegex.IsMatch(row))
                {
                    recording = true;
                    continue;
                }

                if (recording)
                {
                    if (propertyRegex.IsMatch(row))
                    {
                        match = propertyRegex.Match(row);
                        return match.Groups[1].Value.Trim();
                    }
                }

                // Record until the next [
                if (row.StartsWith("[")) recording = false;
            }

            return "";
        }

        public static List<string> GetFileContent(string mpkFile, string filename)
        {
            List<string> lines = new List<string>();

            using (StreamReader csv = MpkWrapper.GetFileFromMpk(mpkFile, filename))
            {
                string row;
                while ((row = csv.ReadLine()) != null)
                {
                    lines.Add(row);
                }
            }

            return lines;
        }

        #endregion

    }
}
