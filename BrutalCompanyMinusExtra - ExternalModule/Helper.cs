using System;
using System.Collections.Generic;
using System.Text;


namespace ExternalModule
{
    internal class Helper
    {
        /// <summary>
        /// Method to spawn items
        /// </summary>
        /// <param name="itemName">Name of the item</param>
        /// <param name="matchCase">Is match case sensitive?</param>
        /// <returns>Returns specified item</returns>
        public static Item GetItemByName(string itemName, bool matchCase = true)
        {
            System.StringComparison comparisonType = matchCase ? System.StringComparison.CurrentCulture : System.StringComparison.OrdinalIgnoreCase;

            foreach (var item in StartOfRound.Instance.allItemsList.itemsList)
            {
                if (item.itemName.Equals(itemName, comparisonType))
                {
                    return item;
                }
            }

            return null;
        }
    }

}
