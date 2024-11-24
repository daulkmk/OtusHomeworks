using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SessionsView : MonoBehaviour
{
    [SerializeField] private SessionView _sessionsViewPrefab;
    [SerializeField] private Transform _sessionsContainer;

    [SerializeField] private Text _activeSession;

    public void Initialize(IEnumerable<(string start, string end)> sessions)
    {
        foreach (var (start, end) in sessions)
        {
            var view = CreateSessionView();
            view.SetValues(start, end);
        }
    }

    public void UpdateActiveSession(string session)
    {
        _activeSession.text = session;
    }

    private SessionView CreateSessionView()
    {
        var view = GameObject.Instantiate(_sessionsViewPrefab, _sessionsContainer);
        view.gameObject.SetActive(true);

        return view;
    }
}
