using UnityEngine;

namespace Game.Scripts.Controller.Interface.Item
{
    public interface ITimeChangeable
    {
        Controller.Item.Item FowardTime();
        Controller.Item.Item RewindTime();
    }
}