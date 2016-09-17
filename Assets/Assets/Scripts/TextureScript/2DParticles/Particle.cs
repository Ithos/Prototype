using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Particle {

    protected Vector2 _leftDown = new Vector2();
    protected Vector2 _velocity = new Vector2();
    protected Vector2 _size = new Vector2();
    protected float _lifetime = -1.0f;
    protected bool _active = true;
    protected List<Color> _colors = new List<Color>();


    public bool IsActive
    {
        get { return _active; }
        set { _active = value; }
    }

    public Vector2 Position
    {
        get
        {
            return _leftDown + _size / 2.0f;
        }
    }

    public Particle()
    {
    }

    public Particle(Vector2 sourcePosition, Vector2 sourceSize, Vector2 baseVelocity, Vector2 variableVelocity, Vector2 baseSize, Vector2 variableSize, float baseLifetime, float variableLifetime, Color color, bool active)
    {

        init(sourcePosition, sourceSize, baseVelocity, variableVelocity, baseSize, variableSize, baseLifetime, variableLifetime, color, active);

    }

    protected void init(Vector2 sourcePosition, Vector2 sourceSize, Vector2 baseVelocity, Vector2 variableVelocity, Vector2 baseSize, Vector2 variableSize, float baseLifetime, float variableLifetime, Color color, bool active)
    {
        _velocity.x = baseVelocity.x + Random.Range(0, variableVelocity.x);
        _velocity.y = baseVelocity.y + Random.Range(0, variableVelocity.y);

        _size.x = baseSize.x + Random.Range(0, variableSize.x);
        _size.y = baseSize.y + Random.Range(0, variableSize.y);

        _leftDown.x = sourcePosition.x + Random.Range(0, sourceSize.x) - _size.x / 2.0f;
        _leftDown.y = sourcePosition.y + Random.Range(0, sourceSize.y) - _size.y / 2.0f;

        _lifetime = baseLifetime + Random.Range(0, variableLifetime);
        _active = active;

        for (int i = 0; i < _size.y; ++i)
        {
            for (int j = 0; j < _size.x; ++j)
            {
                _colors.Add(color);
            }
        }
    }

    virtual public bool Update(float tick, Texture2D destination)
    {
        bool limitedlifetime = false;

        if(_lifetime > 0)
        {
            limitedlifetime = true;
            _lifetime -= tick;
        }

        _leftDown.x += tick * _velocity.x;
        _leftDown.y += tick * _velocity.y;

        if (_active)
        {
            if (_leftDown.x < 0)
            {
                _size.x += _leftDown.x;
                _leftDown.x = 0;
            }
            if ((_leftDown.x + _size.x) > destination.width)
            {
                _size.x = (destination.width - _leftDown.x);
            }
            if (_leftDown.y < 0)
            {
                _size.y += _leftDown.y;
                _leftDown.y = 0;
            }
            if ((_leftDown.y + _size.y) > destination.height)
            {
                _size.y = (destination.height - _leftDown.y);
            }
            destination.SetPixels((int)_leftDown.x, (int)_leftDown.y, (int)(_size.x), (int)(_size.y), _colors.ToArray());
        }

        if(limitedlifetime && _lifetime <= 0)
        {
            return true;
        }

        if((_leftDown.x) > destination.width || (_leftDown.x + _size.x ) < 0 || (_leftDown.y) > destination.height || (_leftDown.y  + _size.y) < 0)
        {
            return true;
        }

        return false;
    }
}
	
