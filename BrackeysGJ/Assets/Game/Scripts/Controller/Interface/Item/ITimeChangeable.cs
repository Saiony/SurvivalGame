using Game.Scripts.Controller.Item;
using UnityEngine;

namespace Game.Scripts.Controller.Interface.Item
{
    public interface ITimeChangeable
    {
        ItemController FowardTime();
        ItemController RewindTime();
    }
}