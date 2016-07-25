using UnityEngine;
using System.Collections;

public class PauseMenu : MonoBehaviour {

    public GUISkin skin = null;

    public Material graphMaterial = null;

    public string PauseKeyCode = "PAUSE";

    private float _gldepth = 0.5f;
    private float _startTime = 0.1f;

    private long _tris = 0;
    private long _verts = 0;
    private float _lastTimeScale = 1.0f;

    private bool _showFPS = false;
    private bool _showTris = false;
    private bool _showVerts = false;
    private bool _showFPSgraph = false;

    public Color lowFPSColor = Color.red;
    public Color highFPSColor = Color.green;

    public int lowFPS = 30;
    public int highFPS = 50;

    public GameObject start = null;

    public Color startColor = Color.yellow;

    public string[] credits =
    {
        "Pause menu by Ithos",
        "based on a script from Phil Chu",
        "http://wiki.unity3d.com/index.php?title=PauseMenu"
    };

    public Texture[] creditIcons = null;

    public enum Page
    {
        None, Main, Options, Credits
    }

    private Page _currentPage = Page.None;

    private float[] _FPSArray = null;
    private float _FPS = 0;

    private int _toolBarInt = 0;
    private string[] _toolBarStrings = { "Audio", "Graphics", "Stats", "System" };



    // Use this for initialization
    void Start () {
        _FPSArray = new float[Screen.width];
        PauseGame();

	}

    void OnPostRender()
    {
        if(_showFPSgraph && graphMaterial != null)
        {
            GL.PushMatrix(); // Save current matrix
            GL.LoadPixelMatrix(); // Load matrix for pixel 2D representation
            for(int i = 0; i < graphMaterial.passCount; ++i) //for every pass required for this material
            {
                graphMaterial.SetPass(i);
                GL.Begin( GL.LINES );
                for(int j = 0; j < _FPSArray.Length; ++j)
                {
                    GL.Vertex3(j, _FPSArray[j], _gldepth);
                }
                GL.End();
            }
            GL.PopMatrix(); //Recover the last saved matrix
            ScrollFPS();
        }
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    void LateUpdate()
    {
        if(_showFPS || _showFPSgraph)
        {
            FPSUpdate();
        }

        if(Input.GetButtonUp(PauseKeyCode))
        {
            switch (_currentPage)
            {
                case Page.None:
                    PauseGame();
                    break;
                case Page.Main:
                    if(!IsBeggining())
                    {
                        UnPauseGame();
                    }
                    break;
                default:
                    _currentPage = Page.Main;
                    break;
            }
        }
    }

    void OnGUI()
    {
        if(skin != null)
        {
            GUI.skin = skin;
        }

        ShowStatNums();
        if(IsPaused())
        {
            GUI.color = startColor;
            switch(_currentPage)
            {
                case Page.Main: MainPauseMenu(); break;
                case Page.Options: ShowToolBar(); break;
                case Page.Credits: ShowCredits(); break;

            }
        }


    }

    private void ScrollFPS()
    {
        for(int i = 1; i < _FPSArray.Length; ++i)
        {
            _FPSArray[i - 1] = _FPSArray[i];
        }

        if(_FPS < 1000)
        {
            _FPSArray[_FPSArray.Length - 1] = _FPS; 
        }
    }

    private bool IsBeggining()
    {
        return (Time.time < _startTime);
    }

    private void ShowCredits()
    {
        BeginPage(300, 300); // This shold be passed to the class as a parameter

        foreach(string credit in credits)
        {
            GUILayout.Label(credit);
        }

        foreach(Texture credit in creditIcons)
        {
            GUILayout.Label(credit);
        }

        EndPage();
    }

    private void ShowToolBar()
    {
        BeginPage(300, 300); // This should be passed to the class as a parameter
        _toolBarInt = GUILayout.Toolbar(_toolBarInt, _toolBarStrings);
        switch(_toolBarInt)
        {
            case 0:
                VolumeControl();
                break;
            case 1:
                Qualities();
                QualityControl();
                break;
            case 2:
                StatControl();
                break;
            case 3:
                ShowDevice();
                break;
        }

        EndPage();
    }

    private void VolumeControl()
    {
        GUILayout.Label("Volume"); /// TODO -- this has to be a parameter -- ///
        AudioListener.volume = GUILayout.HorizontalSlider(AudioListener.volume, 0, 1);
    }

    private void Qualities()
    {
        switch(QualitySettings.GetQualityLevel())
        {
            case 0:
                GUILayout.Label("Fastest"); /// TODO -- all of this literals should be parameters -- ///
                break;
            case 1:
                GUILayout.Label("Fast");
                break;
            case 2:
                GUILayout.Label("Simple");
                break;
            case 3:
                GUILayout.Label("Good");
                break;
            case 4:
                GUILayout.Label("Beautiful");
                break;
            default:
                GUILayout.Label("Fantastic");
                break;
        }
    }

    private void QualityControl()
    {
        GUILayout.BeginHorizontal();
        if(GUILayout.Button("Increase"))/// TODO -- all of this literals should be parameters -- ///
        {
            QualitySettings.IncreaseLevel();
        }
        if(GUILayout.Button("Decrease"))
        {
            QualitySettings.DecreaseLevel();
        }

        GUILayout.EndHorizontal();
    }

    private void StatControl()
    {
        GUILayout.BeginHorizontal();

        _showFPS = GUILayout.Toggle(_showFPS, "FPS"); /// TODO -- all of this literals should be parameters -- ///
        _showTris = GUILayout.Toggle(_showTris, "Triangles");
        _showVerts = GUILayout.Toggle(_showVerts, "Vertices");
        _showFPSgraph = GUILayout.Toggle(_showFPSgraph, "FPS Graph");

        GUILayout.EndHorizontal();
    }

    private void ShowDevice()
    {
        /// TODO -- All this text has to be parameterized -- ///
        GUILayout.Label("Unity player version " + Application.unityVersion);
        GUILayout.Label("Graphics: " + SystemInfo.graphicsDeviceName + " " +
                SystemInfo.graphicsMemorySize + "MB\n" +
                SystemInfo.graphicsDeviceVersion + "\n" +
                SystemInfo.graphicsDeviceVendor
            );
        GUILayout.Label("Shadows: " + SystemInfo.supportsShadows);
        GUILayout.Label("Image Effects: " + SystemInfo.supportsImageEffects);
        GUILayout.Label("Render Textures: " + SystemInfo.supportsRenderTextures);
    }

    private void BeginPage(int width, int height)
    {
        GUILayout.BeginArea(new Rect((Screen.width - width) / 2, (Screen.height - height) / 2, width, height));
    }

    private void EndPage()
    {
        GUILayout.EndArea();

        if(_currentPage != Page.Main)
        {
            ShowBackButton();
        }
    }

    private void ShowBackButton()
    {
        if(GUI.Button(new Rect(20, Screen.height - 50, 50, 20), "Back")){ /// TODO -- parameters -- ///
            _currentPage = Page.Main;
        }
    }

    private void FPSUpdate()
    {
        float delta = Time.smoothDeltaTime;
        if(!IsPaused() && delta != 0.0f)
        {
            _FPS = 1 / delta;
        }
    }

    private void PauseGame()
    {
        _lastTimeScale = Time.timeScale;
        Time.timeScale = 0;
        AudioListener.pause = true;

        _currentPage = Page.Main;
    }

    private void UnPauseGame()
    {
        Time.timeScale = _lastTimeScale;
        AudioListener.pause = false;
        _currentPage = Page.None;

        if(IsBeggining() && start != null)
        {
            start.SetActive(true);
        }
    }

    private void ShowStatNums()
    {
        /// TODO -- parameters -- ///
        GUILayout.BeginArea(new Rect(Screen.width - 100, 10, 100, 200));

        if(_showFPS)
        {
            string FPSstring = _FPS.ToString("#,##0 fps");
            GUI.color = Color.Lerp(lowFPSColor, highFPSColor, (_FPS - lowFPS) / (highFPS - lowFPS));
            GUILayout.Label(FPSstring);
        }
        if(_showTris||_showVerts)
        {
            GetObjectStats();
            GUI.color = startColor;
        }
        if(_showTris)
        {
            GUILayout.Label(_tris + "triangles");
        }
        if(_showVerts)
        {
            GUILayout.Label(_verts + "vertex");
        }

        GUILayout.EndArea();
    }

    private void GetObjectStats()
    {
        _verts = 0;
        _tris = 0;
        GameObject[] objs = FindObjectsOfType(typeof(GameObject)) as GameObject[];
        foreach(GameObject obj in objs)
        {
            GetObjectStats(obj);
        }
    }

    private void GetObjectStats(GameObject obj)
    {
        Component[] filters;
        filters = obj.GetComponentsInChildren<MeshFilter>();
        foreach(MeshFilter mesh in filters)
        {
            _tris += mesh.sharedMesh.triangles.Length / 3;
            _verts += mesh.sharedMesh.vertexCount;
        }
    }

    private void MainPauseMenu()
    {
        /// TODO -- parameters -- ///
        BeginPage(200, 200);

        if(GUILayout.Button(IsBeggining() ? "Play" : "Continue"))
        {
            UnPauseGame();
        }

        if(GUILayout.Button("Options"))
        {
            _currentPage = Page.Options;
        }
        if (GUILayout.Button("Credits"))
        {
            _currentPage = Page.Credits;
        }

        EndPage();
    }

    private bool IsPaused()
    {
        return (Time.timeScale == 0);
    }

    void OnApplicationPause(bool pause)
    {
        if (IsPaused())
        {
            AudioListener.pause = true;
        }
    }
}
