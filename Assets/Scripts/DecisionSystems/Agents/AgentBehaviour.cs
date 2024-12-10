using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CharactersBehaviour
{
    public abstract class AgentBehaviour : MonoBehaviour, IAgent
    {
        protected Dictionary<string, float> agentVariables = new Dictionary<string, float>();

        public GameObject GetAgentGameObject()
        {
            return gameObject;
        }

        public float GetAgentVariable(string name)
        {
            return agentVariables[name];
        }
    }
}
