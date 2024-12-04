using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CharactersBehaviour
{
    public class TestAgentBehaviour : AgentBehaviour
    {
        UtilitySystem behaviourSystem;
        string _timeWithoutMoving = "timeWithoutMoving";

        // Start is called before the first frame update
        void Start()
        {
            AgentVariables[_timeWithoutMoving] = 0f;
            behaviourSystem = new UtilitySystem(new List<UtilityBasedAction>() { new UtilityBasedAction(new MoveAction(this), new LeafFactor(_timeWithoutMoving, 0, 0.5f))}, this);
        }

        // Update is called once per frame
        void Update()
        {
            AgentVariables[_timeWithoutMoving] += Time.deltaTime * 0.1f;
            behaviourSystem.UpdateBehaviour();
        }
    }
}
