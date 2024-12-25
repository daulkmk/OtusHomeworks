using System;
using Game.GameEngine.Mechanics;
using Lessons.MetaGame.Inventory;

//KISS: all effects in one class
public class PlayerEffector : IComponent_Effector
{
    private readonly Player _player;

    public event Action<IEffect> OnApplied;
    public event Action<IEffect> OnDiscarded;

    public PlayerEffector(Player player)
    {
        _player = player;
    }

    public void Apply(IEffect effect)
    {
        if (effect.TryGetParameter(EffectId.DAMAGE, out int damage))
        {
            _player.Damage += damage;
        }
        else if (effect.TryGetParameter(EffectId.HIT_POINTS, out int hitPoints))
        {
            _player.MaxHitPoints += hitPoints;
        }
        else if (effect.TryGetParameter(EffectId.MOVE_SPEED, out float speed))
        {
            _player.Speed += speed;
        }
    }

    public void Discard(IEffect effect)
    {
        if (effect.TryGetParameter(EffectId.DAMAGE, out int damage))
        {
            _player.Damage -= damage;
        }
        else if (effect.TryGetParameter(EffectId.HIT_POINTS, out int hitPoints))
        {
            _player.MaxHitPoints -= hitPoints;
        }
        else if (effect.TryGetParameter(EffectId.MOVE_SPEED, out float speed))
        {
            _player.Speed -= speed;
        }
    }
}
