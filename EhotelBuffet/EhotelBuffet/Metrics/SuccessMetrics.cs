using EhotelBuffet.Service.Logger;

namespace EhotelBuffet.Metrics;

public class SuccessMetrics
{
    public double CycleWaste { get; private set; }
    public double BreakfastTotalWaste { get; private set; }
    public int CycleUnhappyGuests { get; private set; }
    public int BreakfastTotalUnhappyGuests { get; private set; }
    public int CycleTotalGuests { get; private set; }
    public int DailyTotalGuests { get; private set; }
    public double CycleWasteRate { get; private set; }
    public double CycleUnhappyGuestRate { get; private set; }
    public double DinnerTotalWaste { get; private set; }
    public int DinnerTotalUnhappyGuests { get; private set; }
    public double OverallDinnerSatisfaction { get; private set; }
    private ILogger _logger;

    public SuccessMetrics(ILogger logger)
    {
        _logger = logger;
    }

    public void ResetCycleMetrics()
    {
        CycleWaste = default;
        CycleUnhappyGuests = default;
        CycleTotalGuests = default;
    }
    
    public void ResetDailyMetrics()
    {
        ResetCycleMetrics();
        BreakfastTotalWaste = default;
        BreakfastTotalUnhappyGuests = default;
        DinnerTotalWaste = default;
        DinnerTotalUnhappyGuests = default;
        OverallDinnerSatisfaction = default;
        DailyTotalGuests = default;
    }

    public void SetBreakfastMetrics(double cycleWaste, int cycleUnhappyGuests, int cycleTotalGuests)
    {
        CycleWaste = cycleWaste;
        BreakfastTotalWaste += cycleWaste;

        CycleUnhappyGuests = cycleUnhappyGuests;
        BreakfastTotalUnhappyGuests += cycleUnhappyGuests;

        CycleTotalGuests = cycleTotalGuests; 
        DailyTotalGuests += cycleTotalGuests;
    }

    public void SetDinnerMetrics(double totalDinnerWaste, int totalDinnerUnhappyGuests,
        double overallDinnerSatisfaction)
    {
        DinnerTotalWaste = totalDinnerWaste;
        DinnerTotalUnhappyGuests = totalDinnerUnhappyGuests;
        OverallDinnerSatisfaction = overallDinnerSatisfaction;
    }

    private void CalculateCycleRates()
    {
        CycleWasteRate = (CycleWaste / BreakfastTotalWaste) * 100;
        CycleUnhappyGuestRate = Math.Round((CycleUnhappyGuests / (double)CycleTotalGuests) * 100, 2);
    }

    public void PrintCycleMetrics(int cycleNum)
    {
        CalculateCycleRates();
        _logger.LogInfo($"CYCLE {cycleNum+1} statistics: \n Waste: ${CycleWaste} \n Unhappy guests: {CycleUnhappyGuests} \n Total guest: {CycleTotalGuests} \n Rate of unhappy guests: {CycleUnhappyGuestRate}%");
    }

    public void PrintBreakfastStatistics(DateTime currentDay)
    {
        _logger.LogInfo($"CURRENT DAY: {currentDay.Year}-{currentDay.Month}-{currentDay.Day} \n\n\t\t\t\t\t\t\t\t\t\t\t\t\t\tBREAKFAST statistics: \nWaste: ${BreakfastTotalWaste} \nUnhappy guests: {BreakfastTotalUnhappyGuests} \nTotal guest: {DailyTotalGuests} \n\n\n\n");
        _logger.LogInfo("\n\n\t\t\t\t\t\t\t\t\t\t\t\t\t\t*** DINNER STARTS ***");
    }
    
    public void PrintDinnerStatistics(DateTime currentDay)
    {
        _logger.LogInfo($"CURRENT DAY: {currentDay.Year}-{currentDay.Month}-{currentDay.Day} \n\n\t\t\t\t\t\t\t\t\t\t\t\t\t\tDINNER statistics: \nWaste: ${DinnerTotalWaste} \nUnhappy guests: {DinnerTotalUnhappyGuests} \nOverall dinner satisfaction: {OverallDinnerSatisfaction}% \nTotal guest: {DailyTotalGuests}");
    }
    
    public void PrintDailyMetrics(DateTime currentDay)
    {
        decimal guestHappinessRate =
            Math.Round(100 - (((decimal)(BreakfastTotalUnhappyGuests + DinnerTotalUnhappyGuests) / 2 * DailyTotalGuests) / 100),
                2);
        _logger.LogInfo($"CURRENT DAY:  {currentDay.Year}-{currentDay.Month}-{currentDay.Day} \n\n\t\t\t\t\t\t\t\t\t\t\t\t\t\tStatistics for the day: \nWaste: ${BreakfastTotalWaste + DinnerTotalWaste} \nGuest happiness rate: {guestHappinessRate}% \nTotal guest: {DailyTotalGuests*2}\n");
    }
}