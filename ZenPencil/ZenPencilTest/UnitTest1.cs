using System;
using System.Runtime.CompilerServices;
using Xunit;

namespace ZenPencilTest
{
    class Item
    {
        private decimal OriginalPrice { get; set; }
        private decimal CurrentPrice { get; set; }

        public Item(decimal originalPrice)
        {
            CurrentPrice = OriginalPrice = originalPrice;
        }

        public void NewPrice(decimal price)
        {
            CurrentPrice = price;
        }
        
        public bool IsRedPencil()
        {
            var upper_bound = CurrentPrice + 0.30m * OriginalPrice;
            var lower_bound = CurrentPrice + 0.05m * OriginalPrice;
            
            return OriginalPrice <= upper_bound && OriginalPrice >= lower_bound;
        }
    }


    public class RedPencilSaleTest
    {
        private const decimal OriginalLaptopPrice = 1200m;


        readonly Item laptop = new Item(originalPrice: OriginalLaptopPrice);

        [Fact]
        public void IsRedPencil_NotRedPencil_When_NotOnSale()
        {
            Assert.False(laptop.IsRedPencil());
        }

        [Fact]
        public void IsRedPencil_RedPencil_When_ReducedByFivePercent()
        {
            laptop.NewPrice(OriginalLaptopPrice * 0.95m);

            Assert.True(laptop.IsRedPencil());
        }

        [Fact]
        public void IsRedPencil_NotRedPencil_When_ReducedLessThanFivePercent()
        {
            laptop.NewPrice(OriginalLaptopPrice * 0.96m);

            Assert.False(laptop.IsRedPencil());
        }

        [Fact]
        public void IsRedPencil_RedPencil_When_ReducedByLessThanThirtyPercent()
        {
            laptop.NewPrice(OriginalLaptopPrice * 0.70m);

            Assert.True(laptop.IsRedPencil());
        }


        [Fact]
        public void IsRedPencil_NotRedPencil_When_ReducedByMoreThanThirtyPercent()
        {
            laptop.NewPrice(OriginalLaptopPrice * 0.69m);

            Assert.False(laptop.IsRedPencil());
        }

    }
}