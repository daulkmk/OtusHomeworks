using Client.Components;
using Leopotam.EcsLite.Entities;
using UnityEngine;

namespace Client.Helpers
{
    public sealed class CharacterFireController : MonoBehaviour
    {
        [SerializeField] private Entity _character;

        private void Update()
        {
            if (FireInput.IsFirePressDown())
            {
                _character.SetData(new FireRequest());
            }
        }
    }
}
