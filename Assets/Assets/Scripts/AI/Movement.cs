using UnityEngine;
using System.Collections;

public abstract class Movement : MonoBehaviour {

    public enum RunningState
    {
        Stopped,
        Running,
        TimedStop
    }

    public RunningState _running = RunningState.Stopped;
    protected float _stopTimeLeft = 0.0f;

    abstract public void StartMovement();
    abstract public void StopMovement();
    abstract public bool isMoving();
    abstract public void timedStop(float stopTime);
}
