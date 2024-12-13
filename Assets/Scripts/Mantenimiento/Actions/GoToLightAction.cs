using CharactersBehaviour;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GoToLightAction : ASimpleAction
{
    NavMeshAgent _navAgent;
    GameObject lightSwitch;
    

    public GoToLightAction(IAgent agent) : base(agent) { }

    public override void Enter()
    {
      
        base.Enter();
        lightSwitch=GameObject.FindObjectOfType<LightSwitch>().gameObject;
        _navAgent = agent.GetAgentGameObject().GetComponent<NavMeshAgent>();
        _navAgent.SetDestination(lightSwitch.transform.position);
        agent.SetBark("Walk");
        //agent.SetAnimation("Walk");
    }

    public override void Exit()
    {

    }

    public override void FixedUpdate()
    {

    }

    public override void Update()
    {
        if (!_navAgent.pathPending && _navAgent.remainingDistance <= _navAgent.stoppingDistance)
        {
            finished = true;
        }
    }
}
