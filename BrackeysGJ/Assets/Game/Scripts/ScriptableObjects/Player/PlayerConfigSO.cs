using UnityEngine;

namespace BrackeysGJ.Assets.Game.Scripts.ScriptableObjects.Player
{
    [CreateAssetMenu(fileName = "PlayerConfig", menuName = "ScriptableObjects/Player/PlayerConfig", order = 1)]
    public class PlayerConfigSO : ScriptableObject
    {
        public FoodLevelSO FoodLevel;
    }
}