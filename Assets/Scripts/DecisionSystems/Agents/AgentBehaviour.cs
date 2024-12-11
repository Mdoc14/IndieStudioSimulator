using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CharactersBehaviour
{
    public abstract class AgentBehaviour : MonoBehaviour, IAgent
    {
        protected Dictionary<string, float> agentVariables = new Dictionary<string, float>();
        [SerializeField] private Chair _chair;
        private Chair _bath;

        public GameObject GetAgentGameObject()
        {
            return gameObject;
        }

        public float GetAgentVariable(string name)
        {
            return agentVariables[name];
        }

        public void SetAgentVariable(string name, float value)
        {
            agentVariables[name] = value;
        }

        public Chair GetChair() //La silla del puesto de trabajo del agente
        {
            return _chair;
        }

        public void SetBath(Chair bath)
        {
            _bath = bath;
        }

        public Chair GetBath()
        {
            return _bath;
        }

        public void SetBark(string name)
        {
            Sprite bark = null;
            if(BarkManager.Instance != null) bark = BarkManager.Instance.GetBark(name);
            if (bark != null) transform.GetComponentInChildren<SpriteRenderer>().sprite = bark;
        }

        public void SetAnimation(string triggerName)
        {
            transform.GetComponentInChildren<Animator>().SetTrigger(triggerName);
        }
    }
}
