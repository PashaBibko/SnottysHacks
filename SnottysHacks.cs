using UnityEngine;
using BepInEx;

namespace SnottysHacks
{
    [BepInPlugin("com.pashabibko.snottyhacks", "snottyhacks", "1.0.0")]
    public class SnottysHacksPlugin : BaseUnityPlugin
    {
        private void Awake()
        {
            Logger.LogInfo("Snottys hacks have been loaded");
        }
    }
}
