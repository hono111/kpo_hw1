using VyshKat.Domain.Models.Equipment;
using VyshKat.Domain.Models.Vehicles;
using Xunit;

namespace VyshKat.Tests;

public class DomainValidationTests
{
    [Theory]
    [InlineData(0)]
    [InlineData(11)]
    [InlineData(-5)]
    public void Vehicle_InvalidSimplicityScore_ShouldThrowArgumentOutOfRangeException(int invalidScore)
    {
        Assert.Throws<ArgumentOutOfRangeException>(() =>
            new ElectricScooter("S-01", "Test", simplicityScore: invalidScore, dailyEnergyConsumption: 1.0));
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    [InlineData(null)]
    public void InventoryItem_InvalidTitleOrNumber_ShouldThrowArgumentException(string? invalidString)
    {
        Assert.Throws<ArgumentException>(() =>
            new Helmet(invalidString!, "Valid Title"));

        Assert.Throws<ArgumentException>(() =>
            new Helmet("H-01", invalidString!));
    }

    [Fact]
    public void EnergyConsumer_NegativeEnergy_ShouldThrowArgumentException()
    {
        Assert.Throws<ArgumentException>(() =>
            new ChargingStation("ST-01", "Station", dailyEnergyConsumption: -1.0));
    }
}