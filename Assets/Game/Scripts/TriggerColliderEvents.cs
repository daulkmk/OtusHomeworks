using System;
using UnityEngine;

public class TriggerColliderEvents : MonoBehaviour
{
    public event Action<Collider> OnTriggerEntered;
    public event Action<Collider> OnTriggerExited;

    private void OnTriggerEnter(Collider collider)
    {
        OnTriggerEntered?.Invoke(collider);
    }

    private void OnTriggerExit(Collider collider)
    {
        OnTriggerExited?.Invoke(collider);
    }
}
