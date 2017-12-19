using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CapsuleColliderX : PhysicsBehaviourX
{
    public override bool istatic { get { return false; } }
    public Vector3 position;
    public Quaternion rotation;

    public Vector3 offset, center;
	public float radius, height, mass = 1f, damping = 0f, angdamping = 0f;
    
    public CapsuleCollider Collider;
	public Rigidbody rigid; 
    public BEPUphysics.Entities.Prefabs.Capsule entity;

    //Its important to give a mass when declaring a BEPU collider, otherwise its taken as a static collider !!!!
    public override void XStart()
    {
        if (!isInit)
        {
            Collider = transform.GetComponent<CapsuleCollider>();
            position = Collider.center;
            rotation = Collider.transform.rotation;
            radius = Collider.radius;
            height = Collider.height;

            if (transform.GetComponent<Rigidbody>() != null)
            {
                rigid = transform.GetComponent<Rigidbody>();
                mass = rigid.mass;
            }

            entity = new BEPUphysics.Entities.Prefabs.Capsule(new Vector3().ToBEPU(), height, radius, mass);
            offset = position;
            offset.y += radius;
            entity.position = transform.position.ToBEPU();
            entity.Orientation = rotation.ToBEPU();
            entity.LinearDamping = damping;
            entity.AngularDamping = angdamping;
            PhysicsManagerBehavior.Space.Add(entity);

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
        //oldpos = entity.Position.FromBEPU();
        this.transform.position = entity.Position.FromBEPU() - offset;
        this.transform.rotation = entity.Orientation.FromBEPU();
        //this.transform.rotation = new Quaternion(entity.Orientation.X, entity.Orientation.Y, entity.Orientation.Z, entity.Orientation.W);
        //rigid.velocity = new UnityEngine.Vector3(entity.LinearVelocity.X, entity.LinearVelocity.Y, entity.LinearVelocity.Z); 

        position = this.transform.position;
        rotation = this.transform.rotation;
    }
}