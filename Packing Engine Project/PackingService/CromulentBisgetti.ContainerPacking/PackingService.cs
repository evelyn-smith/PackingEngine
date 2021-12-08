using CromulentBisgetti.ContainerPacking.Algorithms;
using CromulentBisgetti.ContainerPacking.Entities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Serilog;

namespace CromulentBisgetti.ContainerPacking
{
    /// <summary>
    /// The container packing service.
    /// </summary>
    public static class PackingService
    {
        /// <summary>
        /// Attempts to pack the specified containers with the specified items using the specified algorithms.
        /// </summary>
        /// <param name="containers">The list of containers to pack.</param>
        /// <param name="itemsToPack">The items to pack.</param>
        /// <param name="algorithmTypeIDs">The list of algorithm type IDs to use for packing.</param>
        /// <returns>A container packing result with lists of the packed and unpacked items.</returns>
        public static List<ContainerPackingResult> Pack(List<Container> containers, List<Item> itemsToPack, List<int> algorithmTypeIDs)
        {
            Object sync = new Object { };
            List<ContainerPackingResult> result = new List<ContainerPackingResult>();

            Parallel.ForEach(containers, container =>
            {
                ContainerPackingResult containerPackingResult = new ContainerPackingResult();
                containerPackingResult.ContainerId = container.Id;

                Parallel.ForEach(algorithmTypeIDs, algorithmTypeId =>
                {
                    IPackingAlgorithm algorithm = GetPackingAlgorithmFromTypeId(algorithmTypeId);

                    // Until I rewrite the algorithm with no side effects, we need to clone the item list
                    // so the parallel updates don't interfere with each other.
                    List<Item> items = new List<Item>();

                    itemsToPack.ForEach(item =>
                    {
                        items.Add(new Item(item.Id, item.Dim1, item.Dim2, item.Dim3, item.Quantity, item.CanBeFlagpole, item.WeightDef, item.IsAbnormalShape));
                    });

                    Stopwatch stopwatch = new Stopwatch();
                    stopwatch.Start();
                    AlgorithmPackingResult algorithmResult = algorithm.Run(container, items);
                    stopwatch.Stop();

                    algorithmResult.PackTimeInMilliseconds = stopwatch.ElapsedMilliseconds;

                    decimal containerVolume = container.Length * container.Width * container.Height;
                    decimal itemVolumePacked = algorithmResult.PackedItems.Sum(i => i.Volume);
                    decimal itemVolumeUnpacked = algorithmResult.UnpackedItems.Sum(i => i.Volume);

                    algorithmResult.PercentContainerVolumePacked = Math.Round(itemVolumePacked / containerVolume * 100, 2);
                    algorithmResult.PercentItemVolumePacked = Math.Round(itemVolumePacked / (itemVolumePacked + itemVolumeUnpacked) * 100, 2);

                    lock (sync)
                    {
                        containerPackingResult.AlgorithmPackingResults.Add(algorithmResult);
                    }
                });

                containerPackingResult.AlgorithmPackingResults = containerPackingResult.AlgorithmPackingResults.OrderBy(r => r.AlgorithmName).ToList();

                lock (sync)
                {
                    result.Add(containerPackingResult);
                }
            });

            return result;
        }

        /// <summary>
        /// Attempts to pack the specified containers with the specified items using the specified algorithms.
        /// </summary>
        /// <param name="containers">The list of containers to pack.</param>
        /// <param name="itemsToPack">The items to pack.</param>
        /// <param name="algorithmTypeIDs">The list of algorithm type IDs to use for packing.</param>
        /// <returns>A container packing result with lists of the packed and unpacked items.</returns>
        public static List<AlgorithmPackingResult> PackContainer(Container container, List<Item> itemsToPack)
        {
            Log.Information("Beginning packing {Container} with {NumberOfItems} and {QuantityOfItems}", container, itemsToPack.Count, itemsToPack.Sum(x=>x.Quantity));
            //Object sync = new Object { };
            //List<ContainerPackingResult> result = new List<ContainerPackingResult>();

            //Parallel.ForEach(containers, container =>
            //{
            //ContainerPackingResult containerPackingResult = new ContainerPackingResult();
            //containerPackingResult.ContainerId = container.Id;
            

            // Until I rewrite the algorithm with no side effects, we need to clone the item list
            // so the parallel updates don't interfere with each other.
            var items = new List<Item>();

            itemsToPack.ForEach(item =>
            {
                var newItem = new Item(item.Id, item.Dim1, item.Dim2, item.Dim3, item.Quantity, item.CanBeFlagpole, item.WeightDef, item.IsAbnormalShape);
                if(item.MaxDimension > container.MaxDimension)
                {
                    Log.Error(
                        "Cannot pack item {Item} because its max dimension {ItemMaxDimension} is greater than the container's max dimension {ContainerMaxDimension}",
                        newItem, newItem.MaxDimension, container.MaxDimension);
                }
                else
                {
                    //Layer these first if possible-- Doing this by layer messes up the weighting method currently implemented
                    items.AddRange(LayerItems(container, newItem));
                    //items.Add(newItem);
                }
            });


            var indexId = 0;
            var packingResults = new List<AlgorithmPackingResult>();
            while (items.Any())
            {

                var itemsToConsider = GetItemsForGrossVolume(items, container.Volume * 1.5m);//Always only try to pack items that would completely fill 1.5 containers
                var packingResult = PackItems(container, itemsToConsider, indexId);
                indexId++;
                packingResults.Add(packingResult);

                foreach (var item in packingResult.PackedItems)
                {
                    var foundItem = items.FirstOrDefault(x => x.Id == item.Id);
                    items.Remove(foundItem);
                }
            }
            Log.Information("Finished Pack - {NumberOfPackedContainers} containers", packingResults.Count);
            return packingResults;

            //lock (sync)
            //{
            //	containerPackingResult.AlgorithmPackingResults.Add(algorithmResult);
            //}
            //});

            //containerPackingResult.AlgorithmPackingResults = containerPackingResult.AlgorithmPackingResults.OrderBy(r => r.AlgorithmName).ToList();

            //lock (sync)
            //{
            //	result.Add(containerPackingResult);
            //}
            //});

            //return result;
        }

        private static AlgorithmPackingResult PackItems(Container container, List<Item> items, int indexId = 0)
        {

            var algorithmTypeId = 1;
            var algorithm = GetPackingAlgorithmFromTypeId(algorithmTypeId);

            var stopwatch = new Stopwatch();
            stopwatch.Start();
            
            var packingResult = algorithm.Run(container, items);
            stopwatch.Stop();

            packingResult.PackTimeInMilliseconds = stopwatch.ElapsedMilliseconds;

            packingResult.PackedItems.OrderBy(x=>x.CoordZ);

            var containerVolume = container.Length * container.Width * container.Height;
            var itemVolumePacked = packingResult.PackedItems.Sum(i => i.Volume);
            var itemVolumeUnpacked = packingResult.UnpackedItems.Sum(i => i.Volume);

            packingResult.PercentContainerVolumePacked = Math.Round(itemVolumePacked / containerVolume * 100, 2);
            packingResult.PercentItemVolumePacked = Math.Round(itemVolumePacked / (itemVolumePacked + itemVolumeUnpacked) * 100, 2);

            packingResult.IndexId = indexId;
            return packingResult;
        }

        /// <summary>
        /// This will only pack them in full layers if there is a full layer and at least one leftover piece
        /// I am deferring how to tell if the exact quantity was a valid layer until later because I do not have a good idea how to yet
        /// </summary>
        /// <param name="container"></param>
        /// <param name="item"></param>
        /// <returns></returns>
        private static List<Item> LayerItems(Container container, Item item)
        {
            

            var layerContainer = new Container("singleLayer", container.Length, container.Width, item.MaxDimension); //Get a container tall enough to hold a layer of these items, could be issues with really long single dimension items
            var packingResult = PackItems(layerContainer, new List<Item>() { item }); //Get the best layer of these items

            if (!packingResult.PackedItems.Any() || !packingResult.UnpackedItems.Any())
            {
                return new List<Item>() {item};
            }
            
            var layerQuantity = packingResult.PackedItems.Count;
            var layerX = packingResult.PackedItems.Max(x => x.CoordX + x.PackDimX);
            var layerY = packingResult.PackedItems.Max(x => x.CoordY + x.PackDimY);
            var layerZ = packingResult.PackedItems.Max(x => x.CoordZ + x.PackDimZ);

            var layerCount = item.Quantity / layerQuantity;
            var leftoverQuantity = item.Quantity % layerQuantity;

            var layerItem = new Item($"{item.Id}-LayerOf-{layerQuantity}", layerX, layerY, layerZ,  layerCount, item.CanBeFlagpole, item.WeightDef, item.IsAbnormalShape);
            //layerItem.IsLayer = true;
            var leftoverItem = new Item(item.Id, item.Dim1, item.Dim2, item.Dim3, leftoverQuantity, item.CanBeFlagpole, item.WeightDef, item.IsAbnormalShape);

            Log.Information("Was able to layer {Item} into LayerCount {LayerCount} of LayerQuantity {LayerQuantity} with Leftover Quantity {LeftoverQuantity}", item, layerCount, layerQuantity, leftoverQuantity);

            return new List<Item>() { layerItem, leftoverItem };
        }



        private static List<Item> GetItemsForGrossVolume(List<Item> itemsToConsider, decimal grossVolume)
        {
            var items = new List<Item>();
            var counter = 0;
            while (grossVolume > 0 && counter < itemsToConsider.Count)
            {
                items.Add(itemsToConsider[counter]);
                grossVolume -= itemsToConsider[counter].Volume;
                counter++;
            }

            return items;
        }

        /// <summary>
        /// Gets the packing algorithm from the specified algorithm type ID.
        /// </summary>
        /// <param name="algorithmTypeId">The algorithm type ID.</param>
        /// <returns>An instance of a packing algorithm implementing AlgorithmBase.</returns>
        /// <exception cref="System.Exception">Invalid algorithm type.</exception>
        public static IPackingAlgorithm GetPackingAlgorithmFromTypeId(int algorithmTypeId)
        {
            switch (algorithmTypeId)
            {
                case (int)AlgorithmType.EbAfit:
                    return new EbAfit();

                default:
                    throw new Exception("Invalid algorithm type.");
            }
        }
    }
}
