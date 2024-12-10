using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace CharactersBehaviour
{
    public class State_AcariciarGato : AState
    {
        public State_AcariciarGato(StateMachine FSM1, IAgent agent) : base(FSM1, agent) { }

        public override void Enter()
        {
            Debug.Log("Entrando en el estado AcariciarGato");
            
        }

        public override void Exit()
        {
            Debug.Log("Saliendo del estado AcariciarGato");
        }

        public override void Update()
        {
            Debug.Log("Acariciando al gato...");
            
        }

        public override void FixedUpdate()
        {
        }
    }
}