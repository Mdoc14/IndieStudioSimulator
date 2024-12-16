using CharactersBehaviour;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class UseBathroomAction : ASimpleAction
{
    private float _time;
    private bool _reached = false;
    private NavMeshAgent _navAgent;
    Chair _bath;
    StateMachine _context;

    public UseBathroomAction(IAgent agent, StateMachine context = null) : base(agent) 
    {
        _context = context;
    }

    public override void Enter()
    {
        base.Enter();
        _time = Random.Range(3, 20); //Está un tiempo aleatorio usando el baño
        _navAgent = agent.GetAgentGameObject().GetComponent<NavMeshAgent>();
        _bath = agent.GetCurrentChair();
        agent.SetBark("Bathroom");
        agent.SetAnimation("Walk");
        _bath.GetComponent<BathroomInteractable>().OnBreak += OnBreak;
        if (_bath.GetComponent<BathroomInteractable>().broken) OnBreak();
        if(_navAgent.enabled) _navAgent.SetDestination(_bath.transform.position);
    }

    public override void Exit()
    {
 
    }

    public override void FixedUpdate()
    {

    }

    public override void Update()
    {
        if (!_reached)  //Si no ha llegado a él comprueba si está lo suficientemente cerca para usarlo
        {
            if (!_navAgent.pathPending && _navAgent.remainingDistance <= _navAgent.stoppingDistance)
            {
                _reached = true;
                _bath.Sit(agent.GetAgentGameObject());
                agent.SetAnimation("Idle");
            } 
        }
        else //Si lo ha alcanzado el tiempo comienza a descontarse
        {
            _time -= Time.deltaTime;
            if (_time <= 0)
            {
                _bath.GetComponent<BathroomInteractable>().OnBreak -= OnBreak;
                _bath.Leave();
                agent.SetCurrentChair(null);
                finished = true;
            }
        }
    }

    public void OnBreak()
    {
        _time = 1000; //Evitamos transiciones indeseadas
        (agent as AgentBehaviour).currentIncidence = _bath.GetComponent<BathroomInteractable>();
        if (_bath.IsOccupied())
        {
            _bath.Leave(true); //Se deja el baño sin quitar el select
            agent.GetAgentGameObject().transform.LookAt(_bath.transform);
        }
        _context.State = new ReportIncidenceState(_context, agent);
    }
}
