using System;
using packt.FoodyGO.Database;
using packt.FoodyGO.Managers;
using UnityEngine;
using System.Collections.Generic;

namespace packt.FoodyGO.Services
{
    public class MonsterExchangeService : Singleton<MonsterExchangeService>
    {
        /// <summary>
        /// Prices or values all the monsters in the players inventory
        /// making an offer according to the budget of the place 
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        public MonsterOffer PriceMonsters(UI.Result result)
        {
            if (result == null) return null;  //need a place result in order to value monsters

            var monsters = InventoryService.Instance.ReadMonsters();

            //we cycle through all the reviews and merge the text
            //the reviews will give us specific details about each location
            //like the type of cuisine which we will use to match monster skills
            var reviewText = string.Empty;
            foreach(var r in result.reviews)
            {
                reviewText += r.text.ToLower() + " ";
            }

            List<MonsterOffer> offers = new List<MonsterOffer>();

            //determine the budget the place/restaurant has to spend
            var budget = result.rating * (result.price_level + 1) * result.types.Count * 100;

            foreach (var m in monsters)
            {
                var val = 0;
                var skills = m.Skills.Split(',');
                //try to match skill words to reviews for bonus
                foreach (var s in skills)
                {
                    val += findMatches(s.ToLower(), reviewText) * 100;
                }
                var cp = m.Power * m.Level;
                val += cp * 100;
                if (budget > val)
                    offers.Add(new MonsterOffer
                    {
                        Monster = m,
                        Value = val
                    });
            }

            //sort the offers by value, highest to lowest
            offers.Sort();offers.Reverse();

            if (offers.Count > 0)
                return offers[0];
            else
                return null;
        }

        private int findMatches(string s, string reviewText)
        {
            int count = (reviewText.Length - reviewText.Replace(s, "").Length)/s.Length;
            return count;
        }

        /// <summary>
        /// Converts an offer value to items and experience
        /// </summary>
        /// <param name="monsterOffer"></param>
        /// <returns>A summary of the loot</returns>
        public string ConvertOffer(MonsterOffer monsterOffer)
        {
            var value = monsterOffer.Value;
            var summary = string.Empty;
            //calculate experience
            monsterOffer.Experience = Mathf.RoundToInt(value / 10);
            summary += monsterOffer.Experience + " Experience" + Environment.NewLine;

            //convert value into ball units
            //ice-ball=100, dryice-ball=250, nitro-ball=1000
            List<InventoryItem> items = new List<InventoryItem>();
            if(value > 1000)
            {
                int balls = (int)value / 1000;
                for(int i = 0; i < balls; i++)
                {
                    items.Add(new InventoryItem
                    {
                        Name = "Nitro-ball", ItemType = "Ball"
                    });
                }
                summary += balls + " Nitro-balls" + Environment.NewLine;
                value -= balls * 1000;
            }

            if (value > 250)
            {
                int balls = (int)value / 250;
                for (int i = 0; i < balls; i++)
                {
                    items.Add(new InventoryItem
                    {
                        Name = "DryIce-ball",
                        ItemType = "Ball"
                    });
                }
                summary += balls + " DryIce-balls" + Environment.NewLine;
                value -= balls * 250;
            }

            if (value > 100)
            {
                int balls = (int)value / 100;
                for (int i = 0; i < balls; i++)
                {
                    items.Add(new InventoryItem
                    {
                        Name = "Ice-ball",
                        ItemType = "Ball"
                    });
                }
                summary += balls + " Ice-balls" + Environment.NewLine;
                value -= balls * 100;
            }
            monsterOffer.Items = items;
            return summary;
        }
    }

    public class MonsterOffer : IComparable<MonsterOffer>
    {
        public Monster Monster { get; set; }

         public float Value { get; set; }            
       
        public int Experience { get; set; }      

        public List<InventoryItem> Items { get; set; }

        private string offerText;
        public string OfferText
        {
            get
            {
                if(offerText == null)
                {
                    offerText = ConvertOffer();
                }
                return offerText;
            }
        }        

        private string ConvertOffer()
        {
            return MonsterExchangeService.Instance.ConvertOffer(this);
        }

        //override Equals in order to provide custom sorting
        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            var offer = obj as MonsterOffer;
            if (offer == null) return false;
            return Value.Equals(offer.Value);
        }

        //override GetHashCode to provide custom sorting
        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

        //implement CompareTo for custom sorting on Value
        public int CompareTo(MonsterOffer other)
        {
            if (other == null) return 1;

            return Value.CompareTo(other.Value);
        }
    }
}
