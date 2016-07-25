using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameMaster : MonoBehaviour {

    public static GameMaster master = null;

    public string pauseSceneName = "Pause_Scene";

    private PauseMenuStateManager _pauseMenu = null;

	void Awake () {
        if (master == null)
        {
            DontDestroyOnLoad(gameObject);
            master = this;
        }else if(this != master)
        {
            Destroy(gameObject);
        }
	}
	
    public void toPause()
    {
        if (_pauseMenu == null && FindObjectOfType<PauseMenuStateManager>() == null)
        {
            SceneManager.LoadScene(pauseSceneName, LoadSceneMode.Additive);
            return;
        }

        _pauseMenu = FindObjectOfType<PauseMenuStateManager>();

       
        if(_pauseMenu.IsPaused())
        {
            _pauseMenu.UnPauseGame();
        }
        else
        {
            _pauseMenu.PauseGame();
        }
        
    }

    private void resetState()
    {
        _pauseMenu = null;
    }
}
