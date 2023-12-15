using EhotelBuffet.Service.Kitchen;

namespace EhotelBuffet.Model;
using EhotelBuffet.Service.Utils;

public record Kitchen
{
    public Dictionary<DinnerIngredientType, (int numberOfPortions, List<DateTime> expirationDates)> IngredientList = new();
    
    public override string ToString()
    {
        string result = "*.*.*.* Kitchen *.*.*.* \n";
        foreach (var keyValuePair in IngredientList)
        {
            result += $"\n{keyValuePair.Key} has {keyValuePair.Value.numberOfPortions} portions and the expiration times are: \n {string.Join(", \t ", keyValuePair.Value.expirationDates),-30}\n";
        }

        return result.PadLeft((Console.WindowWidth + result.Length) / 2);
    }

    public void SetIngredientPortions(DateTime currentDate, int portionAmount)
    {
        foreach (var ingredientType in (DinnerIngredientType[])Enum.GetValues(typeof(DinnerIngredientType)))
        {
            IngredientList[ingredientType] = (portionAmount, new List<DateTime>());
            for (int i = 0; i < IngredientList[ingredientType].numberOfPortions; i++)
            {
                IngredientList[ingredientType].expirationDates.Add(DateRandomizer.GetRandomDate(currentDate, currentDate.AddDays(10)));
            }
        }
    }
};