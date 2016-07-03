using UnityEngine;
using System.Collections;

public class FragmentContainerCollision : MonoBehaviour {

    public GameManager gameManager = null;
    public string GameManagerTag = "";
    public string playerTag = "";
    public string bottomLimitTag = "";
    public bool bottomFloor = false;
    public int points = 0;

    // Use this for initialization
    void Start () {
        if (gameManager == null)
        {
            gameManager = GameObject.FindGameObjectWithTag(GameManagerTag).GetComponent<GameManager>();
        }
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag != playerTag && !(!bottomFloor && (collision.collider.tag == bottomLimitTag)))
            return;


        Rigidbody rig = GetComponent<Rigidbody>();

        if(rig != null)
        {
            Destroy(rig);

            Transform[] children = GetComponentsInChildren<Transform>();

            for(int i = 0; i < children.Length; ++i)
            {
                children[i].gameObject.AddComponent<Rigidbody>();
                FragmentCollision col = children[i].gameObject.AddComponent<FragmentCollision>();
                col.gameManager = gameManager;
                col.GameManagerTag = GameManagerTag;
                col.bottomLimitTag = bottomLimitTag;
                col.points = points;
            }
        }
    }
}
