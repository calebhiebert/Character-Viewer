using UnityEngine;
using System.Collections;

public class Character
{
    public delegate void HpUpdated(int hp);
    public event HpUpdated OnHpChange;

    public delegate void MaxHpUpdated(int maxHp);
    public event MaxHpUpdated OnMaxHpChange;

    public delegate void NameUpdated(string name);
    public event NameUpdated OnNameChange;

    public delegate void GuildLeaderUpdated(bool isLeader);
    public event GuildLeaderUpdated OnGuildLeaderStatausChange;

    public delegate void AcUpdated(int ac);
    public event AcUpdated OnAcChanged;

    public delegate void ReUpdated(int re);
    public event ReUpdated OnReChanged;

    private uint _charId;
    private uint _ownerId;
    private uint _guildId;
    private int _ac;
    private int _gold;
    private int _hp;
    private int _maxHp;
    private int _init;
    private int _re;
    private int _xp;
    private string _name;
    private bool _guildLeader;

    public Character()
    {
    }

    public Character(uint charId, uint ownerId, uint guildId, int ac, int gold, int hp, int maxHp, int init, int re, int xp, string name)
    {
        _charId = charId;
        _ownerId = ownerId;
        _guildId = guildId;
        _ac = ac;
        _gold = gold;
        _hp = hp;
        _maxHp = maxHp;
        _init = init;
        _re = re;
        _xp = xp;
        _name = name;
    }

    public void CopyFrom(Character newData)
    {
        if (newData.Name != _name)
            Name = newData.Name;

        if (newData.Hp != _hp)
            Hp = newData.Hp;

        if (newData.MaxHp != _maxHp)
            MaxHp = newData.MaxHp;

        if (newData.GuildLeader != _guildLeader)
            GuildLeader = newData.GuildLeader;

        if (newData.Ac != _ac)
            Ac = newData.Ac;

        if (newData.Re != _re)
            Re = newData.Re;
    }

    public bool GuildLeader
    {
        get { return _guildLeader; }
        set
        {
            _guildLeader = value;
            if (OnGuildLeaderStatausChange != null)
                OnGuildLeaderStatausChange(value);
        }
    }

    public uint CharId
    {
        get { return _charId; }
        set { _charId = value; }
    }

    public uint OwnerId
    {
        get { return _ownerId; }
        set { _ownerId = value; }
    }

    public uint GuildId
    {
        get { return _guildId; }
        set { _guildId = value; }
    }

    public int Ac
    {
        get { return _ac; }
        set
        {
            _ac = value;
            if (OnAcChanged != null)
                OnAcChanged(value);
        }
    }

    public int Gold
    {
        get { return _gold; }
        set { _gold = value; }
    }

    public int Hp
    {
        get { return _hp; }
        set
        {
            _hp = value;
            if (OnHpChange != null)
                OnHpChange(value);
        }
    }

    public int MaxHp
    {
        get { return _maxHp; }
        set
        {
            _maxHp = value;
            if (OnMaxHpChange != null)
                OnMaxHpChange(value);
        }
    }

    public int Init
    {
        get { return _init; }
        set { _init = value; }
    }

    public int Re
    {
        get { return _re; }
        set
        {
            _re = value;
            if (OnReChanged != null)
                OnReChanged(value);
        }
    }

    public int Xp
    {
        get { return _xp; }
        set { _xp = value; }
    }

    public string Name
    {
        get { return _name; }
        set
        {
            _name = value;
            if (OnNameChange != null)
                OnNameChange(value);
        }
    }
}
