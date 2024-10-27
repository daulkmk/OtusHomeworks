using Client.Components;
using Leopotam.EcsLite.Entities;
using UnityEngine;

namespace Client.Helpers
{
    public sealed class CharacterMoveController : MonoBehaviour
    {
        [SerializeField] private Entity _character;
        
        private void Update()
        {
            Vector3 inputDirection = MoveInput.GetDirection();
            ref MoveDirection moveDirection = ref _character.GetData<MoveDirection>();
            moveDirection.Value = inputDirection;
        }
    }
}
