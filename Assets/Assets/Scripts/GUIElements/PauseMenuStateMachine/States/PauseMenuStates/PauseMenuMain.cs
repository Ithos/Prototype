using UnityEngine;
using System.Collections;

public class PauseMenuMain : PauseMenuState {

    private int _width = 200, _height = 200;

    private string[] _buttonTexts = { "Continue", "Options", "Credits", "Reset"};

    private GameManager _gameManager = null;

    public PauseMenuMain(PauseMenuStateManager stateManager) : base(stateManager)
    {
    }

    public PauseMenuMain(PauseMenuStateManager stateManager, string gameManagerTag,
        string buttonText1, string buttonText2, string buttonText3, string buttonText4,
        int width, int height) : base(stateManager)
    {
        if(buttonText1 != null && buttonText1 != "")
            _buttonTexts[0] = buttonText1;
        if (buttonText2 != null && buttonText2 != "")
            _buttonTexts[1] = buttonText2;
        if (buttonText3 != null && buttonText3 != "")
            _buttonTexts[2] = buttonText3;
        if (buttonText4 != null && buttonText4 != "")
            _buttonTexts[3] = buttonText4;

        _width = width;
        _height = height;

        if(_gameManager == null)
            _gameManager = GameObject.FindGameObjectWithTag(gameManagerTag).GetComponent<GameManager>();

    }

	override public void OnGUI()
    {
        GUI.color = _stateManager.menuColor;
        MainPauseMenu();
    }

    private void MainPauseMenu()
    {
        BeginPage(_width, _height);

        if(GUILayout.Button(_buttonTexts[0]))
        {
            _stateManager.UnPauseGame();
        }

        if (GUILayout.Button(_buttonTexts[1]))
        {
            _stateManager.setState(PauseMenuStateNames.StateNames[1]);
        }
        if (GUILayout.Button(_buttonTexts[2]))
        {
            _stateManager.setState(PauseMenuStateNames.StateNames[2]);
        }
        if (GUILayout.Button(_buttonTexts[3]))
        {
            if(_gameManager != null)
                _gameManager.resetScene();

            _stateManager.UnPauseGame();
        }

        EndPage();
    }
}
