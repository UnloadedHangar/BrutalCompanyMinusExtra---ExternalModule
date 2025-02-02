using ExternalModule.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.Netcode;
using UnityEngine;

namespace BrutalCompanyMinus.Minus.Events
{
    internal class TakeyGokuPlushBig : MEvent
    {
        public override string Name() => nameof(TakeyGokuPlushBig);

        public static TakeyGokuPlushBig Instance;

        public override void Initalize()
        {
            Instance = this;

            Weight = 1;
            Descriptions = new List<string>() { "TakeyGoku... but harmless", "Big plushie" };
            ColorHex = "#A0DB8E";
            Type = EventType.Neutral;

            scrapTransmutationEvent = new ScrapTransmutationEvent(
                new Scale(0.5f, 0.008f, 0.5f, 0.9f),
                new SpawnableItemWithRarity() { spawnableItem = ExternalModule.Helper.GetItemByName("Takey Goku", false), rarity = 100 }
            );


            EventsToRemove = new List<string>() { "RealityShift", "Pickles", "TakeyPlush", "SussyPaintings", nameof(TakeyGokuPlush), "Dustpans", "Clock" };

            ScaleList.Add(ScaleType.ScrapAmount, new Scale(1.0f, 0.005f, 1.0f, 1.5f));
        }

        /// <summary>
        /// Fail safe that adds the event only if the method returns true
        /// </summary>
        /// <returns>returns a bool value</returns>
        public override bool AddEventIfOnly()
        {
           // if (!Compatibility.takeyPlushPresent) return false;
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

            Manager.scrapAmountMultiplier *= Getf(ScaleType.ScrapAmount);
            scrapTransmutationEvent.Execute();
        }
    }
}
