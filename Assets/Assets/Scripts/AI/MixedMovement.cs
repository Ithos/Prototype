using UnityEngine;
using System.Collections;

public class MixedMovement : HorizontalMovement {

    public float EventProbability = 0.0f;
    public float EventTime = 0.0f;
    public float TimeVariance = 0.0f;
    public float MaxHeight = 0.0f;
    public float MinHeight = 0.0f;
    public float VerticalVelocity = 0.0f;

    private float _time = 0.0f;
    private float _nextEventTime = 0.0f;

    protected void OnValidate()
    {
        base.OnValidate();
        if (EventProbability > 1.0f)
            EventProbability = 1.0f;
    }

    // Use this for initialization
    protected void Start () {
        base.Start();
        _nextEventTime = EventTime;

    }

    // Update is called once per frame
    protected void Update () {
        base.Update();
	}

    protected override void Move()
    {
        base.Move();

        if (_state != MovementState.GoingUp && _state != MovementState.GoingDown)
        {
            _time += Time.deltaTime;

            if (_time >= _nextEventTime)
            {
                _time = 0.0f;
                float tmpTime = UnityEngine.Random.Range(0.0f, TimeVariance);
                _nextEventTime = EventTime + (UnityEngine.Random.Range(0, 1) == 0 ? -tmpTime : tmpTime);
                float tmp = UnityEngine.Random.Range(0.0f, 1.0f);

                if (tmp <= EventProbability)
                {
                    _state = MovementState.GoingUp;
                }
            }
        }
        else
        {
            switch (_state)
            {
                case MovementState.GoingUp:
                    if (_myTransform.position.y < MaxHeight)
                    {
                        _myTransform.position += Vector3.up * VerticalVelocity * Time.deltaTime;
                    }
                    else
                    {
                        _state = MovementState.GoingDown;
                    }
                    break;
                case MovementState.GoingDown:
                    if (_myTransform.position.y > MinHeight)
                    {
                        _myTransform.position -= Vector3.up * VerticalVelocity * Time.deltaTime;
                    }
                    else
                    {
                        _state = UnityEngine.Random.Range(0, 1) == 0 ? MovementState.GoingRight : MovementState.GoingLeft;
                    }
                    break;
            }
        }
        
    }
}
