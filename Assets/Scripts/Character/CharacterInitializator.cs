using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShootEmUp
{
    public class CharacterInitializator : MonoBehaviour
    {
        [SerializeField] private Character _character;
        [Space]
        [SerializeField] private InputMovementController _inputMovement;
        [SerializeField] private InputWeaponController _inputWeapon;

        //TODO injection
        [SerializeField] private InputManager _inputManager;
        [SerializeField] private BulletSystem _bulletSystem;

        private void Awake()
        {
            _character.Initialize(_bulletSystem);
            _inputMovement.Initialize(_inputManager, _character.MoveComponent);
            _inputWeapon.Initialize(_inputManager, _character.WeaponComponent);
        }
    }
}