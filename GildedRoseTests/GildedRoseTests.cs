using GildedRoseKata;
using System.Collections.Generic;
using Xunit;

namespace GildedRoseTests;

public class GildedRoseTests
{
    #region Normal Item Behaviour
    [Fact]
    public void UpdateQuality_Should_Decrease_SellIn_And_Quality_For_Normal_Item()
    {
        // Arrange
        var items = new List<Item> { new Item { Name = "+5 Dexterity Vest", SellIn = 10, Quality = 20 } };

        var gildedRose = new GildedRose(items);

        // Act
        gildedRose.UpdateQuality();

        // Assert
        Assert.Equal(9, items[0].SellIn);
        Assert.Equal(19, items[0].Quality);
    }

    [Fact]
    public void UpdateQuality_Should_Degrade_Quality_Twice_As_Fast_After_SellIn_Date()
    {
        // Arrange
        var items = new List<Item> { new Item { Name = "+5 Dexterity Vest", SellIn = 0, Quality = 20 } };

        var gildedRose = new GildedRose(items);

        // Act
        gildedRose.UpdateQuality();

        // Assert
        Assert.Equal(-1, items[0].SellIn);
        Assert.Equal(18, items[0].Quality);
    }
    #endregion

    #region Quality Boundary Rules
    [Theory]
    [InlineData(40)]
    [InlineData(0)]
    [InlineData(41)]
    [InlineData(-1)]
    public void UpdateQuality_Should_Keep_Quality_Between_0_And_40(int startingQuality)
    {
        // Arrange
        var items = new List<Item> { new Item { Name = "+5 Dexterity Vest", SellIn = 5, Quality = startingQuality } };

        var gildedRose = new GildedRose(items);

        // Act
        gildedRose.UpdateQuality();

        // Assert
        Assert.InRange(items[0].Quality, 0, 40);
    }
    #endregion

    #region Sulfuras Rules
    [Fact]
    public void UpdateQuality_Sulfuras_Does_Not_Change_Quality_Or_SellIn()
    {
        // Arrange
        var items = new List<Item> { new Item { Name = "Sulfuras, Hand of Ragnaros", SellIn = 0, Quality = 40 } };

        var gildedRose = new GildedRose(items);

        // Act
        gildedRose.UpdateQuality();

        // Assert
        Assert.Equal(0, items[0].SellIn);
        Assert.Equal(40, items[0].Quality);
    }
    #endregion

    #region Aged Brie Rules
    [Fact]
    public void UpdateQuality_Should_Increase_Quality_For_AgedBrie()
    {
        // Arrange
        var items = new List<Item> { new Item { Name = "Aged Brie", SellIn = 10, Quality = 20 } };

        var gildedRose = new GildedRose(items);

        // Act
        gildedRose.UpdateQuality();

        // Assert
        Assert.Equal(9, items[0].SellIn);
        Assert.Equal(21, items[0].Quality);
    }

    [Fact]
    public void UpdateQuality_AgedBrie_Should_Not_Exceed_Maximum_Quality()
    {
        // Arrange
        var items = new List<Item> { new Item { Name = "Aged Brie", SellIn = 10, Quality = 40 } };

        var gildedRose = new GildedRose(items);

        // Act
        gildedRose.UpdateQuality();

        // Assert
        Assert.Equal(9, items[0].SellIn);
        Assert.Equal(40, items[0].Quality);
    }
    #endregion

    #region Backstage Pass Rules
    [Theory]
    [InlineData(10, 20, 21)] // +1 normal increase
    [InlineData(7, 20, 23)]  // +3 when 7 days or less
    [InlineData(2, 20, 24)]  // +4 when 2 days or less
    [InlineData(0, 20, 0)]   // drops to 0 after concert
    public void UpdateQuality_Should_Update_BackstagePass_Quality_Correctly(int sellIn, int startingQuality, int expectedQuality)
    {
        // Arrange
        var items = new List<Item> { new Item { Name = "Backstage passes to a TAFKAL80ETC concert", SellIn = sellIn, Quality = startingQuality } };

        var gildedRose = new GildedRose(items);

        // Act
        gildedRose.UpdateQuality();

        // Assert
        Assert.Equal(expectedQuality, items[0].Quality);
    }

    [Fact]
    public void UpdateQuality_Should_Set_BackstagePass_Quality_To_Zero_After_Concert()
    {
        // Arrange
        var items = new List<Item> { new Item { Name = "Backstage passes to a TAFKAL80ETC concert", SellIn = 0, Quality = 20 } };

        var gildedRose = new GildedRose(items);

        // Act
        gildedRose.UpdateQuality();

        // Assert
        Assert.Equal(-1, items[0].SellIn);
        Assert.Equal(0, items[0].Quality);
    }

    [Fact]
    public void UpdateQuality_Should_Keep_BackstagePass_Quality_Within_Limits_Across_Multiple_Days()
    {
        // Arrange
        var items = new List<Item> { new Item { Name = "Backstage passes to a TAFKAL80ETC concert", SellIn = 10, Quality = 49 } };

        var gildedRose = new GildedRose(items);

        // Act - Day 1
        gildedRose.UpdateQuality();

        // Assert - Quality capped at 40
        Assert.Equal(9, items[0].SellIn);
        Assert.Equal(40, items[0].Quality);

        // Act - Day 2
        gildedRose.UpdateQuality();

        // Assert
        Assert.Equal(8, items[0].SellIn);
        Assert.Equal(40, items[0].Quality);

        // Act - Day 3
        gildedRose.UpdateQuality();

        // Assert
        Assert.Equal(7, items[0].SellIn);
        Assert.Equal(40, items[0].Quality);
    }
    #endregion

    #region Conjured Item Rules
    [Fact]
    public void UpdateQuality_Should_Decrease_Conjured_Quality_Twice_As_Fast_As_Normal_Items()
    {
        // Arrange
        var items = new List<Item> { new Item { Name = "Conjured Mana Cake", SellIn = 10, Quality = 20 } };

        var gildedRose = new GildedRose(items);

        // Act
        gildedRose.UpdateQuality();

        // Assert
        Assert.Equal(9, items[0].SellIn);
        Assert.Equal(18, items[0].Quality);
    }

    [Fact]
    public void UpdateQuality_Should_Decrease_Expired_Conjured_Quality_By_Four()
    {
        // Arrange
        var items = new List<Item> { new Item { Name = "Conjured Mana Cake", SellIn = 0, Quality = 20 } };

        var gildedRose = new GildedRose(items);

        // Act
        gildedRose.UpdateQuality();

        // Assert
        Assert.Equal(-1, items[0].SellIn);
        Assert.Equal(16, items[0].Quality);
    }
    #endregion
}