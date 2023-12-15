namespace EhotelBuffet.Model;

public enum DinnerIngredientType
{
    Cheese,
    Meat,
    Pasta,
    TomatoSauce,
    TeriyakiSauce,
    Egg,
    Flour,
    Milk,
    Tortilla,
    Vegetables,
    Rice,
    Ham
}

public static class DinnerIngredientTypeExtensions
{
    private static readonly Dictionary<DinnerIngredientType, (int Cost, IngredientDurability Durability)>
        IngredientTypeMap =
            new Dictionary<DinnerIngredientType, (int Cost, IngredientDurability Durability)>
            {
                { DinnerIngredientType.Cheese, (70, IngredientDurability.Medium) },
                { DinnerIngredientType.Meat, (100, IngredientDurability.Short) },
                { DinnerIngredientType.Ham, (60, IngredientDurability.Short) },
                { DinnerIngredientType.Vegetables, (70, IngredientDurability.Short) },
                { DinnerIngredientType.TomatoSauce, (30, IngredientDurability.Medium) },
                { DinnerIngredientType.TeriyakiSauce, (30, IngredientDurability.Short) },
                { DinnerIngredientType.Egg, (10, IngredientDurability.Medium) },
                { DinnerIngredientType.Milk, (15, IngredientDurability.Medium) },
                { DinnerIngredientType.Tortilla, (30, IngredientDurability.Long) },
                { DinnerIngredientType.Flour, (10, IngredientDurability.Long) },
                { DinnerIngredientType.Pasta, (30, IngredientDurability.Long) },
                { DinnerIngredientType.Rice, (20, IngredientDurability.Long) }
            };

    public static int GetCost(this DinnerIngredientType ingredientType)
    {
        return IngredientTypeMap[ingredientType].Cost;
    }

    public static IngredientDurability GetDurability(this DinnerIngredientType ingredientType)
    {
        return IngredientTypeMap[ingredientType].Durability;
    }
}