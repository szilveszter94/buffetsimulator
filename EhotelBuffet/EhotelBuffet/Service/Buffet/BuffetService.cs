namespace EhotelBuffet.Service.Buffet;
using Model;

public class BuffetService : IBuffetService
{
    public Buffet CurrentBuffet;

    public BuffetService(Buffet currentBuffet)
    {
        CurrentBuffet = currentBuffet;
    }
    
    public double CollectWaste(DateTime currentTime)
    {
        double waste = 0.00;
        foreach (var mealType in CurrentBuffet.MealList.Keys)
        {
            int numberOfWastedMeals = CurrentBuffet.MealList[mealType].expirationDates.Count(expDate => expDate <= currentTime);
            int costOfMealType = mealType.GetCost();
            waste += (double)numberOfWastedMeals * costOfMealType;
            CurrentBuffet.MealList[mealType].expirationDates.RemoveAll(expDate => expDate <= currentTime);
        }
        return waste;
    }
    
    public bool ConsumeFreshest(MealType chosenMeal)
    {
        List<DateTime> mealExpDates = CurrentBuffet.MealList[chosenMeal].expirationDates;
        if (mealExpDates.Count == 0) return false;
        mealExpDates.Sort();
        mealExpDates.RemoveAt(mealExpDates.Count - 1);
        return true;
    }
    
    public void Refill(DateTime currentTime)
    {
        foreach (var mealType in CurrentBuffet.MealList.Keys)
        {
            int deficit = CurrentBuffet.MealList[mealType].numberOfPortions -
                          CurrentBuffet.MealList[mealType].expirationDates.Count;
            if (deficit > 0)
            {
                for (int i = 1; i <= deficit; i++)
                {
                    DateTime newExpirationDate = currentTime.AddMinutes((int)mealType.GetDurability());
                    CurrentBuffet.MealList[mealType].expirationDates.Add(newExpirationDate);
                }
            }
        }
    }
}