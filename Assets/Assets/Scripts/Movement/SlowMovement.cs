using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CharacterController))]
public class SlowMovement : MonoBehaviour {

    public float mass = 1.0f;
    public PhysicMaterial material = null;
    

    private CharacterController _ball = null;

    private Vector3 _impulse = Vector3.zero;
    private Vector3 _force = Vector3.zero;
    private Vector3 _accel = Vector3.zero;
    private Vector3 _vel = Vector3.zero;

    private float _timeDilation = 1.0f;

    private readonly Vector3 _gravity = new Vector3(0.0f, -9.8f, 0.0f);

    public float TimeDilation
    {
        get { return _timeDilation; }
        set
        {
            _timeDilation = value;

            if (_timeDilation > 1.0)
                _timeDilation = 1.0f;
            else if (_timeDilation < 0.0)
                _timeDilation = 0.0f;
        }
    }

    public bool UseGravity
    {
        get;
        set;
    }

	// Use this for initialization
	void Start () {
	    if(_ball == null)
        {
            _ball = GetComponent<CharacterController>();
        }

        if(material == null)
        {
            material = GetComponent<PhysicMaterial>();

            if(material == null)
            {
                material = new PhysicMaterial();
            }
        }
	}
	
	// Update is called once per frame
	void Update () {

        Vector3 force = _force + _impulse + (UseGravity ? _gravity : Vector3.zero);

        _accel = force / mass;

        Vector3 momentum = _impulse / mass;

        _vel += _accel * Time.deltaTime * _timeDilation + momentum;

        _ball.Move(_vel * Time.deltaTime * _timeDilation);

        _impulse = Vector3.zero;
    }

    public void AddForce(Vector3 force, UnityEngine.ForceMode mode)
    {

        switch(mode)
        {
            case ForceMode.Acceleration:
                _accel += force;
                break;
            case ForceMode.Force:
                _force += force;
                break;
            case ForceMode.Impulse:
                _impulse += force;
                break;
            case ForceMode.VelocityChange:
                _vel += force;
                break;
        }

    }

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Rigidbody body = hit.collider.attachedRigidbody;
        SlowMovement controller = hit.controller.GetComponentInParent<SlowMovement>();

        Vector3 impulse = ((2 - material.bounciness) * _vel) * mass;

        if (body != null && !body.isKinematic)
        {
            body.AddForce(impulse, ForceMode.Impulse);
        }
        else if(controller != null)
        {
            controller.AddForce(impulse, ForceMode.Impulse);
        }

        if(Vector3.Dot(_vel, hit.normal) < 0)
            _vel = Vector3.Reflect(_vel, hit.normal) * material.bounciness;
    }
}
