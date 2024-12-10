using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CharactersBehaviour;

public class BossBehaviour : AgentBehaviour
{
    [SerializeField] private float _maxWorkTime;
    private StateMachine _bossMachine = new StateMachine();

    void Awake()
    {
        float irritability = Random.Range(0, 1); //El jefe tendr� una irritabilidad variable en cada ejecuci�n
        agentVariables.Add("Irritability", irritability);
        agentVariables.Add("MaxWorkTime", _maxWorkTime);
        _bossMachine.State = new BossWorkState(_bossMachine, this);
    }

    void Update()
    {
        _bossMachine.UpdateBehaviour();
    }

    private void FixedUpdate()
    {
        _bossMachine.FixedUpdateBehaviour();
    }
}
