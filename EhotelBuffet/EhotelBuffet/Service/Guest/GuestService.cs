
namespace EhotelBuffet.Service.Guest;
using Model;

public class GuestService : IGuestService
{
    // private DateTime seasonStart = new DateTime(2023, 06, 01);
    // private DateTime seasonEnd = new DateTime(2023, 09, 30);
    private Random _random = new Random();
    private DateTime checkIn;
    private DateTime checkOut;
    
    public Guest GenerateRandomGuest(DateTime seasonStart, DateTime seasonEnd)
    {
        string fullName = $"{GenerateRandomEnum<FirstName>()} {GenerateRandomEnum<FamilyName>()}";
        checkIn = GenerateRandomDate(seasonStart, seasonEnd);
        checkOut = GenerateRandomDate(checkIn.AddDays(1), checkIn.AddDays(7));
        ValidateCheckInOutDates(seasonEnd);

        return new Guest(fullName, GenerateRandomEnum<GuestType>(), checkIn, checkOut);
    }

    public IEnumerable<Guest> GetGuestsForDay(List<Guest> guests, DateTime date)
    {
        foreach (var guest in guests)
        {
            if (guest.CheckIn <= date && guest.CheckOut >= date)
                yield return guest;
        }
    }

    private T GenerateRandomEnum<T>()
    {
        int randomNum = _random.Next(0, Enum.GetNames(typeof(T)).Length);
        T randomGuestType = (T)Enum.ToObject(typeof(T), randomNum);

        return randomGuestType;
    }

    private DateTime GenerateRandomDate(DateTime seasonStart, DateTime seasonEnd)
    {
        int range = (seasonEnd - seasonStart).Days;
        return seasonStart.AddDays(_random.Next(range));
    }

    private void ValidateCheckInOutDates(DateTime seasonEnd)
    {
        if (checkOut > seasonEnd)
            checkOut = seasonEnd;
        if (checkIn == seasonEnd)
            checkIn = checkIn.AddDays(-1);
    }

}