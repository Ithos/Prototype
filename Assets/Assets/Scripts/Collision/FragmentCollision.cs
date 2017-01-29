using UnityEngine;
using System.Collections;

public class FragmentCollision : MonoBehaviour {

    public GameManager gameManager = null;
    public string GameManagerTag = "";
    public string bottomLimitTag = "";
    public int points = 0;

    private bool _active = true;

	// Use this for initialization
	void Start () {
	    if(gameManager == null)
        {
            gameManager = GameObject.FindGameObjectWithTag(GameManagerTag).GetComponent<GameManager>();
        }

	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnCollisionEnter(Collision collision)
    {
        string colTag = collision.collider.gameObject.tag;

        if(colTag == bottomLimitTag && _active)
        {
            gameManager.Score += points;
            _active = false;
        }
    }

    public bool isActive()
    {
        return _active;
    }
}
