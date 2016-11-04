using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using Newtonsoft.Json;
using Random = System.Random;

public class Controls : MonoBehaviour
{
    [SerializeField]
    private string _inputString;

    void Update()
    {
        if (Input.inputString.Length > 0 && char.IsDigit(Input.inputString[0]))
        {
            _inputString += Input.inputString[0];
            SnackBar.ShowMessage(_inputString, 3);
        }

        if (Input.GetKeyDown(KeyCode.Minus) || Input.GetKeyDown(KeyCode.KeypadMinus))
        {
            var controller = BarSelector.CurrentlySelected.GetComponent<CharacterBarController>();
            int newHpValue = controller.Character.Hp - int.Parse(_inputString);
            StartCoroutine(AlterHp(controller.Character.CharId, newHpValue));
            _inputString = string.Empty;
            controller.Character.Hp = newHpValue;
        }

        if (Input.GetKeyDown(KeyCode.Plus) || Input.GetKeyDown(KeyCode.KeypadPlus))
        {
            var controller = BarSelector.CurrentlySelected.GetComponent<CharacterBarController>();
            int newHpValue = controller.Character.Hp + int.Parse(_inputString);
            StartCoroutine(AlterHp(controller.Character.CharId, newHpValue));
            _inputString = string.Empty;
            controller.Character.Hp = newHpValue;
        }
    }

    public void FillHp()
    {
        StartCoroutine(ExecuteAction("/guild/1/actions/fillhp"));

        foreach (var bar in CharacterBarController.Controllers)
        {
            bar.Character.Hp = bar.Character.MaxHp;
        }
    }

    public void RandHp()
    {
        StartCoroutine(ExecuteAction("/guild/1/actions/randhp"));

        var rand = new Random();

        foreach (var bar in CharacterBarController.Controllers)
        {
            bar.Character.Hp = rand.Next(bar.Character.MaxHp);
        }
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

    IEnumerator AlterHp(uint charId, int newHpValue)
    {
        WWWForm form = new WWWForm();
        form.AddField("hp", newHpValue);

        WWW www = new WWW(Rest.ServerAddress + "/character/" + charId + "/edit", form);

        yield return www;

        if (www.error == null)
        {
            Debug.Log("OK " + www.text);
        }
        else
        {
            Debug.LogError("ERR " + www.error);
        }
    }
}
