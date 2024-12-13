using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CharactersBehaviour;

public class ActionDormir : ASimpleAction
{
    private float _cansancio;
    private float _multiplier = 0.1f;

    public ActionDormir(IAgent agent) : base(agent) { }

    public override void Enter()
    {
        base.Enter();
        _cansancio = agent.GetAgentVariable("cansancio");
        Debug.Log("Está durmiendo...");
        agent.SetBark("Sleep");
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
        _cansancio -= Time.deltaTime * _multiplier;
        agent.SetAgentVariable("cansancio", _cansancio);
        if (_cansancio <= 0)
        {
            finished = true;
        }
    }
}
