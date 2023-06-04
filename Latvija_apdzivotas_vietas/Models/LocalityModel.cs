using CsvHelper.Configuration;
using CsvHelper.Configuration.Attributes;

namespace Latvija_apdzivotas_vietas.Models
{
    public class LocalityModel
    {
        [Index(0)]
        public string Code { get; set; } = "";

        [Index(1)]
        public string Tips_CD { get; set; } = "";

        [Index(2)]
        public string Title { get; set; } = "";

        [Index(3)]
        public string VKUR_CD { get; set; } = "";

        [Index(4)]
        public string VKUR_Tips { get; set; } = "";

        [Index(5)]
        public string STD { get; set; } = "";

        [Index(6)]
        public string Coord_X { get; set; } = "";

        [Index(7)]
        public string Coord_Y { get; set; } = "";

        [Index(8)]
        public string DD_N { get; set; } = "";

        [Index(9)]
        public string DD_E { get; set; } = "";
    }
}
