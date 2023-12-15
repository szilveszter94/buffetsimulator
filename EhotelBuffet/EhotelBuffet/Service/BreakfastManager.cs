using EhotelBuffet.Metrics;
using EhotelBuffet.Model;
using EhotelBuffet.Service.Buffet;

namespace EhotelBuffet.Service;

public class BreakfastManager
{
    private DateTime _currentTime;
    private readonly BuffetService _buffetService;
    private SuccessMetrics _successMetrics;

    public BreakfastManager(DateTime currentDay, BuffetService buffetService, SuccessMetrics successMetrics)
    {
        _currentTime = currentDay.AddHours(6);
        _buffetService = buffetService;
        _successMetrics = successMetrics;
    }
    
    public void Serve(Dictionary<int, List<Model.Guest>> guestListForDay)
    {
        Random random = new Random();
        for (int i = 0; i < 8; i++)
        {
            int cycleUnhappyGuests = 0;
            if (guestListForDay.ContainsKey(i)) {
                foreach (var guest in guestListForDay[i])
                {
                    List<MealType> guestPreferences = guest.Type.GetMealPreferences();
                    MealType chosenMeal = guestPreferences.ElementAt(random.Next(0, guestPreferences.Count));
                    bool guestSatisfaction = _buffetService.ConsumeFreshest(chosenMeal);
                    if (!guestSatisfaction) cycleUnhappyGuests++;
                }
                
            }
           
            double cycleWaste = _buffetService.CollectWaste(_currentTime);
            _currentTime = _currentTime.AddMinutes(30);
            if (guestListForDay.ContainsKey(i))  _successMetrics.SetBreakfastMetrics(cycleWaste, cycleUnhappyGuests, guestListForDay[i].Count);
            _successMetrics.PrintCycleMetrics(i);
            _buffetService.Refill(_currentTime);
        }
        _successMetrics.PrintBreakfastStatistics(_currentTime);
    }
}