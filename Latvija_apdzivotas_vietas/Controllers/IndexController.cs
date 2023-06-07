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
        public IActionResult Index(List<LocalityModel>? localities = null)
        {
            var response = localities == null ? new List<LocalityModel>() : localities;

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
                    string filename = "archive.zip"; 
                    wc.DownloadFile("https://data.gov.lv/dati/dataset/0c5e1a3b-0097-45a9-afa9-7f7262f3f623/resource/1d3cbdf2-ee7d-4743-90c7-97d38824d0bf/download/aw_csv.zip.", filename); // NOTE: could add a file extension here

                    var response = GetLocalitiesData(filename);

                    return Index(response);
                }
                catch (System.Exception ex)
                {
                    Console.WriteLine("Something went wrong with file downloading:");
                    Console.WriteLine(ex.Message);
                }
            }
            return Index(null);
        }

        public List<LocalityModel> GetLocalitiesData(string zipFile)
        {
            var currentDir = Directory.GetCurrentDirectory();
            var pathCSV = currentDir + "\\" + "AW_VIETU_CENTROIDI.CSV";

            var zip = ZipFile.OpenRead(zipFile);
            var entry = zip.GetEntry("AW_VIETU_CENTROIDI.CSV");

            if (entry != null)
            {
                entry.ExtractToFile("AW_VIETU_CENTROIDI.CSV", true);
            }

            var localities = _csvService.ReadCSV<LocalityModel>(pathCSV);

            return (List<LocalityModel>)localities;
        }

        public IActionResult GetRemoteLocalities()
        {
            var remoteLocalities = new RemoteLocalitiesService();

            List<LocalityModel> list = (List<LocalityModel>)_csvService.ReadCSV<LocalityModel>(Directory.GetCurrentDirectory() + "\\" + "AW_VIETU_CENTROIDI.CSV");

            var response = remoteLocalities.FindRemoteLocalities(list);

            ViewBag.RemoteLocalities = response;
            ViewData["RemoteLocality"] = (RemoteLocalityModel)response;

            return View("Index");
        }
    }
}
