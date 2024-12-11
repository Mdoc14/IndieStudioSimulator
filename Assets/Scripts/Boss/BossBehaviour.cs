using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CharactersBehaviour;
using UnityEngine.AI;

public class BossBehaviour : AgentBehaviour
{
    [SerializeField] private float _maxWorkTime;
    private StateMachine _bossMachine = new StateMachine();
    [SerializeField] private Transform[] _patrolWaypoints;
    private List<int[]> _adjacencyList = new List<int[]>() { new int[] { 3, 1 }, new int[] { 0, 2 }, new int[] { 1, 3 }, new int[] { 2, 0 } };
    private int _currentWayPoint;
    private IAgent _agentToScold = null;

    void Awake()
    {
        //El enfado es un medidor que va de 0 a 100. En el momento que llega a 100 el jefe regaña a un empleado en su oficina
        //En cada simulación, el jefe puede ser más o menos propenso al enfado, y comienza con un enfado aleatorio
        agentVariables.Add("Irritability", Random.Range(0.10f, 1)); //El jefe tendrá una irritabilidad variable en cada ejecución
        agentVariables.Add("MaxWorkTime", _maxWorkTime);
        agentVariables.Add("CurrentAnger", Random.Range(0, 100));
        agentVariables.Add("Speed", GetComponent<NavMeshAgent>().speed);
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

    public Transform GetFirstWaypoint()
    {
        _currentWayPoint = 0;
        return _patrolWaypoints[_currentWayPoint];
    }

    public Transform GetCurrentWaypoint()
    {
        return _patrolWaypoints[_currentWayPoint];
    }

    public Transform GetNextWaypoint()
    {
        int rand = Random.Range(0, 2);
        int index = _adjacencyList[_currentWayPoint][rand];
        _currentWayPoint = index;
        return _patrolWaypoints[index];
    }

    public IAgent ScoldedAgent { get { return _agentToScold; } set { if(_agentToScold == null || value == null) _agentToScold = value; } }
}
