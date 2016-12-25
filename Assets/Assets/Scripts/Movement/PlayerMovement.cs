using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

    public bool horizontalMovement = true;
    public bool lookUpDown = true;
    public bool lookRightLeft = true;

    

    public float horizontalVelocity = 10.0f;
    public float XAngularVelocity = 3.0f;
    public float YAngularVelocity = 3.0f;

    public float maxX = 10.0f;
    public float minX = -10.0f;

    public float maxXAngle = 60.0f;
    public float minXAngle = -15.0f;

    public float maxYAngleCos = 0.5f;
    public float minYAngleCos = -0.5f;

    public Rigidbody activeBall;
    public SlowMovement activeControlledBall;
    public string playerBallTag;
    public float forceConstant = 1.0f;
    public float ballSpawnDelay = 0.3f;

    public GameManager gameManager = null;
    public string GameManagerTag = "";

    private Transform _myTransform;
    private float _delayTime = 0.0f;

    private bool _init = false;

	// Use this for initialization
	void Start () {
        _myTransform = transform;

        if (gameManager == null)
        {
            gameManager = GameObject.FindGameObjectWithTag(GameManagerTag).GetComponent<GameManager>();
        }

    }
	
	// Update is called once per frame
	void Update () {

        init();

        checkBallRespawn();

    }

    public bool moveHorizontally(float sign)
    {
        
        bool flag = false;

        if (sign >= 0 &&  _myTransform.position.x > minX)
        {
            _myTransform.position += Vector3.left * horizontalVelocity * Time.deltaTime;
            flag = true;
        }
        else if (sign < 0 && _myTransform.position.x < maxX)
        {
            _myTransform.position += Vector3.right * horizontalVelocity * Time.deltaTime;
            flag = true;
        }

        return flag;
    }

    public bool rotateUpDown(float sign)
    {
        bool flag = false;

        if (sign >= 0 && adaptAngle(_myTransform.rotation.eulerAngles.x) < maxXAngle)
        {
            _myTransform.Rotate(Vector3.right * XAngularVelocity * Time.deltaTime);
            flag = true;
        }
        else if (sign < 0 && adaptAngle(_myTransform.rotation.eulerAngles.x) > minXAngle)
        {
            _myTransform.Rotate(Vector3.left * XAngularVelocity * Time.deltaTime);
            flag = true;
        }

        return flag;
    }

    public bool rotateRightLeft( float sign)
    {
        bool flag = false;

        if (sign >= 0 && adaptAngle(calculateYAngle()) < maxYAngleCos)
        {
            _myTransform.RotateAround(Vector3.up, YAngularVelocity * Time.deltaTime);
            flag = true;
        }
        else if (sign < 0 && adaptAngle(calculateYAngle()) > minYAngleCos)
        {
            _myTransform.RotateAround(Vector3.down, YAngularVelocity * Time.deltaTime);
            flag = true;
        }

        return flag;
    }

    private float calculateYAngle()
    {
        Vector3 projection = _myTransform.forward;
        projection.y = 0.0f;
        projection.Normalize();

        float dotResult = Vector3.Dot(projection, Vector3.right);
        //Debug.Log("Y angle:" + dotResult );
        return dotResult;
        
    }

    private float adaptAngle(float angle)
    {
        if (angle < 180.0f)
            return angle;
        else
            return (angle - 360.0f);
    }

    public void launchBall()
    {
        if(activeBall != null)
        {
            activeBall.GetComponent<Transform>().parent = null;
            activeBall.AddForce(-_myTransform.forward * (gameManager.CurrentCharge / gameManager.secondsToCharge) * forceConstant, ForceMode.Impulse);
            activeBall.useGravity = true;
            activeBall = null;
            gameManager.emptyCharge();
        }
        else if(activeControlledBall != null)
        {
            activeControlledBall.GetComponent<Transform>().parent = null;
            activeControlledBall.AddForce(-_myTransform.forward * (gameManager.CurrentCharge / gameManager.secondsToCharge) * forceConstant, ForceMode.Impulse);
            activeControlledBall.UseGravity = true;
            activeControlledBall = null;
            gameManager.emptyCharge();
        }
    }

    public bool chargeBall()
    {
        

        if (gameManager.CurrentCharge >= gameManager.secondsToCharge)
            return false;

        if(activeBall != null || activeControlledBall != null)
        {
            gameManager.addCharge(Time.deltaTime);
            return true;
        }

        return false;
    }

    public void setActiveBall(GameObject ball)
    {
        activeBall = ball.GetComponent<Rigidbody>();
        activeControlledBall = ball.GetComponent<SlowMovement>();
    }

    private void checkBallRespawn()
    {
        if (activeBall != null || activeControlledBall != null)
            return;

        if(_delayTime < ballSpawnDelay)
        {
            _delayTime += Time.deltaTime;
        }
        else
        {
            _delayTime = 0;
            GameObject tmp = gameManager.generatePlayerBall();
            if (tmp != null)
            {
                activeBall = tmp.GetComponent<Rigidbody>();
                activeControlledBall = tmp.GetComponent<SlowMovement>();
                gameManager.swapTargetBall(tmp);
            }
        }
    }

    private void init()
    {
        if(!_init)
        {
            if (activeBall == null && activeControlledBall == null)
            {
                GameObject tmp = GameObject.FindGameObjectWithTag(playerBallTag);
                activeBall = tmp.GetComponent<Rigidbody>();
                activeControlledBall = tmp.GetComponent<SlowMovement>();
            }

            _init = true;
        }
    }
}
