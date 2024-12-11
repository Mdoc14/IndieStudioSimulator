using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace CharactersBehaviour
{
    public class WanderAction : ASimpleAction
    {
        float wanderDistance = 10f;

        NavMeshAgent _agentNavMesh;

        int numberWanders;
        float timer = 2f;
        float timeBetweenWanders = 2f;

        public WanderAction(IAgent agent) : base(agent)
        {
        }

        public override void Enter()
        {
            base.Enter();
            numberWanders = Random.Range(3, 5);
            _agentNavMesh = agent.GetAgentGameObject().GetComponent<NavMeshAgent>();
        }

        public override void Exit()
        {
        }

        public override void Update()
        {
            if (HasReachDestination())
            {
                timer += Time.deltaTime;
            }

            if (timer >= timeBetweenWanders)
            {
                timer = 0f;
                Vector3 newDestination = Wander(agent.GetAgentGameObject().transform.position, wanderDistance);
                _agentNavMesh.SetDestination(newDestination);
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

        bool HasReachDestination()
        {
            return _agentNavMesh.remainingDistance <= _agentNavMesh.stoppingDistance && !_agentNavMesh.pathPending;
        }
    }
}
