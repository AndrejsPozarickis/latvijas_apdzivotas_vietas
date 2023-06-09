using Microsoft.AspNetCore.Mvc;
using System.Net;

using Latvija_apdzivotas_vietas.Models;
using Latvija_apdzivotas_vietas.Interfaces;
using System.IO.Compression;
using Latvija_apdzivotas_vietas.Services;

namespace Latvija_apdzivotas_vietas.Controllers
{
    public class IndexController : Controller
    {
        private readonly ICSVService _csvService;

        public IndexController(ICSVService csvService)
        {
            this._csvService = csvService;
        }


        [HttpGet]
        public IActionResult Index(List<ShortLocalityModel>? localities = null)
        {
            var response = localities == null ? new List<ShortLocalityModel>() : localities;

            return View(response);
        }

        [HttpPost]
        public IActionResult Index()
        {
            using (WebClient wc = new WebClient())
            {
                wc.Headers.Add("Cookie: Authentication=user");
                try
                {
                    // archive downloading
                    string filename = "archive.zip"; 
                    wc.DownloadFile("https://data.gov.lv/dati/dataset/0c5e1a3b-0097-45a9-afa9-7f7262f3f623/resource/1d3cbdf2-ee7d-4743-90c7-97d38824d0bf/download/aw_csv.zip.", filename); // NOTE: could add a file extension here
                    
                    // CSV file extracting
                    var pathCSV = Directory.GetCurrentDirectory() + "\\" + "AW_VIETU_CENTROIDI.CSV";
                    var zip = ZipFile.OpenRead(filename);
                    var entry = zip.GetEntry("AW_VIETU_CENTROIDI.CSV");

                    if (entry != null)
                    {
                        entry.ExtractToFile("AW_VIETU_CENTROIDI.CSV", true);
                    }
                    
                    zip.Dispose();

                    return Index(null);
                }
                catch (System.Exception ex)
                {
                    Console.WriteLine("Something went wrong with getting data:");
                    Console.WriteLine(ex.Message);
                }
            }
            return Index(null);
        }

        public List<LocalityModel> GetLocalities()
        {
            string targetFile = Directory.GetCurrentDirectory() + "\\" + "AW_VIETU_CENTROIDI.CSV";

            List<LocalityModel> list = (List<LocalityModel>)_csvService.ReadCSV<LocalityModel>(targetFile);

            return list;
        }

        public JsonResult SearchLocality(string searchStr)
        {
            if (!string.IsNullOrEmpty(searchStr))
            {
                List<LocalityModel> list = GetLocalities();

                List<LocalityModel> searchResult = list.Where(e => e.Title.Contains(searchStr)).ToList();

                List<ShortLocalityModel> response = new List<ShortLocalityModel>();

                var service = new RemoteLocalitiesService();
                foreach (var item in searchResult)
                {
                    item.Title = service.StrFormat(item.Title);
                    item.DD_E = service.StrFormat(item.DD_E);
                    item.DD_N = service.StrFormat(item.DD_N);

                    response.Add(new ShortLocalityModel
                    {
                        Title = item.Title,
                        East = Convert.ToDouble(item.DD_E),
                        North = Convert.ToDouble(item.DD_N)
                    });
                }

                return Json(response);
            }
            return Json(null);
        }

        [HttpPost]
        public JsonResult GetRemoteLocalities()
        {
            var remoteLocalities = new RemoteLocalitiesService();

            List<LocalityModel> list = GetLocalities();

            var response = remoteLocalities.FindRemoteLocalities(list);

            return Json(response);
        }
    }
}
