using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using BepInEx.Bootstrap;
using HarmonyLib;
using Unity.Netcode;
using UnityEngine;
using static BrutalCompanyMinus.Minus.EventManager;
using BrutalCompanyMinus.Minus.Events;
using BrutalCompanyMinus.Minus;
using BepInEx;
using Mono.Cecil.Cil;

namespace ExternalModule
{
    [HarmonyPatch]
    internal class CompatibilityChecker
    {

        internal static bool
            BCMEPresent = false,
            BCMERPresent = false;

        [HarmonyPostfix]
        [HarmonyPatch(typeof(PreInitSceneScript), "Awake")]
        private static void OnGameLoad()
        {

            BCMEPresent = IsModPresent("UnloadedHangar.BrutalCompanyMinusExtra", "BCME found, patching!");

            BCMERPresent = IsModPresent("SoftDiamond.BrutalCompanyMinusExtraReborn", "BCMER found, patching!");
        }

        private static bool IsModPresent(string name, string logMessage, params MEvent[] associatedEvents)
        {
            bool isPresent = Chainloader.PluginInfos.ContainsKey(name);
            if (isPresent)
            {
                moddedEvents.AddRange(associatedEvents);
                Log.LogInfo($"{name} is present. {logMessage}");
            }
            return isPresent;
        }
    }
}
