using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CharactersBehaviour;

public class ActionDormir : ASimpleAction
{
    private float _sleepTime;
    public ActionDormir(IAgent agent) : base(agent) { }

    public override void Enter()
    {
        base.Enter();
        _sleepTime = Random.Range(5, 10);
        Debug.Log("Está durmiendo...");
        agent.SetBark("Sleep");
    }

    public override void Exit()
    {

    }

    public override void FixedUpdate()
    {

    }

    public override void Update()
    {
        _sleepTime -= Time.deltaTime;
        if (_sleepTime <= 0)
        {
            Debug.Log("Fin de la siesta!");
            finished = true;
        }
    }
}
