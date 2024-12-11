using CharactersBehaviour;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.AI;

public class PerformReunionAction : ASimpleAction
{
    private float _time;
    public PerformReunionAction(IAgent agent) : base(agent) { }

    public override void Enter()
    {
        base.Enter();
        _time = Random.Range(5, 60);
        Debug.Log("Todos los trabajadores presentes. Comenzando reunión...");
    }

    public override void Exit()
    {

    }

    public override void FixedUpdate()
    {

    }

    public override void Update()
    {
        _time -= Time.deltaTime;
        if (_time <= 0)
        {
            GameObject.Find("BossReunionChair").GetComponent<Chair>().Leave();
            WorldManager.Instance.ReunionEnded();
            finished = true;
        }
    }
}
