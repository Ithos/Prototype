using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class OnClickLoadLastKnownScene : MonoBehaviour {

    public GameMaster master = null;
    public string masterTag = "MASTER";

	// Use this for initialization
	void Start () {
	    if(master == null)
        {
            master = GameObject.FindGameObjectWithTag(masterTag).GetComponent<GameMaster>();
        }

        if(master == null)
        {
            Debug.LogError("No GameMaster found.");
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void OnMouseUpAsButton()
    {
        SceneManager.LoadScene(master.lastScene);
    }
}
