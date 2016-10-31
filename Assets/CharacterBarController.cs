using UnityEngine;
using UnityEngine.UI;

public class CharacterBarController : MonoBehaviour
{
    private Character _character;

    [SerializeField] private Text _name;

    [SerializeField] private Image _healthBar;

    [SerializeField] private Text _healthText;

    [SerializeField] private Image _leaderBadge;

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
        _healthBar.fillAmount = (float) _character.Hp/_character.MaxHp;
        _healthText.text = _character.Hp + "/" + _character.MaxHp;
        _leaderBadge.enabled = _character.GuildLeader;
    }

    public Character Character
    {
        get { return _character; }
        set { _character = value; }
    }
}
