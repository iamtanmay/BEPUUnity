using System.Collections;
using System.Collections.Generic;
using BEPUutilities;
using UnityEngine;

public class TerrainColliderX : PhysicsBehaviourX
{
    public override bool istatic { get { return true; } }
	public UnityEngine.Vector3 position;
	public UnityEngine.Quaternion rotation;

    public UnityEngine.Vector3 scale = new UnityEngine.Vector3(1f, 1f ,1f);
	public float mass = 1f;

	public Mesh mesh;
    public MeshCollider Collider;
	public MeshFilter meshfilter;
	public Rigidbody rigid;
    public BEPUphysics.BroadPhaseEntries.StaticMesh entity;

	// Use this for initialization
	public override void XStart()
	{
        if (!isInit)
        {
            meshfilter = transform.GetComponent<MeshFilter>();
            Collider = transform.GetComponent<MeshCollider>();
            position = Collider.bounds.center;
            rotation = meshfilter.transform.rotation;
            scale = meshfilter.transform.localScale;

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

            entity = new BEPUphysics.BroadPhaseEntries.StaticMesh(vertices.ToArray(), indices, new AffineTransform(scale.ToBEPU(), rotation.ToBEPU(), position.ToBEPU()));
            entity.Sidedness = TriangleSidedness.DoubleSided;
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
			position = this.transform.position;
			rotation = this.transform.rotation;
		}
	}
}