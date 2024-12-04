using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CharactersBehaviour
{
    public abstract class AgentBehaviour : MonoBehaviour, IAgent
    {
        private Dictionary<string, float> _agentVariables = new Dictionary<string, float>();

        protected Dictionary<string, float> AgentVariables { get { return _agentVariables; } }

        public GameObject GetAgentGameObject()
        {
            return gameObject;
        }

        public float GetAgentVariable(string name)
        {
            return AgentVariables[name];
        }
    }
}
