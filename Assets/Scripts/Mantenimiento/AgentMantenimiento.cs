using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CharactersBehaviour
{
    public class AgentMantenimiento : AgentBehaviour
    {

        // Implementación del método GetAgentGameObject:
        protected Dictionary<string, float> agentVariables = new Dictionary<string, float>();
        //[SerializeField] private Chair _chair;

        /*
        [SerializeField] private int  lastState;
        [SerializeField] private float cansancio;
        void Awake()
        {
           
            agentVariables.Add("lastState", 0);
            agentVariables.Add("cansancio", 0);
            
        }
        */
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

        /*
        public Chair GetChair() //La silla del puesto de trabajo del agente
        {
            //return _chair;
        }*/

        
    }
}