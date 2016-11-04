using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SnackBar : MonoBehaviour
{

    private static SnackBar _snackBar;

    [SerializeField] private Gradient _fade;

    [SerializeField] private float _fadeTime;

    [SerializeField] private Image _panelImage;

    [SerializeField] private Text _text;

    private float _lastModified;

	void Awake ()
	{
	    _snackBar = this;
	}
	
	void Update ()
    {
        if (Time.time - _lastModified <= _fadeTime)
        {
            var color = _fade.Evaluate((Time.time - _lastModified)/_fadeTime);

            _panelImage.color = new Color(_panelImage.color.r, _panelImage.color.g, _panelImage.color.b, color.a);
            _text.color = color;
        }
    }

    public static void ShowMessage(string message, float duration = 2.5f)
    {
        _snackBar._lastModified = Time.time;
        _snackBar._text.text = message;
        _snackBar._fadeTime = duration;
    }
}
