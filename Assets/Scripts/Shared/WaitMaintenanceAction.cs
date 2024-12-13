using CharactersBehaviour;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WaitingMaintenanceAction : ASimpleAction
{
    Chair _maintenanceChair;
    Chair _incidenceChair;

    public WaitingMaintenanceAction(IAgent agent) : base(agent) { }

    public override void Enter()
    {
        base.Enter();
        _maintenanceChair = GameObject.FindWithTag("MaintenanceChair").GetComponent<Chair>();
        _incidenceChair = GameObject.FindWithTag("IncidenceChair").GetComponent<Chair>();
        agent.SetBark("Wait");
        agent.SetAnimation("Idle");
    }

    public override void Exit()
    {

    }

    public override void FixedUpdate()
    {

    }

    public override void Update()
    {
        if(!_incidenceChair.IsOccupied() && !_incidenceChair.selected && _maintenanceChair.IsOccupied()) //Si nadie está reportando y el de mantenimiento está en su oficina se sienta
        {
            _incidenceChair.selected = true;
            finished = true;
        }
    }
}
