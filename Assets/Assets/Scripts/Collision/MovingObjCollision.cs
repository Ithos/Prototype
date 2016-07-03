using UnityEngine;
using System.Collections;

public class MovingObjCollision : MonoBehaviour
{

    public GameManager gameManager;
    public string GameManagerTag = "";
    public string playerTag = "";
    public float stopTime = 0.0f;
    public float dispersionRange = 0.0f;
    public int points = 0;

    // Use this for initialization
    void Start()
    {
        if(gameManager == null)
        {
            gameManager = GameObject.FindGameObjectWithTag(GameManagerTag).GetComponent<GameManager>();
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.tag != playerTag)
            return;

        Movement mov = GetComponent<Movement>();

        if (mov != null && mov.isMoving())
        {
            /// TODO -- run animation -- ///

            mov.timedStop(stopTime + UnityEngine.Random.Range(-dispersionRange, dispersionRange));
            gameManager.Score += points;
        }

    }
}
