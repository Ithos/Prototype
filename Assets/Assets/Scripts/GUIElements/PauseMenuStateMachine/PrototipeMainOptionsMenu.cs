using UnityEngine;
using System.Collections;

public class PrototipeMainOptionsMenu : MenuStateManager
{

    public string gameManagerTag = "GAME_MANAGER";
    public string mainCameraTag = "MainCamera";

    public int[] OptionsPageSize = { 300, 300 };
    public string[] toolbarString = { "Audio", "Graphics", "System" };
    public string volumeLabel = "Volume";
    public string[] qualities =
    {
        "Fastest",
        "Fast",
        "Simple",
        "Good",
        "Beautiful",
        "Fantastic"
    };
    public string[] qualityButtonsText =
    {
        "Increase",
        "Decrease"
    };
    public string[] deviceText =
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

    public float[] StatsSize = { 100, 10, 100, 200 };
    public string FPSLabelFormat = "#,##0 fps";
    public string vertexText = "vertex";
    public string trianglesText = "triangles";
    public Material graphMaterial = null;
    public Color lowFPSColor = Color.red;
    public Color highFPSColor = Color.green;
    public int lowFPS = 30;
    public int highFPS = 50;
    public float gldepth = 0.5f;

    public GameObject[] listToActivate;
    public GameObject[] listToDeactivate;

    void Awake()
    {

        addState(PauseMenuStateNames.StateNames[1],
            new MainOptionsMenu(this, graphMaterial, mainCameraTag, gldepth, StatsSize[0], StatsSize[1], StatsSize[2], StatsSize[3],
            lowFPS, highFPS, lowFPSColor, highFPSColor, FPSLabelFormat, trianglesText, vertexText, OptionsPageSize[0],
            OptionsPageSize[1], toolbarString, volumeLabel, qualities, qualityButtonsText, deviceText, listToActivate, listToDeactivate)
            );

    }

    void OnEnable()
    {
        setState(PauseMenuStateNames.StateNames[1]);
    }

}
