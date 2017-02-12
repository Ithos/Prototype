using UnityEngine;
using System.Collections;

public class LevelActivationManager : MonoBehaviour {

    public GameMaster Master = null;
    public string GameMasterTag = "MASTER";
    public OnClickLoadScene[] LevelButtons;

	// Use this for initialization
	void Start () {
	    if(Master == null)
        {
            Master = GameObject.FindGameObjectWithTag(GameMasterTag).GetComponent<GameMaster>();
        }

        for(int i = 0; i < LevelButtons.Length; ++i)
        {
            if (i > Master.LastLevelActive) break;

            LevelButtons[i].SetActivate(true);
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
