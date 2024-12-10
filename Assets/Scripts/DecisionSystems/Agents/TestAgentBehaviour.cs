using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CharactersBehaviour
{
    public class TestAgentBehaviour : AgentBehaviour
    {
        UtilitySystem behaviourSystem;
        string _timeWithoutMoving = "timeWithoutMoving";
        float timeCounter = 0f;

        // Start is called before the first frame update
        void Start()
        {
            agentVariables[_timeWithoutMoving] = 0f;
            List<UtilityBasedAction> actions = new List<UtilityBasedAction>();
            actions.Add(new UtilityBasedAction(new MoveAction(this), new LeafFactor(_timeWithoutMoving, 0, 0.5f)));
            behaviourSystem = new UtilitySystem(actions, this);
        }

        // Update is called once per frame
        void Update()
        {
            timeCounter += Time.deltaTime;
            if (timeCounter >= 5f)
            {
                behaviourSystem.activated = true;
            }
            agentVariables[_timeWithoutMoving] += Time.deltaTime * 0.1f;
            behaviourSystem.UpdateBehaviour();
        }
    }
}
