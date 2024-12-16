using UnityEngine;
using UnityEngine.AI;

namespace CharactersBehaviour
{
    public class WaitForLightState : AState
    {
        Vector3 prevDestination;
        public WaitForLightState(StateMachine context, IAgent agent) : base(context, agent)
        {
        }

        public override void Enter()
        {
            GameObject.FindObjectOfType<LightSwitch>().LightsOn += OnLightsOn;
            agent.SetAnimation("Idle");
            agent.SetBark("Wait");
            if(agent.GetAgentGameObject().GetComponent<NavMeshAgent>().enabled) agent.GetAgentGameObject().GetComponent<NavMeshAgent>().isStopped = true;
        }

        public override void Exit()
        {
            GameObject.FindObjectOfType<LightSwitch>().LightsOn -= OnLightsOn;
            if(agent.GetAgentGameObject().GetComponent<NavMeshAgent>().enabled) agent.GetAgentGameObject().GetComponent<NavMeshAgent>().isStopped = false;
        }

        public override void Update()
        {
            
        }


        public override void FixedUpdate()
        {
            
        }

        public void OnLightsOn()
        {
            context.State = context.PreviousStates.Pop();
        }
    }
}