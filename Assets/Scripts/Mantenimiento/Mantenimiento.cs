using CharactersBehaviour;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


namespace CharactersBehaviour
{
    public class Mantenimiento : AgentMantenimiento
    {
        //Maquina de estado FSM1
        private CharactersBehaviour.StateMachine FSM1;

        //Estados de FSM1 SOLO TENGO QUE CREAR EL ESTADO INCIAL YA EL RESTO SE VAN CREANDO SOBRE A MARCHA
        //private CharactersBehaviour.State_AcariciarGato acariciarGatoState;
        //private CharactersBehaviour.State_ArreglarLuz arreglarLuzState;
        private CharactersBehaviour.State_FSM2 trabajarState;

        //Start is called before the first frame update
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

        //Update is called once per frame
        void Update()
        {
            FSM1.UpdateBehaviour();

        }
    }
}