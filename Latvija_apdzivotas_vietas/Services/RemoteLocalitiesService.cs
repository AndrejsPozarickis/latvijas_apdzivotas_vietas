using Latvija_apdzivotas_vietas.Models;

namespace Latvija_apdzivotas_vietas.Services
{
    public class RemoteLocalitiesService
    {
        private String StrFormat(string str)
        {
            str = str.Substring(str.IndexOf("#"), str.Length - 1);
            return str;
        }

        public List<LocalityModel> FindRemoteLocalities(List<LocalityModel> list)
        {
            MostRemoteLocalities[] remoteLocalities = new MostRemoteLocalities[3];

            foreach (var el in list)
            {
                // get away from #
                el.Title = StrFormat(el.Title);
                el.Coord_X = StrFormat(el.Coord_X);
                el.Coord_Y = StrFormat(el.Coord_Y);

                if (remoteLocalities.Length > 0)
                {
                    if(Convert.ToDouble(el.Coord_Y) > remoteLocalities[0].Y)
                    {
                        remoteLocalities[0].Y = Convert.ToDouble(el.Coord_Y);
                    }
                    if (Convert.ToDouble(el.Coord_X) > remoteLocalities[0].X)
                    {
                        remoteLocalities[0].X = Convert.ToDouble(el.Coord_X);
                    }
                    if (Convert.ToDouble(el.Coord_Y) < remoteLocalities[0].Y)
                    {
                        remoteLocalities[0].Y = Convert.ToDouble(el.Coord_Y);
                    }
                    if (Convert.ToDouble(el.Coord_X) < remoteLocalities[0].X)
                    {
                        remoteLocalities[0].X = Convert.ToDouble(el.Coord_X);
                    }
                } 
                else
                {
                    #region Defaults
                    remoteLocalities[0].Direction = 'Z';
                    remoteLocalities[0].X = Convert.ToDouble(el.Coord_X);
                    remoteLocalities[0].Y = Convert.ToDouble(el.Coord_Y);

                    remoteLocalities[0].Direction = 'A';
                    remoteLocalities[0].X = Convert.ToDouble(el.Coord_X);
                    remoteLocalities[0].Y = Convert.ToDouble(el.Coord_Y);

                    remoteLocalities[0].Direction = 'D';
                    remoteLocalities[0].X = Convert.ToDouble(el.Coord_X);
                    remoteLocalities[0].Y = Convert.ToDouble(el.Coord_Y);

                    remoteLocalities[0].Direction = 'R';
                    remoteLocalities[0].X = Convert.ToDouble(el.Coord_X);
                    remoteLocalities[0].Y = Convert.ToDouble(el.Coord_Y);
                    #endregion
                }
            }

            return list;
        }
    }

    public class MostRemoteLocalities {
        public char Direction { get; set; }
        public double X { get; set; }
        public double Y { get; set; }
    }
}
