using UnityEngine;
using System.Collections;

public abstract class MenuState {

    public float[] BackButtonMargins = { 20, 50, 50, 20 };

    public string BackButtonText = "Back";

    protected MenuStateManager _stateManager = null;

    public MenuState(MenuStateManager stateManager)
    {
        _stateManager = stateManager;
    }

    abstract public void OnGUI();

    virtual protected void BeginPage(int width, int height)
    {
        GUILayout.BeginArea(new Rect((Screen.width - width) / 2, (Screen.height - height) / 2, width, height));
    }

    virtual protected void EndPage()
    {
        GUILayout.EndArea();
    }

    virtual protected bool ShowBackButton()
    {
        return GUI.Button(new Rect(BackButtonMargins[0], Screen.height - BackButtonMargins[1],
            BackButtonMargins[2], BackButtonMargins[3]),
            BackButtonText);
    }
}
