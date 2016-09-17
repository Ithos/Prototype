using UnityEngine;
using System.Collections;

public class RadialParticleSource : ParticleSource {
    [System.Serializable]
    public struct RadialSourceData
    {
        public Material material;
        public int particleBaseVelocity;
        public int particleVariableVelocity;
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
        public float particleGrowingRate;
        public float particleVariableGrowingRate;
        public Color particleColor;
        public bool active;
    }

    private float _growingBaseRate = 0;
    private float _growingVariableRate = 0;
    private float _particleModuleBaseVel = 0;
    private float _particleModuleVariableVel = 0;

    public RadialParticleSource(RadialSourceData data) : base(data.material, Vector2.zero, data.origin, data.particlesPool, Vector2.zero, Vector2.zero,  data.particleColor, data.sourceSize, data.particleBaseSize,
        data.particleVariableSize, data.emmisionBaseDelay, data.particleBaseLifetime, data.particleVariableLifetime, data.emmisionVariableDelay, data.active) 
    {
        init(data.particleGrowingRate, data.particleVariableGrowingRate, data.particleBaseVelocity, data.particleVariableVelocity);
    }

    override public void Update(float tick)
    {
        Texture2D texture = null;

        checkAndSetTexture(ref texture);

        if (texture == null)
            Debug.LogError("No texture recovered");

        addParticle(new GrowingParticle(_origin, _sourceSize, _particleModuleBaseVel, _particleModuleVariableVel, _particleBaseSize,
                        _particleVariableSize, _particleBaseLifetime, _particleVariableLifetime, _growingBaseRate, _growingVariableRate, _particleColor, _active), tick);

        updateParticles(tick, ref texture);

        applyTexture(ref texture);
    }

    protected void init(float growingBaseRate, float growingVariableRate, float particleModuleBaseVel, float particleModuleVariableVel)
    {
        _growingBaseRate = growingBaseRate;
        _growingVariableRate = growingVariableRate;
        _particleModuleBaseVel = particleModuleBaseVel;
        _particleModuleVariableVel = particleModuleVariableVel;
    }
}
