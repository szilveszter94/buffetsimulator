using EhotelBuffet.Model;

namespace EhotelBuffet.Metrics;

public class FoodMetrics
{
    private Dictionary<DinnerMealType, int> _dinnerPopularity = new();
    
    public FoodMetrics()
    {
        foreach (var dinnerMealType in (DinnerMealType[])Enum.GetValues(typeof(DinnerMealType)))
        {
            _dinnerPopularity[dinnerMealType] = 0;
        }
    }

    public Dictionary<DinnerMealType, int> GetDinnerPopularityRate()
    {
        int total = 0;
        Dictionary<DinnerMealType, int> dinnerPopularityRate = new Dictionary<DinnerMealType, int>();
        foreach (var kv in _dinnerPopularity)
        {
            total += Math.Max(kv.Value, 1);
        }

        foreach (var kv in _dinnerPopularity)
        {
            int percentage = kv.Value * 100 / total;
            dinnerPopularityRate[kv.Key] = percentage;
        }

        return dinnerPopularityRate;
    }
    
    public void SetDinnerMealTypePopularity(DinnerMealType dinnerMealType)
    {
        _dinnerPopularity[dinnerMealType]++;
    }

    public int GetDinnerMealTypePopularity(DinnerMealType dinnerMealType)
    {
        return _dinnerPopularity[dinnerMealType];
    }
}