using CharactersBehaviour;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JanitorBehaviour : AgentBehaviour
{
    //Variables del conserje
    public string restTime = "RestTime";
    public string trashCleaned = "TrashCleaned";

    //Lista de las habitaciones de la oficina y lista de habitaciones ya visitadas
    public List<GameObject> officeRooms;
    List<int> visitatedRooms = new List<int>();

    //Generador de numeros aleatorios
    System.Random rand;

    //Controladores del sistema de comportamiento
    StateMachine stateMachine;

    //Linterna de hombro para cuando se apaga la luz:
    [SerializeField] private Light flashlight;

    private void Start()
    {
        //Inicializar las variables del conserje
        agentVariables[restTime] = Random.Range(5f, 10f);
        agentVariables[trashCleaned] = 0;

        //Creamos la maquina de estados con el descanso como estado inicial
        stateMachine = new StateMachine();
        stateMachine.State = new JanitorRestState(stateMachine, this, restTime);
    }

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

    //Determina la sala a la que debe ir de forma aleatoria, pero evitando repetir salas
    public int GetRandomRoom() 
    {
        rand = new System.Random();
        int roomIndex;

        //Al comienzo de la ejecucion
        if(visitatedRooms.Count == 0)
        {
            roomIndex = rand.Next(0, officeRooms.Count);
            visitatedRooms.Add(roomIndex);
            return roomIndex;
        }
        else
        {
            roomIndex = rand.Next(0, officeRooms.Count);
            while (visitatedRooms.Contains(roomIndex)) 
            {
                roomIndex = rand.Next(0, officeRooms.Count);
            }
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
        CalculateRestTime();
        visitatedRooms.Clear();
    }

    public void CalculateRestTime() 
    {
        agentVariables[restTime] = agentVariables[trashCleaned] * 10;
    }

    public void RoomDetectorsAreActive(bool value)
    {
        foreach (var room in officeRooms)
        { 
            room.SetActive(value);
        }
    }

    public void ToggleFlashlight(bool on)
    {
        flashlight.enabled = on;
        if(on) flashlight.transform.parent.GetComponent<Renderer>().materials[2].EnableKeyword("_EMISSION");
        else flashlight.transform.parent.GetComponent<Renderer>().materials[2].DisableKeyword("_EMISSION");
    }
}
