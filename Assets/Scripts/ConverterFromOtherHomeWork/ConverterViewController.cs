using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using VContainer.Unity;

public class ConverterViewController : IConverterViewController, IInitializable, IDisposable
{
    private readonly ConverterView _view;
    private readonly Converter _converter;
    private CancellationTokenSource _progressCts;

    public event Action OnClick
    {
        add { _view.OnClick += value; }
        remove { _view.OnClick -= value;}
    }

    public ConverterViewController(ConverterView view, Converter converter)
    {
        _view = view;
        _converter = converter;
    }

    void IInitializable.Initialize()
    {
        _converter.OnInProgressChanged += OnInProgressChanged;
        _converter.OnLoadedResourcesChanged += OnLoadedResourcesChanged;
        _converter.OnUnloadedResourcesChanged += OnUnloadedResourcesChanged;

        _view.OnLoadResourceRequested += OnOnLoadResourceRequested;
        _view.OnUnloadResourceRequested += OnUnloadResourceRequested;
    }

    private void OnUnloadResourceRequested()
    {
        if (_converter.GetUnloadedResource())
        {
            UnityEngine.Debug.Log("Sell resource");
        }
    }

    private void OnOnLoadResourceRequested()
    {
        _converter.LoadResource();
    }

    private void OnUnloadedResourcesChanged()
    {
        _view.SetUnloadedCount(_converter.UnloadedResourcesCount);
    }

    private void OnLoadedResourcesChanged()
    {
        _view.SetLoadedCount(_converter.LoadedResourcesCount);
    }

    private void OnInProgressChanged()
    {
        _view.SetProgress(0);

        if (_converter.IsInProgress)
        {
            _view.Play();
            StartProgressRoutine();   
        }
        else
        {
            _view.Stop();
            CancelAndDisposeProgressRoutine();
        }
    }

    private void StartProgressRoutine()
    {
        CancelAndDisposeProgressRoutine();
        _progressCts = new CancellationTokenSource();
        UpdateProgressRoutine(_progressCts.Token).Forget();
    }

    private async UniTask UpdateProgressRoutine(CancellationToken cancellationToken)
    {
        while (!cancellationToken.IsCancellationRequested)
        {
            _view.SetProgress(_converter.Progress);

            bool canceled = await UniTask.NextFrame(cancellationToken: cancellationToken)
                .SuppressCancellationThrow();

            if (canceled)
                break;
        }
    }

    private void CancelAndDisposeProgressRoutine()
    {
        if (_progressCts != null)
        {
            _progressCts.Cancel();
            _progressCts.Dispose();
            _progressCts = null;
        }
    }

    void IDisposable.Dispose()
    {
        CancelAndDisposeProgressRoutine();

        _converter.OnInProgressChanged += OnInProgressChanged;
        _converter.OnLoadedResourcesChanged += OnLoadedResourcesChanged;
        _converter.OnUnloadedResourcesChanged += OnUnloadedResourcesChanged;
    }
}
