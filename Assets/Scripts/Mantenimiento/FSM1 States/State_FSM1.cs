using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CharactersBehaviour
{
    public class State_FSM1: AState
    {
        private StateMachine FSM1; //FSM interna que se manejará en este estado

        //Estados de FSM1 
        private State_Trabajar trabajarState;
        public State_FSM1(StateMachine FSM1, IAgent agent) : base(FSM1, agent)
        {
            FSM1 = new StateMachine(); //Inicializar máquina de estados FSM2
        }

        public override void Enter()
        {
            Debug.Log("Entrando en el estado Trabajar");

            //Establecer el estado inicial para FSM1
            trabajarState = new State_Trabajar(FSM1, agent);
            FSM1.State = trabajarState;
        }

        public override void Exit()
        {
            Debug.Log("Saliendo del estado Trabajar");
            //Limpiar o resetear FSM1 si es necesario
        }

        public override void Update()
        {
            FSM1.UpdateBehaviour();

        }

        public override void FixedUpdate()
        {
            FSM1.FixedUpdateBehaviour();
        }
    }
}