using UnityEngine;
using System.Collections.Generic;

public class PauseMenuStateManager : MenuStateManager {

    private float _lastTimeScale = 1.0f;

    private PauseMenuStats _currentStats = null;

    virtual protected void Start()
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

    override protected void OnGUI()
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
}
