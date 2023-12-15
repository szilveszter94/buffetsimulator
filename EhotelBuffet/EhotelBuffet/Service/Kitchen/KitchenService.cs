using EhotelBuffet.Metrics;

namespace EhotelBuffet.Service.Kitchen;
using Model;

public class KitchenService
{
    public Kitchen _currentKitchen;
    private FoodMetrics _foodMetrics;

    public KitchenService(Kitchen currentKitchen, FoodMetrics foodMetrics)
    {
        _currentKitchen = currentKitchen;
        _foodMetrics = foodMetrics;
    }
    // consume meal preference 
    public int ConsumePreference(List<DinnerMealType> preferenceList, DateTime currentDate)
    {
        List<int> results = new List<int>();
        int counter = 0;
        foreach (var mealType in preferenceList)
        {
            List<DinnerIngredientType> ingredients = mealType.GetIngredients();
            bool isReady = CheckIfRecipeMakeable(ingredients);
            if (isReady)
            {
                ManageKitchenStore(ingredients, currentDate, results);
                _foodMetrics.SetDinnerMealTypePopularity(mealType);
                break;
            }

            counter++;
        }

        if (counter == preferenceList.Count) return 0;
        int average = (int)Queryable.Average(results.AsQueryable());
        return Math.Max(0, average - counter * 3);
    }

    public void ManageKitchenStore(List<DinnerIngredientType> ingredients, DateTime currentDate, List<int> results)
    {
        foreach (DinnerIngredientType ingredient in ingredients)
        {
            var availableIngredients = _currentKitchen.IngredientList[ingredient];
            List<DateTime> expirationDates = availableIngredients.expirationDates;
            expirationDates.Sort();
            DateTime latestDate = expirationDates[expirationDates.Count-1];
            expirationDates.RemoveAt(expirationDates.Count-1);
            CalculateHappinessResult(latestDate, currentDate, ingredient, results);
        }
    }

    public void CalculateHappinessResult(DateTime latestDate, DateTime currentDate, DinnerIngredientType ingredient, List<int> results)
    {
        var diff = latestDate - currentDate;
        int dayDiff = diff.Days;
        if (dayDiff <= 1)
        {
            dayDiff = 1;
        }
        {
            int ingredientDurability = (int)ingredient.GetDurability();
            int percentage = (int)Math.Ceiling((decimal)dayDiff * 100 / ingredientDurability);
            percentage += (100 - percentage) / 2;
            results.Add(percentage);
        }
    }

    public bool CheckIfRecipeMakeable(List<DinnerIngredientType> ingredients)
    {
        foreach (DinnerIngredientType ingredient in ingredients)
        {
            var availableIngredients = _currentKitchen.IngredientList[ingredient];
            List<DateTime> expirationDates = availableIngredients.expirationDates;
            if (expirationDates.Count == 0)
            {
                return false;
            }
        }

        return true;
    }
    
    // refill kitchen ingredients
    public void Refill(DateTime currentTime)
    {
        foreach (var ingredientType in _currentKitchen.IngredientList.Keys)
        {
            int deficit = _currentKitchen.IngredientList[ingredientType].numberOfPortions - _currentKitchen.IngredientList[ingredientType].expirationDates.Count;
            if (deficit > 0)
            {
                for (int i = 1; i <= deficit; i++)
                {
                    DateTime newExpirationDate = currentTime.AddMinutes((int)ingredientType.GetDurability());
                    _currentKitchen.IngredientList[ingredientType].expirationDates.Add(newExpirationDate);
                }
            }
        }
    }
    
    public double CollectKitchenWaste(DateTime currentTime)
    {
        double waste = 0.00;
        foreach (var dinnerIngredientType in _currentKitchen.IngredientList.Keys)
        {
            int numberOfWastedIngredients = _currentKitchen.IngredientList[dinnerIngredientType].expirationDates.Count(expDate => expDate <= currentTime);
            int costOfIngredientType = dinnerIngredientType.GetCost();
            waste += (double)numberOfWastedIngredients * costOfIngredientType;
            _currentKitchen.IngredientList[dinnerIngredientType].expirationDates.RemoveAll(expDate => expDate <= currentTime);
        }
        return waste;
    }
}