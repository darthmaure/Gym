namespace Gym.Services
{
    public interface IColorService
    {
        SkiaSharp.SKColor Resolve(int value, int limit);
    }
}
