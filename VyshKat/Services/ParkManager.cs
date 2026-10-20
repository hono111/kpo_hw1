using VyshKat.Domain.Interfaces;
using VyshKat.Domain.Models;
using VyshKat.Services.Interfaces;

namespace VyshKat.Services;

public class ParkManager : IParkManager
{
    private readonly IInspectionService _inspectionService;
    private readonly List<Vehicle> _vehicles = new();
    private readonly List<IInventoryItem> _items = new();

    public ParkManager(IInspectionService inspectionService)
    {
        _inspectionService = inspectionService ?? throw new ArgumentNullException(nameof(inspectionService));
    }

    public bool RegisterVehicle(Vehicle vehicle)
    {
        ArgumentNullException.ThrowIfNull(vehicle);
        if (!_inspectionService.Inspect(vehicle))
        {
            return false;
        }
        _vehicles.Add(vehicle);
        return true;
    }

    public void RegisterItem(IInventoryItem item)
    {
        ArgumentNullException.ThrowIfNull(item);
        _items.Add(item);
    }

    public IReadOnlyCollection<Vehicle> GetVehicles() => _vehicles.AsReadOnly();

    public IReadOnlyCollection<Vehicle> GetSuitableForBeginners()
    {
        return _vehicles.Where(v => v.IsSuitableForBeginners).ToList().AsReadOnly();
    }

    public IReadOnlyCollection<IInventoryItem> GetAllInventory()
    {
        return _vehicles.Cast<IInventoryItem>().Concat(_items).ToList().AsReadOnly();
    }

    public double GetTotalDailyEnergyConsumption()
    {
        double vehicleEnergy = _vehicles
            .OfType<IEnergyConsumer>()
            .Sum(v => v.DailyEnergyConsumption);

        double itemEnergy = _items
            .OfType<IEnergyConsumer>()
            .Sum(i => i.DailyEnergyConsumption);
        
        return vehicleEnergy + itemEnergy;
    }
}