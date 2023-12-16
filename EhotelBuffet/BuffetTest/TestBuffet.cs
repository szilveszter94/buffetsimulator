public class TestBuffet
{
    [Test]
    public void TestMealPreference()
    {
        DateTime currentDay = new DateTime(2023, 07, 14);
        FoodMetrics foodMetrics = new FoodMetrics();
        Kitchen kitchen = new Kitchen();
        KitchenService kitchenService = new KitchenService(kitchen, foodMetrics);
        kitchen.SetIngredientPortions(currentDay);
        List<DinnerMealType> prefList = new List<DinnerMealType>()
            { DinnerMealType.Burrito, DinnerMealType.Pancakes, DinnerMealType.Pizza, DinnerMealType.Salad };
        Assert.That(kitchenService.ConsumePreference(prefList, currentDay), Is.Positive);
    }
}
