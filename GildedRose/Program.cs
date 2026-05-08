using System;
using System.Collections.Generic;

namespace GildedRoseKata;

public class Program
{
    public static void Main(string[] args)
    {
        try
        {
            Console.WriteLine("Gilded Rose Inventory System");

            IList<Item> items = new List<Item>
            {
                new Item {Name = "+5 Dexterity Vest", SellIn = 10, Quality = 20},
                new Item {Name = "Aged Brie", SellIn = 2, Quality = 0},
                new Item {Name = "Elixir of the Mongoose", SellIn = 5, Quality = 7},
                new Item {Name = "Sulfuras, Hand of Ragnaros", SellIn = 0, Quality = 80},
                new Item {Name = "Sulfuras, Hand of Ragnaros", SellIn = -1, Quality = 80},
                new Item {Name = "Backstage passes to a TAFKAL80ETC concert",SellIn = 15,Quality = 20},
                new Item {Name = "Backstage passes to a TAFKAL80ETC concert",SellIn = 10,Quality = 49},
                new Item {Name = "Backstage passes to a TAFKAL80ETC concert",SellIn = 5,Quality = 49},
                new Item {Name = "Conjured Mana Cake", SellIn = 3, Quality = 6}
            };

            var app = new GildedRose(items);

            int days = GetNumberOfDays();
            Console.WriteLine("");

            for (var day = 0; day < days; day++)
            {
                Console.WriteLine("-------- day " + day + " --------");
                Console.WriteLine($"{nameof(Item.Name)} | {nameof(Item.SellIn)} | {nameof(Item.Quality)}");
                Console.WriteLine("-----------------------");

                for (var item = 0; item < items.Count; item++)
                {
                    Console.WriteLine($"{items[item].Name}, {items[item].SellIn}, {items[item].Quality}");
                }

                Console.WriteLine("");
                app.UpdateQuality();
            }
        }
        catch (Exception ex)
        {
            ErrorLog.LogException(ex);
            Console.WriteLine("An unexpected error occurred. Please check the error log.");
        }
    }

    private static int GetNumberOfDays()
    {
        const int defaultDays = 5;

        Console.Clear();
        Console.Write("Enter number of days: ");

        string userInput = Console.ReadLine();

        if (int.TryParse(userInput, out int parsedDays) && parsedDays >= 0)
        {
            return parsedDays + 1;
        }

        var ex = new ArgumentException($"Invalid days argument supplied: {userInput}");
        ErrorLog.LogException(ex);
        Console.WriteLine($"Invalid input supplied. Defaulting to {defaultDays} days.");

        return defaultDays;
    }
}