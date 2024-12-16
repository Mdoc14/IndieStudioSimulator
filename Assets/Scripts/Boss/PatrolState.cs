using CharactersBehaviour;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PatrolState : AState
{
    public PatrolState(StateMachine sm, IAgent agent) : base(sm, agent) { }
    GoToPositionAction _goToStartingPos;
    BehaviourTree _bt;
    float _patrolTime;

    public override void Enter()
    {
        Debug.Log("ENTRANDO EN ESTADO DE PATRULLAR");
        (agent as BossBehaviour).SetPatrolling(true);
        context.PreviousStates.Push(this);
        if (_goToStartingPos == null) //Si es la primera vez que entra en el estado se inicializa todo
        {
            _goToStartingPos = new GoToPositionAction(agent, agent.GetAgentGameObject().GetComponent<BossBehaviour>().GetFirstWaypoint().position);
            _goToStartingPos.Enter();
            _patrolTime = UnityEngine.Random.Range(10, 60);
        }
        InitializeTree();
    }

    public override void Exit()
    {
        Debug.Log("ESTADO DE PATRULLA FINALIZADO");
        agent.GetAgentGameObject().GetComponent<NavMeshAgent>().speed = agent.GetAgentVariable("Speed");
        (agent as BossBehaviour).SetPatrolling(false);
    }

    public override void FixedUpdate()
    {
        _goToStartingPos?.FixedUpdate();
    }

    public override void Update()
    {
        if (!_goToStartingPos.Finished)
        {
            _goToStartingPos?.Update();
        }
        else
        {
            _bt?.UpdateBehaviour();
            _patrolTime -= Time.deltaTime;
            if (_patrolTime <= 0 && (agent as BossBehaviour).ScoldedAgent == null) context.State = new BossWorkState(context, agent);
        }
    }

    private void InitializeTree()
    {
        _bt = new BehaviourTree();
        //Secuencia correspondiente a regañar en la oficina:
        List<IBehaviourNode> nodes = new List<IBehaviourNode>();
        nodes.Add(new ConditionNode(SendOfficeCondition()));
        nodes.Add(new ActionNode(new SendToOfficeAction(agent), _bt));
        nodes.Add(new ActionNode(new ScoldAction(agent, true, context), _bt));
        IBehaviourNode scoldOfficeSequenceNode = new SequenceNode(new List<IBehaviourNode>(nodes));
        //Selector correspondiente a regañar en el sitio:
        nodes.Clear();
        nodes.Add(scoldOfficeSequenceNode);
        nodes.Add(new ActionNode(new ScoldAction(agent, false, context), _bt));
        IBehaviourNode scoldSelector = new SelectorNode(new List<IBehaviourNode>(nodes));
        //Secuencia correspondiente a comprobar si ve a algún trabajador holgazaneando:
        nodes.Clear();
        nodes.Add(new ConditionNode(() => (agent as BossBehaviour).ScoldedAgent != null));
        nodes.Add(scoldSelector);
        IBehaviourNode slackerSearchSequence = new SequenceNode(new List<IBehaviourNode>(nodes));
        //Selector correspondiente a buscar holgazanes y patrullar:
        nodes.Clear();
        nodes.Add(slackerSearchSequence);
        nodes.Add(new ActionNode(new PatrolAction(agent), _bt));
        IBehaviourNode baseSelectorNode = new SelectorNode(new List<IBehaviourNode>(nodes));

        IBehaviourNode rootNode = new LoopNNode(baseSelectorNode, 0);
        _bt.Root = rootNode;
    }

    private Func<bool> SendOfficeCondition()
    {
        return () =>
        {
            if (agent.GetAgentVariable("CurrentAnger") >= 100 || 
                    ((agent as BossBehaviour).ScoldedAgent as EmployeeBehaviour).numScolds == 2)
            {
                return true;
            }
            return false;
        };
    }
}
