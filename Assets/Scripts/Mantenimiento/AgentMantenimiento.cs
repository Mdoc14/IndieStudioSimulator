using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CharactersBehaviour
{
    public class AgentMantenimiento : MonoBehaviour, IAgent
    {

        private bool isTired;  //Variable que determina si est� cansado

        // Implementaci�n del m�todo GetAgentGameObject:
        public GameObject GetAgentGameObject()
        {
            return this.gameObject; //Devuelve el GameObject asociado al componente
        }

        public float GetAgentVariable(string name)
        {
            throw new System.NotImplementedException();
        }

        public Chair GetChair()
        {
            throw new System.NotImplementedException();
        }

        public void SetAgentVariable(string name, float value)
        {
            throw new System.NotImplementedException();
        }
    }
}