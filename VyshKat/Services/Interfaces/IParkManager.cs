using VyshKat.Domain.Interfaces;
using VyshKat.Domain.Models;

namespace VyshKat.Services.Interfaces;

public interface IParkManager
{
    bool RegisterVehicle(Vehicle vehicle);
    void RegisterItem(IInventoryItem item);

    IReadOnlyCollection<Vehicle> GetVehicles();
    IReadOnlyCollection<Vehicle> GetSuitableForBeginners();
    IReadOnlyCollection<IInventoryItem> GetAllInventory();
    double GetTotalDailyEnergyConsumption();
}