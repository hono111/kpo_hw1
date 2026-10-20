using VyshKat.Domain.Interfaces;

namespace VyshKat.Domain.Models.Vehicles;

public class ElectricBike : Vehicle, IEnergyConsumer
{
    public double DailyEnergyConsumption { get; }
    public ElectricBike(string inventoryNumber, string title, int simplicityScore, double dailyEnergyConsumption)
        : base(inventoryNumber, title, simplicityScore)
    {
        if (dailyEnergyConsumption < 0)
            throw new ArgumentException("Потребление энергии не может быть отрицательным.", nameof(dailyEnergyConsumption));

        DailyEnergyConsumption = dailyEnergyConsumption;
    }
}