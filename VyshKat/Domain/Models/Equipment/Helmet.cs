using VyshKat.Domain.Interfaces;

namespace VyshKat.Domain.Models.Equipment;

public class Helmet : IInventoryItem
{
    public string InventoryNumber { get; }
    public string Title { get; }
    public Helmet(string inventoryNumber, string title)
    {
        if (string.IsNullOrWhiteSpace(inventoryNumber))
            throw new ArgumentException("Инвентарный номер не может быть пустым.", nameof(inventoryNumber));

        if (string.IsNullOrWhiteSpace(title))
            throw new ArgumentException("Название не может быть пустым.", nameof(title));
        
        InventoryNumber = inventoryNumber;
        Title = title;
    }
}