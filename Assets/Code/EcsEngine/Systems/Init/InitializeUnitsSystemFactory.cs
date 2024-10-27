using Leopotam.EcsLite.Entities;
using UnityEngine;
using Client;

public class InitializeUnitsSystemFactory : MonoBehaviour
{
    [SerializeField] private Entity _prefabUnit1;
    [SerializeField] private Entity _prefabUnit2;

    [SerializeField] private int _teamSize = 1;
    [SerializeField] private int _lineLength = 1;
    [SerializeField] private float _offset = 2f;
    

    public InitializeUnitsSystem Create()
    {
        return new InitializeUnitsSystem(
            unitPrefabTeam1: _prefabUnit1,
            unitPrefabTeam2: _prefabUnit2,
            teamSize: _teamSize,
            offset: _offset,
            lineLength: _lineLength
        );
    }
}
