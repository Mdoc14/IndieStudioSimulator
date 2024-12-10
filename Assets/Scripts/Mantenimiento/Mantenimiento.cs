using CharactersBehaviour;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Mantenimiento : AgentBehaviour
{
    //Agente 
    public GameObject agentObject; 

    //Maquina de estado FSM1
    private CharactersBehaviour.StateMachine FSM1;

    //Estados de FSM1
    private CharactersBehaviour.State_AcariciarGato acariciarGatoState;
    private CharactersBehaviour.State_ArreglarLuz arreglarLuzState;
    private CharactersBehaviour.State_FSM2 trabajarState;

    //Start is called before the first frame update
    void Start()
    {

        //Obtener el componente que implementa IAgent
        IAgent agent = agentObject.GetComponent<AgentMantenimiento>();

        //Inicializar máquina de estados FSM1
        FSM1 = new CharactersBehaviour.StateMachine();

        //Inicializar los estados de FSM1
        acariciarGatoState = new CharactersBehaviour.State_AcariciarGato(FSM1, agent);
        arreglarLuzState = new CharactersBehaviour.State_ArreglarLuz(FSM1, agent);
        trabajarState = new CharactersBehaviour.State_FSM2(FSM1, agent); // Aquí se gestiona FSM2

        //Establecer el estado inicial
        FSM1.State = trabajarState;
    }

    //Update is called once per frame
    void Update()
    {
        FSM1.UpdateBehaviour();

        //Transiciones entre estados de FSM1
        if (Input.GetKeyDown(KeyCode.G)) //Si el gato quiere mimos
        {
            FSM1.State = acariciarGatoState;
        }

        if (Input.GetKeyDown(KeyCode.L)) //Si se rompe la luz
        {
            FSM1.State = arreglarLuzState;
        }

        if (Input.GetKeyDown(KeyCode.T)) //Si empieza a trabajar
        {
            FSM1.State = trabajarState; //Esto cambia a la maquina de estados FSM2
        }

    }
}
