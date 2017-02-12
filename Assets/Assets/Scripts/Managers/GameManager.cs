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
    public string activeElementsManagerTag = "ELEMENTS_MANAGER";
    public ActiveElementsManager elementsManager = null;

    public string ballName = "PlayerBall";

    public int SceneIdNumber = 0;
    public int MinimumPoints = 300;

    private GameObject[] _privateGameObjects;

    private Configuration _configuration;
    private Savefile _savefile;

    public float secondsToCharge = 2.0f;

    public int playerAmmo = 3;
    public bool infiniteAmmo = false;

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

    public int ActiveBalls
    {
        get { return _playerAmmo; }
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

        if(elementsManager == null)
        {
            elementsManager = GameObject.FindGameObjectWithTag(activeElementsManagerTag).GetComponent<ActiveElementsManager>();
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

    public GameObject generatePlayerBall()
    {
        checkPoints();

        if (_playerAmmo < playerAmmo || infiniteAmmo)
        {
            
            _privateGameObjects[SpawnableObjects.Length + _playerAmmo] =
                Instantiate(BallPrefab, BallSpawn.position, Quaternion.identity) as GameObject;

            _privateGameObjects[SpawnableObjects.Length + _playerAmmo].GetComponent<Transform>().
                SetParent(BallSpawn.parent.GetComponent<Transform>());

            ++_playerAmmo;

            GameObject ball = _privateGameObjects[SpawnableObjects.Length + _playerAmmo - 1];

            ball.name = ballName + _playerAmmo.ToString();

            if (infiniteAmmo && _playerAmmo > playerAmmo)
            {
                _playerAmmo = 1;
            }

            return ball;
        }

        ++_playerAmmo;

        return null;
    }

    public GameObject getPlayerBall(int index)
    {
        if (_privateGameObjects.Length < SpawnableObjects.Length + index || index > playerAmmo)
        {
            Debug.LogError("GameManager >>> Wrong player ball index.");
            return null;
        }

        return _privateGameObjects[SpawnableObjects.Length + index];
    }

    private int findBallIndex(GameObject ball)
    {
        int index = -1;

        for(int i = SpawnableObjects.Length; i < _privateGameObjects.Length; ++i)
        {
            if(_privateGameObjects[i] == ball)
            {
                index = i - SpawnableObjects.Length;
            }
        }

        return index;
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

    public void swapTargetBall(GameObject newActiveBall)
    {
        elementsManager.setActiveBall(newActiveBall, findBallIndex(newActiveBall));
    }

    public void swapTargetBall()
    {
        elementsManager.setActiveBall();
    }

    public void decreaseActiveTime()
    {
        elementsManager.decreaseActiveTime();
    }

    public void increaseActiveTime()
    {
        elementsManager.increaseActiveTime();
    }

    public GameObject getActiveBall()
    {
        SlowMovement ret = elementsManager.getActiveBall();
        if (ret == null)
            return null;

        return ret.gameObject;
    }

    public void SetCompleteMap()
    {
        gameMaster.LastLevelActive = SceneIdNumber;
    }

    public bool IsCompleteMap()
    {
        return Score >= MinimumPoints;
    }

    private void checkPoints()
    {
        if(IsCompleteMap())
        {
            SetCompleteMap();
        }
    }
}
