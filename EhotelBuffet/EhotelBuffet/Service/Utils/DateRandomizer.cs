namespace EhotelBuffet.Service.Utils;

public static class DateRandomizer
{
    public static DateTime GetRandomDate(DateTime startDate, DateTime endDate)
    {
        Random random = new Random();
        int range = (endDate - startDate).Days;
        return startDate.AddDays(random.Next(range));
    }
}