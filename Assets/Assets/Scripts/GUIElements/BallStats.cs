using UnityEngine;
using System.Collections;

public class BallStats : GUIElement {

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
        _frameRect = new Rect(frameMargin / 2, frameMargin / 2, width - frameMargin, height - frameMargin);
        _labelRect = new Rect(_frameRect.x + xLabelMargin / 2, _frameRect.y + yLabelMargin, _frameRect.width - xLabelMargin, _frameRect.height - yLabelMargin);
    }
	
	// Update is called once per frame
	void Update () {
        base.Update();
	}

    override public void OnGUIDraw()
    {
        GUI.Window(_windowId, _windowRect, BallStatsWindow, text);
    }

    private void BallStatsWindow(int id)
    {
        GameObject activeBall = null;

        if (manager != null)
            activeBall = manager.getActiveBall();

        text = string.Empty;
        GUI.contentColor = Color.black;
        string content = string.Empty;

        if (activeBall != null)
        {
            Material mat = activeBall.GetComponent<Material>();

            if (mat != null)
                GUI.contentColor = mat.color;

            SlowMovement mov = activeBall.GetComponent<SlowMovement>();

            if (mov != null)
                content = System.Math.Round(mov.TimeDilation, 1).ToString();

            text = activeBall.name;

        }

        GUI.Label(_labelRect, content);



    }
}
