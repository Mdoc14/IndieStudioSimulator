using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace CharactersBehaviour
{
    public class WanderAction : ASimpleAction
    {
        float wanderDistance = 15f;

        NavMeshAgent _agentNavMesh;

        int currentWander = 0;
        int numberWanders;

        float timer;
        float timeBetweenWanders;

        public WanderAction(IAgent agent) : base(agent)
        {
        }

        public override void Enter()
        {
            base.Enter();
            numberWanders = Random.Range(3, 5);
            timer = 0f;
            _agentNavMesh = agent.GetAgentGameObject().GetComponent<NavMeshAgent>();
        }

        public override void Exit()
        {
        }

        public override void Update()
        {
            if (HasReachDestination())
            {
                if (currentWander >= numberWanders) { finished = true; } else { timer -= Time.deltaTime; }
            }

            if (timer <= 0f)
            {
                timeBetweenWanders = Random.Range(2f, 4f);
                timer = timeBetweenWanders;
                Vector3 newDestination = Wander(agent.GetAgentGameObject().transform.position, wanderDistance);
                _agentNavMesh.SetDestination(newDestination);
                currentWander++;
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
