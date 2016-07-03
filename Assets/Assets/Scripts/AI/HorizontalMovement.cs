using UnityEngine;
using System.Collections;
using System;

public class HorizontalMovement : Movement {

    public enum MovementState
    {
        GoingLeft,
        GoingRight,
        GoingUp,
        GoingDown
    }

    public float MaxXCoord = 0.0f;
    public float MinXCoord = 0.0f;

    public float Velocity = 0.0f;
    public float RandomRange = 0.0f;

    protected Transform _myTransform;
    protected float _privateVelocity;
    protected MovementState _state;

    protected void OnValidate()
    {
        if(MinXCoord > MaxXCoord)
        {
            MinXCoord = MaxXCoord;
        }

        if (Velocity < 0)
            Velocity = 0.0f;

        if (RandomRange < 0)
            RandomRange = 0.0f;
    }

    // Use this for initialization
    protected void Start () {
        _myTransform = transform;
        _state = UnityEngine.Random.Range(0, 1) == 0 ? MovementState.GoingRight : MovementState.GoingLeft;
        float tmpVel = UnityEngine.Random.Range(0.0f, 1.0f) * RandomRange;
        _privateVelocity = Velocity + (UnityEngine.Random.Range(0, 1) == 0 ? -tmpVel : tmpVel);
        if (_privateVelocity < 0)
            _privateVelocity = 0.0f;
    }

    // Update is called once per frame
    protected void Update () {
        if (_running == RunningState.Running)
        {
            Move();
        }
        else
        {
            if (_running == RunningState.TimedStop)
            {
                if (_stopTimeLeft > 0.0f)
                {
                    _stopTimeLeft -= Time.deltaTime;
                }
                else
                {
                    _running = RunningState.Running;
                    _stopTimeLeft = 0.0f;
                }
            }
        }
	}

    override public void StartMovement()
    {
        _running = RunningState.Running;
    }

    public override void StopMovement()
    {
        _running = RunningState.Stopped;
    }

    public override void timedStop(float stopTime)
    {
        _stopTimeLeft = stopTime;
    }

    public override bool isMoving()
    {
        return _running == RunningState.Running;
    }

    protected virtual void Move()
    {
        switch(_state)
        {
            case MovementState.GoingLeft:

                if(_myTransform.position.x < MaxXCoord)
                {
                    _myTransform.position += Vector3.right * (Time.deltaTime * _privateVelocity);
                }
                else
                {
                    _state = MovementState.GoingRight;
                }

            break;
            case MovementState.GoingRight:

                if (_myTransform.position.x > MinXCoord)
                {
                    _myTransform.position -= Vector3.right * (Time.deltaTime * _privateVelocity);
                }
                else
                {
                    _state = MovementState.GoingLeft;
                }


                break;
        }
    }
}
