namespace EhotelBuffet.Model;

public enum DinnerMealType
{
    PastaBolognese,
    MeatSoup,
    Pizza,
    MacNCheese,
    Quesedilla,
    FriedRice,
    Salad,
    TeriyakiChicken,
    Burrito,
    Pancakes
}

public static class DinnerMealExtension
{
    private static readonly Dictionary<GuestType, List<DinnerMealType>> DinnerPreferencesMap =
        new Dictionary<GuestType, List<DinnerMealType>>
        {
            {
                GuestType.Business,
                new List<DinnerMealType>
                {
                    DinnerMealType.Salad,
                    DinnerMealType.TeriyakiChicken,
                    DinnerMealType.Quesedilla,
                    DinnerMealType.MeatSoup
                }
            },
            {
                GuestType.Tourist,
                new List<DinnerMealType>
                {
                    DinnerMealType.Burrito,
                    DinnerMealType.FriedRice,
                    DinnerMealType.Pizza,
                    DinnerMealType.PastaBolognese,
                    DinnerMealType.MeatSoup
                    
                }
            },
            {
                GuestType.Kid,
                new List<DinnerMealType>
                {
                    DinnerMealType.Pancakes,
                    DinnerMealType.Quesedilla,
                    DinnerMealType.Pizza,
                    DinnerMealType.MacNCheese
                }
            }
        };
    
    private static readonly Dictionary<DinnerMealType, List<DinnerIngredientType>> MealIngredients =
        new Dictionary<DinnerMealType, List<DinnerIngredientType>>
        {
            {
                DinnerMealType.PastaBolognese,
                new List<DinnerIngredientType>
                {
                    DinnerIngredientType.Cheese,
                    DinnerIngredientType.Meat,
                    DinnerIngredientType.Pasta,
                    DinnerIngredientType.TomatoSauce
                }
            },
            {
                DinnerMealType.MeatSoup,
                new List<DinnerIngredientType>
                {
                    DinnerIngredientType.Meat,
                    DinnerIngredientType.Vegetables,
                    DinnerIngredientType.Pasta
                }
            },
            {
                DinnerMealType.Pizza,
                new List<DinnerIngredientType>
                {
                    DinnerIngredientType.Cheese,
                    DinnerIngredientType.Egg,
                    DinnerIngredientType.Flour,
                    DinnerIngredientType.Ham,
                    DinnerIngredientType.TomatoSauce
                }
            },
            {
                DinnerMealType.MacNCheese,
                new List<DinnerIngredientType>
                {
                    DinnerIngredientType.Cheese,
                    DinnerIngredientType.Pasta,
                    DinnerIngredientType.Milk
                }
            },
            {
                DinnerMealType.Quesedilla,
                new List<DinnerIngredientType>
                {
                    DinnerIngredientType.Vegetables,
                    DinnerIngredientType.Tortilla,
                    DinnerIngredientType.Meat
                }
            },
            {
                DinnerMealType.FriedRice,
                new List<DinnerIngredientType>
                {
                    DinnerIngredientType.Rice,
                    DinnerIngredientType.Vegetables,
                    DinnerIngredientType.Egg
                }
            },
            {
                DinnerMealType.Salad,
                new List<DinnerIngredientType>
                {
                    DinnerIngredientType.Vegetables
                }
            },
            {
                DinnerMealType.TeriyakiChicken,
                new List<DinnerIngredientType>
                {
                    DinnerIngredientType.Meat,
                    DinnerIngredientType.Rice,
                    DinnerIngredientType.TeriyakiSauce
                }
            },
            {
                DinnerMealType.Burrito,
                new List<DinnerIngredientType>
                {
                    DinnerIngredientType.Ham,
                    DinnerIngredientType.Cheese,
                    DinnerIngredientType.Tortilla,
                    DinnerIngredientType.Vegetables,
                    DinnerIngredientType.Egg
                }
            },
            {
                DinnerMealType.Pancakes,
                new List<DinnerIngredientType>
                {
                    DinnerIngredientType.Milk,
                    DinnerIngredientType.Egg,
                    DinnerIngredientType.Flour
                }
            }
        };

    public static List<DinnerMealType> GetDinnerMealPreferences(this GuestType guestType)
    {
        return DinnerPreferencesMap[guestType];
    }

    public static List<DinnerIngredientType> GetIngredients(this DinnerMealType dinnerMealType)
    {
        return MealIngredients[dinnerMealType];
    }

    public static HashSet<DinnerMealType> GenerateIndividualPreferenceList(this Guest guest)
    {
        Random random = new Random();
        GuestType guestType = guest.Type;
        List<DinnerMealType> typePreferences = guestType.GetDinnerMealPreferences();
        HashSet<DinnerMealType> individualPreferenceList = new();
        while(individualPreferenceList.Count < 3)
        {
            individualPreferenceList.Add(typePreferences[random.Next(0, typePreferences.Count)]);
        }

        return individualPreferenceList;
    }
    
    // public static IEnumerable<GuestType> GetGuestTypesByMealType(this MealType mealType)
    // {
    //     foreach (var kvp in MealPreferencesMap)
    //     {
    //         if (kvp.Value.Contains(mealType))
    //         {
    //             yield return kvp.Key;
    //         }
    //     }
    // }
}