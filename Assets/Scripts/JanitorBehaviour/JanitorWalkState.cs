using CharactersBehaviour;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class JanitorWalkState : AState
{
    List<GameObject> officeRooms;
    NavMeshAgent navMeshAgent;
    GameObject destinationRoom;
    JanitorBehaviour janitorBehaviour;

    bool isWalking = false;
    bool isOnObjectiveRoom = false;

    public JanitorWalkState(StateMachine sm, IAgent agent, List<GameObject> rooms) : base(sm, agent) 
    {
        officeRooms = rooms;
    }

    public override void Enter()
    {
        janitorBehaviour = agent.GetAgentGameObject().GetComponent<JanitorBehaviour>();

        if (janitorBehaviour.HaveToRest())
        {
            Debug.Log("El conserje ya ha visitado todas las salas, va a descansar");
            janitorBehaviour.ClearVisitatedRooms();
            janitorBehaviour.RoomDetectorsAreActive(false);
            context.State = new JanitorRestState(context, agent, janitorBehaviour.restTime);
        }
        else 
        {
            Debug.Log("Entrando al estado de caminar a sala...");
            agent.SetBark("Walk");
            agent.SetAnimation("Walk");

            //Determina la sala a la que debe ir de forma aleatoria
            int roomIndex = janitorBehaviour.GetRandomRoom();

            //Suscripcion al evento en el que el conserje entra en el collider de una sala
            destinationRoom = officeRooms[roomIndex];
            destinationRoom.GetComponent<Room>().OnColliderTriggered += IsOnObjective;

            //Indicamos la sala a la que el agente debe caminar
            navMeshAgent = agent.GetAgentGameObject().GetComponent<NavMeshAgent>();
            navMeshAgent.isStopped = false;
            navMeshAgent.SetDestination(destinationRoom.transform.position);
            Debug.Log("Caminando a la sala " + officeRooms[roomIndex].name);

            isWalking = true;
            janitorBehaviour.RoomDetectorsAreActive(true);
        }
    }

    public override void Exit()
    {
    }

    public override void FixedUpdate()
    {
    }

    public override void Update()
    {
        if (isWalking) return;

        //Cuando llega a la sala objetivo
        if (isOnObjectiveRoom) 
        {
            navMeshAgent.isStopped = true;
            context.State = new JanitorBehaviourTree(context, agent, destinationRoom.GetComponent<Room>());
        }
    }

    //Ejecutado cuando se activa el evento de cada sala
    void IsOnObjective(GameObject go)
    {
        isOnObjectiveRoom = true;
        isWalking = false;
        destinationRoom.GetComponent<Room>().OnColliderTriggered -= IsOnObjective;
    }
}
