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

        public MostRemoteLocalities[] FindRemoteLocalities(List<LocalityModel> list)
        {
            MostRemoteLocalities[] remoteLocalities = new MostRemoteLocalities[3];

            foreach (var el in list)
            {
                // get away from #
                el.Title = StrFormat(el.Title);
                el.DD_E = StrFormat(el.DD_E);
                el.DD_N = StrFormat(el.DD_N);

                if (remoteLocalities.Length > 0)
                {
                    if(Convert.ToDouble(el.DD_N) > remoteLocalities[0].N)
                    {
                        remoteLocalities[0].N = Convert.ToDouble(el.DD_N);
                    }
                    if (Convert.ToDouble(el.DD_E) > remoteLocalities[1].E)
                    {
                        remoteLocalities[1].E = Convert.ToDouble(el.DD_E);
                    }
                    if (Convert.ToDouble(el.DD_N) < remoteLocalities[2].N)
                    {
                        remoteLocalities[2].N = Convert.ToDouble(el.DD_N);
                    }
                    if (Convert.ToDouble(el.DD_E) < remoteLocalities[3].E)
                    {
                        remoteLocalities[3].E = Convert.ToDouble(el.DD_E);
                    }
                } 
                else
                {
                    #region Defaults
                    remoteLocalities[0].Direction = 'Z';
                    remoteLocalities[0].E = Convert.ToDouble(el.DD_E);
                    remoteLocalities[0].N = Convert.ToDouble(el.DD_N);

                    remoteLocalities[1].Direction = 'A';
                    remoteLocalities[1].E = Convert.ToDouble(el.DD_E);
                    remoteLocalities[1].N = Convert.ToDouble(el.DD_N);

                    remoteLocalities[2].Direction = 'D';
                    remoteLocalities[2].E = Convert.ToDouble(el.DD_E);
                    remoteLocalities[2].N = Convert.ToDouble(el.DD_N);

                    remoteLocalities[3].Direction = 'R';
                    remoteLocalities[3].E = Convert.ToDouble(el.DD_E);
                    remoteLocalities[3].N = Convert.ToDouble(el.DD_N);
                    #endregion
                }
            }

            return remoteLocalities;
        }
    }

    public class MostRemoteLocalities {
        public char Direction { get; set; }
        public double E { get; set; }
        public double N { get; set; }
    }
}
