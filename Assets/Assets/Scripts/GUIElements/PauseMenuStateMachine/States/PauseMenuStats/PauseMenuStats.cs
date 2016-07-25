using System.Collections;

public abstract class PauseMenuStats{

    public bool IsPaused = false;

    public abstract void OnPostRender();

    public abstract void LateUpdate();

    public abstract void OnGUI();
}
