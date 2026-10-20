using VyshKat.Domain.Models;
using VyshKat.Services.Interfaces;

namespace VyshKat.Services;

public class ServiceCenter : IInspectionService
{
    public bool Inspect(Vehicle vehicle)
    {
        ArgumentNullException.ThrowIfNull(vehicle);
        if (vehicle.Title.Contains("Неисправен", StringComparison.OrdinalIgnoreCase))
            return false;

        return true;
    }
}