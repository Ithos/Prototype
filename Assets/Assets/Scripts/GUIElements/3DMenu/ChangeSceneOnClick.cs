using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class ChangeSceneOnClick : MonoBehaviour {


    public GameObject[] toRevealArray;
    public GameObject[] toHideArray;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void OnMouseUpAsButton()
    {
        gameObject.SendMessage("OnMouseExit");

        foreach (GameObject go in toHideArray)
        {
            go.SetActive(false);
        }

        foreach(GameObject go in toRevealArray)
        {
            go.SetActive(true);
        }
    }
}
