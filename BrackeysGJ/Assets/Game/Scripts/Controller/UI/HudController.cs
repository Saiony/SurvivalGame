using System;
using Game.Scripts.Controller.Crafting.Construction;
using UnityEngine;

namespace Game.Scripts.Controller.UI
{
    public class HudController : MonoBehaviour
    {
        [SerializeField]
        private PlayerStatsUIController _playerStats;

        [SerializeField] 
        private ConstructionWindowController _constructionWindow;

        public static HudController Instance;

        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            _playerStats.Init();
            _constructionWindow.Init();
        }

        public void ShowConstructionWindow()
        {
            _constructionWindow.Show();
        }

        public void HideConstructionWindow()
        {
            _constructionWindow.Hide();
        }
    }
}