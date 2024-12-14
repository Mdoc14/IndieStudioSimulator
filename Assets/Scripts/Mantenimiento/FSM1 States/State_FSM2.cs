using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using UnityEngine;
namespace CharactersBehaviour
{
    public class State_FSM2 : AState
    {
        private StateMachine FSM2; //FSM interna que se manejará en este estado

        public State_FSM2(StateMachine FSM1, IAgent agent) : base(FSM1, agent)
        {
            FSM2 = new StateMachine(); //Inicializar máquina de estados FSM2
        }

        public override void Enter()
        {
            Debug.Log("Entrando en el estado Trabajar");
            context.PreviousStates.Push(this);
            //Establecer estado inicial para FSM2
            if (FSM2.State == null) FSM2.State = new State_TrabajarOficina(FSM2, agent);
            else FSM2.State.Enter();
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