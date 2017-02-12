using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class GameMaster : MonoBehaviour {

    public static GameMaster master = null;

    public string pauseSceneName = "Pause_Scene";

    public string defaultlastScene = "Scene_01";

    private int _lastLevelActive = 0;
    public int LastLevelActive
    {
        get { return _lastLevelActive; }
        set
        {
            if(_lastLevelActive < value)
            {
                _lastLevelActive = value;
            }
        }
    }

    private PauseMenuStateManager _pauseMenu = null;

    private Savefile _savefile = null;
    private Configuration _config = null;
    private string _scene = string.Empty;

    public Savefile LoadedSavefile
    {
        get { return _savefile; }
    }

    public Configuration LoadedConfiguration
    {
        get { return _config; }
    }

    public string LastScene
    {
        get { return _scene; }
        set { _scene = value; }
    }

	void Awake () {
        if (master == null)
        {
            DontDestroyOnLoad(gameObject);
            master = this;
            _savefile = Savefile.getInstance();
            _config = Configuration.getConfiguration();
            _scene = defaultlastScene;

            if (Savefile.checkSavefilesDir())
            {
                _savefile.loadSavefile();
                Dictionary<string, string> data = _savefile.getSavedData();
                if(data.ContainsKey(ConfigurationConstants.SAVEFILE_LAST_LEVEL))
                {
                    _scene = data[ConfigurationConstants.SAVEFILE_LAST_LEVEL];
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
        _savefile.addData(ConfigurationConstants.SAVEFILE_LAST_LEVEL, _scene);
        _savefile.writeSavefile();
    }

    public void Quit()
    {
        /// TODO -- free resources -- ///
        Application.Quit();
    }

}
