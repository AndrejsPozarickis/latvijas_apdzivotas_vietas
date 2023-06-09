using Latvija_apdzivotas_vietas.Models;

namespace Latvija_apdzivotas_vietas.Services
{
    public class RemoteLocalitiesService
    {
        public String StrFormat(string str)
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

        public List<ShortLocalityModel> FindRemoteLocalities(List<LocalityModel> list)
        {
            List<ShortLocalityModel> remoteLocalities = new List<ShortLocalityModel>();

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
                        remoteLocalities[0].East = Convert.ToDouble(el.DD_E);
                        remoteLocalities[0].Title = el.Title;
                    }
                    if (Convert.ToDouble(el.DD_E) > remoteLocalities[1].East)
                    {
                        remoteLocalities[1].North = Convert.ToDouble(el.DD_N);
                        remoteLocalities[1].East = Convert.ToDouble(el.DD_E);
                        remoteLocalities[1].Title = el.Title;
                    }
                    if (Convert.ToDouble(el.DD_N) < remoteLocalities[2].North)
                    {
                        remoteLocalities[2].North = Convert.ToDouble(el.DD_N);
                        remoteLocalities[2].East = Convert.ToDouble(el.DD_E);
                        remoteLocalities[2].Title = el.Title;
                    }
                    if (Convert.ToDouble(el.DD_E) < remoteLocalities[3].East)
                    {
                        remoteLocalities[3].North = Convert.ToDouble(el.DD_N);
                        remoteLocalities[3].East = Convert.ToDouble(el.DD_E);
                        remoteLocalities[3].Title = el.Title;
                    }
                } 
                else
                {
                    #region Defaults
                    remoteLocalities.Add( new ShortLocalityModel
                    {
                        Title = el.Title,
                        East = Convert.ToDouble(el.DD_E),
                        North = Convert.ToDouble(el.DD_N)
                    });
                    remoteLocalities.Add(new ShortLocalityModel
                    {
                        Title = el.Title,
                        East = Convert.ToDouble(el.DD_E),
                        North = Convert.ToDouble(el.DD_N)
                    });
                    remoteLocalities.Add(new ShortLocalityModel
                    {
                        Title = el.Title,
                        East = Convert.ToDouble(el.DD_E),
                        North = Convert.ToDouble(el.DD_N)
                    });
                    remoteLocalities.Add(new ShortLocalityModel
                    {
                        Title = el.Title,
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
