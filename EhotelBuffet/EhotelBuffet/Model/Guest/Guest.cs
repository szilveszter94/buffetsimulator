namespace EhotelBuffet.Model;

public record Guest(string Name, GuestType Type, DateTime CheckIn, DateTime CheckOut)
{
    public override string ToString()
    {
        return $"Name: {Name}, Type: {Type}, Checkin: {CheckIn}, Checkout: {CheckOut}";
    }
}