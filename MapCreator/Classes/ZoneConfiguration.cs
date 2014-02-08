using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace MapCreator
{
    public class ZoneConfiguration : IDisposable
    {
        const double zoneMaxCoordinate = 65536.0;

        private string m_zoneId;
        public string ZoneId
        {
            get { return m_zoneId; }
        }

        private GameExpansion m_expansion;
        public GameExpansion Expansion
        {
            get { return m_expansion; }
        }

        private string m_zoneDirectory;
        public string ZoneDirectory
        {
            get { return m_zoneDirectory; }
        }

        private StreamReader m_sectorDatStreamReader;
        public StreamReader SectorDatStreamReader
        {
            get { return m_sectorDatStreamReader; }
        }

        private int m_targetMapSize = 1024;
        public int TargetMapSize
        {
            get { return m_targetMapSize; }
        }

        private double m_locScale = 1;
        public double LocScale
        {
            get { return m_locScale; }
        }

        private double m_mapScale = 1;
        public double MapScale
        {
            get { return m_mapScale; }
        }

        private double m_locsPerPixel = 1;
        public double LocsPerLixel
        {
            get { return m_locsPerPixel; }
        }

        private MapHeightmap m_heightmap;
        public MapHeightmap Heightmap
        {
            get { return m_heightmap; }
        }

        private double[,] m_riverHeights;
        public double[,] RiverHeights
        {
            get { return m_riverHeights; }
            set { m_riverHeights = value; }
        } 

        #region MPK files

        private string m_datMpk;
        public string DatMpk
        {
            get { return m_datMpk; }
        }

        private string m_cvsMpk;
        public string CvsMpk
        {
            get { return m_cvsMpk; }
        }

        private string m_lodMpk;
        public string LodMpk
        {
            get { return m_lodMpk; }
        }

        private string m_terMpk;
        public string TerMpk
        {
            get { return m_terMpk; }
        }

        private string m_texMpk;
        public string TexMpk
        {
            get { return m_texMpk; }
        }

        #endregion

        public ZoneConfiguration(string zoneId, int mapSize)
        {
            m_zoneId = zoneId;

            // Get expansion from zones.xml
            m_expansion = DataWrapper.GetExpansionByZone(zoneId);

            // Zone Directory
            m_zoneDirectory = this.GetZoneDirectory();

            // Some mpk files
            m_datMpk = string.Format("{0}\\dat{1}.mpk", m_zoneDirectory, m_zoneId);
            m_cvsMpk = string.Format("{0}\\csv{1}.mpk", m_zoneDirectory, m_zoneId);
            m_lodMpk = string.Format("{0}\\lod{1}.mpk", m_zoneDirectory, m_zoneId);
            m_terMpk = string.Format("{0}\\ter{1}.mpk", m_zoneDirectory, m_zoneId);
            m_texMpk = string.Format("{0}\\tex{1}.mpk", m_zoneDirectory, m_zoneId);

            // Check if the zone gets its data from an other zone

            // Check if file exists, else map to datXXX.mpk
            if (!File.Exists(m_cvsMpk)) m_cvsMpk = m_datMpk;
            if (!File.Exists(m_lodMpk)) m_lodMpk = m_datMpk;
            if (!File.Exists(m_terMpk)) m_terMpk = m_datMpk;
            if (!File.Exists(m_texMpk)) m_texMpk = m_datMpk;

            // Useful for everything, so open it
            m_sectorDatStreamReader = MpkWrapper.GetFileFromMpk(m_datMpk, "sector.dat");

            // for math
            m_targetMapSize = mapSize;
            m_mapScale = m_targetMapSize / 256.0;
            m_locsPerPixel = zoneMaxCoordinate / mapSize;
            m_locScale = m_targetMapSize / zoneMaxCoordinate;

            // Heightmap
            this.m_heightmap = new MapHeightmap(this);

            // Prepare river heights array
            m_riverHeights = new double[m_targetMapSize, m_targetMapSize];
        }

        public string GetZoneDirectory(string zoneId = null)
        {
            if (string.IsNullOrEmpty(zoneId)) zoneId = m_zoneId;
            GameExpansion expansion = DataWrapper.GetExpansionByZone(zoneId);
            string zoneDataDirectory = "";

            switch (expansion)
            {
                case GameExpansion.Foundations:
                    zoneDataDirectory = string.Format("{0}\\phousing\\zones\\zone{1}", Properties.Settings.Default.game_path, zoneId);
                    break;
                case GameExpansion.NewFrontiers:
                    zoneDataDirectory = string.Format("{0}\\frontiers\\zones\\zone{1}", Properties.Settings.Default.game_path, zoneId);
                    break;
                default:
                    zoneDataDirectory = string.Format("{0}\\zones\\zone{1}", Properties.Settings.Default.game_path, zoneId);
                    break;
            }

            // Special path for tutorial zones
            if (zoneId == "027" || zoneId == "027" || zoneId == "027")
            {
                zoneDataDirectory = string.Format("{0}\\tutorial\\zones\\zone{1}", Properties.Settings.Default.game_path, zoneId);
            }

            return zoneDataDirectory;
        }

        public double ZoneCoordinateToMapCoordinate(double zoneCoordinate)
        {
            return (m_targetMapSize * zoneCoordinate) / zoneMaxCoordinate;
        }

        public double MapCoordinateToZoneCoordinate(double mapCoordinate)
        {
            return (zoneMaxCoordinate * mapCoordinate) / m_targetMapSize;
        }

        public ImageMagick.MagickImage GetWaterMap()
        {
            ImageMagick.MagickImage watermap = new ImageMagick.MagickImage(MpkWrapper.GetFileBytesFromMpk(this.m_datMpk, "water.pcx"));
            watermap.Resize(this.m_targetMapSize, this.m_targetMapSize);

            return watermap;
        }

        public ImageMagick.MagickImage GetTerrainMap()
        {
            ImageMagick.MagickImage terrainmap = new ImageMagick.MagickImage(MpkWrapper.GetFileBytesFromMpk(this.m_datMpk, "terrain.pcx"));
            return terrainmap;
        }

        public ImageMagick.MagickImage GetOffsetMap()
        {
            ImageMagick.MagickImage offsetmap = new ImageMagick.MagickImage(MpkWrapper.GetFileBytesFromMpk(this.m_datMpk, "offset.pcx"));
            return offsetmap;
        }

        public void Dispose()
        {
            if (m_sectorDatStreamReader != null) m_sectorDatStreamReader.Close();
            m_heightmap.Dispose();
        }
    }
}
