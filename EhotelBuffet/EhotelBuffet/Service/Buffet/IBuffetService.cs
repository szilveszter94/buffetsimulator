namespace EhotelBuffet.Service.Buffet;
using EhotelBuffet.Model;

public interface IBuffetService
{
    double CollectWaste(DateTime currentTime);
    bool ConsumeFreshest(MealType chosenMeal);
    void Refill(DateTime currentTime);
}