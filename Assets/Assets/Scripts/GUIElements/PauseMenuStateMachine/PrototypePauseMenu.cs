using UnityEngine;
using System.Collections;

public class PrototypePauseMenu : PauseMenuStateManager {

    public string gameManagerTag = "GAME_MANAGER";
    public string mainCameraTag = "MainCamera";

    public int[] MainPageSize = { 200, 200 };
    public string[] MainPageButtonTexts = { "Continue", "Options", "Credits", "Reset"};

    public int[] OptionsPageSize = { 300, 300 };
    public string[] toolbarString = { "Audio", "Graphics", "Stats", "System" };
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
    public string[] statsText =
    {
        "FPS",
        "Triangles",
        "Vertex",
        "FPS Graph"
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

    public int[] CreditsPageSize = { 300, 300};
    public string[] Credits =
    {
        "Pause menu by Ithos",
        "based on a script from Phil Chu",
        "http://wiki.unity3d.com/index.php?title=PauseMenu"
    };
    public Texture[] CreditIcons;

    void Awake()
    {
        addState(PauseMenuStateNames.StateNames[0], 
            new PauseMenuMain(this, gameManagerTag,
            MainPageButtonTexts[0], MainPageButtonTexts[1], MainPageButtonTexts[2], MainPageButtonTexts[3],
            MainPageSize[0], MainPageSize[1]) );

        addState(PauseMenuStateNames.StateNames[1],
            new PauseMenuOptions(this, graphMaterial, mainCameraTag, gldepth, StatsSize[0], StatsSize[1], StatsSize[2], StatsSize[3],
            lowFPS, highFPS, lowFPSColor, highFPSColor, FPSLabelFormat, trianglesText, vertexText, OptionsPageSize[0], 
            OptionsPageSize[1], toolbarString, volumeLabel, qualities, qualityButtonsText, statsText, deviceText)
            );


        addState(PauseMenuStateNames.StateNames[2],
            new PauseMenuCredits(this, Credits, CreditIcons, CreditsPageSize[0], CreditsPageSize[1]) );
    }

    override public void PauseGame()
    {
        base.PauseGame();

        setState(PauseMenuStateNames.StateNames[0]);
    }

}
