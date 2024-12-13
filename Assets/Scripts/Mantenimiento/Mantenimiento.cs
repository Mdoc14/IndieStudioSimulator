using CharactersBehaviour;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


namespace CharactersBehaviour
{
    public class Mantenimiento : AgentBehaviour
    {
        //Maquina de estado FSM1
        private CharactersBehaviour.StateMachine FSM1;

        //Estados de FSM1 SOLO TENGO QUE CREAR EL ESTADO INCIAL YA EL RESTO SE VAN CREANDO SOBRE A MARCHA
        //private CharactersBehaviour.State_AcariciarGato acariciarGatoState;
        //private CharactersBehaviour.State_ArreglarLuz arreglarLuzState;
        private CharactersBehaviour.State_FSM2 trabajarState;

        // Referencia al interruptor de luz
        [SerializeField] private LightSwitch lightSwitch;

        void Awake()
        {

            agentVariables.Add("cansancio", 0);
            agentVariables.Add("lastState", 0);
            lightSwitch.LightsOut += OnLightsOut;
        }

        void Start()
        {
            
            //Inicializar máquina de estados FSM1
            FSM1 = new CharactersBehaviour.StateMachine();

            //Inicializar los estados de FSM1
            //acariciarGatoState = new CharactersBehaviour.State_AcariciarGato(FSM1, this);
            //arreglarLuzState = new CharactersBehaviour.State_ArreglarLuz(FSM1, this);
            trabajarState = new CharactersBehaviour.State_FSM2(FSM1, this); // Aquí se gestiona FSM2

            //Establecer el estado inicial
            FSM1.State = trabajarState;

        }
        private void OnLightsOut()
        {
            Debug.Log("¡Se han ido las luces! El personal de mantenimiento va a areglarlo.");
            FSM1.State = new CharactersBehaviour.State_ArreglarLuz(FSM1, this);
        }

        //Update is called once per frame
        void Update()
        {
            FSM1.UpdateBehaviour();

        }
    }
}