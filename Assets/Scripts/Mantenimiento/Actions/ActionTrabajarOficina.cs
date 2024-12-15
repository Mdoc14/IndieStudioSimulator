using CharactersBehaviour;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ActionTrabajarOficina : ASimpleAction
{
    private float _workTime;
    public ActionTrabajarOficina(IAgent agent) : base(agent) { }
    
    public override void Enter()
    {
        base.Enter();
        _workTime = Random.Range(5, 10);
        Debug.Log("Está trabajando en su ordenador...");
        agent.SetBark("MaintenanceWork");
        agent.SetAnimation("Work");
    }

    public override void Exit()
    {

    }

    public override void FixedUpdate()
    {

    }

    public override void Update()
    {
        _workTime -= Time.deltaTime;
        if (_workTime <= 0)
        {
            Debug.Log("Fin del trabajo");
            finished = true;
        }
    }
}