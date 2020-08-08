using ShoppingCart.Bll.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ShoppingCart.Bll.Services
{
    public class ShoppingCartService : IShoppingCartService
    {
        public decimal CalculateShoppingCart(IEnumerable<Plu> cartItems)
        {
            // Regler for prisberegning i eksemplene som er gitt er kun varetype for varetype - ikke på tvers av ulike varetyper.
            // Gitt denne forutsetningen er det nok å gruppere på varetype og kjøre separat beregning for hver varetype.

            var totalPrice = 0m;

            var pluGroups = cartItems.GroupBy(x => x.PluType);

            foreach (var pluGroup in pluGroups)
            {
                var priceRule = PriceRules.GetPriceRule(pluGroup.Key);
                totalPrice += priceRule(pluGroup);
            }

            return Math.Round(totalPrice,
                              decimals: 2,
                              mode: MidpointRounding.AwayFromZero);
        }
    }
}
