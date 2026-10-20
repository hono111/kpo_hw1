using VyshKat.Domain.Models;
using VyshKat.Services.Interfaces;

namespace VyshKat.Tests.Fakes;

public class FakeInspectionService : IInspectionService
{
    private readonly bool _shouldPass;

    public FakeInspectionService(bool shouldPass)
    {
        _shouldPass = shouldPass;
    }

    public bool Inspect(Vehicle vehicle) => _shouldPass;
}