using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TextureAnimation : MonoBehaviour {

    public int columns = 2;                                             // Number of columns for the texture animation
    public int rows = 2;                                                // Number of rows for the texture animation
    public Vector2 scale = new Vector3(1.0f, 1.0f);                     // Scale of the texture. Must be non-zero. Negative flips the image.
    public Vector2 offset = Vector2.zero;                               // Offsets the texture. It can be used to displace the texture from the center.
    public Vector2 buffer = Vector2.zero;                               // Buffer that can be used to store frames in order to hide borders or artifacts.
    public float framesPerSecond = 10f;                                 // Frames per second that the animation is trying to run at
    public bool playOnce = false;                                       // Set to true if you want the animation to play only once
    public bool disableUponCompletion = false;                          // Set to true to disable the render of the texture after the animation is played.
    public bool enableEvents = false;                                   // Sets an event that fires when the animation finishes
    public bool playOnEnable = true;                                    // Sets the animation to play when the entity is enabled
    public bool newMaterialInstance = false;                            // Set to true to create a new material instance

    private int _index = 0;                                             // Keeps track of the current frame
    private Vector2 _textureSize = Vector2.zero;                        // Keeps track of the texture scale
    private Material _materialInstance = null;                          // Reference to the material that we will create if necessary
    private bool _hasMaterialInstace = false;                           // A flag that keeps track of the presence of a material
    private bool _isPlaying = false;                                    // A flag that indicates if the animation is palying
    private Vector2 _globalOffset = new Vector2();                      // Vector that saves the offset state
    private bool _init = true;                                          // Flag that indicates if its the first loop     

    public delegate void VoidEvent();                                   // The event delegate
    private List<VoidEvent> _voidEventCallbackList;                     // List of fuctions to call if events are online

    private const string UPDATE_TILING_COROUTINE = "updateTilling";

    // Method to register a callback function
    public void registerCallback(VoidEvent cbFunction)
    {
        _voidEventCallbackList.Add(cbFunction);
    }

    // Method to unregister a callback function
    public void unregisterCallback(VoidEvent cbFunction)
    {
        if(_voidEventCallbackList.Contains(cbFunction))
        {
            _voidEventCallbackList.Remove(cbFunction);
        }
    }

    public void Play()
    {
        // stop the animation if it's playing
        if(_isPlaying)
        {
            StopCoroutine(UPDATE_TILING_COROUTINE);
            _isPlaying = false;
        }

        // Enable renderer just in case
        GetComponent<Renderer>().enabled = true;

        _index = columns;

        StartCoroutine(updateTilling());
    }

    public void ChangeMaterial(Material newMaterial, bool newInstance = false)
    {
        Renderer renderer = GetComponent<Renderer>();
        if (newInstance)
        {
            // If we already have a material instance, and we want to create a new one 
            // we have to clean the previous one
            Object.Destroy(renderer.sharedMaterial);

            // Create new material
            _materialInstance = new Material(newMaterial);

            // Assign material to the renderer
            renderer.sharedMaterial = _materialInstance;

            // Set material flag
            _hasMaterialInstace = true;
        }
        else
        {
            renderer.sharedMaterial = newMaterial;
        }

        CalcTextureSize();

        // Assign texture size
        renderer.sharedMaterial.SetTextureScale("_MainTex", _textureSize);

    }

    private void Awake()
    {
        // Allocate memory for events if needed
        if (enableEvents)
            _voidEventCallbackList = new List<VoidEvent>();

        //Set The global offset to the initial offset
        _globalOffset = offset;

        // Create the material instance and/or recalculate texture size
        ChangeMaterial(GetComponent<Renderer>().sharedMaterial, _hasMaterialInstace);
    }

    private void OnDestroy()
    {
        if(_hasMaterialInstace)
        {
            Object.Destroy(GetComponent<Renderer>().sharedMaterial);
            _hasMaterialInstace = false;
        }
    }

    private void OnEnable()
    {
        CalcTextureSize();

        if (playOnEnable)
            Play();
    }

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    private IEnumerator updateTilling()
    {
        _isPlaying = true;

        // Max number of frames
        int checkAgainst = (rows * columns);

        while(true)
        {
            // If this is the last frame we stop or go back to the top
            if (_index >= checkAgainst)
            {
                // Reset the index
                _index = 0;

                // If we are only playing the animation one time
                if (playOnce && !_init)
                {
                    if (checkAgainst == columns)
                    {
                        // Fire the event if needed
                        if (enableEvents)
                        {
                            HandleCallbacks(_voidEventCallbackList);
                        }

                        if (disableUponCompletion)
                        {
                            GetComponent<Renderer>().enabled = false;
                        }

                        _isPlaying = false;

                        // break the loop
                        yield break;
                    }
                    else
                    {
                        // Loop through another row
                        checkAgainst = columns;
                    }
                }
            }

            // Apply the offset to move the frame 
            ApplyOffset();

            // Increment the index
            ++_index;

            // Set the init flag to false
            _init = false;

            yield return new WaitForSeconds(1f / framesPerSecond);
        }
    }

    private void HandleCallbacks(List<VoidEvent> cbList)
    {
        // Loop through all the callbacs and invoke them
        for(int i = 0; i < cbList.Count; ++i)
        {
            cbList[i]();
        }
    }

    private void ApplyOffset()
    {
        Vector2 localoffset = new Vector2((float)_index/columns - (_index/columns), 
                                        1 - (((_index/columns) / (float)rows )));

        if (localoffset.y == 1.0f)
            localoffset.y = 0.0f;

        //If the texture has been sacaled reposition it to the center
        localoffset.x += ((1.0f / columns) - _textureSize.x ) / 2.0f;
        localoffset.y += ((1.0f / rows) - _textureSize.y) / 2.0f;

        //Add offset to global offset
        _globalOffset += localoffset;

        // Update material
        GetComponent<Renderer>().sharedMaterial.SetTextureOffset("_MainTex", _globalOffset);

        Debug.Log("offset index:" + _index);
    }

    private void CalcTextureSize()
    {
        // Sets the tile size of the texture (in UV units), based in rows and columns
        _textureSize = new Vector2(1.0f/columns, 1.0f/rows);

        // factor in the scale
        _textureSize.x /= scale.x;
        _textureSize.y /= scale.y;

        // Buffer part of the image
        _textureSize -= buffer;

    }
}
