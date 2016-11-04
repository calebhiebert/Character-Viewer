using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts;
using Newtonsoft.Json;

public class UnitDisplay : MonoBehaviour
{
    private static UnitDisplay _display;

    public uint guildId;
    public float updateRate = 1;

    public GameObject CharBar;
    public float barHeight = 50;

    private List<GameObject> _activeBars;

    private float _lastUpdate = 0;

	void Awake()
	{
	    _display = this;
        _activeBars = new List<GameObject>();
	}

    void Update()
    {
        if (Time.time - _lastUpdate >= updateRate)
        {
            StartCoroutine(GetCharacters());
            _lastUpdate = Time.time;
        }
    }

    public void Populate(List<Character> characters)
    {
        var newCharacters = new List<Character>();

        foreach (var character in characters)
            newCharacters.Add(character);

        foreach (var character in characters)
        {
            if (GuildInfo.Guild != null && GuildInfo.Guild.Leader == character.CharId)
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
            _activeBars[i].GetComponent<CharacterBarController>().TargetLocation = new Vector2(0, (i * -45) - 5);
        }
    }

    public static void ExecuteCharUpdate(List<Character> characters)
    {
        _display.Populate(characters);
    }

    private IEnumerator GetCharacters()
    {
        string url = string.Format("{0}/guild/{1}/characters", Rest.ServerAddress, guildId);

        WWW www = new WWW(url);
        yield return www;

        if (www.error == null)
        {
            var chars = JsonConvert.DeserializeObject<List<Character>>(www.text);

            if(chars != null)
                Populate(chars);
            else
                Debug.LogError("Character parsing error");
        }
    }
}