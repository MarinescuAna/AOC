using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AOC.DaysClass
{
    public static class Day21Process
    {
        private static Day21 Day = new Day21();
        public static void Process()
        {
            Day.Part1();
            Day.Part2();
        }
    }
   
    public class Day21
    {
        private static StreamReader file = new StreamReader(Constants.Path + "In21.txt");
        private List<string> Foods  = new List<string>();
        private Dictionary<string, (string allergen, int number)> Ingredients = new Dictionary<string, (string, int)>();
        private Dictionary<string, List<string>> Allergens = new Dictionary<string, List<string>>();
        public Day21()
        {
            ReadInput();
        }
        private void ReadInput()
        {
            var line = string.Empty;
            while ((line = file.ReadLine()) != null)
            {
                Foods.Add(line);
            }
        }
        public void Part1()
        {
            foreach(var food in Foods)
            {
                var splitFood = food.Split(" (contains ");
                var allergens = splitFood[1].Split(new char[] { ' ', ',', ')' }, StringSplitOptions.RemoveEmptyEntries);

                foreach(var allergen in allergens)
                {
                    if (Allergens.ContainsKey(allergen))
                    {
                        Allergens[allergen].RemoveAll(a => Allergens[allergen].Except(splitFood[0].Split(' ')).Contains(a));
                    }
                    else
                    {
                        Allergens.Add(allergen, splitFood[0].Split(' ').ToList());
                    }
                }

                foreach(var ingredient in splitFood[0].Split(' ').ToList())
                {
                    if (Ingredients.ContainsKey(ingredient))
                    {
                        Ingredients[ingredient] = ("", Ingredients[ingredient].number + 1);

                    }
                    else
                    {
                        Ingredients.Add(ingredient, ("", 1));
                    }
                }

            }

            while (Allergens.Values.Where(l => l.Count > 1).Count() > 0)
                foreach (List<string> ingredient in Allergens.Values.Where(l => l.Count == 1).ToArray())
                    foreach (string multiple in Allergens.Keys.Where(a => Allergens[a].Count > 1 && Allergens[a].Contains(ingredient[0])))
                        Allergens[multiple].Remove(ingredient[0]);

            foreach (string allergen in Allergens.Keys)
            {
                string ingredient = Allergens[allergen][0];
                Ingredients[ingredient] = (allergen, Ingredients[ingredient].number);
            }
            
            Console.WriteLine("Day 21: Allergen Assessment {0} (part1)", Ingredients.Where(u=>string.IsNullOrEmpty(u.Value.allergen)).Sum(i => i.Value.number));
        }
          public void Part2()
        {

            var dangerousIngredientList = new StringBuilder();
            foreach(var ingredient in Ingredients.Where(u => !string.IsNullOrEmpty(u.Value.allergen)).OrderBy(i => i.Value.allergen))
            {
                if (!string.IsNullOrEmpty(ingredient.Value.allergen))
                {
                    dangerousIngredientList.Append($"{ingredient.Key},");
                }
            }
            
            Console.WriteLine($"Day 21: Allergen Assessment {dangerousIngredientList} (part2)");
        }
    }

}
