using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class OnClickLoadScene : MonoBehaviour {

    public float DisabledAlpha = 0.3f;
    public bool Enabled = false;
    public string SceneToLoad = "Tutorial_Scene";


	// Use this for initialization
	void Start () {
	    if(!Enabled)
        {
            SetActivate(false);
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void OnMouseUpAsButton()
    {
        if(Enabled)
            SceneManager.LoadScene(SceneToLoad);
    }

    public void SetActivate(bool isActivated)
    {
        Enabled = isActivated;
        MeshRenderer renderer = GetComponent<MeshRenderer>();
        if(renderer != null)
        {
            if (!Enabled)
            {
                renderer.material.color *= DisabledAlpha;
                MouseOverHighlight highlight = GetComponent<MouseOverHighlight>();
                if (highlight != null)
                {
                    highlight.Enabled = false;
                }
            }
            renderer.material.color = new Color(renderer.material.color.r, renderer.material.color.g, renderer.material.color.b, Enabled ? 0 : DisabledAlpha);
            
            
        }
    }
}
