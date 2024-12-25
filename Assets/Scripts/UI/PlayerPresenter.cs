using Lessons.MetaGame.Inventory;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

public class PlayerPresenter : MonoBehaviour
{
    [SerializeField] private Text _speed;
    [SerializeField] private Text _maxHP;
    [SerializeField] private Text _damage;

    private Player _player;

    [Inject]    
    public void Construct(Player player)
    {
        _player = player;

        _player.OnSpeedChanged += OnSpeedChanged;
        _player.OnMaxHitPointsChanged += OnMaxHitPointsChanged;
        _player.OnDamageChanged += OnDamageChanged;

        OnSpeedChanged(_player.Speed);
        OnMaxHitPointsChanged(_player.MaxHitPoints);
        OnDamageChanged(_player.Damage);
    }

    private void OnDamageChanged(int value)
    {
        _damage.text = "DAMAGE: " + value.ToString();
    }

    private void OnMaxHitPointsChanged(int value)
    {
        _maxHP.text = "MAX HP: " + value.ToString();
    }

    private void OnSpeedChanged(float value)
    {
        _speed.text = "SPEED: " + value.ToString(".00");
    }

    private void OnDestroy()
    {
        if (_player != null)
        {
            _player.OnSpeedChanged -= OnSpeedChanged;
            _player.OnMaxHitPointsChanged -= OnMaxHitPointsChanged;
            _player.OnDamageChanged -= OnDamageChanged;
        }
    }
}
