using UnityEngine;
using System.Collections;

public class Guild
{
    private uint guildId;
    private string guildName;
    private uint leader;
    private string location;

    public Guild()
    {
    }

    public Guild(uint guildId, string guildName, uint leader, string location)
    {
        this.guildId = guildId;
        this.guildName = guildName;
        this.leader = leader;
        this.location = location;
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

    public string Location
    {
        get { return location; }
        set { location = value; }
    }
}
