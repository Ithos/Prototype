using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class GameMaster : MonoBehaviour {

    public static GameMaster master = null;

    public string pauseSceneName = "Pause_Scene";

    public string lastScene = "Scene_01";

    private PauseMenuStateManager _pauseMenu = null;

    private Savefile _savefile = null;
    private Configuration _config = null;

    public Savefile LoadedSavefile
    {
        get { return _savefile; }
    }

    public Configuration LoadedConfiguration
    {
        get { return _config; }
    }

	void Awake () {
        if (master == null)
        {
            DontDestroyOnLoad(gameObject);
            master = this;
            _savefile = Savefile.getInstance();
            _config = Configuration.getConfiguration();

            if (Savefile.checkSavefilesDir())
            {
                _savefile.loadSavefile();
                Dictionary<string, string> data = _savefile.getSavedData();
                if(data.ContainsKey(ConfigurationConstants.SAVEFILE_LAST_LEVEL))
                {
                    lastScene = data[ConfigurationConstants.SAVEFILE_LAST_LEVEL];
                }
            }
            
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

    public void SaveLastScene()
    {
        _savefile.addData(ConfigurationConstants.SAVEFILE_LAST_LEVEL, lastScene);
        _savefile.writeSavefile();
    }

}
