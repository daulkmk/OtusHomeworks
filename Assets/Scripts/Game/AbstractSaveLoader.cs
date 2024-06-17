using SaveLoad;
using Cysharp.Threading.Tasks;

public abstract class AbstractSaveLoader<TSnapshot> : ISaveLoader
{
    public string RepositoryKey => GetType().Name;

    protected readonly IGameRepository _repository;

    public AbstractSaveLoader(IGameRepository repository)
    {
        _repository = repository;
    }

    protected abstract void LoadWithoutSnapshot();
    protected abstract void LoadWithSnapshot(TSnapshot snapshot);
    protected abstract TSnapshot CreateSnapshot();

    public async UniTask Load()
    {
        if (await _repository.ContainsKey(RepositoryKey))
        {
            var snapshot = await _repository.Load<TSnapshot>(RepositoryKey);
            LoadWithSnapshot(snapshot);
        }
        else
            LoadWithoutSnapshot();
    }

    public UniTask Save()
    {
        var snapshot = CreateSnapshot();
        return _repository.Save(RepositoryKey, snapshot);
    }
}
