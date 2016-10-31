using UnityEngine;
using System.Collections;

public class Guild
{
    private uint guildId;
    private string guildName;
    private uint leader;

    public Guild()
    {
    }

    public Guild(uint guildId, string guildName, uint leader)
    {
        this.guildId = guildId;
        this.guildName = guildName;
        this.leader = leader;
    }

    public uint GuildId
    {
        get { return guildId; }
        set { guildId = value; }
    }

    public string GuildName
    {
        get { return guildName; }
        set { guildName = value; }
    }

    public uint Leader
    {
        get { return leader; }
        set { leader = value; }
    }
}
