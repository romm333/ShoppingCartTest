using ShoppingCart.Bll.Enums;
using ShoppingCart.Bll.Models;
using System.Collections.Generic;
using Xunit;

namespace ShoppingCart.Tests
{
    public class ShoppingCartServiceTest
    {
        private Dictionary<EPluType, PLU> NamesAndPrices { get; }

        public ShoppingCartServiceTest()
        {
            NamesAndPrices.Add(EPluType.A, new PLU { PluType = EPluType.A, Name = "Gummihansker", Price = 59.9M }); ;
            NamesAndPrices.Add(EPluType.A, new PLU { PluType = EPluType.B, Name = "Stetoskop", Price = 399M }); ;
            NamesAndPrices.Add(EPluType.A, new PLU { PluType = EPluType.A, Name = "Talkum", Price = 19.54M }); ;
        }

        [Fact]
        private void CalculateShoppingCard_Plu_A_150pieces()
        {
            // Shopping Cart contains only PLU A
        }

        [Fact]
        private void CalculateShoppingCard_Plu_B_11pieces()
        {
            // Shopping Cart contains only PLU B
        }

        [Fact]
        private void CalculateShoppingCard_Plu_C_2kg()
        {
            // Shopping Cart contains only PLU C
        }

        [Fact]
        private void CalculateShoppingCard_Plu_A_150pieces_B_11pieces_C_2kg()
        {
            // Shopping Cart contains altogether:  PLU A, B, C 
        }


        /// <summary>
        /// returns test data for ShoppingCardService
        /// </summary>
        /// <param name="pluType">EPlutType</param>
        /// <param name="amount">int, > 0</param>
        /// <returns></returns>
        private IEnumerable<PLU> SetTestProducts(EPluType pluType, int amount)
        {
            var _shoppingCardProducts = new List<PLU>();
            for (int i = 0; i < amount; i++)
            {
                _shoppingCardProducts.Add(NamesAndPrices[pluType]);
            }
            return _shoppingCardProducts;
        }
    }
}
