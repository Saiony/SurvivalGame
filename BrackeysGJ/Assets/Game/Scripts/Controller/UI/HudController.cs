using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

namespace BrackeysGJ.Assets.Game.Scripts.Controller.UI
{
    public class HudController : MonoBehaviour
    {
        [SerializeField]
        private PlayerStatsUIController PlayerStats;

        private void Start()
        {
            PlayerStats.Init();
        }
    }
}