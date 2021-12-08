using System;
using System.Collections.Generic;
using System.Linq;
using CromulentBisgetti.ContainerPacking.Entities;
using CromulentBisgetti.ContainerPacking.Services.Contracts;
using PackingService.Sql.Services.Models;

namespace PackingService.Sql.Services.Proxies
{
    public class MtBoxes : IBoxes
    {
        public List<Container> GetContainers()
        {
            var containers = new List<Container>();
            using (var db = new PackingEngineDB())
            {
                foreach (var box in db.Boxdatas)
                {
                    var container = new Container(box.Name, (decimal)box.Length, (decimal)box.Width, (decimal)box.Height);
                    containers.Add(container);
                }
            }

            return containers;
        }

        public List<Container> GetPallets()
        {
            var containers = new List<Container>();
            using (var db = new PackingEngineDB())
            {
                foreach (var box in db.BoxdataPallets)
                {
                    var container = new Container(box.Name, (decimal)box.Length, (decimal)box.Width, (decimal)box.Height);
                    containers.Add(container);
                }
            }

            return containers;
        }

        public List<Item> GetItemsToPack(string custOrderNo)
        {
            var items = new List<Item>();
            
            using (var db = new PackingEngineDB())
            {
                foreach (var i in db.ItemsToPalletizes.Where(x => x.CustOrderNo == custOrderNo))
                {
                    var item = new Item(i.Alias, (decimal)i.BoxLength, (decimal)i.BoxHeight, (decimal)i.BoxWidth, i.QTY, i.CanBeFlagpole, i.CharWeight, i.AbnormalShape);
                    items.Add(item);
                }
            }

            var groupedItems = GroupItems(items);

            return groupedItems;
        }

        private List<Item> GroupItems(List<Item> items)
        {
            var groups = items.GroupBy(x => x.Id);
            var newItems = new List<Item>();
            foreach (var group in groups)
            {
                var defaultItem = group.FirstOrDefault();
                var item = new Item(group.Key, defaultItem.Dim1, defaultItem.Dim2, defaultItem.Dim3, group.Count(), defaultItem.CanBeFlagpole, defaultItem.WeightDef, defaultItem.IsAbnormalShape);
                newItems.Add(item);
            }

            return newItems;
        }

        //public List<Item> GetItemsToPack(string custOrderNo)
        //{
        //    var items = new List<Item>();
        //    using (var db = new PackingEngineDB())
        //    {
        //        foreach (var i in db.OrderDetails.Where(x=>x.OrderId == custOrderNo))
        //        {
        //            var item = new Item(i.OrderDetailsID, (decimal)i.Lenght, (decimal)i.Height, (decimal)i.Width, i.Quantity);
        //            items.Add(item);
        //        }
        //    }

        //    return items;
        //}
    }
}
