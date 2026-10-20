using VyshKat.Domain.Interfaces;

namespace VyshKat.Domain.Models.Equipment;

public class ChargingStation : IInventoryItem, IEnergyConsumer
{
    public string InventoryNumber { get; }
    public string Title { get; }
    public double DailyEnergyConsumption { get; }
    public ChargingStation(string inventoryNumber, string title, double dailyEnergyConsumption)
    {
        if (string.IsNullOrWhiteSpace(inventoryNumber))
            throw new ArgumentException("Инвентарный номер не может быть пустым.", nameof(inventoryNumber));

        if (string.IsNullOrWhiteSpace(title))
            throw new ArgumentException("Название не может быть пустым.", nameof(title));

        if (dailyEnergyConsumption < 0)
            throw new ArgumentException("Потребление энергии не может быть отрицательным.", nameof(dailyEnergyConsumption));

        InventoryNumber = inventoryNumber;
        Title = title;
        DailyEnergyConsumption = dailyEnergyConsumption;
    }
}