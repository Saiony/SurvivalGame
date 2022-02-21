using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using BrackeysGJ.Assets.Game.Scripts.Manager.Interface;

namespace BrackeysGJ.Assets.Game.Scripts.Manager
{
    public class GameManager : MonoBehaviour
    {
        private ManagerProvider ManagerProvider;

        private void Awake() 
        {
            Debug.Log("Booting...");
            SetInitialScene();
            BootManagers();

            FinishBoot();
        }

        private void FinishBoot()
        {
            Debug.Log("Finished Booting");
            SceneManager.LoadScene("Game");
        }

        private void BootManagers()
        {
            var managers = new List<IBaseManager>();
            managers.Add(new MessageManager());

            ManagerProvider = new ManagerProvider();
            ManagerProvider.Init(managers);
        }

        private void SetInitialScene()
        {
            var initialScene = AssetDatabase.LoadAssetAtPath<SceneAsset>("Assets/Game/Scenes/Boot.unity");
            EditorSceneManager.playModeStartScene = initialScene;
        }
    }
}