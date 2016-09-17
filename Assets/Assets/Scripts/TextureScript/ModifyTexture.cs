using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ModifyTexture : MonoBehaviour {

    public ParticleSource.SourceData[] particleSourceData;
    public RadialParticleSource.RadialSourceData[] radialSourceData;

    private List<ParticleSource> _sourceList = new List<ParticleSource>();


	// Use this for initialization
	void Start () {
        for(int i = 0; i < particleSourceData.Length; ++i)
        {
            _sourceList.Add(new ParticleSource(particleSourceData[i]));
        }

        for (int i = 0; i < radialSourceData.Length; ++i)
        {
            _sourceList.Add(new RadialParticleSource(radialSourceData[i]));
        }
    }
	
	// Update is called once per frame
	void Update () {

	    foreach(ParticleSource current in _sourceList)
        {
            current.Update(Time.deltaTime);
        }
	}

    void OnDisable()
    {
        foreach (ParticleSource current in _sourceList)
        {
            current.resetOriginalTexture();
        }
    }
}
