using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace CharactersBehaviour
{
    public interface IAgent
    {
        public GameObject GetAgentGameObject();
        public float GetAgentVariable(string name);
        public void SetAgentVariable(string name, float value);
        public Chair GetChair();
        public void SetBath(Chair bath);
        public Chair GetBath();
        public void SetBark(string name);
    }
}
