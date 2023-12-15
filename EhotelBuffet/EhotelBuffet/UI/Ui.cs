using EhotelBuffet.Metrics;
using EhotelBuffet.Model;
using EhotelBuffet.Service;
using EhotelBuffet.Service.Buffet;
using EhotelBuffet.Service.Guest;
using EhotelBuffet.Service.Kitchen;
using EhotelBuffet.Service.Logger;

namespace EhotelBuffet.Ui;




public class Ui
    {
        private readonly ILogger logger;
        private readonly BreakfastRefillStrategy breakfastRefillStrategyGenerator;
        private readonly SuccessMetrics successMetrics;
        private readonly FoodMetrics foodMetrics;
        private readonly Buffet currentBuffet;
        private readonly Kitchen currentKitchen;
        private DateTime currentDay;
        private readonly GuestService guestService;
        private readonly GroupGenerator groupGenerator;
        private readonly BuffetService buffetService;
        private readonly KitchenService kitchenService;
        private readonly BreakfastManager breakfastManager;
        private readonly DinnerManager dinnerManager;

        public Ui()
        {
            // Initialize services
            logger = new ConsoleLogger();
            breakfastRefillStrategyGenerator = new BreakfastRefillStrategy();
            successMetrics = new SuccessMetrics(logger);
            foodMetrics = new FoodMetrics();
            currentBuffet = new Buffet();
            currentKitchen = new Kitchen();
            currentDay = new DateTime(2023, 07, 15);
            guestService = new GuestService();
            groupGenerator = new GroupGenerator();
            buffetService = new BuffetService(currentBuffet);
            kitchenService = new KitchenService(currentKitchen, foodMetrics);
            breakfastManager = new BreakfastManager(currentDay, buffetService, successMetrics);
            dinnerManager = new DinnerManager(currentDay, kitchenService, foodMetrics, successMetrics);
        }

        public void Run()
{
    // Get the number of guests
    int numberOfGuests;
    while (true)
    {
        logger.LogInfo("Enter the number of guests between 1000 and 10000: ");
        string userInput = Console.ReadLine();

        if (int.TryParse(userInput, out numberOfGuests) && numberOfGuests >= 1000)
        {
            break;
        }
        else
        {
            logger.LogInfo("Invalid input. Please enter a positive integer that is greater than 1000.");
        }
    }

    // Generate guests for the season
    List<Guest> listOfGuests = new List<Guest>();
    DateTime seasonStart = new DateTime(2023, 06, 01);
    DateTime seasonEnd = new DateTime(2023, 09, 30);
    
    for (int i = 0; i < numberOfGuests; i++)
    {
        listOfGuests.Add(guestService.GenerateRandomGuest(seasonStart, seasonEnd));
    }
    
    
    while (true)
    {
        logger.LogInfo($"Enter the current day between {seasonStart.ToShortDateString()} and {seasonEnd.ToShortDateString()}: ");
        string userInput = Console.ReadLine();

        if (DateTime.TryParse(userInput, out currentDay) && currentDay >= seasonStart && currentDay <= seasonEnd)
        {
            break;
        }
        else
        {
            logger.LogInfo($"Invalid input. Please enter a date between {seasonStart.ToShortDateString()} and {seasonEnd.ToShortDateString()}.");
        }
    }
    // Generate guests for the current day
    IEnumerable<Guest> listofGuestsForDay = guestService.GetGuestsForDay(listOfGuests, currentDay);
    // Generate groups of people for breakfast cycles
    groupGenerator.CreateGroups(listofGuestsForDay);
        
    // calculate how many people are in each guest types
    breakfastRefillStrategyGenerator.CalculateGuestTypes(listofGuestsForDay);
        
    // generate menu in buffet for a day 
    currentBuffet.SetNumberOfPortions(breakfastRefillStrategyGenerator);
    buffetService.Refill(currentDay.AddHours(6));
    logger.LogInfo(currentBuffet.ToString());
        
    // Run breakfast buffet
    breakfastManager.Serve(groupGenerator.Groups);
        
    // generate ingredients in the kitchen
    currentKitchen.SetIngredientPortions(currentDay, DinnerRefillStrategy.SetDinnerPortionsAmount(listofGuestsForDay.Count()));
    logger.LogInfo(currentKitchen.ToString());
        
    // Run dinner
    dinnerManager.Serve(listofGuestsForDay);
        
    // Print daily statistics22200
    successMetrics.PrintDailyMetrics(currentDay);
    successMetrics.ResetDailyMetrics();
}

    }