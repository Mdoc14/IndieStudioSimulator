using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace CharactersBehaviour
{
    public class State_FSM2 : AState
    {
        private StateMachine FSM2; //FSM interna que se manejará en este estado

        //Estados de FSM2
       
        private State_TrabajarOficina trabajarOficinaState;
        //private State_AtenderIncidencia atenderIncidenciaState;
        //private State_ExaminarIncidencia examinarIncidenciaState;
        //private State_BuscarHerramientas buscarHerramientasState;
        //private State_RepararIncidencia repararIncidenciaState;
        //private State_Dormir dormirState;

        public State_FSM2(StateMachine FSM1, IAgent agent) : base(FSM1, agent)
        {
            FSM2 = new StateMachine(); //Inicializar máquina de estados FSM2
        }

        public override void Enter()
        {
            Debug.Log("Entrando en el estado Trabajar");

            //Inicializar los estados de FSM2
            trabajarOficinaState = new State_TrabajarOficina(FSM2, agent);
            //atenderIncidenciaState = new State_AtenderIncidencia(FSM2, agent);
            //examinarIncidenciaState = new State_ExaminarIncidencia(FSM2, agent);
            //buscarHerramientasState = new State_BuscarHerramientas(FSM2, agent);
            //repararIncidenciaState = new State_RepararIncidencia(FSM2, agent);
            //dormirState = new State_Dormir(FSM2, agent);

            //Establecer estado inicial para FSM2
            FSM2.State = trabajarOficinaState;
        }

        public override void Exit()
        {
            Debug.Log("Saliendo del estado Trabajar");
            //Limpiar o resetear FSM2 si es necesario
        }

        public override void Update()
        {
            FSM2.UpdateBehaviour();

          

        }


        public override void FixedUpdate()
        {
            FSM2.FixedUpdateBehaviour();
        }
    }
}