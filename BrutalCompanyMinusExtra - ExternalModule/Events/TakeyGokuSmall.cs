using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.Netcode;
using UnityEngine;
using BrutalCompanyMinus.Minus;
using BrutalCompanyMinus.Minus.Events;

namespace ExternalModule.Events
{
    public class TakeyGokuPlush : MEvent
    {
        public override string Name() => nameof(TakeyGokuPlush);

        public static TakeyGokuPlush Instance;

        public override void Initalize()
        {
            Instance = this;

            Weight = 1;
            Descriptions = new List<string>() { "TakeyGoku... but small", "TakeyGoku plushie" };
            ColorHex = "#008000";
            Type = EventType.Good;

            scrapTransmutationEvent = new ScrapTransmutationEvent(
                new Scale(0.5f, 0.008f, 0.5f, 0.9f),
                new SpawnableItemWithRarity() { spawnableItem = Helper.GetItemByName("Smol Takey Goku", false), rarity = 100 }
            );

            EventsToRemove = new List<string>() { "RealityShift", "Pickles", nameof(TakeyPlush), "SussyPaintings", nameof(TakeyGokuPlushBig), "Dustpans", "Clock" };

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
