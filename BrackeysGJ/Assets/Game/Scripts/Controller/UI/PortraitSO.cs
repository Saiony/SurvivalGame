using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.ScriptableObjects
{
    [CreateAssetMenu(fileName = "Portrait", menuName = "ScriptableObjects/Portrait", order = 1)]
    public class PortraitSO : ScriptableObject
    {
        public string Name;
        public Sprite Avatar;
    }
}
