using UnityEngine;
using System.Collections;
using UnityEditor;
using UnityEditor.SceneManagement;

public class GameManager : MonoBehaviour {

    public GameObject[] SpawnableObjects;
    public Transform[] SpawnPointsInOrder;

    public GameObject BallPrefab;
    public Transform BallSpawn;

    public string gameMasterTag = "MASTER";
    public GameMaster gameMaster = null;

    private GameObject[] _privateGameObjects;

    private Configuration _configuration;
    private Savefile _savefile;

    public float secondsToCharge = 2.0f;

    public int playerAmmo = 3;

    private int _score = 0;
    private float _time = 0;
    private int _playerAmmo = 0;
    private float _charge = 0;


    public int Score
    {
        set { _score = value; }
        get { return _score; }
    }

    public int BallsLeft
    {
        get { return playerAmmo - _playerAmmo + 1; }
    }

    public float CurrentCharge
    {
        get { return _charge; }
    }

    public Configuration Config
    {
        get { return _configuration; }
    }

    public Savefile Svfile
    {
        get { return _savefile; }
    }

    void OnValidate()
    {
        if(SpawnableObjects != null && SpawnPointsInOrder != null && (SpawnableObjects.Length != SpawnPointsInOrder.Length) )
        {
            Debug.LogError("The number of objects and spawn points is not equal.");
        }
    }

	// Use this for initialization
	void Start () {
        _privateGameObjects = new GameObject[SpawnableObjects.Length + playerAmmo + 1];
        generateObjects();
        generatePlayerBall();

        if(gameMaster == null)
        {
            gameMaster = GameObject.FindGameObjectWithTag(gameMasterTag).GetComponent<GameMaster>();
        }

        if(gameMaster != null)
        {
            gameMaster.LastScene = EditorSceneManager.GetActiveScene().name;
            _configuration = gameMaster.LoadedConfiguration;
            _savefile = gameMaster.LoadedSavefile;
        }
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnDestroy()
    {
        if(gameMaster != null)
        {
            gameMaster.SaveLastScene();
        }
    }

    public void Pause()
    {
        gameMaster.toPause();
    }

    private void generateObjects()
    {
        if (SpawnableObjects.Length == SpawnPointsInOrder.Length)
        {
            for (int i = 0; i < SpawnableObjects.Length; ++i)
            {
                if(SpawnableObjects[i] != null && SpawnPointsInOrder[i] != null)
                {
                    _privateGameObjects[i] = Instantiate(SpawnableObjects[i], SpawnPointsInOrder[i].position, Quaternion.identity) as GameObject;
                }
            }
        }
        else
        {
            Debug.LogError("The number of objects and spawn points is not equal.");
        }
    }

    public Rigidbody generatePlayerBall()
    {
        if(_playerAmmo < playerAmmo)
        {
            
            _privateGameObjects[SpawnableObjects.Length + _playerAmmo] =
                Instantiate(BallPrefab, BallSpawn.position, Quaternion.identity) as GameObject;

            _privateGameObjects[SpawnableObjects.Length + _playerAmmo].GetComponent<Transform>().
                SetParent(BallSpawn.parent.GetComponent<Transform>());

            ++_playerAmmo;

            return _privateGameObjects[SpawnableObjects.Length + _playerAmmo - 1].GetComponent<Rigidbody>();
        }

        ++_playerAmmo;

        return null;
    }

    public void addCharge(float charge)
    {
        _charge += charge;
    }

    public void emptyCharge()
    {
        _charge = 0;
    }

    private void deleteAllObjects()
    {
        for(int i = 0; i < _privateGameObjects.Length; ++i)
        {
            Destroy(_privateGameObjects[i]);
        }
    }

    private void resetParameters()
    {
        _score = 0;
        _time = 0;
        _playerAmmo = 0;
        _charge = 0;
}

    public void resetScene()
    {
        deleteAllObjects();
        generateObjects();
        resetParameters();
    }
}
