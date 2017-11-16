using SkiaSharp;

namespace Gym.Services
{
    public class ColorService : IColorService
    {
        public SKColor Resolve(int value, int limit)
        {
            return value >= limit ? SKColors.Green : SKColors.Orange;
        }
    }
}
