using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ParticleSource {

    [System.Serializable]
    public struct SourceData
    {
        public Material material;
        public Vector2 particleBaseVelocity;
        public Vector2 particleVariableVelocity;
        public Vector2 origin;
        public Vector2 sourceVelocity;
        public int particlesPool;
        public float particleBaseLifetime;
        public float particleVariableLifetime;
        public float emmisionBaseDelay;
        public float emmisionVariableDelay;
        public Vector2 sourceSize;
        public Vector2 particleBaseSize;
        public Vector2 particleVariableSize;
        public Color particleColor;
        public bool active;
    }

    protected Material _material = null;
    protected Vector2 _particleBaseVelocity = new Vector2();
    protected Vector2 _particleVariableVelocity = new Vector2();
    protected Vector2 _origin = new Vector2();
    protected Vector2 _sourceVelocity = new Vector2();
    protected int _particlesPool = 0;
    protected float _particleBaseLifetime = -1.0f;
    protected float _particleVariableLifetime = 0;
    protected float _emmisionBaseDelay = 0; 
    protected float _emmisionVariableDelay = 0;
    protected Vector2 _sourceSize = new Vector2();
    protected Vector2 _particleBaseSize = new Vector2();
    protected Vector2 _particleVariableSize = new Vector2();
    protected Color _particleColor = new Color();
    protected bool _active = true;

    protected List<Particle> _particles = new List<Particle>();
    protected Texture _originalTexture = null;
    protected Color32[] _privateTextureCopy = null;
    protected int _texHeight = 0, _texWidth = 0;
    protected float _time = 0.0f;

    public Vector2 particleBaseVel
    {
        get { return _particleBaseVelocity; }
        set { _particleBaseVelocity = value; }
    }

    public Vector2 particleVariableVel
    {
        get { return _particleVariableVelocity; }
        set { _particleVariableVelocity = value; }
    }

    public Vector2 sourceVel
    {
        get { return _sourceVelocity; }
        set { _sourceVelocity = value; }
    }

    public Vector2 sourceSize
    {
        get { return _sourceSize; }
        set { sourceSize = value; }
    }

    public float baseLifetime
    {
        get { return _particleBaseLifetime; }
        set { _particleBaseLifetime = value; }
    }

    public float variableLifetime
    {
        get { return _particleVariableLifetime; }
        set { _particleVariableLifetime = value; }
    }

    public float emmisionBaseDelay
    {
        get { return _emmisionBaseDelay; }
        set { _emmisionBaseDelay = value; }
    }

    public float emmisionVariableDelay
    {
        get { return _emmisionVariableDelay; }
        set { _emmisionVariableDelay = value; }
    }

    public bool active
    {
        get { return _active; }
        set { _active = value; }
    }

    public Color particlesColor
    {
        get { return _particleColor; }
        set { _particleColor = value; }
    }

    public int particlePool
    {
        get { return _particlesPool; }
        set { _particlesPool = value; } /// revisit
    }

    public ParticleSource()
    {
        init(new SourceData());
    }

    public ParticleSource(Material material, Vector2 origin, int poolSize, Vector2 particleBaseVelocity, Color particleColor, float emmisionBaseDelay = 0, 
        float particleBaseLifetime = 0, float particleVariableLifetime = 0, float emmisionVariableDelay = 0, bool active = true)
    {
        init(material, particleBaseVelocity, origin, poolSize, particleBaseLifetime,  Vector2.zero, Vector2.zero, particleVariableLifetime, emmisionBaseDelay, emmisionVariableDelay, active, 
            Vector2.zero, Vector2.zero, Vector2.zero, particleColor);
    }

    public ParticleSource(Material material, Vector2 origin, int poolSize, Vector2 particleBaseVelocity, Color particleColor, Vector2 sourceSize, Vector2 particleBaseSize, Vector2 particleVariableSize, float emmisionBaseDelay = 0,
        float particleBaseLifetime = 0, float particleVariableLifetime = 0, float emmisionVariableDelay = 0, bool active = true)
    {
        init(material, particleBaseVelocity, origin, poolSize, particleBaseLifetime, Vector2.zero, Vector2.zero, particleVariableLifetime, emmisionBaseDelay, emmisionVariableDelay, active,
            sourceSize, particleBaseSize, particleVariableSize, particleColor);
    }

    public ParticleSource(Material material, Vector2 particleBaseVelocity, Vector2 origin, int poolSize, Vector2 particleVariableVelocity, Color particleColor, float emmisionBaseDelay = 0, 
        float particleBaseLifetime = 0, float particleVariableLifetime = 0, float emmisionVariableDelay = 0, bool active = true)
    {
        init(material, particleBaseVelocity, origin, poolSize, particleBaseLifetime, Vector2.zero, particleVariableVelocity, particleVariableLifetime, emmisionBaseDelay, emmisionVariableDelay, active, 
            Vector2.zero, Vector2.zero, Vector2.zero, particleColor);
    }

    public ParticleSource(Material material, Vector2 particleBaseVelocity, Vector2 origin, int poolSize, Vector2 particleVariableVelocity, Vector2 sourceVelocity, Color particleColor, float emmisionBaseDelay = 0, 
        float particleBaseLifetime = 0, float particleVariableLifetime = 0, float emmisionVariableDelay = 0, bool active = true)
    {
        init(material, particleBaseVelocity, origin, poolSize, particleBaseLifetime, sourceVelocity, particleVariableVelocity, particleVariableLifetime, emmisionBaseDelay, emmisionVariableDelay, active, 
            Vector2.zero, Vector2.zero, Vector2.zero, particleColor);
    }

    public ParticleSource(Material material, Vector2 particleBaseVelocity, Vector2 origin, int poolSize, Vector2 particleVariableVelocity, Vector2 sourceVelocity, Color particleColor,
        Vector2 sourceSize, Vector2 particleBaseSize, Vector2 particleVariableSize , float emmisionBaseDelay = 0,
        float particleBaseLifetime = 0, float particleVariableLifetime = 0, float emmisionVariableDelay = 0, bool active = true)
    {
        init(material, particleBaseVelocity, origin, poolSize, particleBaseLifetime, sourceVelocity, particleVariableVelocity, particleVariableLifetime, emmisionBaseDelay, emmisionVariableDelay, active,
            sourceSize, particleBaseSize, particleVariableSize , particleColor);
    }

    public ParticleSource(SourceData data)
    {
        init(data);
    }

    protected void init(SourceData data)
    {
        init(data.material, data.particleVariableVelocity != null ? data.particleBaseVelocity : Vector2.zero, data.origin != null ? data.origin : Vector2.zero, data.particlesPool, data.particleBaseLifetime,
            data.sourceVelocity != null ? data.sourceVelocity : Vector2.zero, data.particleVariableVelocity != null ? data.particleVariableVelocity : Vector2.zero, data.particleVariableLifetime,
            data.emmisionBaseDelay, data.emmisionVariableDelay, data.active, data.sourceSize != null ? data.sourceSize : Vector2.one, data.particleBaseSize != null ? data.particleBaseSize : Vector2.one,
            data.particleVariableSize != null ? data.particleVariableSize : Vector2.zero, data.particleColor != null ? data.particleColor : new Color());
    }

    protected void init(Material material, Vector2 particleBaseVelocity, Vector2 origin, int poolSize, float particleBaseLifetime, 
        Vector2 sourceVelocity, Vector2 particleVariableVelocity, float particleVariableLifetime, float emmisionBaseDelay, float emmisionVariableDelay, bool active, 
        Vector2 sourceSize, Vector2 particleBaseSize, Vector2 particleVariableSize , Color particleColor)
    {
        _material = material;
        _originalTexture = _material.GetTexture("_MainTex");
        _privateTextureCopy = ((Texture2D)_originalTexture).GetPixels32();
        _texWidth = _material.GetTexture("_MainTex").width;
        _texHeight = _material.GetTexture("_MainTex").height;
        _particleBaseVelocity = particleBaseVelocity;
        _particleVariableVelocity = particleVariableVelocity;
         _origin = origin;
         _sourceVelocity = sourceVelocity;
         _particlesPool = poolSize;
        _particleBaseLifetime = particleBaseLifetime;
        _particleVariableLifetime = particleVariableLifetime;
        _emmisionBaseDelay = emmisionBaseDelay;
        _emmisionVariableDelay = emmisionVariableDelay;
        _sourceSize = sourceSize;
        _particleBaseSize = particleBaseSize;
        _particleVariableSize = particleVariableSize;
        _particleColor = particleColor;
        _active = active;

        checkErrors();
    }

    virtual public void Update(float tick)
    {
        Texture2D texture = null;

        checkAndSetTexture(ref texture);

        if (texture == null)
            Debug.LogError("No texture recovered");

        addParticle(new Particle(_origin, _sourceSize, _particleBaseVelocity, _particleVariableVelocity, _particleBaseSize,
                        _particleVariableSize, _particleBaseLifetime, _particleVariableLifetime, _particleColor, _active), tick);

        updateParticles(tick, ref texture);

        applyTexture(ref texture);
    }

    public void hide()
    {
        activateAllParticles(false);
    }

    public void show()
    {
        activateAllParticles(true);
    }

    public void activateAllParticles(bool active)
    {
        foreach (Particle current in _particles)
        {
            current.IsActive = active;
        }
    }

    protected void checkErrors()
    {
        if (_material == null)
            Debug.LogError("Object material is null");
        if (_particleBaseVelocity == null)
            Debug.LogError("ParticleBaseVelocity is null");
        if (_particleVariableVelocity == null)
            Debug.LogError("ParticleVariableVelocity is null");
        if (_origin == null)
            Debug.LogError("Source origin coordinates are null");
        if (_sourceVelocity == null)
            Debug.LogError("Source velocity is null");
        if (_sourceSize == null)
            Debug.LogError("Source size is null");
        if (_particleBaseSize == null)
            Debug.LogError("ParticleBaseSize is null");
        if (_particleVariableSize == null)
            Debug.LogError("ParticleVariableSize is null");
        if (_particleColor == null)
            Debug.LogError("ParticleColor is null");
    }

    public void resetOriginalTexture()
    {
        _material.SetTexture("_MainTex", _originalTexture);
    }

    protected void checkAndSetTexture(ref Texture2D texture)
    {
        if (_material.GetTexture("_MainTex") != _originalTexture)
            MonoBehaviour.Destroy(_material.GetTexture("_MainTex"));

        texture = new Texture2D(_texWidth, _texHeight);
        texture.SetPixels32(_privateTextureCopy);
    }

    protected void addParticle(Particle particle, float tick)
    {
        if (_active)
        {
            _time += tick;

            if (_time >= _emmisionBaseDelay + Random.Range(0, _emmisionVariableDelay))
            {
                _time = 0.0f;

                if (_particles.Count < _particlesPool)
                {
                    _particles.Add(particle);
                }
            }
        }
        else
        {
            _time = 0.0f;
        }
    }

    protected void updateParticles(float tick, ref Texture2D texture)
    {
        List<Particle> deleteList = new List<Particle>();

        foreach (Particle current in _particles)
        {
            if (current.Update(tick, texture))
            {
                deleteList.Add(current);
            }
        }

        foreach (Particle current in deleteList)
        {
            _particles.Remove(current);
        }
    }

    protected void applyTexture(ref Texture2D texture)
    {
        texture.Apply();

        _material.SetTexture("_MainTex", texture);
    }
}
