using System.Collections.Generic;

namespace GildedRoseKata;

public class GildedRose
{
    IList<Item> Items;
    private const int LowestQuality = 0;
    private const int HighestQuality = 40;

    public GildedRose(IList<Item> Items)
    {
        this.Items = Items;
    }

    private const string AgedBrie = "Aged Brie";
    private const string Sulfuras = "Sulfuras, Hand of Ragnaros";
    private const string Conjured = "Conjured Mana Cake";
    private const string BackstagePass = "Backstage passes to a TAFKAL80ETC concert";

    public void UpdateQuality()
    {
        foreach (var item in Items)
        {
            // "Sulfuras" never has to be sold or decreases in Quality
            if (item.Name == Sulfuras)
                continue;

            item.SellIn--;

            switch (item.Name)
            {
                case AgedBrie:
                    IncreaseQuality(item);
                    break;

                case BackstagePass:
                    UpdateBackstagePass(item);
                    break;

                case Conjured:
                {
                    int qualityDecreaseAmount = 2;

                    if (item.SellIn < 0)
                        qualityDecreaseAmount = 4;

                    DecreaseQuality(item, qualityDecreaseAmount);
                    break;
                }

                default:
                {
                    int qualityDecreaseAmount = 1;

                    if (item.SellIn < 0)
                        qualityDecreaseAmount = 2;

                    DecreaseQuality(item, qualityDecreaseAmount);
                    break;
                }
            }

            EnsureQualityWithinLimits(item);
        }

    }

    private void UpdateBackstagePass(Item item)
    {
        if (item.SellIn < 0)
        {
            item.Quality = LowestQuality;
            return;
        }

        if (item.SellIn <= 2)
        {
            IncreaseQuality(item, 4);
        }
        else if (item.SellIn <= 7)
        {
            IncreaseQuality(item, 3);
        }
        else
        {
            IncreaseQuality(item);
        }
    }

    private void IncreaseQuality(Item item, int amount = 1)
    {
        item.Quality += amount;
    }

    private void DecreaseQuality(Item item, int amount = 1)
    {
        item.Quality -= amount;
    }

    private void EnsureQualityWithinLimits(Item item)
    {
        if (item.Quality < LowestQuality)
            item.Quality = LowestQuality;

        if (item.Quality > HighestQuality)
            item.Quality = HighestQuality;
    }
}