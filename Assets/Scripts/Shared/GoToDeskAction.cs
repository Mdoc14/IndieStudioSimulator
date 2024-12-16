using CharactersBehaviour;
using UnityEngine.AI;

public class GoToDeskAction : ASimpleAction
{
    NavMeshAgent _navAgent;
    Chair _agentChair;

    public GoToDeskAction(IAgent agent, Chair chair = null) : base(agent) { _agentChair = chair; }

    public override void Enter()
    {
        base.Enter();
        _navAgent = agent.GetAgentGameObject().GetComponent<NavMeshAgent>();
        if(agent.GetChair().IsOccupied()) agent.GetChair().Leave();
        if(agent.GetCurrentChair() != null && agent.GetCurrentChair().IsOccupied()) agent.GetCurrentChair().Leave();
        if(_agentChair == null) _agentChair = agent.GetChair();
        if(_navAgent.enabled) _navAgent.SetDestination(_agentChair.transform.position);
        agent.SetBark("Walk");
        agent.SetAnimation("Walk");
    }

    public override void Exit()
    {

    }

    public override void FixedUpdate()
    {
        
    }

    public override void Update()
    {
        if(!_navAgent.pathPending && _navAgent.remainingDistance <= _navAgent.stoppingDistance)
        {
            _agentChair.Sit(agent.GetAgentGameObject());
            if(_agentChair != agent.GetChair()) agent.SetCurrentChair(_agentChair);
            finished = true;
        }
    }
}
