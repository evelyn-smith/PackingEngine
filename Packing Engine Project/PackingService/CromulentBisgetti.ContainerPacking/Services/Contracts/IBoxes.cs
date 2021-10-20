using System.Collections.Generic;
using CromulentBisgetti.ContainerPacking.Entities;

namespace CromulentBisgetti.ContainerPacking.Services.Contracts
{
    public interface IBoxes
    {
        List<Container> GetContainers();
        List<Item> GetItemsToPack(string salesId);
    }
}