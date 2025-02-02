using BepInEx;
using HarmonyLib;
using UnityEngine;
using UnityEngine.Rendering.HighDefinition;
using GameNetcodeStuff;
using BepInEx.Configuration;
using System;
using System.Collections.Generic;
using TMPro;
//using HDLethalCompany.Patch;
//using HDLethalCompany.Tools;
using System.Reflection;
using System.IO;
using UnityEngine.Rendering;
//using LethalLevelLoader;
//using NonShippingAuthorisation.Patch;
using Unity.Netcode;
using static BrutalCompanyMinus.Minus.EventManager;
using BrutalCompanyMinus.Minus;
using BrutalCompanyMinus.Minus.Events;
using ExternalModule.Events;
using ExternalModule.Patches;
using UnityEngine.PlayerLoop;
using BrutalCompanyMinus;


namespace ExternalModule
{
    [HarmonyPatch]
    [BepInPlugin(GUID, NAME, VERSION)]
    [BepInDependency("UnloadedHangar.BrutalCompanyMinusExtra",BepInDependency.DependencyFlags.SoftDependency)]
    [BepInDependency("SoftDiamond.BrutalCompanyMinusExtraReborn", BepInDependency.DependencyFlags.SoftDependency)]
    internal class ExternalModule : BaseUnityPlugin
    {   
        private const string GUID = "BCME-ExternalModule";
        private const string NAME = "BCME-ExternalModule-UnloadedHangar";
        private const string VERSION = "0.2.6";
        private static readonly Harmony harmony = new Harmony(GUID);

        void Awake()
        {
            // Logger
            Log.Initalize(Logger);
            Logger.LogInfo(GUID + " loaded");
            try
            {
                if (CompatibilityChecker.BCMEPresent && CompatibilityChecker.BCMERPresent)
                {
                    Log.LogFatal("Fatal collision dettected!");
                }
            }
            catch (Exception e)
            {
                Log.LogFatal($"Raw exception:{e}");
                // harmony.UnpatchSelf();
            }
            try
            {
                Patches.Manager.EventAddition();
             //   Patches.Manager.EventAdditionGeneric(new TakeyPlush());
            }
            catch(Exception e)
            {
                Log.LogFatal( new DllNotFoundException($"No supported distribution of BrutalCompanyMinusExtra/Reborn was found, {GUID} can not continue... Aborting!"));
                Log.LogFatal($"Raw exception:{e}");
               // harmony.UnpatchSelf();
            }
            // finally { harmony.UnpatchSelf(); }

            //  harmony.PatchAll(typeof(Patches.Manager));
            harmony.PatchAll(typeof(CompatibilityChecker));
            

            
        }
    }
}   

namespace ExternalModule.Patches
{
    /// <summary>
    /// Class responsible for registering the events
    /// </summary>
    public class Manager 
    { 
     /// <summary>
     /// Event registry
     /// </summary>
        public static void EventAddition()
        {
            EventManager.moddedEvents.Add(new TakeyGokuPlush()); //<--- Adds event to the registry list
            Log.LogWarning("Addidng TakeyGokuPlush event");
            EventManager.moddedEvents.Add(new ZombiesPlush()); //<--- Adds event to the registry list
            Log.LogWarning("Addidng ZombiesPlush event");
            EventManager.moddedEvents.Add(new TakeyGokuPlushBig()); //<--- Adds event to the registry list
            Log.LogWarning("Addidng TakeyGokuPlushBig event");
            EventManager.moddedEvents.Add(new TakeyPlush()); //<--- Adds event to the registry list
            Log.LogWarning("Addidng TakeyPlush event");

        }

        //Do NOT use
     /*   /// <summary>
        /// Generic method for adding events
        /// </summary>
        /// <param name="mEvent"></param>
        public static void EventAdditionGeneric(MEvent mEvent)
        {
            EventManager.moddedEvents.Add(mEvent);
            Log.LogWarning($"Addidng {nameof(mEvent)} event");

        }*/

    }
}

