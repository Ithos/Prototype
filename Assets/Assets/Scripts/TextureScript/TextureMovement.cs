using UnityEngine;
using System.Collections;

public class TextureMovement : MonoBehaviour {

    public float movVelx = 0.35f;
    public float movVely = 0.35f;
    public Renderer rend = null;

    private Vector2 _acumOffset = new Vector2();


	// Use this for initialization
	void Start () {
	    if(rend == null)
        {
            rend = GetComponent<Renderer>();
        }

        if(rend != null)
        {
            _acumOffset = rend.material.GetTextureOffset("_MainTex");
        }
	}
	
	// Update is called once per frame
	void Update () {
        float offsetx = Time.deltaTime * movVelx;
        float offsety = Time.deltaTime * movVely;

        _acumOffset.x += offsetx;
        _acumOffset.y += offsety;

        if (rend != null)
        {
            rend.material.SetTextureOffset("_MainTex", new Vector2(_acumOffset.x, _acumOffset.y));
        }

	}
}
