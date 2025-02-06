using Atomic.Objects;
using Lessons.Lesson_Components;
using Lessons.Lesson_Components.UI;

namespace Tutorial
{
    public class KillZombiesStep : TutorialStep
    {
        private readonly ZombiesSpawner _zombiesSpawner;

        public KillZombiesStep(TutorialScreen tutorialScreen, ZombiesSpawner zombiesSpawner)
            : base(tutorialScreen)
        {
            _zombiesSpawner = zombiesSpawner;
        }

        public override Step Step => Step.KillZombies;

        public override void Start()
        {
            _tutorialScreen.Show("Shoot the zombies or they will kill you");

            _zombiesSpawner.OnZombieDie += OnZombieDie;
        }

        private void OnZombieDie(AtomicEntity entity)
        {
            FireCompleteEvent();
        }

        public override void End()
        {
            _zombiesSpawner.OnZombieDie -= OnZombieDie;
            _tutorialScreen.Hide();
        }
    }
}