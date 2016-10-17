using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PauseMenuMain : PauseMenuState {

    private int _width = 200, _height = 200;

    private string[] _buttonTexts = { "Continue", "Options", "Credits", "Reset", "Main Menu"};

    private GameManager _gameManager = null;

    private string _menuSceneName = "MainMenu_Scene";

    public PauseMenuMain(PauseMenuStateManager stateManager) : base(stateManager)
    {
    }

    public PauseMenuMain(PauseMenuStateManager stateManager, string gameManagerTag, string menuSceneName, 
        string buttonText1, string buttonText2, string buttonText3, string buttonText4, string buttonText5,
        int width, int height) : base(stateManager)
    {
        if(!string.IsNullOrEmpty(buttonText1))
            _buttonTexts[0] = buttonText1;
        if (!string.IsNullOrEmpty(buttonText2))
            _buttonTexts[1] = buttonText2;
        if (!string.IsNullOrEmpty(buttonText3))
            _buttonTexts[2] = buttonText3;
        if (!string.IsNullOrEmpty(buttonText4))
            _buttonTexts[3] = buttonText4;
        if (!string.IsNullOrEmpty(buttonText5))
            _buttonTexts[4] = buttonText5;

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
        if(GUILayout.Button(_buttonTexts[4]))
        {
            if (_gameManager != null)
                _gameManager.resetScene();

            SceneManager.LoadScene(_menuSceneName);

            _stateManager.UnPauseGame();
        }

        EndPage();
    }
}
