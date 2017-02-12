using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class TutorialManager : MonoBehaviour {

    public enum TutorialStates
    {
        LEFT_RIGHT,
        UP_DOWN,
        TURN_LEFT_RIGHT,
        PRESS_PAUSE,
        INCREASE_DECREASE_TIME,
        SPACE,
        HIT_OBJECT,
        TUTORIAL_ENDED,
        NONE
    }

    public enum KeyPressedStates
    {
        LEFT_PRESSED,
        RIGHT_PRESSED,
        UP_PRESSED,
        DOWN_PRESSED,
        INCREASE_PRESSED,
        DECREASE_PRESSED,
        NONE
    }

    public int[] StateToTexture = { 1, 2, 3, 4, 5, 6};

    public string InputManagerTag = "INPUT_MANAGER";
    public InputManager InputManager = null;

    public string TutorialUITag = "TUTORIAL_UI";
    public TutorialTextures TutorialUI = null;

    public GameObject[] TutorialClues; 

    private TutorialStates _state = TutorialStates.NONE;

    private KeyPressedStates _keyState = KeyPressedStates.NONE;

    private bool _stateIntialised = false;

    private GameManager _gameManager = null;
    public string GameManagerTag = "GAME_MANAGER";

    public string MenuSceneName = "MainMenu_Scene";




    // Use this for initialization
    void Start () {
	    if(InputManager == null)
        {
            InputManager = GameObject.FindGameObjectWithTag(InputManagerTag).GetComponent<InputManager>();
        }
        if(TutorialUI == null)
        {
            TutorialUI = GameObject.FindGameObjectWithTag(TutorialUITag).GetComponent<TutorialTextures>();
        }
        if (_gameManager == null)
            _gameManager = GameObject.FindGameObjectWithTag(GameManagerTag).GetComponent<GameManager>();

        _state = TutorialStates.LEFT_RIGHT;
        TutorialUI.SetActiveImage(StateToTexture[0]);
    }
	
	// Update is called once per frame
	void Update () {
	
        switch(_state)
        {
            case TutorialStates.LEFT_RIGHT:
                checkLeftRight();
                break;
            case TutorialStates.UP_DOWN:
                checkUpDown();
                break;
            case TutorialStates.TURN_LEFT_RIGHT:
                checkTurn();
                break;
            case TutorialStates.PRESS_PAUSE:
                checkPause();
                break;
            case TutorialStates.INCREASE_DECREASE_TIME:
                checkIncreaseDecreaseTime();
                break;
            case TutorialStates.SPACE:
                checkLaunch();
                break;
            case TutorialStates.HIT_OBJECT:
                checkObjectHit();
                break;
            case TutorialStates.TUTORIAL_ENDED:
                exitTutorial();
                break;
            default:
                Debug.LogError("Tutorial state machine failed.");
                break;
        }


	}

    private void checkLeftRight()
    {
        float sign = Input.GetAxis(InputManager.horizontalMovementButton);

        if (Input.GetButtonUp(InputManager.horizontalMovementButton))
        {
            if (_keyState == KeyPressedStates.NONE)
            {
                if(sign > 0)
                {
                    _keyState = KeyPressedStates.RIGHT_PRESSED;
                }
                else
                {
                    _keyState = KeyPressedStates.LEFT_PRESSED;
                }
            }
            else
            {
                if ( (_keyState == KeyPressedStates.RIGHT_PRESSED && sign < 0) ||
                    (_keyState == KeyPressedStates.LEFT_PRESSED && sign > 0) )
                {
                    _keyState = KeyPressedStates.NONE;
                    _state = TutorialStates.UP_DOWN;
                    TutorialUI.SetActiveImage(StateToTexture[1]);
                }
            }
        }
    }

    private void checkUpDown()
    {
        float sign = Input.GetAxis(InputManager.lookUpDownButton);

        if (Input.GetButtonUp(InputManager.lookUpDownButton))
        {
            if(_keyState == KeyPressedStates.NONE)
            {
                if(sign > 0 )
                {
                    _keyState = KeyPressedStates.UP_PRESSED;
                }
                else
                {
                    _keyState = KeyPressedStates.DOWN_PRESSED;
                }
            }
            else
            {
                if( (_keyState == KeyPressedStates.UP_PRESSED && sign < 0) || 
                    (_keyState == KeyPressedStates.DOWN_PRESSED && sign > 0) )
                {
                    _keyState = KeyPressedStates.NONE;
                    _state = TutorialStates.TURN_LEFT_RIGHT;
                    TutorialUI.SetActiveImage(StateToTexture[2]);
                }
            }
        }
    }

    private void checkTurn()
    {
        float sign = Input.GetAxis(InputManager.lookLeftRightButton);

        if (Input.GetButtonUp(InputManager.lookLeftRightButton))
        {
            if (_keyState == KeyPressedStates.NONE)
            {
                if (sign > 0)
                {
                    _keyState = KeyPressedStates.RIGHT_PRESSED;
                }
                else
                {
                    _keyState = KeyPressedStates.LEFT_PRESSED;
                }
            }
            else
            {
                if ((_keyState == KeyPressedStates.RIGHT_PRESSED && sign < 0) ||
                    (_keyState == KeyPressedStates.LEFT_PRESSED && sign > 0))
                {
                    _keyState = KeyPressedStates.NONE;
                    _state = TutorialStates.INCREASE_DECREASE_TIME;
                    TutorialUI.SetActiveImage(StateToTexture[3]);
                }
            }
        }
    }

    private void checkIncreaseDecreaseTime()
    {
        if(_keyState == KeyPressedStates.NONE)
        {
            if(Input.GetButtonUp(InputManager.increaseTimeButton))
            {
                _keyState = KeyPressedStates.INCREASE_PRESSED;
            }
            if(Input.GetButtonUp(InputManager.decreaseTimeButton))
            {
                _keyState = KeyPressedStates.DECREASE_PRESSED;
            }
        }
        else
        {
            if( (_keyState == KeyPressedStates.INCREASE_PRESSED && Input.GetButtonUp(InputManager.decreaseTimeButton)) ||
                    _keyState == KeyPressedStates.DECREASE_PRESSED && Input.GetButtonUp(InputManager.increaseTimeButton))
            {
                _keyState = KeyPressedStates.NONE;
                _state = TutorialStates.SPACE;
                TutorialUI.SetActiveImage(StateToTexture[4]);
            }
        }
    }

    private void checkPause()
    {
        if(Input.GetButtonUp(InputManager.pauseButton))
        {
            _keyState = KeyPressedStates.NONE;
            _state = TutorialStates.HIT_OBJECT;
            TutorialUI.gameObject.SetActive(false);
            foreach(GameObject go in TutorialClues)
            {
                go.SetActive(true);
            }
        }
    }

    private void checkLaunch()
    {
        if(Input.GetButtonUp(InputManager.chargeButton))
        {
            _keyState = KeyPressedStates.NONE;
            _state = TutorialStates.PRESS_PAUSE;
            TutorialUI.SetActiveImage(StateToTexture[5]);
        }
    }

    private void checkObjectHit()
    {
        
        foreach (GameObject go in TutorialClues)
        {
            if (!_stateIntialised)
                go.SetActive(true);
        }


        _stateIntialised = true;

        if (_gameManager.Score > 0)
        {
            _stateIntialised = false;
            _state = TutorialStates.TUTORIAL_ENDED;
        }


    }

    private void exitTutorial()
    {
        _gameManager.SetCompleteMap();
        if (_gameManager != null)
            _gameManager.resetScene();

        SceneManager.LoadScene(MenuSceneName);
    }
}
