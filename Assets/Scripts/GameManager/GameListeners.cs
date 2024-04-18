namespace ShootEmUp
{
    public interface IPauseGameListener : IGameManagerListener
    {
        void OnGamePaused(bool paused);
    }

    public interface IFinishGameListener : IGameManagerListener
    {
        void OnGameFinished();
    }

    public interface IStartGameListener : IGameManagerListener
    {
        void OnGameStarting();
    }

    public interface IUpdatable : IGameManagerListener
    {
        void OnUpdate(float deltaTime);
    }

    public interface IFixedUpdatable : IGameManagerListener
    {
        void OnFixedUpdate(float deltaTime);
    }

    //To simplify interfaces aggregation
    public interface IGameManagerListener
    {

    }
}