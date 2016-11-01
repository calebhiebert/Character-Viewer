using UnityEngine;
using UnityEngine.UI;

public class CharacterBarController : MonoBehaviour
{
    private Character _character;

    [SerializeField] private Text _name;

    [SerializeField] private Image _healthBar;

    [SerializeField] private Image _healthBarEffects;

    [SerializeField] private Text _healthText;

    [SerializeField] private Image _leaderBadge;

    private float _hpFillTarget;

    void Update()
    {
        if (_healthBar.fillAmount < _hpFillTarget)
        {
            _healthBar.fillAmount = Mathf.Lerp(_healthBar.fillAmount, _hpFillTarget, Time.deltaTime*12);
            _healthBarEffects.fillAmount = _hpFillTarget;
        } else if (_healthBar.fillAmount > _hpFillTarget)
        {
            _healthBarEffects.fillAmount = Mathf.Lerp(_healthBar.fillAmount, _hpFillTarget, Time.deltaTime*12);
            _healthBar.fillAmount = _hpFillTarget;
        }
    }

    void Start()
    {
        if(_character != null)
            UpdateFromCharacter();
        else
            Debug.LogError("Bar was created with no character!");

        _character.OnNameChange += charName => { _name.text = charName; };
        _character.OnGuildLeaderStatausChange += isLeader => { UpdateFromCharacter(); };
        _character.OnHpChange += hp => { UpdateFromCharacter(); };
        _character.OnMaxHpChange += maxHp => { UpdateFromCharacter(); };
    }

    private void UpdateFromCharacter()
    {
        _name.text = _character.Name;
        _hpFillTarget = (float) _character.Hp/_character.MaxHp;
        _healthText.text = _character.Hp + "/" + _character.MaxHp;
        _leaderBadge.enabled = _character.GuildLeader;
    }

    public Character Character
    {
        get { return _character; }
        set { _character = value; }
    }
}
