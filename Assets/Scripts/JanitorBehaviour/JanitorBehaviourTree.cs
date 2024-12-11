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
        WorldManager.Instance.GenerateTrash(new Vector3(7.51f, 0.61f, 6.13f));

        Debug.Log("He entrado al behaviour tree....");
        behaviourTree = new BehaviourTree();

        //Lista de nodos del arbol
        List<IBehaviourNode> treeNodes = new List<IBehaviourNode>();

        //Creacion de los nodos de la primera secuencia -> Hay basura?, Ir a la basura, Recoger basura, ir a papelera, tirar basura
        List<IBehaviourNode> sequenceNodeList = new List<IBehaviourNode>();

        //Hay basura?
        sequenceNodeList.Add(new ConditionNode(() => { return currentRoom.IsDirty(); }));

        //Ir a la basura
        sequenceNodeList.Add(new ActionNode(
            //new GoToPositionAction(agent, currentRoom.GetTrashPosition()), behaviourTree));
            new GoToAction(agent, currentRoom.GetTrashPosition), behaviourTree));
        //Recoger basura
        sequenceNodeList.Add(new ActionNode(
            new PickUpTrashAction(agent), behaviourTree));

        //Ir a la papelera (de momento las tira en el mismo sitio donde las recoge)
        sequenceNodeList.Add(new ActionNode(
            //new GoToPositionAction(agent, currentRoom.GetTrashPosition()), behaviourTree));
            new GoToAction(agent, currentRoom.GetTrashPosition), behaviourTree));

        //Tirar basura
        sequenceNodeList.Add(new ActionNode(
            new ThrowTrashAction(agent), behaviourTree));

        //Creamos la primera secuencia de acciones del arbol
        SequenceNode firstSequence = new SequenceNode(new List<IBehaviourNode>(sequenceNodeList));
        sequenceNodeList.Clear();

        //Creacion de los nodos de la tercera secuencia -> Esta vacia la maquina?, Reponer
        //Esta vacia?
        sequenceNodeList.Add(new ConditionNode(() => { return currentRoom.IsMachineEmpty(); }));

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
            new GoToAction(agent, currentRoom.GetCatBoxPosition), behaviourTree));

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
       SequenceNode secondSecuence = new SequenceNode(new List<IBehaviourNode>(sequenceNodeList));
        sequenceNodeList.Clear();

        //Creacion de la lista final para la primera secuencia
        sequenceNodeList.Add(firstSequence);
        sequenceNodeList.Add(secondSecuence);

        behaviourTree.Root = new SelectorNode(new List<IBehaviourNode>(sequenceNodeList));
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
    }

}
