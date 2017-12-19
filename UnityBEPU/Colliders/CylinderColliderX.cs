using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CylinderColliderX : PhysicsBehaviourX
{
    public override bool istatic { get { return false; } }
    public Vector3 position;
    public Quaternion rotation;

	public Vector3 center;
	public float radius, height;
    public float mass = 1f;
    public float damping = 0f, angdamping = 0f;

    public CapsuleCollider Collider;
	public Rigidbody rigid; 
    public BEPUphysics.Entities.Prefabs.Cylinder entity;

	// Use this for initialization
    public override void XStart()
    {
        if (!isInit)
        {
            Collider = transform.GetComponent<CapsuleCollider>();
            position = Collider.transform.position;
            rotation = Collider.transform.rotation;
            center = Collider.center;
            radius = Collider.radius;
            height = Collider.height;

            if (transform.GetComponent<Rigidbody>() != null)
            {
                rigid = transform.GetComponent<Rigidbody>();
                mass = rigid.mass;
            }

            entity = new BEPUphysics.Entities.Prefabs.Cylinder(position.ToBEPU(), height, radius, mass);
            entity.Orientation = rotation.ToBEPU();
            entity.LinearDamping = damping;
            entity.AngularDamping = angdamping;
            PhysicsManagerBehavior.Space.Add(entity);

            //Collider.enabled = false;
            this.enabled = false;
            isInit = true;
        }
    }

    public override void XAwake()
    {

    }

    public override void XUpdate()
    {

	}

    public override void XPhysicsUpdate()
    {
        this.transform.position = new UnityEngine.Vector3(entity.Position.X, entity.Position.Y, entity.Position.Z);
        this.transform.rotation = new Quaternion(entity.Orientation.X, entity.Orientation.Y, entity.Orientation.Z, entity.Orientation.W);
        rigid.velocity = new UnityEngine.Vector3(entity.LinearVelocity.X, entity.LinearVelocity.Y, entity.LinearVelocity.Z); 

        position = this.transform.position;
        rotation = this.transform.rotation;
    }
}