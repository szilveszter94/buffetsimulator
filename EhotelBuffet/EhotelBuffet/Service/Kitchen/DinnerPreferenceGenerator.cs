using EhotelBuffet.Metrics;
using EhotelBuffet.Model;

namespace EhotelBuffet.Service.Kitchen;

public static class DinnerPreferenceGenerator
{
    public static List<DinnerMealType> GenerateDinnerGuestPreferences(FoodMetrics foodMetrics)
    {
        List<DinnerMealType> selectedPreferences = new List<DinnerMealType>();
        var allDinnerMealTypes = foodMetrics.GetDinnerPopularityRate();
        var randomPreferences = WeightedRandomSelection<DinnerMealType>(allDinnerMealTypes, 4);
        foreach (var pref in randomPreferences)
        {
            selectedPreferences.Add(pref);
        }

        return selectedPreferences;
    }
    
    static List<T> WeightedRandomSelection<T>(Dictionary<T, int> weightedData, int count)
    {
        List<T> selectedElements = new List<T>();
        List<T> keyList = new List<T>();
        foreach (var kv in weightedData)
        {
            for (var i = 0; i <= kv.Value+1; i++)
            {
                keyList.Add(kv.Key); 
            }
        }
        Shuffle(keyList);
        HashSet<T> distinctKeys = new HashSet<T>(keyList);
        int counter = 0;
        foreach (var key in distinctKeys)
        {
            if (counter >= count) break;
            selectedElements.Add(key);
            counter++;
        }
        
        return selectedElements;
    }
    
    static void Shuffle<T>(List<T> list)
    {
        Random rng = new Random();
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            (list[k], list[n]) = (list[n], list[k]);
        }
    }
}