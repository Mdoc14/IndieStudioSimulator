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

        Debug.Log("He entrado al BT State ");
        behaviourTree = new BehaviourTree();
        GameObject.FindObjectOfType<LightSwitch>().LightsOut += OnLightsOff;
        GameObject.FindObjectOfType<LightSwitch>().LightsOn += OnLightsOn;

        //Lista de nodos del arbol
        List<IBehaviourNode> treeNodes = new List<IBehaviourNode>();

        //Creacion de los nodos de la primera secuencia -> Observar sala, Hay basura?, Ir a la basura, Recoger basura, ir a papelera, tirar basura
        List<IBehaviourNode> sequenceNodeList = new List<IBehaviourNode>();

        //Observar la sala
        sequenceNodeList.Add(new ActionNode(
            new JanitorLookAction(agent), behaviourTree));

        //Hay basura?
        sequenceNodeList.Add(new ConditionNode(() => { return currentRoom.IsDirty(); }));

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
            new GoToAction(agent, currentRoom.GetTrashcanPosition, "Walk"), behaviourTree));

        //Tirar basura
        sequenceNodeList.Add(new ActionNode(
            new ThrowTrashAction(agent, currentRoom), behaviourTree));

        //Creamos la primera secuencia de acciones del arbol
        SequenceNode firstSequence = new SequenceNode(new List<IBehaviourNode>(sequenceNodeList));
        sequenceNodeList.Clear();

        //Creacion de los nodos de la tercera secuencia -> Ir a la maquina, Observar, Esta vacia la maquina?, Reponer
        //Ir a la máquina
        sequenceNodeList.Add(new ActionNode(
            new GoToAction(agent, currentRoom.GetMachinePosition, "Walk"), behaviourTree));

        //Observar la máquina
        sequenceNodeList.Add(new ActionNode(
            new JanitorLookAction(agent), behaviourTree));

        //Esta vacia?
        sequenceNodeList.Add(new ConditionNode(() => { return currentRoom.IsMachineEmpty(); }));

        //Ir a la maquina
        sequenceNodeList.Add(new ActionNode(
            new GoToAction(agent, currentRoom.GetMachinePosition, "Walk"), behaviourTree));

        //Reponer
        sequenceNodeList.Add(new ActionNode(
            new RefillAction(agent, currentRoom), behaviourTree));

        //Creacion de la tercera secuencia
        SequenceNode thirdSequence = new SequenceNode(new List<IBehaviourNode>(sequenceNodeList));
        sequenceNodeList.Clear();

        //Creacion de los nodos de la cuarta secuencia -> Ir a la caja de arena, Esta la caja sucia?, limpiar caja, ir al cubo, tirar basura
        //Ir a la caja
        sequenceNodeList.Add(new ActionNode(
            new GoToAction(agent, currentRoom.GetCatBoxPosition, "Walk"), behaviourTree));

        //Observar la caja
        sequenceNodeList.Add(new ActionNode(
            new JanitorLookAction(agent), behaviourTree));

        //Esta sucia?
        sequenceNodeList.Add(new ConditionNode(() => {return currentRoom.IsCatBoxDirty(); }));

        //Limpiar caja
        sequenceNodeList.Add(new ActionNode(
            new CleanCatBoxAction(agent, currentRoom), behaviourTree));

        //Ir a la papelera
        sequenceNodeList.Add(new ActionNode(
            //new GoToPositionAction(agent, currentRoom.GetTrashPosition()), behaviourTree));
            new GoToAction(agent, currentRoom.GetTrashcanPosition, "Walk"), behaviourTree));

        //Tirar heces
        sequenceNodeList.Add(new ActionNode(
            new ThrowCatFecesAction(agent, currentRoom), behaviourTree));

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
        GameObject.FindObjectOfType<LightSwitch>().LightsOut -= OnLightsOff;
        GameObject.FindObjectOfType<LightSwitch>().LightsOn -= OnLightsOn;
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

    private void OnLightsOff()
    {
        (agent as JanitorBehaviour).ToggleFlashlight(true);
    }

    private void OnLightsOn()
    {
        (agent as JanitorBehaviour).ToggleFlashlight(false);
    }
}
