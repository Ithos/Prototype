using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[RequireComponent(typeof(Image))]
public class TutorialTextures : MonoBehaviour {

    public Texture2D[] TutorialImages;

    public float BlinkTime = 0.1f;

    public int x = 0;
    public int y = 0;
    public int width = 100;
    public int height = 80;

    public float alpha = 0.5f;

    private Texture2D _activeImage = null;
    private Texture2D _defaultImage = null;
    private Texture2D _textureToDraw = null;

    private float _time = 0.0f;
    private Color _alphaColor = Color.white;
    private Color _saveColor = Color.white;

	// Use this for initialization
	void Start () {
        if(TutorialImages.Length > 0)
        {
            _defaultImage = TutorialImages[0];
            _activeImage = TutorialImages[0];
            _textureToDraw = _defaultImage;
        }

        _alphaColor = new Color(GUI.color.r, GUI.color.g, GUI.color.b, alpha);
        _saveColor = GUI.color;

    }
	
	// Update is called once per frame
	void Update () {

        if(_time < BlinkTime)
        {
            _time += Time.deltaTime;
        }
        else
        {
            if(_textureToDraw == _defaultImage)
            {
                _textureToDraw = _activeImage;
            }
            else
            {
                _textureToDraw = _defaultImage;
            }

            _time = 0.0f;
        }
	
	}

    void OnGUI()
    {
        GUI.color = _alphaColor;
        GUI.DrawTexture(new Rect(x, y, width, height), _textureToDraw, ScaleMode.ScaleToFit, true);
        GUI.color = _saveColor;

    }

    public void SetActiveImage(int num)
    {
        if(num < TutorialImages.Length)
        {
            _activeImage = TutorialImages[num];
        }
        else
        {
            Debug.LogError("Texture index not found.");
        }
    }


}
