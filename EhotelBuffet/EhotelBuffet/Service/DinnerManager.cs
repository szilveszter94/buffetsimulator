using EhotelBuffet.Metrics;
using EhotelBuffet.Model;
using EhotelBuffet.Service.Kitchen;

namespace EhotelBuffet.Service;

public class DinnerManager
{
    private DateTime _currentTime;
    private readonly KitchenService _kitchenService;
    private FoodMetrics _foodMetrics;
    private int _unhappyGuests;
    private int _overallHappiness;
    private double _waste;
    private SuccessMetrics _successMetrics;

    public DinnerManager(DateTime currentDay, KitchenService kitchenService, FoodMetrics foodMetrics, SuccessMetrics successMetrics)
    {
        _currentTime = currentDay;
        _kitchenService = kitchenService;
        _foodMetrics = foodMetrics;
        _unhappyGuests = 0;
        _overallHappiness = 0;
        _waste = 0.00;
        _successMetrics = successMetrics;
    }
    
    public void Serve(IEnumerable<Model.Guest> guestListForDay)
    {
        foreach (var guest in guestListForDay)
        {
            var individualPreferenceList = DinnerPreferenceGenerator.GenerateDinnerGuestPreferences(_foodMetrics);
            int guestHappiness = _kitchenService.ConsumePreference(individualPreferenceList, _currentTime);
            if (guestHappiness <= 0)
            {
                _unhappyGuests++;
                continue;
                
            }
            _overallHappiness += guestHappiness;
        }
        
        _waste += _kitchenService.CollectKitchenWaste(_currentTime);
        _kitchenService.Refill(_currentTime);
        _overallHappiness /= guestListForDay.Count();
        _successMetrics.SetDinnerMetrics(_waste, _unhappyGuests, _overallHappiness);
        _successMetrics.PrintDinnerStatistics(_currentTime);
    }
}