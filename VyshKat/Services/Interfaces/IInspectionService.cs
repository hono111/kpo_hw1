using VyshKat.Domain.Models;

namespace VyshKat.Services.Interfaces;

public interface IInspectionService
{
    bool Inspect(Vehicle vehicle);
}