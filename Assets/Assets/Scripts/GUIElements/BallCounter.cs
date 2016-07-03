using UnityEngine;
using System.Collections;

public class BallCounter : GUIElement
{
    public Texture counterTexture = null;

    protected Rect _innerRect;


    // Use this for initialization
    void Start () {
        base.Start();
        _innerRect = new Rect(0.0f, 0.0f, width, height);
    }
	
	// Update is called once per frame
	void Update () {
        base.Update();
    }

    override public void OnGUIDraw()
    {
        //GUI.Window(_windowId, _windowRect, BallCounterWindow, "Balls");

        BallCounterWindow();
    }

    private void BallCounterWindow()
    {
        if (counterTexture != null && manager != null)
        {
            GUI.DrawTextureWithTexCoords(
                new Rect(_innerRect.x, _innerRect.y, _innerRect.width * manager.BallsLeft, _innerRect.height), 
                counterTexture, new Rect(0.0f, 0.0f, manager.BallsLeft, 1.0f)); //Tex coords depend on the numbers of balls left
        }
    }


}
