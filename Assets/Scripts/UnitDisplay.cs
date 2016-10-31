using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

public class UnitDisplay : MonoBehaviour
{

    private const string svrAddr = "http://localhost:8080";

    public uint guildId;
    public float updateRate = 1;

    public Text GuildNameText;

    public GameObject CharBar;
    public float barHeight = 50;

    private List<GameObject> _activeBars;

    private Guild _guild;

    private float _lastUpdate = 0;

	// Use this for initialization
	void Start () {
        _activeBars = new List<GameObject>();
	    StartCoroutine(UpdateGuildInfo());
	}

    void Update()
    {
        if (Time.time - _lastUpdate >= updateRate)
        {
            StartCoroutine(GetCharacters());
            StartCoroutine(UpdateGuildInfo());
            _lastUpdate = Time.time;
        }
    }

    private void Populate(List<Character> characters)
    {
        var newCharacters = new List<Character>();

        foreach (var character in characters)
            newCharacters.Add(character);

        foreach (var character in characters)
        {
            if (_guild != null && _guild.Leader == character.CharId)
                character.GuildLeader = true;

            foreach (var bar in _activeBars)
            {
                var controller = bar.GetComponent<CharacterBarController>();

                if (character.CharId == controller.Character.CharId)
                {
                    controller.Character.CopyFrom(character);
                    newCharacters.Remove(character);
                }
            }
        }

        foreach (var newChar in newCharacters)
        {
            var bar = Instantiate(CharBar);
            bar.GetComponent<CharacterBarController>().Character = newChar;
            _activeBars.Add(bar);
        }

        for (var i = 0; i < _activeBars.Count; i++)
        {
            _activeBars[i].transform.SetParent(transform, false);
            _activeBars[i].GetComponent<RectTransform>().anchoredPosition = new Vector2(0, (i * -55) - 5);
        }
    }

    IEnumerator GetCharacters()
    {
        string url = string.Format("{0}/guild/{1}/characters", svrAddr, guildId);

        WWW www = new WWW(url);
        yield return www;

        Populate(JsonConvert.DeserializeObject<List<Character>>(www.text));
    }

    IEnumerator UpdateGuildInfo()
    {
        string url = string.Format("{0}/guild/{1}", svrAddr, guildId);

        WWW www = new WWW(url);

        yield return www;

        _guild = JsonConvert.DeserializeObject<Guild>(www.text);

        if (_guild != null)
        {
            GuildNameText.text = _guild.GuildName;
        }
    }
}
