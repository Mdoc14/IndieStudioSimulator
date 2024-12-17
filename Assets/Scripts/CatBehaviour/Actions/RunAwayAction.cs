using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace CharactersBehaviour
{
    public class RunAwayAction : ASimpleAction
    {
        float runDistance = 30f;

        NavMeshAgent _navAgent;

        bool _reached = false;

        public RunAwayAction(IAgent agent) : base(agent)
        {
        }

        public override void Enter()
        {
            base.Enter();
            _reached = false;
            _navAgent = agent.GetAgentGameObject().GetComponent<NavMeshAgent>();
            agent.SetAnimation("Walk");
            _navAgent.speed = 7f;
            agent.SetAnimationSpeed(2f);
            Vector3 newDestination = Wander(agent.GetAgentGameObject().transform.position, runDistance);
            _navAgent.SetDestination(newDestination);
        }

        public override void Exit()
        {
        }

        public override void Update()
        {
            if (!_reached)
            {
                if (!_navAgent.pathPending && _navAgent.remainingDistance <= _navAgent.stoppingDistance)
                {
                    _reached = true;
                    _navAgent.speed = 3.5f;
                    agent.SetAnimationSpeed(1f);
                    agent.SetAnimation("Idle");
                    finished = true;
                }
            }
        }

        public override void FixedUpdate()
        {
        }

        Vector3 Wander(Vector3 origin, float distance)
        {
            Vector3 randomDestination = Random.insideUnitSphere * distance;
            randomDestination += origin;

            NavMeshHit hit;
            NavMesh.SamplePosition(randomDestination, out hit, distance, NavMesh.AllAreas);

            return hit.position;
        }
    }
}
