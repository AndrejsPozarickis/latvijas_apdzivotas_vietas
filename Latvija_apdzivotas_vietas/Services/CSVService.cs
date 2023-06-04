using CsvHelper;
using CsvHelper.Configuration;
using Latvija_apdzivotas_vietas.Interfaces;
using System.Globalization;
using System.Text;

namespace Latvija_apdzivotas_vietas.Services
{
    public class CSVService : ICSVService
    {
        public IEnumerable<T> ReadCSV<T>(string filename)
        {
            List<T> list = new List<T>();

            var reader = new StreamReader(filename);
            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                BadDataFound = null,
                Delimiter = ";",
                Encoding = Encoding.UTF8
            };
            var csv = new CsvReader(reader, config);

            csv.Read();
            csv.ReadHeader();

            while (csv.Read())
            {
                var record = csv.GetRecord<T>();
                list.Add(record);
            }
            return list;
        }
    }
}
