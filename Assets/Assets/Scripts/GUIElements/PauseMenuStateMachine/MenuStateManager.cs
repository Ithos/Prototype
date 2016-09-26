using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MenuStateManager : MonoBehaviour {

    protected GUISkin skin = null;
    public Color menuColor = Color.yellow;

    protected MenuState _currentState = null;

    protected Dictionary<string, MenuState> _stateMap = new Dictionary<string, MenuState>();

    virtual protected void OnGUI()
    {
        if (skin != null)
        {
            GUI.skin = skin;
        }

        if (_currentState != null)
        {
            _currentState.OnGUI();
        }
    }

    public void addState(string name, MenuState state)
    {
        if (!_stateMap.ContainsKey(name))
        {
            _stateMap.Add(name, state);
        }
    }

    public void setState(string name)
    {
        if (_stateMap.ContainsKey(name))
        {
            _currentState = _stateMap[name];
        }
    }
}
