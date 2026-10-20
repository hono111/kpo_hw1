using VyshKat.Domain.Models.Equipment;
using VyshKat.Domain.Models.Vehicles;
using VyshKat.Services;
using VyshKat.Tests.Fakes;
using Xunit;

namespace VyshKat.Tests;

public class ParkManagerTests
{
    [Fact]
    public void RegisterVehicle_WhenInspectionPasses_ShouldAddVehicleToPark()
    {

        var fakeInspection = new FakeInspectionService(shouldPass: true);
        var manager = new ParkManager(fakeInspection);
        var scooter = new ElectricScooter("S-01", "Ninebot", simplicityScore: 8, dailyEnergyConsumption: 1.5);

        bool result = manager.RegisterVehicle(scooter);

        Assert.True(result);
        Assert.Single(manager.GetVehicles());
        Assert.Contains(scooter, manager.GetVehicles());
    }

    [Fact]
    public void RegisterVehicle_WhenInspectionFails_ShouldRejectAndNotAddToPark()
    {
        var fakeInspection = new FakeInspectionService(shouldPass: false);
        var manager = new ParkManager(fakeInspection);
        var scooter = new ElectricScooter("S-02", "Kugoo", simplicityScore: 7, dailyEnergyConsumption: 1.2);

        bool result = manager.RegisterVehicle(scooter);

        Assert.False(result);
        Assert.Empty(manager.GetVehicles());
    }

    [Theory]
    [InlineData(6, true)] 
    [InlineData(8, true)]
    [InlineData(5, false)]
    [InlineData(1, false)]
    public void GetSuitableForBeginners_ShouldFilterBySimplicityScoreThreshold(int score, bool expectedSuitable)
    {
        var fakeInspection = new FakeInspectionService(shouldPass: true);
        var manager = new ParkManager(fakeInspection);
        var vehicle = new ElectricScooter("TEST-01", "Test Scooter", simplicityScore: score, dailyEnergyConsumption: 1.0);
        manager.RegisterVehicle(vehicle);

        var suitableList = manager.GetSuitableForBeginners();

        if (expectedSuitable)
        {
            Assert.Contains(vehicle, suitableList);
        }
        else
        {
            Assert.DoesNotContain(vehicle, suitableList);
        }
    }

    [Fact]
    public void GetTotalDailyEnergyConsumption_ShouldSumOnlyEnergyConsumers()
    {
        var fakeInspection = new FakeInspectionService(shouldPass: true);
        var manager = new ParkManager(fakeInspection);

        var scooter = new ElectricScooter("S-01", "Ninebot", simplicityScore: 7, dailyEnergyConsumption: 1.5);
        var eBike = new ElectricBike("B-01", "Himo", simplicityScore: 6, dailyEnergyConsumption: 2.5);
        var station = new ChargingStation("ST-01", "Hub", dailyEnergyConsumption: 10.0);

        var classicBike = new Bicycle("CB-01", "Format", simplicityScore: 9);
        var helmet = new Helmet("H-01", "City Helmet");

        manager.RegisterVehicle(scooter);
        manager.RegisterVehicle(eBike);
        manager.RegisterVehicle(classicBike);
        manager.RegisterItem(station);
        manager.RegisterItem(helmet);

        double totalEnergy = manager.GetTotalDailyEnergyConsumption();

        Assert.Equal(14.0, totalEnergy, precision: 2);
    }

    [Fact]
    public void GetAllInventory_ShouldContainBothVehiclesAndEquipment()
    {
        var fakeInspection = new FakeInspectionService(shouldPass: true);
        var manager = new ParkManager(fakeInspection);

        var scooter = new ElectricScooter("S-01", "Ninebot", 8, 1.5);
        var helmet = new Helmet("H-01", "Urban Helmet");

        manager.RegisterVehicle(scooter);
        manager.RegisterItem(helmet);

        var allItems = manager.GetAllInventory();

        Assert.Equal(2, allItems.Count);
        Assert.Contains(allItems, item => item.InventoryNumber == "S-01");
        Assert.Contains(allItems, item => item.InventoryNumber == "H-01");
    }
}