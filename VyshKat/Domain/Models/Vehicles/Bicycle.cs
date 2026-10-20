namespace VyshKat.Domain.Models.Vehicles;

public class Bicycle : Vehicle
{
    public Bicycle(string inventoryNumber, string title, int simplicityScore)
        : base(inventoryNumber, title, simplicityScore)
    {
    }
}