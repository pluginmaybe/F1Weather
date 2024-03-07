
namespace F1Weather.Services;

public interface ICircuitService
{
    Task<List<Circuits>> GetCircuits();
}