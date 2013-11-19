using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Windows.Forms;
using System.IO;
using System.Text.RegularExpressions;

namespace MapCreator
{
    static class DataWrapper
    {
        private static XDocument m_xml = null;

        /// <summary>
        /// Gets all realms
        /// </summary>
        /// <returns></returns>
        public static List<string> GetRealms()
        {
            if (m_xml == null)
            {
                m_xml = XDocument.Load(string.Format("{0}\\data\\zones.xml", Application.StartupPath));
            }

            return m_xml.Descendants("realm").Attributes("name").Select(x => x.Value).ToList();
        }

        /// <summary>
        /// Gets all Expansion of a realm
        /// </summary>
        /// <param name="realm"></param>
        /// <returns></returns>
        public static List<string> GetExpansionsByRealm(string realm)
        {
            if (m_xml == null)
            {
                XDocument xml = XDocument.Load(string.Format("{0}\\data\\zones.xml", Application.StartupPath));
            }

            List<string> expansions = m_xml.Descendants("expansion")
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
            if (m_xml == null)
            {
                XDocument xml = XDocument.Load(string.Format("{0}\\data\\zones.xml", Application.StartupPath));
            }

            if (string.IsNullOrEmpty(realm)) return new List<string>();
            if (string.IsNullOrEmpty(expansion)) return new List<string>();

            var zoneTypes = m_xml.Descendants("zone")
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
            if (m_xml == null)
            {
                XDocument xml = XDocument.Load(string.Format("{0}\\data\\zones.xml", Application.StartupPath));
            }

            if (string.IsNullOrEmpty(realm)) return new Dictionary<string, string>();
            if (string.IsNullOrEmpty(expansion)) return new Dictionary<string, string>();
            if (string.IsNullOrEmpty(type)) return new Dictionary<string, string>();

            var zones = m_xml.Descendants("zone")
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
            if (m_xml == null)
            {
                XDocument xml = XDocument.Load(string.Format("{0}\\data\\zones.xml", Application.StartupPath));
            }

            var results = m_xml.Descendants("zone").Where(z => z.Attribute("id").Value == zoneId).Select(e => e.Parent.Attribute("name").Value);

            if (results.Count() > 0)
            {
                return (GameExpansion)Enum.Parse(typeof(GameExpansion), results.First().Replace(" ", ""), true);
            }

            return GameExpansion.Unknown;
        }

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

    }
}
