using UnityEngine;
using System.Collections;

public class EnergyBar : GUIElement
{
    public Texture backgroundTexture = null;
    public Texture fillerTexture = null;

    public float frameWidth = 10;

    private Rect _textureRect;

    // Use this for initialization
    void Start () {
        base.Start();
        _textureRect = new Rect(0, 0, 1, 1);
    }
	
	// Update is called once per frame
	void Update () {
        base.Update();
	}

    override public void OnGUIDraw()
    {
        EnergyWindow();
    }

    private void EnergyWindow()
    {
        if (manager != null )
        {
            float alpha = (manager.CurrentCharge / manager.secondsToCharge);
            float modHeight = height - frameWidth;
            GUI.DrawTexture(_windowRect, backgroundTexture);
            if (manager.CurrentCharge != 0)
            {
                GUI.DrawTexture(
                    new Rect(x + frameWidth/2, y + modHeight * (1 - alpha) + frameWidth/2, width - frameWidth, alpha * modHeight),
                    fillerTexture);
            }
        }
    }
}
