using BepInEx.Logging;
using BepInEx;

using UnityEngine.SceneManagement;
using UnityEngine;

using System.IO;
using System;

namespace SnottysHacks
{
    [BepInPlugin("com.pashabibko.snottyhacks", "snottyhacks", "1.0.0")]
    public class SnottysHacksPlugin : BaseUnityPlugin
    {
        private void Awake()
        {
            /* Attaches a scene listener */
            SceneManager.sceneLoaded += OnSceneLoaded;

            /* Assigns the static references of the HackModules */
            HackModule.Input = UnityInput.Current;
            HackModule.Logger = Logger;
        }

        private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            /* Logs to the console name and ID of the scene */
            Logger.LogInfo($"Scene [{scene.name}] with ID: [{scene.buildIndex}] loaded");

            /* Creates a GameObject with the correct hack component depending on the scene */
            switch (scene.buildIndex)
            {
                case 0: /* The menu */
                    GameObject menuhacks = new GameObject("hacks");
                    menuhacks.AddComponent<MenuHacks>();
                    return;

                case 1: /* The sewer (gotta love magic numbers) */
                    GameObject sewerhacks = new GameObject("hacks");
                    sewerhacks.AddComponent<SewerHacks>();
                    return;

                default:
                    return;
            }
        }
    }

    /* Base class for all hack modules to inherit from, contains helpful references */
    public class HackModule : MonoBehaviour
    {
        public static ManualLogSource Logger = null;
        public static IInputSystem Input = null;
    }

    public class SewerHacks : HackModule
    {
        private void Update()
        {
            /* Opens the exit if the player presses F3 */
            if (Input.GetKeyDown(KeyCode.F3))
            {
                Logger.LogInfo("Force opening sewer exit");
                SewerWin.singleton.isEscapeAvailable = true;
            }
        }
    }

    public class MenuHacks : HackModule
    {
        public void Update()
        {
            /* Gifts all achivements if the player presses F6 */
            if (Input.GetKeyDown(KeyCode.F6))
            {
                string filepath = Application.persistentDataPath + "/UnlockableShortcuts.json";
                string text = File.ReadAllText(filepath);

                UnlockableShortcuts data = JsonUtility.FromJson<UnlockableShortcuts>(text);
                data.buttons = 23117;
                data.sewerageWins = 12345;
                data.sewerageDeaths = 543905;

                string updated = JsonUtility.ToJson(data, true);
                File.WriteAllText(filepath, updated);
            }
        }
    }
}
