using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using Newtonsoft.Json;

public class Controls : MonoBehaviour {

    public void FillHp()
    {
        StartCoroutine(ExecuteAction("/guild/1/actions/fillhp"));
    }

    public void RandHp()
    {
        StartCoroutine(ExecuteAction("/guild/1/actions/randhp"));
    }

    IEnumerator ExecuteAction(string url)
    {
        var form = new WWWForm();
        form.AddField("dummy", "data");

        var www = new WWW(Rest.ServerAddress + url, form);

        yield return www;

        if (www.error != null)
        {
            Debug.LogError(www.error);
        }
        else
        {
            UnitDisplay.ExecuteCharUpdate(JsonConvert.DeserializeObject<List<Character>>(www.text));
        }
    }
}
