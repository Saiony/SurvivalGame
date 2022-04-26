using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Game.Scripts.Domain.Crafting;

namespace Game.Scripts.Service.Interface
{
    public interface ICraftingService : IBaseService
    {
        CraftingList CraftingList { get; }
    }
}