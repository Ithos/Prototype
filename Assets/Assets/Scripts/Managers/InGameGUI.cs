using UnityEngine;
using System.Collections;

public class InGameGUI : MonoBehaviour {

    public GUIElement[] guiElementList;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnGUI()
    {
        for(int i = 0; i < guiElementList.Length; ++i)
        {
            guiElementList[i].OnGUIDraw();
        }
    }
}
