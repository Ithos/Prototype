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

    protected static int _windowId = 0;
    protected Rect _windowRect;

    // Use this for initialization
    protected void Start () {
        ++_windowId;

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
