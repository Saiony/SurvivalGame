using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using BrackeysGJ.Assets.Game.Scripts.Manager.Interface;
using Game.Scripts.Service;
using Game.Scripts.Service.Interface;

namespace BrackeysGJ.Assets.Game.Scripts.Manager
{
    public class GameManager : MonoBehaviour
    {
        private ManagerProvider ManagerProvider;
        private ServiceProvider ServiceProvider;

        private void Awake() 
        {
            Debug.Log("Booting...");
            SetInitialScene();
            BootServices();
            BootManagers();

            FinishBoot();
        }

        private void BootManagers()
        {
            var managers = new List<IBaseManager>();
            managers.Add(new MessageManager());

            ManagerProvider = new ManagerProvider(managers);
        }

        private void BootServices()
        {
            var services = new List<IBaseService>();
            services.Add(new CraftingService());
            
            ServiceProvider = new ServiceProvider(services);
        }

        private void FinishBoot()
        {
            Debug.Log("Finished Booting");
            SceneManager.LoadScene("Game");
        }

        private void SetInitialScene()
        {
            var initialScene = AssetDatabase.LoadAssetAtPath<SceneAsset>("Assets/Game/Scenes/Boot.unity");
            EditorSceneManager.playModeStartScene = initialScene;
        }
    }
}