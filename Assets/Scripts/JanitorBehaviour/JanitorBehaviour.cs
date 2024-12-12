using CharactersBehaviour;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JanitorBehaviour : AgentBehaviour
{
    //Variables del conserje
    public string restTime = "RestTime";

    public List<GameObject> officeRooms;
    List<int> visitatedRooms = new List<int>();
    int lastRoomID;

    System.Random rand;

    //Controladores del sistema de comportamiento
    StateMachine stateMachine;

    private void Start()
    {
        //Inicializar las variables del conserje
        agentVariables[restTime] = Random.Range(5f, 10f);
        Debug.Log("El conserje descansará " + agentVariables[restTime] + " segundos");
        lastRoomID = -1;
        //Creamos la maquina de estados con el descanso como estado inicial
        stateMachine = new StateMachine();
        stateMachine.State = new JanitorRestState(stateMachine, this, restTime);

        WorldManager.Instance.GenerateTrash(new Vector3(7.51f, 0.61f, 6.13f));
        WorldManager.Instance.GenerateTrash(new Vector3(9.19f, 0.61f, 15.045f));
    }

    // Update is called once per frame
    void Update()
    {
        stateMachine.UpdateBehaviour();
    }

    private void FixedUpdate()
    {
        stateMachine.FixedUpdateBehaviour();
    }

    public List<GameObject> GetOfficeRooms()
    {
        return officeRooms;
    }

    public int GetRandomRoom() 
    {
        //Determina la sala a la que debe ir de forma aleatoria
        rand = new System.Random();
        int roomIndex;

        //Al comienzo de la ejecucion
        if(lastRoomID == -1)
        {
            roomIndex = rand.Next(0, officeRooms.Count);
            lastRoomID = roomIndex;
            visitatedRooms.Add(roomIndex);
            return roomIndex;
        }
        else
        {
            roomIndex = rand.Next(0, officeRooms.Count);
            while (roomIndex == lastRoomID) 
            {
                roomIndex = rand.Next(0, officeRooms.Count);
            }
            lastRoomID = roomIndex;
            visitatedRooms.Add(roomIndex);
            return roomIndex;
        }
        
    }

    public bool HaveToRest()
    {
        return visitatedRooms.Count == officeRooms.Count;
    }

    public void ClearVisitatedRooms()
    {
        Debug.Log("borrando lista");
        visitatedRooms.Clear();
        lastRoomID = -1;
    }
}
