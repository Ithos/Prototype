using UnityEngine;
using System.Collections;

public class PauseMenuCredits : PauseMenuState {

    private int[] _size = { 300, 300};

    private string[] _credits =
    {
        "Pause menu by Ithos",
        "based on a script from Phil Chu",
        "http://wiki.unity3d.com/index.php?title=PauseMenu"
    };

    private Texture[] _creditIcons = null;

    public PauseMenuCredits(PauseMenuStateManager stateManager) : base(stateManager)
    {
    }

    public PauseMenuCredits(PauseMenuStateManager stateManager, string[] credits, Texture[] icons, int width, int height) : 
        base(stateManager)
    {
        _credits = credits;
        _creditIcons = icons;
        _size[0] = width;
        _size[1] = height;
    }

    override public void OnGUI()
    {
        GUI.color = _stateManager.menuColor;
        showCredits();
    }

    private void showCredits()
    {
        BeginPage(_size[0], _size[1]);

        foreach(string credit in _credits)
        {
            GUILayout.Label(credit);
        }

        foreach(Texture icon in _creditIcons)
        {
            GUILayout.Label(icon);
        }

        EndPage();

        if(ShowBackButton())
        {
            _stateManager.setState(PauseMenuStateNames.StateNames[0]);
        }
    }

}
