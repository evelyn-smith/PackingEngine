using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CromulentBisgetti.ContainerPacking.Entities;
using PackingService.Sql.Services.Models;
using PackingService.Sql.Services.Proxies;

namespace PackingService.Blazor.Data
{
    public class BoxService
    {
        public Task<List<int>> GetAlgorithmsAsync()
        {
            var algorithms = new List<int>(){1};
            return Task.FromResult(algorithms);
        }

        public Task<List<Item>> GetItemsAsync(string custOrderNo)
        {
            var items = new MtBoxes().GetItemsToPack(custOrderNo);
            return Task.FromResult(items);
        }

        public Task<List<Container>> GetContainersAsync()
        {
            var pallets = new List<Container>() { new Container("54in Pallet", 40.0m, 48.0m, 78.0m) };
            return Task.FromResult(pallets);
        }

        public Task<List<Container>> GetPalletsAsync()
        {
            var pallets = new List<Container>() { new Container("54in Pallet", 40.0m, 48.0m, 78.0m) };
            return Task.FromResult(pallets);
        }

        public Task<List<Container>> GetSmallPackageBoxesAsync()
        {
            var db = new PackingEngineDB();
            var smallPackageBoxes = db.Boxdatas.Select(x => new Container(x.Name, (decimal) x.Length, (decimal)x.Width, (decimal)x.Height)).ToList();
            return Task.FromResult(smallPackageBoxes);

           // var pallets = new List<Container>() { new Container("54in Pallet", 40.0m, 48.0m, 78.0m) };
          //  return Task.FromResult(pallets);
        }
    }
}