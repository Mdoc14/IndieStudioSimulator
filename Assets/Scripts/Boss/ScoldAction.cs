using CharactersBehaviour;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ScoldAction : ASimpleAction
{
    float _time;
    bool _inOffice;
    StateMachine _context;

    public ScoldAction(IAgent agent, bool inOffice, StateMachine context) : base(agent) { _inOffice = inOffice; _context = context; }

    public override void Enter()
    {
        base.Enter();
        //Acciones de mirar con la cabeza hacia el trabajador y comenzar la animación y audios de regañar.
        _time = Random.Range(5, 10);
        if (_inOffice) _time *= 3; //Si regaña al trabajador en la oficina está más tiempo
        ((agent as BossBehaviour).ScoldedAgent as EmployeeBehaviour).SetState("SCOLDED");
        (agent as BossBehaviour).transform.forward = ((agent as BossBehaviour).ScoldedAgent as EmployeeBehaviour).transform.position - (agent as BossBehaviour).transform.position;
        agent.SetBark("Scold");
        agent.SetAnimation("Scolding");
        if(!agent.GetChair().IsOccupied()) agent.GetAgentGameObject().GetComponent<NavMeshAgent>().isStopped = true;
    }

    public override void Exit()
    {
        ((agent as BossBehaviour).ScoldedAgent as EmployeeBehaviour).numScolds++;
        ((agent as BossBehaviour).ScoldedAgent as EmployeeBehaviour).SetState("WORK");
        (agent as BossBehaviour).ScoldedAgent = null;
        if (!_inOffice)
        {
            float newAnger = agent.GetAgentVariable("CurrentAnger") + 100 * agent.GetAgentVariable("Irritability");
            agent.SetAgentVariable("CurrentAnger", newAnger);
        }
        else agent.SetAgentVariable("CurrentAnger", 0); //Cuando regaña a alguien en su despacho se calma
        if(agent.GetAgentGameObject().GetComponent<NavMeshAgent>().enabled) agent.GetAgentGameObject().GetComponent<NavMeshAgent>().isStopped = false;
        if (_inOffice) _context.State = new BossWorkState(_context, agent);
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
