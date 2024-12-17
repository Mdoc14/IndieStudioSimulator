using CharactersBehaviour;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssignReunionChairAction : ASimpleAction
{
    public AssignReunionChairAction(IAgent agent) : base(agent) { }

    public override void Enter()
    {
        base.Enter();
        if (agent.GetCurrentChair() != null) return;
        try
        {
            foreach (GameObject obj in GameObject.FindGameObjectsWithTag("ReunionChair"))
            {
                if (!obj.GetComponent<Chair>().selected)
                {
                    agent.GetAgentGameObject().GetComponent<EmployeeBehaviour>().SetReunionChair(obj);
                    agent.SetCurrentChair(obj.GetComponent<Chair>());
                    obj.GetComponent<Chair>().selected = true;
                    return;
                }
            }
        }
        catch
        {
            Console.Write("No se ha encontrado una silla disponible");
            agent.GetAgentGameObject().GetComponent<EmployeeBehaviour>().SetReunionChair(agent.GetChair().gameObject);
        }
    }

    public override void Exit()
    {
    }

    public override void FixedUpdate()
    {

    }

    public override void Update()
    {
        finished = true;
    }
}
