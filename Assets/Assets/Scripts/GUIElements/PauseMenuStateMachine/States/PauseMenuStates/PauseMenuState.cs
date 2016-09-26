using UnityEngine;
using System.Collections;

public abstract class PauseMenuState : MenuState {

    new protected PauseMenuStateManager _stateManager = null;

    public PauseMenuState(PauseMenuStateManager stateManager) : base(stateManager)
    {
        _stateManager = stateManager;
    }
}
