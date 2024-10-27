using Client.Systems;
using UnityEngine;

public class SpawnRequestSystemFactory : MonoBehaviour
{
    [SerializeField] private Transform _container;

    public SpawnRequestSystem Create()
    {
        return new SpawnRequestSystem(_container);
    }
}
