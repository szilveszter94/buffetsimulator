using EhotelBuffet.Service.Buffet;

namespace EhotelBuffet.Model;

public record Buffet
{
    public Dictionary<MealType, (int numberOfPortions, List<DateTime> expirationDates)> MealList = new();
    
    
    public override string ToString()
    {
        string result = "*.*.*.* Menu *.*.*.* \n\n";
        foreach (var keyValuePair in MealList)
        {
            result += $"\n{keyValuePair.Key} has {keyValuePair.Value.numberOfPortions} portions and the expiration times are: {string.Join(", ", keyValuePair.Value.expirationDates),-30}\n\n";
        }

        return result;
    }
  


    public void SetNumberOfPortions(BreakfastRefillStrategy breakfastRefillStrategy)
    {
        MealList = new Dictionary<MealType, (int numberOfPortions, List<DateTime> currentDurability)>();
        foreach (var mealType in (MealType[])Enum.GetValues(typeof(MealType)))
        {
            int maxPortions = breakfastRefillStrategy.GetGuestCountByMealType(mealType);
            MealList[mealType] = (maxPortions, new List<DateTime>());
        }
    }
}