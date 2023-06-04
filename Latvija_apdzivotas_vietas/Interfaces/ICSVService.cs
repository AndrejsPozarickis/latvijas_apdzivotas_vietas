namespace Latvija_apdzivotas_vietas.Interfaces
{
   public interface ICSVService
    {
        public IEnumerable<T> ReadCSV<T>(string filename);
    }
}
