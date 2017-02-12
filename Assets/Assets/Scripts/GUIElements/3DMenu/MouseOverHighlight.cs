using UnityEngine;
using System.Collections;

public class MouseOverHighlight : MonoBehaviour {

    public Color highlightColor;
    public bool Enabled = true;
    public GameObject[] objectParts;

    private Color[] _defaulColors;

	// Use this for initialization
	void Start () {
        _defaulColors = new Color[objectParts.Length];

        if (objectParts.Length > 0)
        {
            for (int i = 0; i < objectParts.Length; ++i)
            {
                _defaulColors[i] = objectParts[i].GetComponent<Renderer>().material.GetColor("_Color");
            }
        }
	}

    public void OnMouseEnter()
    {
        if(Enabled)
            Highlight(true);
    }

    public void OnMouseExit()
    {
        if(Enabled)
            Highlight(false);
    }

    private void Highlight(bool glow)
    {

        if (objectParts.Length > 0)
        {
            for (int i = 0; i < objectParts.Length; ++i)
            {
                if (glow)
                {
                    objectParts[i].GetComponent<Renderer>().material.SetColor("_Color", highlightColor);
                }
                else
                {
                    objectParts[i].GetComponent<Renderer>().material.SetColor("_Color", _defaulColors[i]);
                }

            }
        }
    }
}
