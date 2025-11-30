using CodeBase.Infrastructure.Services;

namespace CodeBase.Infrastructure.Randomizer
{
  public interface IRandomService : IService
  {
    int Next(int minValue, int maxValue);
  }
}