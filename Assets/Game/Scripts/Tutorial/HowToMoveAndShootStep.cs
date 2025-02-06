using Lessons.Lesson_Components;
using Lessons.Lesson_Components.UI;
using UnityEngine;

namespace Tutorial
{
    public class HowToMoveAndShootStep : TutorialStep
    {
        private readonly ZombiesSpawner _zombiesSpawner;
        private readonly PlayerCharacterController _playerController;

        public override Step Step => Step.HowToMoveAndShoot;

        private bool _moved;
        private bool _shooted;

        public HowToMoveAndShootStep(ZombiesSpawner zombiesSpawner, PlayerCharacterController playerController, TutorialScreen tutorialScreen)
            : base(tutorialScreen)
        {
            _zombiesSpawner = zombiesSpawner;
            _playerController = playerController;
        }

        public override void Start()
        {
            _tutorialScreen.Show("Move with WASD\nShoot with LMB");
            _zombiesSpawner.SpawnEnabled = false;
            _playerController.OnMoveDirectionChanged += OnMoveDirectionChanged;
            _playerController.OnShootRequested += OnShootRequested;

            FireStartEvent();
        }

        private void OnShootRequested()
        {
            _shooted = true;
            CheckComplition();
        }

        private void OnMoveDirectionChanged(Vector3 vector)
        {
            _moved = true;
            CheckComplition();
        }

        private void CheckComplition()
        {
            if (_moved && _shooted)
                FireCompleteEvent();
        }

        public override void End()
        {
            _tutorialScreen.Hide();
            _zombiesSpawner.SpawnEnabled = true;
            _playerController.OnMoveDirectionChanged -= OnMoveDirectionChanged;
            _playerController.OnShootRequested -= OnShootRequested;
        }
    }
}