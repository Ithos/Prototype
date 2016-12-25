using UnityEngine;
using System.Collections;

public class ActiveElementsManager : MonoBehaviour {

    public GameManager gameManager = null;
    public string gameManagerTag = "GAME_MANAGER";

    public float modificationRatio = 0.1f;

    private SlowMovement _activeBall = null;
    private int nextIndex = 0;

	// Use this for initialization
	void Start () {

        if (gameManager == null)
        {
            gameManager = GameObject.FindGameObjectWithTag(gameManagerTag).GetComponent<GameManager>();
        }

    }
	
	// Update is called once per frame
	void Update () {
        checkActiveBall();
	}

    public void setActiveBall()
    {
        if(gameManager != null)
        {
            GameObject ball = gameManager.getPlayerBall(nextIndex);
            if(ball== null)
            {
                Debug.LogError("ActiveElementsManager >>> No player ball found");
                return;
            }
            _activeBall = ball.GetComponent<SlowMovement>();
            ++nextIndex;
            if (nextIndex > gameManager.ActiveBalls - 1)
                nextIndex = 0;
        }
    }

    public void setActiveBall(GameObject newActiveBall, int newIndex)
    {
        if (newActiveBall == null) return;

        _activeBall = newActiveBall.GetComponent<SlowMovement>();
        if (newIndex < 0 || newIndex > gameManager.ActiveBalls - 1) newIndex = 0;
        else
            nextIndex = newIndex;
    }

    public void increaseActiveTime()
    {
        if(_activeBall != null)
        {
            _activeBall.TimeDilation += modificationRatio;
        }
    }

    public void decreaseActiveTime()
    {
        if(_activeBall != null)
        {
            _activeBall.TimeDilation -= modificationRatio;
        }
    }

    public void checkActiveBall()
    {
        if (_activeBall != null)
            return;

        setActiveBall();
    }

    public SlowMovement getActiveBall()
    {
        return _activeBall;
    }
}
