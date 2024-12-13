using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace CharactersBehaviour
{
    public class State_FSM2 : AState
    {
        private StateMachine FSM2; //FSM interna que se manejar� en este estado

        public State_FSM2(StateMachine FSM1, IAgent agent) : base(FSM1, agent)
        {
            FSM2 = new StateMachine(); //Inicializar m�quina de estados FSM2
        }

        public override void Enter()
        {
            Debug.Log("Entrando en el estado Trabajar");
            //Establecer estado inicial para FSM2
            FSM2.State = new State_TrabajarOficina(FSM2, agent);
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