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
        GameObject.Find("BossReunionChair").GetComponent<Chair>().Sit(agent.GetAgentGameObject());
        WorldManager.Instance.StartReunion();
        Debug.Log("Reunión notificada. Esperando a los trabajadores...");
    }

    public override void Exit()
    {

    }

    public override void FixedUpdate()
    {
        
    }

    public override void Update()
    {
        if (WorldManager.Instance.AreWorkersReady())
        {
            finished = true;
        }
    }
}
