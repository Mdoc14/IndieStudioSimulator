using CharactersBehaviour;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgrammerBehaviour : AgentBehaviour
{
    [SerializeField] float _lastMealTimer;
    [SerializeField] float _lastWCTimer;
    private StateMachine _programmerFSM = new StateMachine();
    void Awake()
    {
        float motivation = Random.Range(0.8f, 1f);
        float boredom = Random.Range(0f, 0.2f);
        float stress = Random.Range(0f, 0.2f);
        float maxWorkTime = Random.Range(15f, 30f);
        agentVariables.Add("Motivation", motivation);
        agentVariables.Add("Boredom", boredom);
        agentVariables.Add("Stress", stress);
        agentVariables.Add("maxWorkTime", maxWorkTime);
        agentVariables.Add("LastMealTimer", _lastMealTimer);
        agentVariables.Add("LastWCTimer", _lastWCTimer);
        _programmerFSM.State = new ProgrammerWorkState(_programmerFSM, this);
    }

    void Update()
    {
        _programmerFSM.UpdateBehaviour();
    }
    void FixedUpdate()
    {
        _programmerFSM.FixedUpdateBehaviour();
    }
}
