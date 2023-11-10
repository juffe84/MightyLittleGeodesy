/*
 * MightyLittleGeodesy 
 * RT90, SWEREF99 and WGS84 coordinate transformation library
 * 
 * Read my blog @ http://blog.sallarp.com
 * 
 * 
 * Copyright (C) 2009 Björn Sållarp
 * Permission is hereby granted, free of charge, to any person obtaining a copy of this 
 * software and associated documentation files (the "Software"), to deal in the Software 
 * without restriction, including without limitation the rights to use, copy, modify, 
 * merge, publish, distribute, sublicense, and/or sell copies of the Software, and to 
 * permit persons to whom the Software is furnished to do so, subject to the following 
 * conditions:
 * 
 * The above copyright notice and this permission notice shall be included in all copies or 
 * substantial portions of the Software.
 * 
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING 
 * BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND 
 * NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, 
 * DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, 
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
 */

using MightyLittleGeodesy.Classes;

namespace MightyLittleGeodesy.Positions
{
    public class ETRS_Position : Position
    {
        public enum ETRSProjection
        {
            etrs_tm35 = 0,
            etrs_gk19 = 1,
            etrs_gk20 = 2,
            etrs_gk21 = 3,
            etrs_gk22 = 4,
            etrs_gk23 = 5,
            etrs_gk24 = 6,
            etrs_gk25 = 7,
            etrs_gk26 = 8,
            etrs_gk27 = 9,
            etrs_gk28 = 10,
            etrs_gk29 = 11,
            etrs_gk30 = 12,
            etrs_gk31 = 13,
            etrs_gk32 = 14,
        }

        /// <summary>
        /// Create a Sweref99 position from double values with 
        /// Sweref 99 TM as default projection.
        /// </summary>
        /// <param name="n"></param>
        /// <param name="e"></param>
        public ETRS_Position(double n, double e)
            : base(n, e, Grid.ETRS)
        {
            Projection = ETRSProjection.etrs_tm35;
        }

        /// <summary>
        /// Create a Sweref99 position from double values. Supply the projection
        /// for values other than Sweref 99 TM
        /// </summary>
        /// <param name="n"></param>
        /// <param name="e"></param>
        /// <param name="projection"></param>
        public ETRS_Position(double n, double e, ETRSProjection projection)
            : base(n, e, Grid.ETRS)
        {
            Projection = projection;
        }

        /// <summary>
        /// Create a RT90 position by converting a WGS84 position
        /// </summary>
        /// <param name="position">WGS84 position to convert</param>
        /// <param name="rt90projection">Projection to convert to</param>
        public ETRS_Position(WGS84Position position, ETRSProjection projection)
            : base(Grid.ETRS)
        {
            GaussKreuger gkProjection = new GaussKreuger();
            gkProjection.swedish_params(GetProjectionString(projection));

            var lat_lon = gkProjection.geodetic_to_grid(position.Latitude, position.Longitude);
            Latitude = lat_lon[0];
            Longitude = lat_lon[1];
            Projection = projection;
        }

        /// <summary>
        /// Convert the position to WGS84 format
        /// </summary>
        /// <returns></returns>
        public WGS84Position ToWGS84()
        {
            GaussKreuger gkProjection = new GaussKreuger();
            gkProjection.swedish_params(ProjectionString);
            var lat_lon = gkProjection.etrs_to_geodetic(Latitude, Longitude);

            WGS84Position newPos = new WGS84Position()
            {
                Latitude = lat_lon[0],
                Longitude = lat_lon[1],
                GridFormat = Grid.WGS84
            };

            return newPos;
        }

        private string GetProjectionString(ETRSProjection projection)
        {
            string retVal = string.Empty;
            switch (projection)
            {
                case ETRSProjection.etrs_tm35:
                    retVal = "etrs_tm35";
                    break;
                case ETRSProjection.etrs_gk19:
                    retVal = "etrs_gk19";
                    break;
                case ETRSProjection.etrs_gk20:
                    retVal = "etrs_gk20";
                    break;
                case ETRSProjection.etrs_gk21:
                    retVal = "etrs_gk21";
                    break;
                case ETRSProjection.etrs_gk22:
                    retVal = "etrs_gk22";
                    break;
                case ETRSProjection.etrs_gk23:
                    retVal = "etrs_gk23";
                    break;
                case ETRSProjection.etrs_gk24:
                    retVal = "etrs_gk24";
                    break;
                case ETRSProjection.etrs_gk25:
                    retVal = "etrs_gk25";
                    break;
                case ETRSProjection.etrs_gk26:
                    retVal = "etrs_gk26";
                    break;
                case ETRSProjection.etrs_gk27:
                    retVal = "etrs_gk27";
                    break;
                case ETRSProjection.etrs_gk28:
                    retVal = "etrs_gk28";
                    break;
                case ETRSProjection.etrs_gk29:
                    retVal = "etrs_gk29";
                    break;
                case ETRSProjection.etrs_gk30:
                    retVal = "etrs_gk30";
                    break;
                case ETRSProjection.etrs_gk31:
                    retVal = "etrs_gk31";
                    break;
                case ETRSProjection.etrs_gk32:
                    retVal = "etrs_gk32";
                    break;
                default:
                    retVal = "etrs_tm35tm";
                    break;
            }

            return retVal;
        }

        public ETRSProjection Projection { get; set; }
        public string ProjectionString
        {
            get
            {
                return GetProjectionString(Projection);
            }
        }

        public override string ToString()
        {
            return string.Format("N: {0} E: {1} Projection: {2}", Latitude, Longitude, ProjectionString);
        }
    }
}
