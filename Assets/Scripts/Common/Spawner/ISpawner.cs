namespace ShootEmUp
{
    public interface ISpawner<T>
    {
        bool Initialized { get; }

        void Initialize();

        T Spawn();
        void Despawn(T obj);
    }
}