using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ImageMagick;
using System.IO;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace MapCreator
{
    class MapBounds
    {
        /// <summary>
        /// The current zoneConfiguration
        /// </summary>
        ZoneConfiguration zoneConfiguration;

        /// <summary>
        /// If true, each shape will be drawn with its coordinates in data/debug/zoneXXX
        /// </summary>
        bool debug = false;

        /// <summary>
        /// The final shapes
        /// </summary>
        List<List<int[]>> m_bounds = new List<List<int[]>>();

        /// <summary>
        /// Background Color
        /// </summary>
        Color m_boundsColor = Color.Black;

        /// <summary>
        /// Background color
        /// </summary>
        public Color BoundsColor
        {
            set { m_boundsColor = value; }
        }

        /// <summary>
        /// Opacity
        /// </summary>
        int m_boundsOpacity = 30;

        /// <summary>
        /// Opacity
        /// </summary>
        public int BoundsOpacity
        {
            set { m_boundsOpacity = value; }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="zoneConfiguration"></param>
        public MapBounds(ZoneConfiguration zoneConfiguration)
        {
            this.zoneConfiguration = zoneConfiguration;
            ParseBounds();
        }

        /// <summary>
        /// Gets the raw lines from bound.csv
        /// </summary>
        /// <returns></returns>
        private List<string> GetCoordinateLines()
        {
            List<string> lines = new List<string>();
            using (StreamReader csv = MpkWrapper.GetFileFromMpk(zoneConfiguration.DatMpk, "bound.csv"))
            {
                string row;
                while ((row = csv.ReadLine()) != null)
                {
                    lines.Add(row);
                }
            }

            return lines;
        }

        /// <summary>
        /// Parses the shapes
        /// </summary>
        private void ParseBounds()
        {
            // Parsed bound lines
            List<List<int[]>> pointLines = new List<List<int[]>>();

            foreach (string l in GetCoordinateLines())
            {
                List<int[]> points = new List<int[]>();

                string[] coordsRaw = l.Split(',');
                for (int i = 2; i < coordsRaw.Length; i++)
                {
                    if (string.IsNullOrEmpty(coordsRaw[i]) || string.IsNullOrEmpty(coordsRaw[i + 1])) continue;

                    points.Add(new int[] { Convert.ToInt32(coordsRaw[i]), Convert.ToInt32(coordsRaw[i + 1]) });
                    i++;
                }

                // Draw only bounds with more than 2 coordinates
                if (points.Count > 2)
                {
                    pointLines.Add(points);
                }
            }

            List<int> deleteIndexes = new List<int>();
            // Check if some bounds stick together
            for (int i = 0; i < pointLines.Count; i++)
            {
                bool found = false;
                if (deleteIndexes.Contains(i)) continue;

                int lastX = pointLines[i].Last()[0];
                int lastY = pointLines[i].Last()[1];

                for (int j = 0; j < pointLines.Count; j++)
                {
                    if (i == j) continue;
                    if (deleteIndexes.Contains(j)) continue;

                    int firstX = pointLines[j].First()[0];
                    int firstY = pointLines[j].First()[1];

                    if (lastX == firstX && lastY == firstY)
                    {
                        pointLines[i].AddRange(pointLines[j]);

                        deleteIndexes.Add(j);
                        // Reset outer counter
                        found = true;
                    }
                }

                if (found)
                {
                    i = 0;
                }
            }

            deleteIndexes.Sort();
            deleteIndexes.Reverse();
            foreach (int index in deleteIndexes)
            {
                pointLines.RemoveAt(index);
            }

            // Correct some coords
            if (zoneConfiguration.ZoneId == "064")
            {
                // The first shape seems to be in counter clockwise order
                pointLines[0].Reverse();
            }
            else if (zoneConfiguration.ZoneId == "175")
            {
                // river gate is drawn 2 times
                pointLines.RemoveAt(5);
                // There is a 3 edges shape
                pointLines.RemoveAt(4);
                // the second gate shape is not closed
                pointLines[4][0] = pointLines[4].Last();
            }

            // We want to know to which side of the maps the first points need to be drawn to
            // This can also be done with pure math, but I don't want to... (http://www.blackpawn.com/texts/pointinpoly/)
            //
            // These graphic paths defines the draw direction
            // -----------
            // | \  n  / |
            // |  \   /  |
            // |   \ /   |
            // | w  /  e |
            // |   / \   |
            // |  /   \  |
            // | /  s  \ |
            // -----------
            GraphicsPath northTriangle = new GraphicsPath();
            northTriangle.AddLines(new PointF[] { new PointF(0, 0), new PointF(65536, 0), new PointF(32768, 32768) }); // the north triangle; Note: we use 65536 else 65535 will not be visible
            GraphicsPath eastTriangle = new GraphicsPath();
            eastTriangle.AddLines(new PointF[] { new PointF(65536, 0), new PointF(65536, 65536), new PointF(32768, 32768) }); // the east triangle
            GraphicsPath southTriangle = new GraphicsPath();
            southTriangle.AddLines(new PointF[] { new PointF(65536, 65536), new PointF(0, 65536), new PointF(32768, 32768) }); // the south triangle
            GraphicsPath westTriangle = new GraphicsPath();
            westTriangle.AddLines(new PointF[] { new PointF(0, 65536), new PointF(0, 0), new PointF(32768, 32768) }); // the west triangle

            // Fill
            foreach (List<int[]> points in pointLines)
            {
                Point first = new Point((points.First()[0] == 65536) ? 65535 : points.First()[0], (points.First()[1] == 65536) ? 65535 : points.First()[1]); // there are some shapes wich use 65536 as max X or Y
                Point last = new Point((points.Last()[0] == 65536) ? 65535 : points.Last()[0], (points.Last()[1] == 65536) ? 65535 : points.Last()[1]); // there are some shapes wich use 65536 as max X or Y

                // If its a complete polygon where the last equals the first point, don't do anything
                if (first.X == last.X && first.Y == last.Y) continue;

                // Avoid flood fills
                // This happens if the start AND end of the shape are on the same side (see dartmoor, llyn bafog).
                bool avoidFloodFill = false;
                if (first.Y == 0 && last.Y == 0 && first.X > last.X) avoidFloodFill = true; // north
                else if (first.X == 65535 && last.X == 65535 && first.Y > last.Y) avoidFloodFill = true; // east
                else if (first.Y == 65535 && last.Y == 65535 && first.X < last.X) avoidFloodFill = true; // south
                else if (first.X == 0 && last.X == 0 && first.Y < last.Y) avoidFloodFill = true; // west

                // Fill the shape in a clockwise order, maximum 6 required steps

                // Go the next map border at n, e, s, w
                if(northTriangle.IsVisible(first)) points.Insert(0, new int[] { first.X, 0 }); // to north border
                else if (eastTriangle.IsVisible(first)) points.Insert(0, new int[] { 65535, first.Y }); // to east border
                else if (southTriangle.IsVisible(first)) points.Insert(0, new int[] { first.X, 65535 }); // to south border
                else if (westTriangle.IsVisible(first)) points.Insert(0, new int[] { 0, first.Y }); // to west border
                // Do the same for last point
                if (northTriangle.IsVisible(last)) points.Add(new int[] { last.X, 0 }); // to north border
                else if (eastTriangle.IsVisible(last)) points.Add(new int[] { 65535, last.Y }); // to east border
                else if (southTriangle.IsVisible(last)) points.Add(new int[] { last.X, 65535 }); // to south border
                else if (westTriangle.IsVisible(last)) points.Add(new int[] { 0, last.Y }); // to west border

                // No we have first and last at least with one of 0/65535 on x and y

                // Go around
                while (true)
                {
                    first = new Point((points.First()[0] == 65536) ? 65535 : points.First()[0], (points.First()[1] == 65536) ? 65535 : points.First()[1]); // there are some shapes wich use 65536 as max X or Y
                    last = new Point((points.Last()[0] == 65536) ? 65535 : points.Last()[0], (points.Last()[1] == 65536) ? 65535 : points.Last()[1]); // there are some shapes wich use 65536 as max X or Y

                    // Chek if we are finished
                    if ((first.X == last.X || first.Y == last.Y) && !avoidFloodFill) break;

                    if (first.Y == 0 && first.X != 65535 && (last.Y != 0 || first.X > last.X)) points.Insert(0, new int[] { 65535, 0 }); // to north-east corner
                    else if (first.X == 65535 && first.Y != 65535 && (last.X != 65535 || first.Y > last.Y)) points.Insert(0, new int[] { 65535, 65535 }); // to south-east corner
                    else if (first.Y == 65535 && first.X != 0 && (last.Y != 65535 || first.X < last.X)) points.Insert(0, new int[] { 0, 65535 }); // to south-west corner
                    else if (first.X == 0 && first.Y != 0 && (last.X != 0 || first.Y < last.Y)) points.Insert(0, new int[] { 0, 0 }); // to north-west corner
                    else break;
                }
            }

            // Destroy the paths
            northTriangle.Dispose();
            eastTriangle.Dispose();
            southTriangle.Dispose();
            westTriangle.Dispose();

            m_bounds = pointLines;
        }

        /// <summary>
        /// Draw the bounds onto map
        /// </summary>
        /// <param name="map"></param>
        public void Draw(MagickImage map)
        {
            MainForm.ProgressReset();
            MainForm.Log("Drawing bounds...", MainForm.LogLevel.notice);
            MainForm.ProgressStart("Drawing bounds...");

            MagickImage boundMap = new MagickImage(Color.Transparent, zoneConfiguration.TargetMapSize, zoneConfiguration.TargetMapSize);

            DirectoryInfo debugDirectory = new DirectoryInfo(string.Format("{0}\\data\\debug\\zone{1}", System.Windows.Forms.Application.StartupPath, zoneConfiguration.ZoneId));
            if (debug)
            {
                if (!debugDirectory.Exists) debugDirectory.Create();

                //Remove old files
                foreach (FileInfo file in debugDirectory.GetFiles(string.Format("bound{0}*", zoneConfiguration.ZoneId)))
                {
                    file.Delete();
                }
            }

            int boundIndex = 0;
            foreach (List<int[]> coords in m_bounds)
            {
                List<Coordinate> coordinates = new List<Coordinate>();
                foreach (int[] ints in coords) coordinates.Add(new Coordinate(zoneConfiguration.LocToPixel(ints[0]), zoneConfiguration.LocToPixel(ints[1])));

                using(DrawablePolygon poly = new DrawablePolygon(coordinates)) {

                    boundMap.FillColor = Color.FromArgb((255 * m_boundsOpacity / 100), m_boundsColor);
                    boundMap.Draw(poly);

                    if (debug)
                    {
                        using (MagickImage debugMap = new MagickImage(Color.White, zoneConfiguration.TargetMapSize, zoneConfiguration.TargetMapSize))
                        {
                            debugMap.FillColor = Color.FromArgb(60, 255, 0, 0);
                            debugMap.Draw(poly);

                            // Print the point index
                            for (int i = 0; i < coordinates.Count; i++)
                            {
                                double x, y;

                                if (coordinates[i].X > zoneConfiguration.TargetMapSize/2) x = coordinates[i].X - 15;
                                else x = coordinates[i].X + 1;

                                if (coordinates[i].Y < zoneConfiguration.TargetMapSize/2) y = coordinates[i].Y + 15;
                                else y = coordinates[i].Y - 1;

                                debugMap.FontPointsize = 14.0;
                                debugMap.FillColor = Color.Black;
                                using (DrawableText text = new DrawableText(x, y, string.Format("{0} ({1}/{2})", i, coords[i][0], coords[i][1])))
                                {
                                    debugMap.Draw(text);
                                }

                                using (WritablePixelCollection pixels = debugMap.GetWritablePixels())
                                {
                                    int x2, y2;
                                    if (coordinates[i].X == zoneConfiguration.TargetMapSize) x2 = zoneConfiguration.TargetMapSize - 1;
                                    else x2 = (int)coordinates[i].X;
                                    if (coordinates[i].Y == zoneConfiguration.TargetMapSize) y2 = zoneConfiguration.TargetMapSize - 1;
                                    else y2 = (int)coordinates[i].Y;

                                    pixels.Set(x2, y2, new float[] { 0, 0, 65536, 0 });
                                }
                            }

                            debugMap.Quality = 100;

                            // Debug file
                            FileInfo debugFile = new FileInfo(string.Format("{0}\\bound{1}_{2}.jpg", debugDirectory.FullName, zoneConfiguration.ZoneId, boundIndex));
                            debugMap.Write(debugFile.FullName);
                        }
                    }
                }

                MainForm.ProgressUpdate(100 * boundIndex / m_bounds.Count - 10);
                boundIndex++;
            }
            
            map.Composite(boundMap, 0, 0, CompositeOperator.SrcOver);

            MainForm.ProgressUpdate(100);
            MainForm.Log("Finished bounds!", MainForm.LogLevel.success);
        }

    }
}
