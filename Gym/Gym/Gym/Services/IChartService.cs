using Microcharts;

namespace Gym.Services
{
    public interface IChartService
    {
        Chart CreateLast7DaysChart(int limit);
    }
}
