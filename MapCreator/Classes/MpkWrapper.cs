using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MPKLib;
using System.IO;
using System.Text.RegularExpressions;

namespace MapCreator
{
    static class MpkWrapper
    {

        public static bool CheckGamePath()
        {
            string checkFile = string.Format("{0}\\{1}", Properties.Settings.Default.game_path, "camelot.exe");
            if (!File.Exists(checkFile))
            {
                MainForm.Log("camelot.exe not found in gamepath!");
                return false;
            }

            return true;
        }

        /// <summary>
        /// Gets zones from zones.dat
        /// </summary>
        /// <returns></returns>
        public static Dictionary<int, string> GetZones()
        {
            if (!CheckGamePath())
            {
                return null;
            }

            Dictionary<int, string> zones = new Dictionary<int, string>();
            string zonesMpk = string.Format("{0}\\zones\\zones.mpk", Properties.Settings.Default.game_path);

            MPAK mpak = new MPAK();
            mpak.Load(zonesMpk);

            MPAKFileEntry zonesFile = mpak.GetFile("zones.dat");

            using (Stream stream = new MemoryStream(zonesFile.Data))
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    string row;
                    bool recording = false;

                    int current_zone = 0;
                    string current_zone_name = "";
                    Match match;

                    while ((row = reader.ReadLine()) != null)
                    {
                        if (string.IsNullOrEmpty(row.Trim())) continue;

                        // Sections
                        // ALBION

                        if (row.StartsWith("[zone"))
                        {
                            recording = true;
                            Regex regex = new Regex(@"\[zone(.*)\]", RegexOptions.IgnoreCase);
                            if ((match = regex.Match(row)) != null)
                            {
                                current_zone = Convert.ToInt32(match.Groups[1].Value);
                            }
                        }

                        if (recording && row.StartsWith("name="))
                        {
                            Regex regex = new Regex(@"name=(.*)", RegexOptions.IgnoreCase);
                            if ((match = regex.Match(row)) != null)
                            {
                                current_zone_name = match.Groups[1].Value;
                            }
                        }

                        if (recording && current_zone > 0 && current_zone_name != "")
                        {
                            zones.Add(current_zone, current_zone_name);
                            current_zone = 0;
                            current_zone_name = "";
                            recording = false;
                        }

                    }
                }
            }
            
            return zones;
        }

        public static StreamReader GetFileFromMpk(string mpk, string filename)
        {
            MPAK mpak = new MPAK();
            mpak.Load(mpk);

            if (mpak.Files.Where(f => f.Name.ToLower() == filename.ToLower()).Count() > 0)
            {
                return new StreamReader(new MemoryStream(mpak.GetFile(filename).Data));
            }

            return null;
        }

        public static Byte[] GetFileBytesFromMpk(string mpk, string filename)
        {
            MPAK mpak = new MPAK();
            mpak.Load(mpk);
            return mpak.GetFile(filename).Data;
        }


    }
}
