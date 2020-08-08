using ShoppingCart.Bll.Enums;
using ShoppingCart.Bll.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ShoppingCart.Bll.Services
{
    public static class PriceRules
    {
        public static Func<IEnumerable<Plu>, decimal> GetPriceRule(PluType pluType)
        {
            switch (pluType)
            {
                case PluType.A: return CalculatePriceForPluA;
                case PluType.B: return CalculatePriceForPluB;
                case PluType.C: return CalculatePriceForPluC;
                default: return DefaultPriceRule;
            }
        }

        private static decimal CalculatePriceForPluA(IEnumerable<Plu> pluAItems)
        {
            var count = pluAItems.Count();

            var itemsForFree = count / 3;

            return pluAItems.First(x => x.PluType == PluType.A).Price * (count - itemsForFree);
        }

        private static decimal CalculatePriceForPluB(IEnumerable<Plu> pluBItems)
        {
            var pluBCount = pluBItems.Count();

            var numberOfTriplets = pluBCount / 3;
            var numberOfSingles = pluBCount % 3;

            var priceForTriplets = 999 * numberOfTriplets;
            var priceForSingles = pluBItems.First(x => x.PluType == PluType.B).Price * numberOfSingles;

            return priceForTriplets + priceForSingles;
        }

        private static decimal CalculatePriceForPluC(IEnumerable<Plu> pluCItems)
        {
            // Uklart hvordan oppgaven mener vektvarer er modellert.
            // Det antas at plu.Price er enhetspris * vekt, altså ferdig utregnet pris, før avrundning, for mengden talkum man skal ha.

            var pluCPrice = 0m;

            foreach (var pluC in pluCItems)
            {
                pluCPrice += Math.Round(pluC.Price,
                                        decimals: 2,
                                        mode: MidpointRounding.AwayFromZero);
            }

            return pluCPrice;
        }

        private static decimal DefaultPriceRule(IEnumerable<Plu> pluItems) => pluItems.Sum(x => x.Price);
    }
}
