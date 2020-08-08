using ShoppingCart.Bll.Enums;
using ShoppingCart.Bll.Models;
using ShoppingCart.Bll.Services;
using System.Collections.Generic;
using NUnit.Framework;
using System.Linq;

namespace ShoppingCart.Tests
{
    [TestFixture]
    public class ShoppingCartServiceTest
    {
        private static readonly Plu PluA = new Plu { PluType = PluType.A, Name = "Gummihansker", Price = 59.9m };
        private static readonly Plu PluB = new Plu { PluType = PluType.B, Name = "Stetoskop", Price = 399m };
        private static readonly Plu PluC = new Plu { PluType = PluType.C, Name = "Talkum", Price = 19.54m };
        
        [TestCase(0, 0)]
        [TestCase(1, 1 * 59.90)]
        [TestCase(2, 2 * 59.90)]
        [TestCase(3, 2 * 59.90)] // Third item is free.
        [TestCase(4, 3 * 59.90)]
        [TestCase(5, 4 * 59.90)]
        [TestCase(6, 4 * 59.90)] // Sixth item is free.
        [TestCase(150, 100 * 59.90)] // One third of the items are free.
        public void CalculateShoppingCard_Plu_A(int amount, decimal expected)
        {
            var cartItems = Enumerable.Repeat(PluA, amount);

            var totalPrice = new ShoppingCartService().CalculateShoppingCart(cartItems);

            Assert.AreEqual(expected, totalPrice);
        }

        [TestCase(0, 0)]
        [TestCase(1, 0 * 999 + 1 * 399)]
        [TestCase(2, 0 * 999 + 2 * 399)]
        [TestCase(3, 1 * 999 + 0 * 399)]
        [TestCase(4, 1 * 999 + 1 * 399)]
        [TestCase(5, 1 * 999 + 2 * 399)]
        [TestCase(6, 2 * 999 + 0 * 399)]
        [TestCase(11, 3 * 999 + 2 * 399)]
        public void CalculateShoppingCard_Plu_B(int amount, decimal expected)
        {
            var cartItems = Enumerable.Repeat(PluB, amount);

            var totalPrice = new ShoppingCartService().CalculateShoppingCart(cartItems);

            Assert.AreEqual(expected, totalPrice);
        }

        [TestCase(0, 0)]
        [TestCase(1, 1 * 19.54)]
        [TestCase(2, 2 * 19.54)]
        [TestCase(3, 3 * 19.54)]
        [TestCase(4, 4 * 19.54)]
        [TestCase(5, 5 * 19.54)]
        [TestCase(6, 6 * 19.54)]
        public void CalculateShoppingCard_Plu_C(int amount, decimal expected)
        {
            var cartItems = Enumerable.Repeat(PluC, amount);

            var totalPrice = new ShoppingCartService().CalculateShoppingCart(cartItems);

            Assert.AreEqual(expected, totalPrice);
        }

        [TestCase]
        public void CalculateShoppingCard_Plu_A_150pieces_B_11pieces_C_2kg()
        {
            var cartItems = new List<Plu>();

            cartItems.AddRange(Enumerable.Repeat(PluA, 150));
            cartItems.AddRange(Enumerable.Repeat(PluB, 11));
            cartItems.AddRange(Enumerable.Repeat(PluC, 2));

            var totalPrice = new ShoppingCartService().CalculateShoppingCart(cartItems);

            var expectedPriceForAItems = 100 * 59.90m;
            var expectedPriceForBItems = 3 * 999m + 2 * 399m;
            var expectedPriceForCItems = 2 * 19.54m;
            var expectedTotalPrice = expectedPriceForAItems + expectedPriceForBItems + expectedPriceForCItems;

            Assert.AreEqual(expectedTotalPrice, totalPrice);
        }
    }
}
