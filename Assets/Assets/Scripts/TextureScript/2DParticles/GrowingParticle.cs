using UnityEngine;
using System.Collections;

public class GrowingParticle : Particle {

    private float _growingRate = 0;


	public GrowingParticle(Vector2 sourcePosition, Vector2 sourceSize, float baseVelocity, float variableVelocity, 
        Vector2 baseSize, Vector2 variableSize, float baseLifetime, float variableLifetime, float growingBaseRate, float growingVariableRate, Color color, bool active)
    {
       
        _growingRate = growingBaseRate + Random.Range(0, growingVariableRate);

        float angle = Random.Range(0, 2 * Mathf.PI);

        Vector2 vel = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * (baseVelocity + Random.Range(0, variableVelocity));

        init(sourcePosition, sourceSize, vel, Vector2.zero, baseSize, variableSize, baseLifetime, variableLifetime, color, active);
    }

    override public bool Update(float tick, Texture2D destination )
    {
        Vector2 currentPos = Position;
        _size += _size *_growingRate * tick;
        _leftDown = currentPos - _size / 2.0f;
        currentPos = Position;

        for(int i = 0; _colors.Count < (_size.x * _size.y); ++i)
        {
            _colors.Add(_colors[0]);
        }

        return base.Update(tick, destination);

    }
}
