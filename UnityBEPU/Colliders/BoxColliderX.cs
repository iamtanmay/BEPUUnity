using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxColliderX : PhysicsBehaviourX 
{
    public override bool istatic { get { return false; } }
	public Vector3 position;
	public Quaternion rotation;

	public Vector3 center, size;
    public Vector3 oldpos;
    public float mass = 1f;
    public float damping = 0f, angdamping = 0f;

	public BoxCollider Collider;
	public Rigidbody rigid;
	public BEPUphysics.Entities.Prefabs.Box entity;

	// Use this for initialization
    public override void XStart()
    {
        if (!isInit)
        {
            Collider = transform.GetComponent<BoxCollider>();
            position = Collider.center;            
            rotation = Collider.transform.rotation;

            if (transform.GetComponent<Rigidbody>() != null)
            {
                rigid = transform.GetComponent<Rigidbody>();
                mass = rigid.mass;
            }

            entity = new BEPUphysics.Entities.Prefabs.Box(position.ToBEPU(), 2 * Collider.bounds.extents.x, 2 * Collider.bounds.extents.y, 2 * Collider.bounds.extents.z, mass);
            entity.position = transform.position.ToBEPU();
            entity.Orientation = rotation.ToBEPU();
            entity.LinearDamping = damping;
            entity.AngularDamping = angdamping;
            PhysicsManagerBehavior.Space.Add(entity);
            oldpos = entity.Position.FromBEPU();

            Collider.enabled = false;
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
        //Move distance
        //this.transform.position += new UnityEngine.Vector3(entity.Position.X, entity.Position.Y, entity.Position.Z) - oldpos;
        this.transform.position = entity.Position.FromBEPU(); //new UnityEngine.Vector3(entity.Position.X, entity.Position.Y, entity.Position.Z);
        //oldpos = entity.Position.FromBEPU();

        //this.transform.rotation = new Quaternion(entity.Orientation.X, entity.Orientation.Y, entity.Orientation.Z, entity.Orientation.W);
        this.transform.rotation = entity.Orientation.FromBEPU();
        //rigid.velocity = new UnityEngine.Vector3(entity.LinearVelocity.X, entity.LinearVelocity.Y, entity.LinearVelocity.Z); 

        position = this.transform.position;
        rotation = this.transform.rotation;
    }
}