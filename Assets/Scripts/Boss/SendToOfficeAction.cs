using CharactersBehaviour;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SendToOfficeAction : ASimpleAction
{
    BossBehaviour _boss;
    NavMeshAgent _navAgent;
    bool _bossReady = false;
    bool _workerReady = false;

    public SendToOfficeAction(IAgent agent) : base(agent) { }

    public override void Enter()
    {
        base.Enter();
        _boss = agent.GetAgentGameObject().GetComponent<BossBehaviour>();
        _navAgent = _boss.GetComponent<NavMeshAgent>();
        _navAgent.speed = agent.GetAgentVariable("Speed");
        _navAgent.SetDestination(_boss.GetChair().transform.position);
        ((agent as BossBehaviour).ScoldedAgent as EmployeeBehaviour).SetState("SCOLDED_OFFICE");
        GameObject.FindWithTag("ScoldedChair").GetComponent<Chair>().OnSit += OnWorkerReady;
        agent.SetBark("Scold");
        agent.SetAnimation("Walk");
    }

    public override void Exit()
    {
        GameObject.FindWithTag("ScoldedChair").GetComponent<Chair>().OnSit -= OnWorkerReady;
    }

    public override void FixedUpdate()
    {

    }

    public override void Update()
    {
        if (!_bossReady)
        {
            if (!_navAgent.pathPending && _navAgent.remainingDistance <= _navAgent.stoppingDistance)
            {
                _boss.GetChair().Sit(_boss.gameObject);
                _bossReady = true;
            }
        }
        if (_bossReady && _workerReady) finished = true;
    }

    private void OnWorkerReady()
    {
        _workerReady = true;
    }
}
