using CharactersBehaviour;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class JanitorBehaviourTree : AState
{
    BehaviourTree behaviourTree;
    Room currentRoom;
    public JanitorBehaviourTree(StateMachine sm, IAgent agent, Room room) : base(sm, agent)
    {
        currentRoom = room;
    }

    public override void Enter()
    {

        Debug.Log("He entrado al BT State");
        behaviourTree = new BehaviourTree();

        //Lista de nodos del arbol
        List<IBehaviourNode> treeNodes = new List<IBehaviourNode>();

        //Creacion de los nodos de la primera secuencia -> Hay basura?, Ir a la basura, Recoger basura, ir a papelera, tirar basura
        List<IBehaviourNode> sequenceNodeList = new List<IBehaviourNode>();

        //Observar la sala
        sequenceNodeList.Add(new ActionNode(
            new JanitorLookAction(agent), behaviourTree));

        //Hay basura?
        sequenceNodeList.Add(new ConditionNode(() => { return currentRoom.IsDirty(agent.GetAgentGameObject().GetComponent<AgentBehaviour>()); }));

        //Ir a la basura
        sequenceNodeList.Add(new ActionNode(
            //new GoToPositionAction(agent, currentRoom.GetTrashPosition()), behaviourTree));
            new GoToAction(agent, currentRoom.GetTrashPosition, "Look"), behaviourTree));

        //Recoger basura
        sequenceNodeList.Add(new ActionNode(
            new PickUpTrashAction(agent, currentRoom), behaviourTree));

        //Ir a la papelera
        sequenceNodeList.Add(new ActionNode(
            //new GoToPositionAction(agent, currentRoom.GetTrashPosition()), behaviourTree));
            new GoToAction(agent, currentRoom.GetTrashCanPosition, "Walk"), behaviourTree));

        //Tirar basura
        sequenceNodeList.Add(new ActionNode(
            new ThrowTrashAction(agent, currentRoom), behaviourTree));

        //Creamos la primera secuencia de acciones del arbol
        SequenceNode firstSequence = new SequenceNode(new List<IBehaviourNode>(sequenceNodeList));
        sequenceNodeList.Clear();

        //Creacion de los nodos de la tercera secuencia -> Esta vacia la maquina?, Reponer
        //Esta vacia?
        sequenceNodeList.Add(new ConditionNode(() => { return currentRoom.IsMachineEmpty(); }));

        //Ir a la maquina
        sequenceNodeList.Add(new ActionNode(
            new GoToAction(agent, currentRoom.GetMachinePosition, "Walk"), behaviourTree));
        //Reponer
        sequenceNodeList.Add(new ActionNode(
            new RefillAction(agent), behaviourTree));

        //Creacion de la tercera secuencia
        SequenceNode thirdSequence = new SequenceNode(new List<IBehaviourNode>(sequenceNodeList));
        sequenceNodeList.Clear();

        //Creacion de los nodos de la cuarta secuencia -> Esta la caja sucia?, ir a la caja, limpiar caja
        //Esta sucia?
        sequenceNodeList.Add(new ConditionNode(() => {return currentRoom.IsCatBoxDirty(); }));

        //Ir la caja
        sequenceNodeList.Add(new ActionNode(
            //new GoToPositionAction(agent, currentRoom.GetCatBoxPosition()), behaviourTree));
            new GoToAction(agent, currentRoom.GetCatBoxPosition, "Walk"), behaviourTree));

        //Limpiar caja
        sequenceNodeList.Add(new ActionNode(
            new CleanCatBoxAction(agent), behaviourTree));

        //Creacion de la cuarta secuencia
        SequenceNode fourthSequence = new SequenceNode(new List<IBehaviourNode>(sequenceNodeList));
        sequenceNodeList.Clear();

        sequenceNodeList.Add(thirdSequence);
        sequenceNodeList.Add(fourthSequence);

        //Creacion del nodo selector
        SelectorNode selection = new SelectorNode(new List<IBehaviourNode>(sequenceNodeList));

        sequenceNodeList.Clear();

        //Creacion de los nodos de la segunda secuencia -> Sala de descanso?, selector "selection"
        //Sala de descanso?
        sequenceNodeList.Add(new ConditionNode(() => { return currentRoom.gameObject.name == "BreakRoom"; }));

        //Selector
        sequenceNodeList.Add(selection);


        //Creamos la segunda secuencia de acciones del arbol
       SequenceNode secondSequence = new SequenceNode(new List<IBehaviourNode>(sequenceNodeList));
        sequenceNodeList.Clear();

        //Creacion de la lista final para la primera secuencia
        sequenceNodeList.Add(firstSequence);
        sequenceNodeList.Add(secondSequence);

        behaviourTree.Root = new LoopUntilFailNode(new SelectorNode(new List<IBehaviourNode>(sequenceNodeList)));
    }

    public override void Exit()
    {
    }

    public override void FixedUpdate()
    {
        behaviourTree.FixedUpdateBehaviour();
    }

    public override void Update()
    {
        behaviourTree.UpdateBehaviour();
        if (behaviourTree.State == BehaviourState.Success) 
        {
            Debug.Log("Saliendo del BT");
            context.State = new JanitorWalkState(context, agent, agent.GetAgentGameObject().GetComponent<JanitorBehaviour>().GetOfficeRooms());
        }
    }

}
