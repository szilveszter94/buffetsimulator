using EhotelBuffet.Model;

namespace EhotelBuffet.Service.Kitchen;

public interface IKitchenService
{
    public void KitchenRefill();
    public double CollectKitchenWaste();
    public int ConsumePreference(List<DinnerMealType> preferenceList);
}