using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CharactersBehaviour
{
    public class State_Dormir : AState
    {
        public State_Dormir(StateMachine FSM2, IAgent agent) : base(FSM2, agent) { }

        public override void Enter()
        {
            Debug.Log("Entrando en el estado Dormir");

        }

        public override void Exit()
        {
            Debug.Log("Saliendo del estado Dormir");
        }

        public override void Update()
        {
            Debug.Log("Durmiendo...");

        }

        public override void FixedUpdate()
        {
        }
    }
}