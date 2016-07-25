using UnityEngine;
using System.Collections;

public class FPSGraphic : MonoBehaviour
{
    public bool showFPSGraph = false;
    public float FPS = 0;

    public Material graphMaterial = null;
    public float gldepth = 0.5f;

    private float[] _FPSArray = null;
    

    void Start()
    {
        _FPSArray = new float[Screen.width];
    }

    void OnPostRender()
    {
        if (showFPSGraph && graphMaterial != null)
        {
            GL.PushMatrix(); // Save current matrix
            GL.LoadPixelMatrix(); // Load matrix for pixel 2D representation
            for (int i = 0; i < graphMaterial.passCount; ++i) //for every pass required for this material
            {
                graphMaterial.SetPass(i);
                GL.Begin(GL.LINES);
                for (int j = 0; j < _FPSArray.Length; ++j)
                {
                    GL.Vertex3(j, _FPSArray[j], gldepth);
                }
                GL.End();
            }
            GL.PopMatrix(); //Recover the last saved matrix
            ScrollFPS();
        }
    }

    private void ScrollFPS()
    {
        for (int i = 1; i < _FPSArray.Length; ++i)
        {
            _FPSArray[i - 1] = _FPSArray[i];
        }

        if (FPS < 1000)
        {
            _FPSArray[_FPSArray.Length - 1] = FPS;
        }
    }
}

