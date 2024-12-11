using CharactersBehaviour;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ScoldAction : ASimpleAction
{
    float _time;
    bool _inOffice;

    public ScoldAction(IAgent agent, bool inOffice) : base(agent) { _inOffice = inOffice; }

    public override void Enter()
    {
        base.Enter();
        //Acciones de mirar con la cabeza hacia el trabajaador y comenzar la animación y audios de regañar.
        _time = Random.Range(1, 10);
        if (_inOffice) _time *= 3; //Si regaña al trabajador en la oficina está más tiempo
        agent.SetBark("Scold");
    }

    public override void Exit()
    {
        agent.SetAgentVariable("CurrentAnger", 0);
        agent.GetAgentGameObject().GetComponent<BossBehaviour>().ScoldedAgent = null;
    }

    public override void FixedUpdate()
    {

    }

    public override void Update()
    {
        _time -= Time.deltaTime;
        if (_time <= 0) finished = true;
    }
}
