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
        private ZoneConfiguration zoneConfiguration;

        /// <summary>
        /// If true, each shape will be drawn with its coordinates in data/debug/zoneXXX
        /// </summary>
        private bool debug = false;

        /// <summary>
        /// The final shapes
        /// </summary>
        private List<List<PointF>> m_bounds = new List<List<PointF>>();

        /// <summary>
        /// Substraction bounds
        /// </summary>
        private List<List<int[]>> m_boundsSubstraction = new List<List<int[]>>();

        /// <summary>
        /// Background Color
        /// </summary>
        private Color m_boundsColor = Color.Black;

        /// <summary>
        /// Opacity
        /// </summary>
        private int m_transparency = 30;

        /// <summary>
        /// Removes the area of the bounds from the final image
        /// </summary>
        private bool m_excludeFromMap = false;

        #region Settings

        /// <summary>
        /// Exlude bound from final image
        /// </summary>
        public bool ExcludeFromMap
        {
            get { return m_excludeFromMap; }
            set { m_excludeFromMap = value; }
        }

        /// <summary>
        /// Opacity
        /// </summary>
        public int Transparency
        {
            set { m_transparency = value; }
        }

        /// <summary>
        /// Background color
        /// </summary>
        public Color BoundsColor
        {
            set { m_boundsColor = value; }
        }

        #endregion

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

        private void ParseBounds()
        {
            List<string> lines = GetCoordinateLines();
            List<List<PointF>> polygons = new List<List<PointF>>();

            foreach (string line in lines)
            {
                List<PointF> polygon = new List<PointF>();

                string[] coordsRaw = line.Split(',');

                // The first is 0?; the second the number of points
                int unknown1 = Convert.ToInt32(coordsRaw[0]);
                int count = Convert.ToInt32(coordsRaw[1]);

                for (int i = 1; i <= count; i++)
                {
                    int x = Convert.ToInt32(coordsRaw[i * 2]);
                    if (x == 65536) x = 65535;

                    int y = Convert.ToInt32(coordsRaw[i * 2 + 1]);
                    if (y == 65536) y = 65535;

                    polygon.Add(new PointF(x, y));
                }

                polygons.Add(polygon);
            }

            // Correct some zones here
            
            // 028
            if (zoneConfiguration.ZoneId == "028")
            {
                // The last point is too close to the first
                polygons.First().RemoveAt(polygons.First().Count - 1);
                polygons.First().Add(new PointF(1284, 2100));
            }

            // Housing, some shapes have clockwise order but must be counter clockwise
            if (zoneConfiguration.ZoneId == "064") polygons[0].Reverse();
            if (zoneConfiguration.ZoneId == "117") polygons[2].Reverse();
            if (zoneConfiguration.ZoneId == "122") polygons[1].Reverse();
            if (zoneConfiguration.ZoneId == "218") polygons[1].Reverse();

            polygons = CombinePolygons(polygons);

            // 015 old hadrians wall
            if (zoneConfiguration.ZoneId == "015")
            {
                // The last point is too close to the first
                polygons.RemoveRange(1, 3);
            }
            // Oceanus Notots, the first must be counter clockwise, it must be negated
            if (zoneConfiguration.ZoneId == "076")
            {
                // The last point is too close to the first
                polygons[0].Reverse();
            }
            // DR, the outland zone have one shape in wrong direction which braks the parser
            if (zoneConfiguration.ZoneId == "330") polygons[1].Reverse();
            if (zoneConfiguration.ZoneId == "334") polygons[1].Reverse();
            if (zoneConfiguration.ZoneId == "335") polygons[1].Reverse();

            foreach (List<PointF> polygon in polygons)
            {
                if (polygon.Count < 4) continue;
                if (zoneConfiguration.Expansion == GameExpansion.NewFrontiers && polygon.Count <= 6) continue;

                FillPolygon(polygon);
                m_bounds.Add(polygon);
            }

        }

        private List<List<PointF>> CombinePolygons(List<List<PointF>> polygons)
        {
            for (int i = 0; i < polygons.Count; i++)
            {
                PointF last = polygons[i].Last();

                for (int n = 0; n < polygons.Count; n++)
                {
                    if (i == n) continue;
                    PointF first = polygons[n].First();

                    if (last.X == first.X && last.Y == first.Y)
                    {
                        // found a connection
                        polygons[i].AddRange(polygons[n]);
                        polygons.RemoveAt(n);
                        return CombinePolygons(polygons);
                    }
                }
            }

            return polygons;
        }

        private void FillPolygon(List<PointF> points)
        {
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

            PointF first = new PointF((float)points.First().X, (float)points.First().Y); // there are some shapes wich use 65536 as max X or Y
            PointF last = new PointF((float)points.Last().X, (float)points.Last().Y); // there are some shapes wich use 65536 as max X or Y

            // If its a complete polygon where the last equals the first point, don't do anything
            if (first.X == last.X && first.Y == last.Y) return;

            // Avoid flood fills
            // This happens if the start AND end of the shape are on the same side (see dartmoor, llyn bafog).
            bool avoidFloodFill = false;
            if (first.Y == 0 && last.Y == 0 && first.X > last.X) avoidFloodFill = true; // north
            else if (first.X == 65535 && last.X == 65535 && first.Y > last.Y) avoidFloodFill = true; // east
            else if (first.Y == 65535 && last.Y == 65535 && first.X < last.X) avoidFloodFill = true; // south
            else if (first.X == 0 && last.X == 0 && first.Y < last.Y) avoidFloodFill = true; // west

            // Fill the shape in a clockwise order, maximum 6 required steps
            // But first check if the distance last-point <--> first-point is lower
            double firstLastDistance = Tools.GetPointDistance(first, last);

            // Go the next map border at n, e, s, w
            PointF pointToPrepend = PointF.Empty;
            if (northTriangle.IsVisible(first)) pointToPrepend = new PointF(first.X, 0); // to north border
            else if (eastTriangle.IsVisible(first)) pointToPrepend = new PointF(65535, first.Y); // to east border
            else if (southTriangle.IsVisible(first)) pointToPrepend = new PointF(first.X, 65535); // to south border
            else if (westTriangle.IsVisible(first)) pointToPrepend = new PointF(0, first.Y); // to west border

            double newFirstDistance = Tools.GetPointDistance(pointToPrepend, first);
            if (pointToPrepend != null && firstLastDistance < newFirstDistance) return;

            // Do the same for last point
            PointF pointToAppend = PointF.Empty;
            if (northTriangle.IsVisible(last)) pointToAppend = new PointF(last.X, 0); // to north border
            else if (eastTriangle.IsVisible(last)) pointToAppend = new PointF(65535, last.Y); // to east border
            else if (southTriangle.IsVisible(last)) pointToAppend = new PointF(last.X, 65535); // to south border
            else if (westTriangle.IsVisible(last)) pointToAppend = new PointF(0, last.Y); // to west border

            double newLastDistance = Tools.GetPointDistance(last, pointToAppend);
            if (pointToAppend != null && firstLastDistance < newLastDistance) return;

            // Okay, we need to fill
            if (pointToPrepend != null) points.Insert(0, pointToPrepend);
            if (pointToAppend != null) points.Add(pointToAppend);

            // No we have first and last at least with one of 0/65535 on x and y

            // Go around
            while (true)
            {
                first = new PointF((float)points.First().X, (float)points.First().Y); // there are some shapes wich use 65536 as max X or Y
                last = new PointF((float)points.Last().X, (float)points.Last().Y); // there are some shapes wich use 65536 as max X or Y

                // Chek if we are finished
                if ((first.X == last.X && first.Y == last.Y) && !avoidFloodFill) break;

                if (first.Y == 0 && first.X != 65535 && (last.Y != 0 || first.X > last.X)) points.Insert(0, new PointF(65535, 0)); // to north-east corner
                else if (first.X == 65535 && first.Y != 65535 && (last.X != 65535 || first.Y > last.Y)) points.Insert(0, new PointF(65535, 65535)); // to south-east corner
                else if (first.Y == 65535 && first.X != 0 && (last.Y != 65535 || first.X < last.X)) points.Insert(0, new PointF(0, 65535)); // to south-west corner
                else if (first.X == 0 && first.Y != 0 && (last.X != 0 || first.Y < last.Y)) points.Insert(0, new PointF(0, 0)); // to north-west corner
                else break;
            }
        }

        /// <summary>
        /// Draw the bounds onto map
        /// </summary>
        /// <param name="map"></param>
        public void Draw(MagickImage map)
        {
            if (m_bounds.Count == 0) return;
            MainForm.ProgressStart("Drawing zone bounds ...");

            // Sort the polygons
            List<List<Coordinate>> polygons = new List<List<Coordinate>>();
            List<List<Coordinate>> negatedPolygons = new List<List<Coordinate>>();

            foreach (List<PointF> polygon in m_bounds)
            {
                bool isClockwise = Tools.PolygonHasClockwiseOrder(polygon);
                var polygonConverted = polygon.Select(c => new Coordinate(zoneConfiguration.ZoneCoordinateToMapCoordinate(c.X), zoneConfiguration.ZoneCoordinateToMapCoordinate(c.Y))).ToList();

                // polygons in clockwise order needs to be negated
                if (isClockwise) negatedPolygons.Add(polygonConverted);
                else polygons.Add(polygonConverted);
            }

            MagickColor backgroundColor = MagickColor.Transparent;
            if (polygons.Count == 0) {
                // There are no normal polygons, we need to fill the hole zone and substract negatedPolygons
                backgroundColor = m_boundsColor;
            }

            using (MagickImage boundMap = new MagickImage(backgroundColor, zoneConfiguration.TargetMapSize, zoneConfiguration.TargetMapSize))
            {
                int progressCounter = 0;

                boundMap.Alpha(AlphaOption.Set);
                boundMap.FillColor = m_boundsColor;
                foreach (List<Coordinate> coords in polygons)
                {
                    using (DrawablePolygon poly = new DrawablePolygon(coords))
                    {
                        boundMap.Draw(poly);
                    }

                    progressCounter++;
                    int percent = 100 * progressCounter / m_bounds.Count();
                    MainForm.ProgressUpdate(percent);
                }

                if (negatedPolygons.Count > 0)
                {
                    using (MagickImage negatedBoundMap = new MagickImage(Color.Transparent, zoneConfiguration.TargetMapSize, zoneConfiguration.TargetMapSize))
                    {
                        negatedBoundMap.FillColor = m_boundsColor;

                        foreach (List<Coordinate> coords in negatedPolygons)
                        {
                            using (DrawablePolygon poly = new DrawablePolygon(coords))
                            {
                                negatedBoundMap.Draw(poly);
                            }

                            progressCounter++;
                            int percent = 100 * progressCounter / m_bounds.Count();
                            MainForm.ProgressUpdate(percent);
                        }
                        boundMap.Composite(negatedBoundMap, 0, 0, CompositeOperator.DstOut);
                    }
                }

                MainForm.ProgressStartMarquee("Merging ...");
                if (ExcludeFromMap)
                {
                    map.Composite(boundMap, 0, 0, CompositeOperator.DstOut);
                }
                else
                {
                    if (m_transparency != 0)
                    {
                        boundMap.Alpha(AlphaOption.Set);
                        double divideValue = 100.0 / (100.0 - m_transparency);
                        boundMap.Evaluate(Channels.Alpha, EvaluateOperator.Divide, divideValue);
                    }

                    map.Composite(boundMap, 0, 0, CompositeOperator.SrcOver);
                }
            }

            if (debug)
            {
                DebugMaps();
            }

            MainForm.ProgressReset();
        }

        private void DebugMaps()
        {
            MainForm.Log("Drawing debug water images ...", MainForm.LogLevel.warning);
            MainForm.ProgressStartMarquee("Debug water images ...");

            DirectoryInfo debugDir = new DirectoryInfo(string.Format("{0}\\debug\\bound\\{1}", System.Windows.Forms.Application.StartupPath, zoneConfiguration.ZoneId));
            if (!debugDir.Exists) debugDir.Create();
            debugDir.GetFiles().ToList().ForEach(f => f.Delete());

            int boundIndex = 0;
            foreach (List<PointF> allCoords in m_bounds)
            {
                using (MagickImage bound = new MagickImage(MagickColor.Transparent, zoneConfiguration.TargetMapSize, zoneConfiguration.TargetMapSize))
                {
                    List<Coordinate> coords = allCoords.Select(c => new Coordinate(zoneConfiguration.ZoneCoordinateToMapCoordinate(c.X), zoneConfiguration.ZoneCoordinateToMapCoordinate(c.Y))).ToList();
                    using (DrawablePolygon poly = new DrawablePolygon(coords))
                    {
                        bound.FillColor = new MagickColor(0, 0, 0, 256 * 128);
                        bound.Draw(poly);
                    }

                    // Print Text
                    for (int i = 0; i < coords.Count; i++)
                    {
                        double x, y;

                        if (coords[i].X > zoneConfiguration.TargetMapSize / 2) x = coords[i].X - 15;
                        else x = coords[i].X + 1;

                        if (coords[i].Y < zoneConfiguration.TargetMapSize / 2) y = coords[i].Y + 15;
                        else y = coords[i].Y - 1;

                        bound.FontPointsize = 10.0;
                        bound.FillColor = Color.Black;
                        using (DrawableText text = new DrawableText(x, y, string.Format("{0} ({1}/{2})", i, zoneConfiguration.MapCoordinateToZoneCoordinate(coords[i].X), zoneConfiguration.MapCoordinateToZoneCoordinate(coords[i].Y))))
                        {
                            bound.Draw(text);
                        }

                        using (WritablePixelCollection pixels = bound.GetWritablePixels())
                        {
                            int x2, y2;
                            if (coords[i].X == zoneConfiguration.TargetMapSize) x2 = zoneConfiguration.TargetMapSize - 1;
                            else x2 = (int)coords[i].X;
                            if (coords[i].Y == zoneConfiguration.TargetMapSize) y2 = zoneConfiguration.TargetMapSize - 1;
                            else y2 = (int)coords[i].Y;

                            pixels.Set(x2, y2, new float[] { 0, 0, 65536, 0 });
                        }
                    }

                    //bound.Quality = 100;
                    bound.Write(string.Format("{0}\\bound_{1}.png", debugDir.FullName, boundIndex));

                    boundIndex++;
                }
            }

            MainForm.ProgressReset();
        }
    }
}
