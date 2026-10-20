using VyshKat.Domain.Interfaces;

namespace VyshKat.Domain.Models;

public abstract class Vehicle : IInventoryItem
{
    public string InventoryNumber { get; }
    public string Title { get; }
    public int SimplicityScore { get; }
    protected Vehicle(string inventoryNumber, string title, int simplicityScore)
    {
        if (string.IsNullOrWhiteSpace(inventoryNumber))
            throw new ArgumentException("Инвентарный номер не может быть пустым.", nameof(inventoryNumber));

        if (string.IsNullOrWhiteSpace(title))
            throw new ArgumentException("Название не может быть пустым.", nameof(title));

        if (simplicityScore is < 1 or > 10)
            throw new ArgumentOutOfRangeException(nameof(simplicityScore), "Простота должна быть от 1 до 10.");

        InventoryNumber = inventoryNumber;
        Title = title;
        SimplicityScore = simplicityScore;
    }
    public virtual bool IsSuitableForBeginners => SimplicityScore >= 6;
}