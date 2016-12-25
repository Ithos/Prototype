using UnityEngine;
using System.Collections;

public abstract class GUIElement : MonoBehaviour {

    public float x = 0.0f;
    public float y = 0.0f;
    public float width = 0.0f;
    public float height = 0.0f;

    public string text = "";

    public GameManager manager;
    public string gameManagerTag = "";

    protected static int _staticId = 0;
    protected int _windowId = 0;
    protected Rect _windowRect;

    void OnValidate()
    {
        if (x < 0) x = 0;
        if (x > 1) x = 1;
        if (y < 0) y = 0;
        if (y > 1) y = 1;
        if (width < 0) width = 0;
        if (width > 1) width = 1;
        if (height < 0) height = 0;
        if (height > 1) height = 1;
    }

    // Use this for initialization
    protected void Start () {
        _windowId = _staticId;
        ++_staticId;


         x = x * Camera.allCameras[0].pixelWidth;
         y = y * Camera.allCameras[0].pixelHeight;
        width = width * Camera.allCameras[0].pixelWidth;
        height = height * Camera.allCameras[0].pixelHeight;

        _windowRect = new Rect(x, y, width, height);

        if (manager == null)
        {
            manager = GameObject.FindGameObjectWithTag(gameManagerTag).GetComponent<GameManager>();
        }
    }
	
	// Update is called once per frame
	protected void Update () {
	
	}

    abstract public void OnGUIDraw();


}
