using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarControllers : MonoBehaviour
{
    
    public Player connectedPlayer;
    
    
    public Rigidbody playerRB;
    public WheelColliders colliders;
    public WheelMeshes wheelMeshes;
    public WheelParticles wheelParticles;
    public float gasInput;
    public float brakeInput;
    public float steeringInput;
    public GameObject smokePrefab;
    public float motorPower;
    public float brakePower;
    public float slipAngle;
    public float speed;
    public AnimationCurve steeringCurve;
    public AudioSource engineSound;
    public AudioClip engineClip; 
    public float maxSpeed = 100f;  

    void Start()
    {
        playerRB = gameObject.GetComponent<Rigidbody>();
        engineSound = gameObject.AddComponent<AudioSource>();
        engineSound.clip = engineClip; 
        engineSound.loop = true;
        engineSound.Play();
        InstantiateSmoke();
        
        engineSound.volume = 2f; 
    }

    // smoke for drifting 
    void InstantiateSmoke()
    {
        if (smokePrefab == null) return;
        wheelParticles.FRWheel = Instantiate(smokePrefab, colliders.FRWheel.transform.position - Vector3.up * colliders.FRWheel.radius, Quaternion.identity, colliders.FRWheel.transform)
            .GetComponent<ParticleSystem>();
        wheelParticles.FLWheel = Instantiate(smokePrefab, colliders.FLWheel.transform.position - Vector3.up * colliders.FRWheel.radius, Quaternion.identity, colliders.FLWheel.transform)
            .GetComponent<ParticleSystem>();
        wheelParticles.RRWheel = Instantiate(smokePrefab, colliders.RRWheel.transform.position - Vector3.up * colliders.FRWheel.radius, Quaternion.identity, colliders.RRWheel.transform)
            .GetComponent<ParticleSystem>();
        wheelParticles.RLWheel = Instantiate(smokePrefab, colliders.RLWheel.transform.position - Vector3.up * colliders.FRWheel.radius, Quaternion.identity, colliders.RLWheel.transform)
            .GetComponent<ParticleSystem>();
    }

    void Update()
    {
        speed = playerRB.velocity.magnitude;
        CheckInput();
        ApplyMotor();
        ApplySteering();
        ApplyBrake();
        CheckParticles();
        ApplyWheelPositions();

        // Adjust pitch based on speed
        float pitch = Mathf.Lerp(0.5f, 2f, speed / maxSpeed);  // Adjust the pitch range as needed
        engineSound.pitch = pitch;
    }

    void CheckInput()
    {
        // slip angle calculated
        slipAngle = Vector3.Angle(transform.forward, playerRB.velocity - transform.forward);

        float movingDirection = Vector3.Dot(transform.forward, playerRB.velocity);

        // 
        if (movingDirection < -0.5f && gasInput > 0)
        {
            brakeInput = Mathf.Abs(gasInput);
        }
        else if (movingDirection > 0.5f && gasInput < 0)
        {
            brakeInput = Mathf.Abs(gasInput);
        }
        else
        {
            brakeInput = 0;
        }    
   
        if (slipAngle < 120f) {
            if (gasInput < 0)
            {
                brakeInput = Mathf.Abs( gasInput);
                gasInput = 0;
            }
            else
            {
                brakeInput = 0;
            }
        }
        else
        {
            brakeInput = 0;
        }
    }
    // braking
    void ApplyBrake()
    {
        colliders.FRWheel.brakeTorque = brakeInput * brakePower * 0.7f;
        colliders.FLWheel.brakeTorque = brakeInput * brakePower * 0.7f;

        colliders.RRWheel.brakeTorque = brakeInput * brakePower * 0.3f;
        colliders.RLWheel.brakeTorque = brakeInput * brakePower * 0.3f;
    }
    // apply power
    void ApplyMotor()
    {
        colliders.RRWheel.motorTorque = motorPower * gasInput;
        colliders.RLWheel.motorTorque = motorPower * gasInput;
    }
    // steering
    void ApplySteering()
    {
        float steeringAngle = steeringInput * steeringCurve.Evaluate(speed);
        if (slipAngle < 120f)
        {
            steeringAngle += Vector3.SignedAngle(transform.forward, playerRB.velocity + transform.forward, Vector3.up);
        }
        steeringAngle = Mathf.Clamp(steeringAngle, -90f, 90f);
        colliders.FRWheel.steerAngle = steeringAngle;
        colliders.FLWheel.steerAngle = steeringAngle;
    }
    // spinning the wheels 
    void ApplyWheelPositions()
    {
        UpdateWheel(colliders.FRWheel, wheelMeshes.FRWheel);
        UpdateWheel(colliders.FLWheel, wheelMeshes.FLWheel);
        UpdateWheel(colliders.RRWheel, wheelMeshes.RRWheel);
        UpdateWheel(colliders.RLWheel, wheelMeshes.RLWheel);
    }
    // particles 
    void CheckParticles()
    {
        if (wheelParticles.FRWheel == null) return;
        WheelHit[] wheelHits = new WheelHit[4];
        colliders.FRWheel.GetGroundHit(out wheelHits[0]);
        colliders.FLWheel.GetGroundHit(out wheelHits[1]);
    
        colliders.RRWheel.GetGroundHit(out wheelHits[2]);
        colliders.RLWheel.GetGroundHit(out wheelHits[3]);
    
        float slipAllowance = 0.5f;
        if ((Mathf.Abs(wheelHits[0].sidewaysSlip) + Mathf.Abs(wheelHits[0].forwardSlip) > slipAllowance))
        {
            wheelParticles.FRWheel.Play();
        }
        else
        {
            wheelParticles.FRWheel.Stop();
        }
        if ((Mathf.Abs(wheelHits[1].sidewaysSlip) + Mathf.Abs(wheelHits[1].forwardSlip) > slipAllowance))
        {
            wheelParticles.FLWheel.Play();
        }
        else
        {
            wheelParticles.FLWheel.Stop();
        }
        if ((Mathf.Abs(wheelHits[2].sidewaysSlip) + Mathf.Abs(wheelHits[2].forwardSlip) > slipAllowance))
        {
            wheelParticles.RRWheel.Play();
        }
        else
        {
            wheelParticles.RRWheel.Stop();
        }
        if ((Mathf.Abs(wheelHits[3].sidewaysSlip) + Mathf.Abs(wheelHits[3].forwardSlip) > slipAllowance))
        {
            wheelParticles.RLWheel.Play();
        }
        else
        {
            wheelParticles.RLWheel.Stop();
        }
    }
    void UpdateWheel(WheelCollider coll, MeshRenderer wheelMesh)
    {
        Quaternion quat;
        Vector3 position;
        coll.GetWorldPose(out position, out quat);
        wheelMesh.transform.position = position;
        wheelMesh.transform.rotation = quat;
    }
    
    public void SetSteer(float direction)
    {
        steeringInput = direction;
    }

    public void SetGas(float input)
    {
        gasInput = input;
    }
}
// all wheel components for tweaking in the inspector

[System.Serializable]
public class WheelColliders
{
    public WheelCollider FRWheel;
    public WheelCollider FLWheel;
    public WheelCollider RRWheel;
    public WheelCollider RLWheel;
}
[System.Serializable]
public class WheelMeshes
{
    public MeshRenderer FRWheel;
    public MeshRenderer FLWheel;
    public MeshRenderer RRWheel;
    public MeshRenderer RLWheel;
}
[System.Serializable]
public class WheelParticles
{
    public ParticleSystem FRWheel;
    public ParticleSystem FLWheel;
    public ParticleSystem RRWheel;
    public ParticleSystem RLWheel;
}

