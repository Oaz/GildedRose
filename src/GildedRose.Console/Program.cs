using System.Collections.Generic;

namespace GildedRose.Console
{
    public class Program
    {
        public IList<Item> Items { get; private set; }

        static void Main(string[] args)
        {
            System.Console.WriteLine("OMGHAI!");

            var app = new Program();

            app.UpdateQuality();

            System.Console.ReadKey();

        }

        public Program()
        {
           Items = new List<Item>
                      {
                          new Item {Name = "+5 Dexterity Vest", SellIn = 10, Quality = 20},
                          new Item {Name = "Aged Brie", SellIn = 2, Quality = 0},
                          new Item {Name = "Elixir of the Mongoose", SellIn = 5, Quality = 7},
                          new Item {Name = "Sulfuras, Hand of Ragnaros", SellIn = 0, Quality = 80},
                          new Item
                              {
                                  Name = "Backstage passes to a TAFKAL80ETC concert",
                                  SellIn = 15,
                                  Quality = 20
                              },
                          new Item {Name = "Conjured Mana Cake", SellIn = 3, Quality = 6}
                      };
       }

       public void UpdateQuality()
       {
          OldUpdateQuality();
       }

       public void NewUpdateQuality()
       {
          Func<int,int> noNegative = v => (v<0) ? 0 : v;
          Func<int,int> max50 = v => (v>50) ? 50 : v;

           var behaviors = new List<Behavior>
                      {
                          new Behavior {SellInChange = (s,q) => s-1, QualityChange = (s,q) =>  noNegative((s<0) ? q-2 : q-1)}, // +5 Dexterity Vest
                          new Behavior {SellInChange = (s,q) => s-1, QualityChange = (s,q) => max50((s<0) ? q+2 : q+1)}, // Aged Brie
                          new Behavior {SellInChange = (s,q) => s-1, QualityChange = (s,q) => noNegative((s<0) ? q-2 : q-1)}, // Elixir of the Mongoose
                          new Behavior {SellInChange = (s,q) => s, QualityChange = (s,q) => q}, // Sulfuras, Hand of Ragnaros
                          new Behavior {SellInChange = (s,q) => s-1, QualityChange = (s,q) => (s<10) ? ((s<5) ? ((s<0) ? 0 : q+3) : q+2) : q+1}, // Backstage passes to a TAFKAL80ETC concert
                          new Behavior {SellInChange = (s,q) => s-1, QualityChange = (s,q) =>  (s<0) ? q-2 : q-1} // Conjured Mana Cake
                      };
          for (var i = 0; i < Items.Count; i++)
          {
            Items[i].SellIn = behaviors[i].SellInChange( Items[i].SellIn, Items[i].Quality );
            Items[i].Quality = behaviors[i].QualityChange( Items[i].SellIn, Items[i].Quality );
          }
       }

        public void OldUpdateQuality()
        {
            for (var i = 0; i < Items.Count; i++)
            {
                if (Items[i].Name != "Aged Brie" && Items[i].Name != "Backstage passes to a TAFKAL80ETC concert")
                {
                    if (Items[i].Quality > 0)
                    {
                        if (Items[i].Name != "Sulfuras, Hand of Ragnaros")
                        {
                            Items[i].Quality = Items[i].Quality - 1;
                        }
                    }
                }
                else
                {
                    if (Items[i].Quality < 50)
                    {
                        Items[i].Quality = Items[i].Quality + 1;

                        if (Items[i].Name == "Backstage passes to a TAFKAL80ETC concert")
                        {
                            if (Items[i].SellIn < 11)
                            {
                                if (Items[i].Quality < 50)
                                {
                                    Items[i].Quality = Items[i].Quality + 1;
                                }
                            }

                            if (Items[i].SellIn < 6)
                            {
                                if (Items[i].Quality < 50)
                                {
                                    Items[i].Quality = Items[i].Quality + 1;
                                }
                            }
                        }
                    }
                }

                if (Items[i].Name != "Sulfuras, Hand of Ragnaros")
                {
                    Items[i].SellIn = Items[i].SellIn - 1;
                }

                if (Items[i].SellIn < 0)
                {
                    if (Items[i].Name != "Aged Brie")
                    {
                        if (Items[i].Name != "Backstage passes to a TAFKAL80ETC concert")
                        {
                            if (Items[i].Quality > 0)
                            {
                                if (Items[i].Name != "Sulfuras, Hand of Ragnaros")
                                {
                                    Items[i].Quality = Items[i].Quality - 1;
                                }
                            }
                        }
                        else
                        {
                            Items[i].Quality = Items[i].Quality - Items[i].Quality;
                        }
                    }
                    else
                    {
                        if (Items[i].Quality < 50)
                        {
                            Items[i].Quality = Items[i].Quality + 1;
                        }
                    }
                }
            }
        }

    }

    public delegate TRESULT Func<T,TRESULT>(T t);
    public delegate TRESULT Func<T1,T2,TRESULT>(T1 t1, T2 t2);

    public class Behavior
    {
        public Func<int,int,int> SellInChange { get; set; }
        public Func<int,int,int> QualityChange { get; set; }
    }

    public class Item
    {
        public string Name { get; set; }

        public int SellIn { get; set; }

        public int Quality { get; set; }
    }

}
