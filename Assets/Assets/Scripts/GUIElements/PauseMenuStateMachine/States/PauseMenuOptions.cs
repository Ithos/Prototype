using UnityEngine;
using System.Collections;

public class PauseMenuOptions : PauseMenuState {

    private int[] _size = { 300, 300 };
    private int _toolbarInt = 0;
    private string[] _toolbarString = { "Audio", "Graphics", "Stats", "System" };
    private string _volumeLabel = "Volume";
    private string[] _qualities =
    {
        "Fastest",
        "Fast",
        "Simple",
        "Good",
        "Beautiful",
        "Fantastic"
    };

    private string[] _qualityButtonsText =
    {
        "Increase",
        "Decrease"
    };

    private string[] _statsText =
    {
        "FPS",
        "Triangles",
        "Vertex",
        "FPS Graph"
    };

    private string[] _deviceText =
    {
        "Unity player version ",
        "Graphics: ",
        " ",
        "MB\n",
        "\n",
        "Shadows: ",
        "Image Effects: ",
        "Render Textures: "
    };

    private PerformanceStats _stats = null;

    public PauseMenuOptions(PauseMenuStateManager stateManager) : base(stateManager)
    {
        _stats = new PerformanceStats();
    }

    public PauseMenuOptions(PauseMenuStateManager stateManager, Material graphMaterial, string mainCameraTag,
        float gldepth, float statsXMargin, float statsy, float statsWidth, float statsHeigth,
        int lowFPS, int highFPS, Color lowFPSColor, Color highFPSColor, 
        string fpsFormat, string trianglesText, string vertexText,
        int width, int heigth, string[] toolbarString, string volumeLabel, string[] qualities,
        string[] qualityButtonsText, string[] statsText, string[] deviceText) : base(stateManager)
    {
        _stats = new PerformanceStats(graphMaterial, mainCameraTag, gldepth, statsXMargin, statsy, statsWidth, statsHeigth,
            lowFPS, highFPS, lowFPSColor, highFPSColor, fpsFormat, trianglesText, vertexText);

        _size[0] = width;
        _size[1] = heigth;

        _toolbarString = toolbarString;
        _volumeLabel = volumeLabel;
        _qualities = qualities;
        _qualityButtonsText = qualityButtonsText;
        _statsText = statsText;
        _deviceText = deviceText;
    }



    override public void OnGUI()
    {
        GUI.color = _stateManager.menuColor;
        ShowToolBar();
    }

    private void ShowToolBar()
    {
        BeginPage(_size[0], _size[1]);

        _toolbarInt = GUILayout.Toolbar(_toolbarInt, _toolbarString);

        switch(_toolbarInt)
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

        if(ShowBackButton())
        {
            _stateManager.setState(PauseMenuStateNames.StateNames[0]);
        }
    }

    private void VolumeControl()
    {
        GUILayout.Label(_volumeLabel); 
        AudioListener.volume = GUILayout.HorizontalSlider(AudioListener.volume, 0, 1);
    }

    private void Qualities()
    {
        switch (QualitySettings.GetQualityLevel())
        {
            case 0:
                GUILayout.Label(_qualities[0]);
                break;
            case 1:
                GUILayout.Label(_qualities[1]);
                break;
            case 2:
                GUILayout.Label(_qualities[2]);
                break;
            case 3:
                GUILayout.Label(_qualities[3]);
                break;
            case 4:
                GUILayout.Label(_qualities[4]);
                break;
            default:
                GUILayout.Label(_qualities[5]);
                break;
        }
    }

    private void QualityControl()
    {
        GUILayout.BeginHorizontal();

        if (GUILayout.Button(_qualityButtonsText[0]))
        {
            QualitySettings.IncreaseLevel();
        }
        if (GUILayout.Button(_qualityButtonsText[1]))
        {
            QualitySettings.DecreaseLevel();
        }

        GUILayout.EndHorizontal();
    }

    private void StatControl()
    {

        _stateManager.setPauseStats(_stats);

        GUILayout.BeginHorizontal();

        _stats.showFPS = GUILayout.Toggle(_stats.showFPS, _statsText[0]);
        _stats.showTris = GUILayout.Toggle(_stats.showTris, _statsText[1]);
        _stats.showVerts = GUILayout.Toggle(_stats.showVerts, _statsText[2]);
        _stats.showFPSGraph = GUILayout.Toggle(_stats.showFPSGraph, _statsText[3]);

        GUILayout.EndHorizontal();
    }

    private void ShowDevice()
    {
        GUILayout.Label( _deviceText[0] + Application.unityVersion);
        GUILayout.Label(_deviceText[1] + SystemInfo.graphicsDeviceName + _deviceText[2] +
                SystemInfo.graphicsMemorySize + _deviceText[3] +
                SystemInfo.graphicsDeviceVersion + _deviceText[4] +
                SystemInfo.graphicsDeviceVendor
            );
        GUILayout.Label(_deviceText[5] + SystemInfo.supportsShadows);
        GUILayout.Label(_deviceText[6] + SystemInfo.supportsImageEffects);
        GUILayout.Label(_deviceText[7] + SystemInfo.supportsRenderTextures);
    }
}
