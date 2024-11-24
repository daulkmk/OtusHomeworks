using UnityEngine;
using UnityEngine.UI;

public class SessionView : MonoBehaviour
{
    [SerializeField] private Text _start;
    [SerializeField] private Text _end;

    public void SetValues(string start, string end)
    {
        _start.text = start;
        _end.text = end;
    }
}
