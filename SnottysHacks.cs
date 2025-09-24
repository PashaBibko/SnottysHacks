using BepInEx;

using UnityEngine.SceneManagement;
using UnityEngine;
using BepInEx.Logging;

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
                case 1: /* The sewer (gotta love magic numbers) */
                    GameObject hacks = new GameObject("hacks");
                    hacks.AddComponent<SewerHacks>();
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
        private void Start()
        {
            Logger.LogInfo("Created sewer hacks");
        }

        private void Update()
        {
            /* Opens the exit if the player presses F3 */
            if (Input.GetKeyDown(KeyCode.F3))
            {
                Debug.Log("Force opening sewer exit");
                SewerWin.singleton.isEscapeAvailable = true;
            }
        }
    }
}
