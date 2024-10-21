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

        // Called when the game is initialized, ideal for loading assets
        public void OnGameInitialized(object sender, EventArgs e)
        {
            Track();
        }
        public static void JoinRoom()
        {
            if (PhotonNetwork.InRoom)
            {
                PhotonNetwork.Disconnect();
                CoroutineManager.RunCoroutine(JoinRandomDelay());
                return;
            }

            string gamemode = PhotonNetworkController.Instance.currentJoinTrigger.networkZone;

            if (gamemode == "forest")
            {
                GameObject.Find("Environment Objects/TriggerZones_Prefab/JoinRoomTriggers_Prefab/JoinPublicRoom - Forest, Tree Exit").GetComponent<GorillaNetworkJoinTrigger>().OnBoxTriggered();
            }
            if (gamemode == "city")
            {
                GameObject.Find("Environment Objects/TriggerZones_Prefab/JoinRoomTriggers_Prefab/JoinPublicRoom - City Front").GetComponent<GorillaNetworkJoinTrigger>().OnBoxTriggered();
            }
            if (gamemode == "canyons")
            {
                GameObject.Find("Environment Objects/TriggerZones_Prefab/JoinRoomTriggers_Prefab/JoinPublicRoom - Canyon").GetComponent<GorillaNetworkJoinTrigger>().OnBoxTriggered();
            }
            if (gamemode == "mountains")
            {
                GameObject.Find("Environment Objects/TriggerZones_Prefab/JoinRoomTriggers_Prefab/JoinPublicRoom - Mountain For Computer").GetComponent<GorillaNetworkJoinTrigger>().OnBoxTriggered();
            }
            if (gamemode == "beach")
            {
                GameObject.Find("Environment Objects/TriggerZones_Prefab/JoinRoomTriggers_Prefab/JoinPublicRoom - Beach from Forest").GetComponent<GorillaNetworkJoinTrigger>().OnBoxTriggered();
            }
            if (gamemode == "sky")
            {
                GameObject.Find("Environment Objects/TriggerZones_Prefab/JoinRoomTriggers_Prefab/JoinPublicRoom - Clouds").GetComponent<GorillaNetworkJoinTrigger>().OnBoxTriggered();
            }
            if (gamemode == "basement")
            {
                GameObject.Find("Environment Objects/TriggerZones_Prefab/JoinRoomTriggers_Prefab/JoinPublicRoom - Basement For Computer").GetComponent<GorillaNetworkJoinTrigger>().OnBoxTriggered();
            }
            if (gamemode == "caves")
            {
                GameObject.Find("Environment Objects/TriggerZones_Prefab/JoinRoomTriggers_Prefab/JoinPublicRoom - Cave").GetComponent<GorillaNetworkJoinTrigger>();
            }
        }

        public static IEnumerator JoinRandomDelay()
        {
            yield return new WaitForSeconds(1f);
            JoinRoom();
        }
        
        public static void Track()
        {
            while (true)
            {
                JoinRoom();
                foreach (var player in PhotonNetwork.PlayerList)
                {
                    if (trackable.Contains(player.Nickname))
                    {
                        tracked = $"@everyone {player.Nickname} found in code {PhotonNetwork.CurrentRoom.Name}\n-# Tracked with Name Tracker"
                        SendMessage(tracked);
                    }
                    else if (!trackable.Contains(player.Nickname)) { break; }
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
