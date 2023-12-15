using System.Text.RegularExpressions;

namespace EhotelBuffet.Service.Guest;
using Model;

public class GroupGenerator
{

    public Dictionary<int, List<Guest>> Groups { get; private set; }

    public void CreateGroups (IEnumerable<Guest> guestsForDay)
    {
        Groups = new Dictionary<int, List<Guest>>();
        Random random = new Random();
        
        foreach (var guest in guestsForDay)
        {
            int randomGroup = random.Next(0, 8);
            if (Groups.ContainsKey(randomGroup))
            {
                Groups[randomGroup].Add(guest);
            }
            else
            {
                Groups[randomGroup] = new List<Guest> { guest };
            }
        }

        Groups = Groups.OrderBy(pair => pair.Key).ToDictionary(pair => pair.Key, pair => pair.Value);
    }

    public override string ToString()
    {
        string message = "";
        foreach (var keyValuePair in Groups)
        {
            message += $"Group {keyValuePair.Key + 1} contains: ";
            foreach (var guest in keyValuePair.Value)
            {
                message += guest.Name + ", ";
            }

            message += "\n";
        }

        return message;
    }
}