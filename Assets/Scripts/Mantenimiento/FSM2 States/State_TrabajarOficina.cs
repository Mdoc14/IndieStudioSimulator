using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CharactersBehaviour
{
    public class State_TrabajarOficina : AState
    {
        public State_TrabajarOficina(StateMachine FSM2, IAgent agent) : base(FSM2, agent) { }

        public override void Enter()
        {
            Debug.Log("Entrando en el estado TrabajarOficina");

        }

        public override void Exit()
        {
            Debug.Log("Saliendo del estado TrabajarOficina");
        }

        public override void Update()
        {
            Debug.Log("Trabajando en la oficina...");
            context.State = new State_Dormir(context,agent);

        }

        public override void FixedUpdate()
        {
        }
    }
}