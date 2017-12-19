using System.Collections;
using System.Collections.Generic;
using BEPUutilities;
using UnityEngine;

public class MeshColliderX : PhysicsBehaviourX
{
    public override bool istatic { get { return false; } }
    public UnityEngine.Vector3 position;
    public UnityEngine.Quaternion rotation;

    public float mass = 1f;
    public float damping = 0f, angdamping = 0f;

	public Mesh mesh;

	public MeshFilter meshfilter;
    public Rigidbody rigid; 
    public BEPUphysics.Entities.Prefabs.MobileMesh entity;

	// Use this for initialization
    public override void XStart()
    {
        if (!isInit)
        {
            meshfilter = transform.GetComponent<MeshFilter>();
            position = meshfilter.transform.position;
            rotation = meshfilter.transform.rotation;
            mesh = meshfilter.sharedMesh;

            if (transform.GetComponent<Rigidbody>() != null)
            {
                rigid = transform.GetComponent<Rigidbody>();
                mass = rigid.mass;
            }

            List<BEPUutilities.Vector3> vertices = new List<BEPUutilities.Vector3>();
            int[] indices = new int[mesh.GetIndexCount(0)];
            indices = mesh.GetIndices(0);

            for (int i = 0; i < mesh.vertices.Length; i++)
                vertices.Add(mesh.vertices[i].ToBEPU());

            entity = new BEPUphysics.Entities.Prefabs.MobileMesh(vertices.ToArray(), indices,
                     new AffineTransform(Matrix3x3.CreateFromAxisAngle(BEPUutilities.Vector3.Up, MathHelper.Pi),
                                                     new BEPUutilities.Vector3(0, -10, 0)), BEPUphysics.CollisionShapes.MobileMeshSolidity.Solid);

            entity.Orientation = rotation.ToBEPU();
            entity.LinearDamping = damping;
            entity.AngularDamping = angdamping;
            PhysicsManagerBehavior.Space.Add(entity);

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
		if (!istatic)
		{
			this.transform.position = new UnityEngine.Vector3(entity.Position.X, entity.Position.Y, entity.Position.Z);
			this.transform.rotation = new UnityEngine.Quaternion(entity.Orientation.X, entity.Orientation.Y, entity.Orientation.Z, entity.Orientation.W);
            rigid.velocity = new UnityEngine.Vector3(entity.LinearVelocity.X, entity.LinearVelocity.Y, entity.LinearVelocity.Z); 

			position = this.transform.position;
			rotation = this.transform.rotation;
		}
    }
}