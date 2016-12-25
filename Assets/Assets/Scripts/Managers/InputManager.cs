using UnityEngine;
using System.Collections;

public class InputManager : MonoBehaviour {

    public GameManager gameManager = null;
    public PlayerMovement playerMovement = null;

    public string gameManagerTag = "GAME_MANAGER";
    public string playerMovementTag = "PLAYER";

    public string horizontalMovementButton = "HORIZONTAL_MOVEMENT";
    public string lookUpDownButton = "ROTATE_AROUND_X";
    public string lookLeftRightButton = "ROTATE_AROUND_Y";
    public string chargeButton = "CHARGE_LAUNCH";
    //public string resetButton = "RESTART_LEVEL";
    public string pauseButton = "PAUSE";
    public string swapTargetButton = "SWAP_TARGET";
    public string decreaseTimeButton = "DECREASE_TIME";
    public string increaseTimeButton = "INCREASE_TIME";

    // Use this for initialization
    void Start () {

        if (gameManager == null)
        {
            gameManager = GameObject.FindGameObjectWithTag(gameManagerTag).GetComponent<GameManager>();
        }

        if(playerMovement == null)
        {
            playerMovement = GameObject.FindGameObjectWithTag(playerMovementTag).GetComponent<PlayerMovement>();
        }
    }
	
	// Update is called once per frame
	void Update () {

        if (checkHorizontal() || checkRotateUpDown() || checkRotateRightLeft() || checkCharge()) // Charge ball should be the last
        {
        }
        else
        {
            checkLaunchBall();
        }

        //checkReset();
        checkPause();
        checkTargetSwap();
        checkBallTimeUpdate();
    }

    private bool checkHorizontal()
    {
        if (horizontalMovementButton == "")
            return false;

        float sign = Input.GetAxis(horizontalMovementButton);

        if (Input.GetButton(horizontalMovementButton))
            return playerMovement.moveHorizontally(sign);

        return false;
    }

    private bool checkRotateUpDown()
    {
        if (lookUpDownButton == "")
            return false;

        float sign = Input.GetAxis(lookUpDownButton);

        if (Input.GetButton(lookUpDownButton))
            return playerMovement.rotateUpDown(sign);

        return false;
    }

    private bool checkRotateRightLeft()
    {
        if (lookLeftRightButton == "")
            return false;

        float sign = Input.GetAxis(lookLeftRightButton);

        if (Input.GetButton(lookLeftRightButton))
            return playerMovement.rotateRightLeft(sign);

        return false;
    }

    private bool checkCharge()
    {
        if (chargeButton == "")
            return false;

        if (Input.GetButton(chargeButton))
            return playerMovement.chargeBall();

        return false;
    }

    private void checkTargetSwap()
    {
        if(Input.GetButtonUp(swapTargetButton))
        {
            gameManager.swapTargetBall();
        }
    }

    private void checkBallTimeUpdate()
    {
        if(Input.GetButtonUp(increaseTimeButton))
        {
            gameManager.increaseActiveTime();
        }
        else if (Input.GetButtonUp(decreaseTimeButton))
        {
            gameManager.decreaseActiveTime();
        }
    }

    private void checkLaunchBall()
    {
        if (Input.GetButtonUp(chargeButton))
            playerMovement.launchBall();
    }

    //private void checkReset()
    //{
    //    if(Input.GetButtonUp(resetButton))
    //    {
    //        gameManager.resetScene();
    //    }
    //}

    private void checkPause()
    {
        if(Input.GetButtonUp(pauseButton))
        {
            gameManager.Pause();
        }
    }

}
