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
        _goToStartingPos = new GoToPositionAction(agent, agent.GetAgentGameObject().GetComponent<BossBehaviour>().GetFirstWaypoint().position);
        _goToStartingPos.Enter();
        _patrolTime = UnityEngine.Random.Range(10, 60);
        InitializeTree();
    }

    public override void Exit()
    {
        Debug.Log("ESTADO DE PATRULLA FINALIZADO");
        agent.GetAgentGameObject().GetComponent<NavMeshAgent>().speed = agent.GetAgentVariable("Speed");
    }

    public override void FixedUpdate()
    {
        _goToStartingPos?.FixedUpdate();
    }

    public override void Update()
    {
        if (!_goToStartingPos.HasFinished)
        {
            _goToStartingPos?.Update();
        }
        else
        {
            _bt?.UpdateBehaviour();
            _patrolTime -= Time.deltaTime;
            if (_patrolTime <= 0) context.State = new BossWorkState(context, agent);
        }
    }

    private void InitializeTree()
    {
        _bt = new BehaviourTree();
        //Secuencia correspondiente a regañar en la oficina:
        List<IBehaviourNode> nodes = new List<IBehaviourNode>();
        nodes.Add(new ConditionNode(() => { return agent.GetAgentVariable("CurrentAnger") >= 100; }));
        nodes.Add(new ActionNode(new SendToOfficeAction(agent), _bt));
        nodes.Add(new ActionNode(new ScoldAction(agent, true), _bt));
        IBehaviourNode scoldOfficeSequenceNode = new SequenceNode(new List<IBehaviourNode>(nodes));
        //Selector correspondiente a regañar en el sitio:
        nodes.Clear();
        nodes.Add(scoldOfficeSequenceNode);
        nodes.Add(new ActionNode(new ScoldAction(agent, false), _bt));
        IBehaviourNode scoldSelector = new SelectorNode(new List<IBehaviourNode>(nodes));
        //Secuencia correspondiente a comprobar si ve a algún trabajador holgazaneando:
        nodes.Clear();
        nodes.Add(new ConditionNode(SlackerOnSight()));
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

    private Func<bool> SlackerOnSight()
    {
        return () =>
        {
            if (agent.GetAgentGameObject().GetComponent<BossBehaviour>().ScoldedAgent != null)
            {
                float newAnger = agent.GetAgentVariable("CurrentAnger") + 100 * agent.GetAgentVariable("Irritability");
                agent.SetAgentVariable("CurrentAnger", newAnger);
                return true;
            }
            return false;
        };
    }
}
