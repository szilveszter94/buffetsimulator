namespace EhotelBuffet.Service.Buffet;
using Model;

public class BreakfastRefillStrategy
{
    private Dictionary<GuestType, int> guestTypesCount = new();
    
    public void CalculateGuestTypes(IEnumerable<Guest> guestListForDay)
    {
        foreach (Guest guest in guestListForDay)
        {
            if (guestTypesCount.ContainsKey(guest.Type)) guestTypesCount[guest.Type] += 1;
            else guestTypesCount[guest.Type] = 1;
        }
    }

    public int GetGuestCountByMealType(MealType mealType)
    {
        IEnumerable<GuestType> selectedGuests = mealType.GetGuestTypesByMealType();
        List<int> values = new List<int>();
        foreach (var guest in selectedGuests)
        {
            if (guestTypesCount.TryGetValue(guest, out var value))
            {
                values.Add((int)Math.Floor((double)(value / 8)));
            }
        }

        double average = values.AsQueryable().Average();
        
        return (int)average;
    }

    public override string ToString()
    {
        string message = "";
        foreach (var guestType in guestTypesCount.Keys)
        {
            message += $"{guestType}: {guestTypesCount[guestType]}";
        }

        return message;
    }
}