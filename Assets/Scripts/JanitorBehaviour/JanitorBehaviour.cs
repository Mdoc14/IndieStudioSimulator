using CharactersBehaviour;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JanitorBehaviour : AgentBehaviour
{
    //Variables del conserje
    public string restTime = "RestTime";

    public List<GameObject> officeRooms;

    //Controladores del sistema de comportamiento
    StateMachine stateMachine;

    private void Start()
    {
        //Inicializar las variables del conserje
        agentVariables[restTime] = Random.Range(5f, 10f);
        Debug.Log("El conserje descansará " + agentVariables[restTime] + " segundos");

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
}
