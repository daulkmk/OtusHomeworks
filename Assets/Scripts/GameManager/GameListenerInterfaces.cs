namespace ShootEmUp
{
    public interface IPauseGameListener
    {
        void OnGamePaused(bool paused);
    }

    public interface IFinishGameListener
    {
        void OnGameFinished();
    }

    public interface IStartGameListener
    {
        void OnGameStarting();
    }

    public interface IUpdatable
    {
        void OnUpdate(float deltaTime);
    }

    public interface IFixedUpdatable
    {
        void OnFixedUpdate(float deltaTime);
    }
}
