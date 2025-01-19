using System;

public class ConverterStats
{
    public int LoadCapacity { get; private set; } = 12;
    public int UnloadCapacity { get; private set; } = 24;
    public float Speed { get; private set; } = 0.25f;

    public event Action<int> OnLoadCapacityChanged;
    public event Action<int> OnUnloadCapacityChanged;
    public event Action<float> OnSpeedChanged;

    public void SetLoadCapacity(int value)
    {
        LoadCapacity = value;
        OnLoadCapacityChanged?.Invoke(LoadCapacity);
    }

    public void SetUnloadCapacity(int value)
    {
        UnloadCapacity = value;
        OnUnloadCapacityChanged?.Invoke(UnloadCapacity);
    }

    public void SetSpeed(float value)
    {
        Speed = value;
        OnSpeedChanged?.Invoke(Speed);
    }
}
