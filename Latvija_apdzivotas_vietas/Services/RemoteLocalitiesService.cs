using Latvija_apdzivotas_vietas.Models;

namespace Latvija_apdzivotas_vietas.Services
{
    public class RemoteLocalitiesService
    {
        private String StrFormat(string str)
        {
            str = str.Split('#', '#')[1];

            // depending on the regional settings Convert.ToDouble can not accept double with ","
            // if you have an error about incorrect format, comment out the code below:
            bool startWithNum = char.IsDigit(str[0]);
            if(startWithNum)
            {
                str = str.Replace('.', ',');
            }

            return str;
        }

        public List<RemoteLocalityModel> FindRemoteLocalities(List<LocalityModel> list)
        {
            List<RemoteLocalityModel> remoteLocalities = new List<RemoteLocalityModel>();

            foreach (var el in list)
            {
                // get away from #
                el.Title = StrFormat(el.Title);
                el.DD_E = StrFormat(el.DD_E);
                el.DD_N = StrFormat(el.DD_N);

                if (remoteLocalities.Any())
                {
                    if(Convert.ToDouble(el.DD_N) > remoteLocalities[0].North)
                    {
                        remoteLocalities[0].North = Convert.ToDouble(el.DD_N);
                    }
                    if (Convert.ToDouble(el.DD_E) > remoteLocalities[1].East)
                    {
                        remoteLocalities[1].East = Convert.ToDouble(el.DD_E);
                    }
                    if (Convert.ToDouble(el.DD_N) < remoteLocalities[2].North)
                    {
                        remoteLocalities[2].North = Convert.ToDouble(el.DD_N);
                    }
                    if (Convert.ToDouble(el.DD_E) < remoteLocalities[3].East)
                    {
                        remoteLocalities[3].East = Convert.ToDouble(el.DD_E);
                    }
                } 
                else
                {
                    #region Defaults
                    remoteLocalities.Add( new RemoteLocalityModel
                    {
                        Direction = 'Z',
                        East = Convert.ToDouble(el.DD_E),
                        North = Convert.ToDouble(el.DD_N)
                    });
                    remoteLocalities.Add(new RemoteLocalityModel
                    {
                        Direction = 'A',
                        East = Convert.ToDouble(el.DD_E),
                        North = Convert.ToDouble(el.DD_N)
                    });
                    remoteLocalities.Add(new RemoteLocalityModel
                    {
                        Direction = 'D',
                        East = Convert.ToDouble(el.DD_E),
                        North = Convert.ToDouble(el.DD_N)
                    });
                    remoteLocalities.Add(new RemoteLocalityModel
                    {
                        Direction = 'R',
                        East = Convert.ToDouble(el.DD_E),
                        North = Convert.ToDouble(el.DD_N)
                    });
                    #endregion
                }
            }

            return remoteLocalities;
        }
    }
}
