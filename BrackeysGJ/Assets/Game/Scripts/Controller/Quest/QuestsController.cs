using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;

namespace Game.Controller.NPC
{
    public class QuestsController : MonoBehaviour
    {
        public List<NpcController> npcQuestsOrder;

        private static QuestsController singleton;
        private static int currentQuest = 0;

        void Awake()
        {
            if (singleton != null)
                throw new Exception("Singleton already populated.");
            singleton = this;
        }

        void Start()
        {
            npcQuestsOrder[currentQuest].StartQuest();
        }

        static void NextQuest()
        {
            if (currentQuest + 1 > singleton.npcQuestsOrder.Count)
                ZerouOJogo();
        }

        static void ZerouOJogo()
        {

        }
    }
}