using UnityEngine;
using System.Collections;

public class PlayerPoints : GUIElement
{
    public float frameMargin = 10.0f;
    public float xLabelMargin = 1.0f;
    public float yLabelMargin = 1.0f;

    private Rect _frameRect;
    private Rect _labelRect;
    private Rect _innerRect;




    // Use this for initialization
    void Start () {
        base.Start();

        _innerRect = new Rect(0.0f, 0.0f, width, height);
        _frameRect = new Rect( frameMargin / 2, frameMargin / 2, width - frameMargin , height - frameMargin);
        _labelRect = new Rect(_frameRect.x + xLabelMargin/2, _frameRect.y + yLabelMargin, _frameRect.width - xLabelMargin, _frameRect.height - yLabelMargin);
        
    }
	
	// Update is called once per frame
	void Update () {
        base.Update();
	}

    override public void OnGUIDraw()
    {
        GUI.Window(_windowId, _windowRect, scoreWindow, text);
    }

    private void scoreWindow(int id)
    {
        GUI.contentColor = Color.black;
        //GUI.DrawTexture(_innerRect, ) // Draw Frame for the scoreboard
        //GUI.DrawTexture(_frameRect, ) //Draw Black Rectangle

        //GUI.contentColor = Color.white;

        GUI.Label(_labelRect, manager.Score.ToString());

    }

}
