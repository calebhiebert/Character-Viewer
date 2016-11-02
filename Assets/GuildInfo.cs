using System;
using UnityEngine;
using System.Collections;
using Assets.Scripts;
using Newtonsoft.Json;
using UnityEngine.UI;

public class GuildInfo : MonoBehaviour
{
    [SerializeField] private GameObject _locationPanel;

    [SerializeField] private Text _guildNameText;

    [SerializeField] private Text _guildLocationText;

    [SerializeField] private float _updateRate = 5;

    public static Guild Guild;

    private float _lastUpdate = 0;

	void Start ()
	{
	    StartCoroutine(UpdateGuildInfo(1));
	}
	
	void Update () {
	    if (Time.time - _lastUpdate > _updateRate)
	    {
	        StartCoroutine(UpdateGuildInfo(1));
	    }
	}

    private IEnumerator UpdateGuildInfo(uint guildId)
    {
        string url = string.Format("{0}/guild/{1}", Rest.ServerAddress, guildId);

        var www = new WWW(url);

        yield return www;

        if (www.error == null)
        {
            Guild = JsonConvert.DeserializeObject<Guild>(www.text);

            if (Guild != null)
            {
                _guildNameText.text = Guild.GuildName;
                _guildLocationText.text = Guild.Location;

                _locationPanel.SetActive(!Guild.Location.Equals("Unknown"));
            }
            else
            {
                Debug.LogError("Guild parsing error!");
            }
        }
        else
        {
            Debug.LogError(www.error);
        }
    }
}
