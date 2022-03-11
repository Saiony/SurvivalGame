using UnityEngine;

namespace BrackeysGJ.Assets.Game.Scripts.ScriptableObjects.Player
{
    [CreateAssetMenu(fileName = "FoodLevel", menuName = "ScriptableObjects/Player/FoodLevel", order = 1)]
    public class FoodLevelSO : ScriptableObject
    {
        public int InitialFoodLevel;
        public float FoodLevelTick;
        public int FoodLevelPerTick;

        [Header("Milestones")]

        [Header("Satisfied Milestone")]
        [Range(0, 1)]
        public float SatisfiedMilestone;
        public int HpRestoredPerTick;

        [Header("Normal Milestone")]
        [Range(0, 1)]
        public float NormalMilestone;

        [Header("Hungry Milestone")]
        [Range(0, 1)]
        public float HungryMilestone;
        public int StaminaDebuff;
        public int MovSpeedDebuff;


        [Header("Starving Milestone")]
        [Range(0, 1)]
        public float StarvingMilestone;
        public int HpDecreasedPerTick;
    }
}