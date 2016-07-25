using UnityEngine;
using System.Collections.Generic;

public class PauseMenuStateManager : MonoBehaviour {

    public GUISkin skin = null;
    public Color menuColor = Color.yellow;

    private float _lastTimeScale = 1.0f;

    private PauseMenuState _currentState = null;
    private PauseMenuStats _currentStats = null;

    private Dictionary<string, PauseMenuState> _stateMap = new Dictionary<string, PauseMenuState>();

    void Start()
    {
        PauseGame();
    }

    void LateUpdate()
    {
        if(_currentStats != null)
        {
            _currentStats.IsPaused = IsPaused();
            _currentStats.LateUpdate();
        }
    }

    void OnGUI()
    {
        if (skin != null)
        {
            GUI.skin = skin;
        }

        if (_currentStats != null)
        {
            _currentStats.OnGUI();
        }

        if(IsPaused() && _currentState != null)
        {
            _currentState.OnGUI();
        }
    }

    void OnApplicationPause(bool pause)
    {
        if (IsPaused())
        {
            AudioListener.pause = true;
        }
    }

    virtual public void PauseGame()
    {
        _lastTimeScale = Time.timeScale;
        Time.timeScale = 0.0001f;
        AudioListener.pause = true;
    }

    virtual public void UnPauseGame()
    {
        Time.timeScale = _lastTimeScale;
        AudioListener.pause = false;
        _currentState = null;
    }

    public bool IsPaused()
    {
        return (Time.timeScale <= 0.001);
    }

    public void setPauseStats(PauseMenuStats stats)
    {
        _currentStats = stats;
    }

    public void addState(string name, PauseMenuState state)
    {
        if(!_stateMap.ContainsKey(name))
        {
            _stateMap.Add(name, state);
        }
    }

    public void setState(string name)
    {
        if(_stateMap.ContainsKey(name))
        {
            _currentState = _stateMap[name];
        }
    }
}
