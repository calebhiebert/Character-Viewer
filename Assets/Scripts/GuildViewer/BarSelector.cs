using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.UI;

public class BarSelector : MonoBehaviour {

    private static readonly List<BarSelector> Selectors = new List<BarSelector>();
    private static BarSelector _currentlySelected = null;

    private delegate void SelectionStatusChanged(BarSelector currentlySelected);
    private static event SelectionStatusChanged OnSelectionChanged;

    [SerializeField] private bool _selected;

    [SerializeField] private Image _selectionIndicator;

	void Awake () {
	    Selectors.Add(this);

	    OnSelectionChanged += selected => { _selected = selected == this; };
	}

    void OnDestroy()
    {
        Selectors.Remove(this);
    }

    void Update()
    {
        _selectionIndicator.fillAmount = Mathf.Lerp(_selectionIndicator.fillAmount, _selected ? 1 : 0, Time.deltaTime*12);
    }

    public void Select()
    {
        Select(this);
    }

    public static void Select(BarSelector selector)
    {
        _currentlySelected = selector;

        if (OnSelectionChanged != null)
            OnSelectionChanged(selector);
    }

    public bool Selected
    {
        get { return _selected; }
        set
        {
            Select(value ? this : null);
        }
    }

    public static List<BarSelector> GetSelectors
    {
        get { return Selectors; }
    }

    public static BarSelector CurrentlySelected
    {
        get { return _currentlySelected; }
    }
}
