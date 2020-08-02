using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;

namespace Game.Controller.NPC
{
    public class QuestionsController : MonoBehaviour
    {
        public List<NpcController> npcQuestionOrder;

        private static QuestionsController singleton;
        private static int currentQuestion = 0;

        void Awake()
        {
            if (singleton != null)
                throw new Exception("Singleton already populated.");
            singleton = this;
        }

        void Start()
        {
            npcQuestionOrder[currentQuestion].LetPlayerStartQuestion();
        }

        static void NextQuestion()
        {
            if (currentQuestion + 1 > singleton.npcQuestionOrder.Count)
                ZerouOJogo();
        }

        static void ZerouOJogo()
        {

        }
    }
}