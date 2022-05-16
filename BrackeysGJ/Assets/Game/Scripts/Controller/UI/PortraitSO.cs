using UnityEngine;

namespace Game.Scripts.Controller.UI
{
    [CreateAssetMenu(fileName = "Portrait", menuName = "ScriptableObjects/Portrait", order = 1)]
    public class PortraitSO : ScriptableObject
    {
        public string Name;
        public Sprite Avatar;
    }
}
