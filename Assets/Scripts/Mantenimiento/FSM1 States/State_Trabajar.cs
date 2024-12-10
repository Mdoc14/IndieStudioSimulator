using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CharactersBehaviour
{
    public class State_Trabajar : AState
    {
        public State_Trabajar(StateMachine FSM1, IAgent agent) : base(FSM1, agent) { }

        public override void Enter()
        {
            Debug.Log("Entrando en el estado Trabajar");

        }

        public override void Exit()
        {
            Debug.Log("Saliendo del estado Trabajar");
        }

        public override void Update()
        {
            Debug.Log("Trabajando...");

        }

        public override void FixedUpdate()
        {
        }
    }
}