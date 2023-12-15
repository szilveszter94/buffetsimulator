namespace EhotelBuffet.Service.Kitchen;

public static class DinnerRefillStrategy
{
    public static int SetDinnerPortionsAmount(int allGuests)
    {
        return allGuests * 40 / 100;
    }
}