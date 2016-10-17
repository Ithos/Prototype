using UnityEngine;
using System.Collections;

public class QuitOnClick : MonoBehaviour {

    public GameMaster master = null;
    public string masterTag = "MASTER";

    // Use this for initialization
    void Start()
    {
        if (master == null)
        {
            master = GameObject.FindGameObjectWithTag(masterTag).GetComponent<GameMaster>();
        }

        if (master == null)
        {
            Debug.LogError("No GameMaster found.");
        }
    }

    public void OnMouseUpAsButton()
    {
        master.Quit();
    }
}
