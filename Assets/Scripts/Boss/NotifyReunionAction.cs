using CharactersBehaviour;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.AI;

public class NotifyReunionAction : ASimpleAction
{
    public NotifyReunionAction(IAgent agent) : base(agent) { }

    public override void Enter()
    {
        base.Enter();
        GameObject.FindWithTag("BossReunionChair").GetComponent<Chair>().Sit(agent.GetAgentGameObject());
        WorldManager.Instance.ReunionNotified();
        WorldManager.Instance.OnWorkersReady += StartReunion;
        Debug.Log("Reunión notificada. Esperando a los trabajadores...");
        agent.SetBark("Wait");
        agent.SetAnimation("Idle");
    }

    public override void Exit()
    {
        WorldManager.Instance.OnWorkersReady -= StartReunion;
    }

    public override void FixedUpdate()
    {
        
    }

    public override void Update()
    {

    }

    public void StartReunion()
    {
        finished = true;
    }
}
