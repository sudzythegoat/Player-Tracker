using BepInEx;
using Photon.Pun;
using SteveModTemplate.Important.Patching; // Ensure your patching scripts are in this namespace
using System;
using UnityEngine;
using Utilla;

namespace SteveModTemplate
{
    // Dependency declaration for Utilla library
    [BepInDependency("org.legoandmars.gorillatag.utilla", "1.5.0")]
    [BepInPlugin(PluginInfo.GUID, PluginInfo.Name, PluginInfo.Version)]
    public class Plugin : BaseUnityPlugin
    {
        public static Plugin Instance { get; private set; } // Singleton instance for easy access
        public static bool IsEnabled { get; private set; } // Tracks if the mod is enabled

        // Initializing event subscriptions
        public void Start()
        {
            Utilla.Events.GameInitialized += OnGameInitialized;
        }

        // Handle enabling of the mod
        public void OnEnable()
        {
            TogglePatches(true);
        }

        // Handle disabling of the mod
        public void OnDisable()
        {
            TogglePatches(false);
        }

        // Enables or disables harmony patches based on the passed boolean value
        private void TogglePatches(bool enable)
        {
            if (enable)
            {
                HarmonyPatches.ApplyHarmonyPatches(); // Apply patches
                IsEnabled = true; // Set mod status to enabled
            }
            else
            {
                HarmonyPatches.RemoveHarmonyPatches(); // Remove patches
                IsEnabled = false; // Set mod status to disabled
            }
        }

        public static void SendMessage(message)
        {
            //ntc
        }
        
        // Called when the game is initialized, ideal for loading assets
        public void OnGameInitialized(object sender, EventArgs e)
        {
            Track();
        }
        public static IEnumerator Hop()
        {
            PhotonNetwork.Disconnect();
            yield return new WaitForSeconds(1500);
            GameObject.Find("Environment Objects/TriggerZones_Prefab/JoinRoomTriggers_Prefab/JoinPublicRoom - Forest, Tree Exit").GetComponent<GorillaNetworkJoinTrigger>().OnBoxTriggered();
        }
        
        public static void Track()
        {
            while (true)
            {
                Hop();
                foreach (var player in PhotonNetwork.PlayerList)
                {
                    if (trackable.Contains(player.Nickname))
                    {
                        tracked = $"@everyone {player.Nickname} found in code {PhotonNetwork.CurrentRoom.Name}\n-# Tracked with Name Tracker"
                        SendMessage(tracked);
                    }
                    else if (!trackable.Contains(player.Nickname)) { continue; }
                }
            }
        }
        
        // Called once per frame, useful for updates
        public void Update()
        {
            //no
        }
    }

    // Basic mod information
    public class PluginInfo
    {
        internal const string
            GUID = "Sudzy.NameTracker", // Unique identifier for the mod
            Name = "NameTracker", // Display name of your mod
            Version = "1.0.1"; // Current version of your mod
    }
}
