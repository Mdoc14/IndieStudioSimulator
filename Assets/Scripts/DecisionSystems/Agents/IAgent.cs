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
        public void SetAgentVariable(string name, float value, float minValue, float maxValue);
        public Chair GetChair();
        public Computer GetComputer();
        public void SetCurrentChair(Chair bath);
        public Chair GetCurrentChair();
        public void SetBark(string name);
        public void SetAnimation(string triggerName);
    }
}
