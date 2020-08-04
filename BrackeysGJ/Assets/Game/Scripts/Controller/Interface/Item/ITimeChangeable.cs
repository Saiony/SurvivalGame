using UnityEngine;

namespace Game.Scripts.Controller.Interface.Item
{
    public interface ITimeChangeable
    {
        GameObject FowardTime();
        GameObject RewindTime();
    }
}