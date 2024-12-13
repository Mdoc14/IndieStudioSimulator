using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CharactersBehaviour
{
    public class State_Dormir : AState
    {
        public State_Dormir(StateMachine FSM2, IAgent agent) : base(FSM2, agent) { }
        CompositeAction _irADormir;


        public override void Enter()
        {
            agent.SetAgentVariable("lastState", 2);
            Debug.Log("Entrando en el estado Dormir");

            //Va a su silla y despues duerme
            List<IAction> actions = new List<IAction>();
            if (!agent.GetChair().IsOccupied())
            {
                actions.Add(new GoToDeskAction(agent));
            }
            actions.Add(new ActionDormir(agent));
            _irADormir = new CompositeAction(actions);


        }

        public override void Exit()
        {
            Debug.Log("Saliendo del estado Dormir");
        }

        public override void Update()
        {
            
            _irADormir?.Update();

            if (_irADormir != null && _irADormir.Finished)
            {

                agent.SetAgentVariable("cansancio", 0);
                Debug.Log(agent.GetAgentVariable("cansancio"));
                context.State = new State_TrabajarOficina(context, agent);
                
            }

        }

        public override void FixedUpdate()
        {
            _irADormir?.FixedUpdate();
        }
    }
}