using CharactersBehaviour;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


namespace CharactersBehaviour
{
    public class Mantenimiento : AgentBehaviour
    {

        //FSM interna que se manejará en este estado
        private StateMachine FSM2; 

        //Estados de FSM2
        private State_TrabajarOficina trabajarOficinaState;
        //private State_AtenderIncidencia atenderIncidenciaState;
        //private State_ExaminarIncidencia examinarIncidenciaState;
        //private State_BuscarHerramientas buscarHerramientasState;
        //private State_RepararIncidencia repararIncidenciaState;
        //private State_Dormir dormirState;

        

        //Start is called before the first frame update
        void Start()
        {

            //Inicializar máquina de estados FSM1
            FSM2 = new StateMachine();

            //Establecer el estado inicial
            trabajarOficinaState = new State_TrabajarOficina(FSM2, this);
            FSM2.State = trabajarOficinaState;
        }

        //Update is called once per frame
        void Update()
        {
            FSM2.UpdateBehaviour();

        }
    }
}