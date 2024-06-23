using SaveLoad;
using Cysharp.Threading.Tasks;

public abstract class SaveLoader<TData> : ISaveLoader
{
    public string RepositoryKey => GetType().Name;

    protected readonly IGameRepository _repository;

    public SaveLoader(IGameRepository repository)
    {
        _repository = repository;
    }

    protected abstract void LoadWithoutData();
    protected abstract void LoadWithData(TData data);
    protected abstract TData CreateData();

    public async UniTask Load()
    {
        if (await _repository.ContainsKey(RepositoryKey))
        {
            var data = await _repository.Load<TData>(RepositoryKey);
            LoadWithData(data);
        }
        else
            LoadWithoutData();
    }

    public UniTask Save()
    {
        var data = CreateData();
        return _repository.Save(RepositoryKey, data);
    }
}
