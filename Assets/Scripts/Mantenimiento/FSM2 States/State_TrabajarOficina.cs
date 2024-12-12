using CharactersBehaviour;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace CharactersBehaviour
{
    public class State_TrabajarOficina : AState
    {
        public State_TrabajarOficina(StateMachine FSM2, IAgent agent) : base(FSM2, agent) { }
        CompositeAction _trabajarEnOficina;
        public float cansancioActual;

        public override void Enter()
        {
            agent.SetAgentVariable("lastState", 1);
            Debug.Log("Entrando en el estado TrabajarOficina");

            //Va a su silla y despues utiliza su ordenador 
            List<IAction> actions = new List<IAction>();
            if(!agent.GetChair().IsOccupied())
            {
                actions.Add(new GoToDeskAction(agent));
            }
            actions.Add(new ActionTrabajarOficina(agent));
            _trabajarEnOficina = new CompositeAction(actions);
            

        }

        public override void Exit()
        {
            Debug.Log("Saliendo del estado TrabajarOficina");
        }

        public override void Update()
        {
            //Debug.Log("Trabajando en la oficina...");
            _trabajarEnOficina?.Update();

            if (_trabajarEnOficina != null && _trabajarEnOficina.Finished)
            {
                
                //float rand = Random.Range(0.0f, 1.0f);
                float cansancio = agent.GetAgentVariable("cansancio") + Random.Range(0.1f, 0.5f);
                agent.SetAgentVariable("cansancio",cansancio);
                Debug.Log(agent.GetAgentVariable("cansancio"));
                if (agent.GetAgentVariable("cansancio")<0.5) context.State = new State_TrabajarOficina(context, agent);
                else context.State = new State_Dormir(context, agent);
            }

        }

        public override void FixedUpdate()
        {
            _trabajarEnOficina?.FixedUpdate();
        }
    }
}