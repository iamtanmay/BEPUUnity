using BEPUutilities;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//Physics manager runs the deterministic physics engine.
//Responsible for all non unity colliders
public class PhysicsManagerBehavior : MonoBehaviourX {
	
    public UnityEngine.Vector3 Gravity;
    public bool BufferedStates = true;
    public int IterationLimit = 5;
    public float TimeStep = 1 / 60f;

    public int IDCounter = 0;

	public List<NonMonoBehaviorPhysics> nonMonoPhysics;
    public List<PhysicsBehaviourX> dynamicColliders;
    public List<PhysicsBehaviourX> staticColliders;

    private static BEPUphysics.Space _physicsSpace;
    
    public static BEPUphysics.Space Space
    {
        get
        {
            return _physicsSpace;
        }
    }

    public override bool istatic { get { return false; } }
    
    public override void XStart()
    {
		nonMonoPhysics = new List<NonMonoBehaviorPhysics> ();
        dynamicColliders = new List<PhysicsBehaviourX>();
        staticColliders = new List<PhysicsBehaviourX>();

        _physicsSpace = new BEPUphysics.Space();
        _physicsSpace.ForceUpdater.Gravity = Gravity.ToBEPU();
        _physicsSpace.BufferedStates.Enabled = BufferedStates;
        _physicsSpace.Solver.IterationLimit = IterationLimit; //Don't need many iterations, there's not really any stacking going on in this game.
        _physicsSpace.TimeStepSettings.TimeStepDuration = TimeStep; //A slower timestep gives the Xbox a little breathing 
	}
	
	public override void XAwake()
	{
	}

	public override void XUpdate()
	{
        //UnityEngine.Profiling.Profiler.BeginSample("BEPUPhysics - Update");
        _physicsSpace.Update(Time.deltaTime);

		for (int i = 0; i < nonMonoPhysics.Count; i++)
			nonMonoPhysics [i].XPhysicsUpdate ();

		for (int i = 0; i < dynamicColliders.Count; i++)
			dynamicColliders [i].XPhysicsUpdate ();

        //UnityEngine.Profiling.Profiler.EndSample();
	}

	public void CreateNonMonoBehavior(NonMonoBehaviorPhysics inonmono)
	{
		inonmono.xID = IDCounter;
		inonmono.XStart();
		nonMonoPhysics.Add(inonmono);     
		IDCounter++;
	}

    public void CreateDynamic(PhysicsBehaviourX idynamic)
    {
        idynamic.xID = IDCounter;
        idynamic.XStart();
        dynamicColliders.Add(idynamic);     
        IDCounter++;
    }

    public void CreateStatic(PhysicsBehaviourX istatic)
    {
        istatic.xID = IDCounter;
        istatic.XStart();
        staticColliders.Add(istatic);
        IDCounter++;
    }
}