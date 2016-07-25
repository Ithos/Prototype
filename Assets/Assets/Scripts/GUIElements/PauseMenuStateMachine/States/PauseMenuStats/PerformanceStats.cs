using UnityEngine;
using System.Collections;

public class PerformanceStats : PauseMenuStats {

    public bool showFPS = false;
    public bool showTris = false;
    public bool showVerts = false;
    public bool showFPSGraph = false;

    
    private float _FPS = 0;

    private float[] _size = { 100, 10, 100, 200 };

    private string _FPSLabelFormat = "#,##0 fps";
    private string _vertexText = "vertex";
    private string _trianglesText = "triangles";

    private int _lowFPS = 30;
    private int _highFPS = 50;

    private Color _lowFPSColor = Color.red;
    private Color _highFPSColor = Color.green;

    private long _tris = 0;
    private long _verts = 0;

    
    

    private GameObject _mainCamera = null;
    private FPSGraphic _graphic = null;

    public PerformanceStats()
    {
    }

    public PerformanceStats(Material graphMaterial, string mainCameraTag, float gldepth, float xMargin, float y, float width, float heigth, 
        int lowFPS, int highFPS, Color lowFPSColor, Color highFPSColor, string fpsFormat, string trianglesText,
        string vertexText)
    {
        _size[0] = xMargin; _size[1] = y; _size[2] = width; _size[3] = heigth;
        _lowFPS = lowFPS;
        _highFPS = highFPS;
        _lowFPSColor = lowFPSColor;
        _highFPSColor = highFPSColor;
        _FPSLabelFormat = fpsFormat;
        _trianglesText = trianglesText;
        _vertexText = vertexText;

        if(_mainCamera == null)
        {
            _mainCamera = GameObject.FindGameObjectWithTag(mainCameraTag);
        }

        if(_mainCamera != null)
        {
            _graphic = _mainCamera.AddComponent<FPSGraphic>();
            _graphic.graphMaterial = graphMaterial;
            _graphic.gldepth = gldepth;
        }
        else
        {
            Debug.LogError("No Main camera set.");
        }
    }

    

    override public void LateUpdate()
    {
        if (showFPS || showFPSGraph)
        {
            FPSUpdate();
        }

        if (_graphic != null)
        {
            _graphic.showFPSGraph = showFPSGraph;
            _graphic.FPS = _FPS;
        }
    }

    override public void OnGUI()
    {
        ShowStatNums();
    }

    private void FPSUpdate()
    {
        float delta = Time.smoothDeltaTime;
        if (!IsPaused && delta != 0.0f)
        {
            _FPS = 1 / delta;
        }

        
    }

    private void ShowStatNums()
    {
        GUILayout.BeginArea(new Rect(Screen.width - _size[0], _size[1], _size[2], _size[3]));

        if (showFPS)
        {
            string FPSstring = _FPS.ToString(_FPSLabelFormat);
            GUI.color = Color.Lerp(_lowFPSColor, _highFPSColor, (_FPS - _lowFPS) / (_highFPS - _lowFPS));
            GUILayout.Label(FPSstring);
        }
        if (showTris || showVerts)
        {
            GetObjectStats();
        }
        if (showTris)
        {
            GUILayout.Label(_tris + _trianglesText);
        }
        if (showVerts)
        {
            GUILayout.Label(_verts + _vertexText);
        }

        GUILayout.EndArea();
    }

    private void GetObjectStats()
    {
        _verts = 0;
        _tris = 0;
        GameObject[] objs = GameObject.FindObjectsOfType(typeof(GameObject)) as GameObject[];
        foreach (GameObject obj in objs)
        {
            GetObjectStats(obj);
        }
    }

    private void GetObjectStats(GameObject obj)
    {
        Component[] filters;
        filters = obj.GetComponentsInChildren<MeshFilter>();
        foreach (MeshFilter mesh in filters)
        {
            _tris += mesh.sharedMesh.triangles.Length / 3;
            _verts += mesh.sharedMesh.vertexCount;
        }
    }
}
