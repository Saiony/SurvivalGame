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
        private static int currentQuestion = 0;

        void Awake()
        {
            if (singleton != null)
                throw new Exception("Singleton already populated.");
            singleton = this;
        }

        void Start()
        {
            npcQuestsOrder[currentQuestion].StartQuest();
        }

        static void NextQuestion()
        {
            if (currentQuestion + 1 > singleton.npcQuestsOrder.Count)
                ZerouOJogo();
        }

        static void ZerouOJogo()
        {

        }
    }
}