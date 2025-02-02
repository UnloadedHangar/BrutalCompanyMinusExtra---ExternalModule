using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.Netcode;
using UnityEngine;

namespace BrutalCompanyMinus.Minus.Events
{
    public class TakeyPlush : MEvent
    {
        public override string Name() => nameof(TakeyPlush);

        public static TakeyPlush Instance;

        public override void Initalize()
        {
            Instance = this;

            Weight = 1;
            Descriptions = new List<string>() { "TakeySit", "I know this guy...", "Streamer?" };
            ColorHex = "#008000";
            Type = EventType.Good;

            scrapTransmutationEvent = new ScrapTransmutationEvent(
                new Scale(0.5f, 0.008f, 0.5f, 0.9f),
                new SpawnableItemWithRarity() { spawnableItem = ExternalModule.Helper.GetItemByName("Smol Takey", false), rarity = 100 }
            );

            EventsToRemove = new List<string>() { "RealityShift", "Pickles" };

            ScaleList.Add(ScaleType.ScrapAmount, new Scale(1.0f, 0.005f, 1.0f, 1.5f));
        }

        /// <summary>
        /// Fail safe that adds the event only if the method returns true
        /// </summary>
        /// <returns>returns a bool value</returns>
        public override bool AddEventIfOnly()
        {
           // if (!Compatibility.takeyPlushPresent & streamerEventsEnabled) return false;
            if (!Manager.transmuteScrap)
            {
                Manager.transmuteScrap = true;
                return true;
            }
            return false;
        }

        /// <summary>
        /// Executed when ship lever is pulled
        /// </summary>
        public override void Execute()
        {
            //  if (!Compatibility.takeyPlushPresent) return;
            Manager.scrapAmountMultiplier *= Getf(ScaleType.ScrapAmount);
            scrapTransmutationEvent.Execute();
        }
    }
}