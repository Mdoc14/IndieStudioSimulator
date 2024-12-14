using CharactersBehaviour;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmployeeBehaviour : AgentBehaviour
{
    [SerializeField] float _lastMealTimer;
    [SerializeField] float _lastWCTimer;
    [SerializeField] GameObject reunionChair;
    public bool working;
    protected StateMachine _workerFSM = new StateMachine();
    protected void Awake()
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
    }

    protected void Start()
    {
        WorldManager.Instance.OnNotifyEmployeesStart += MeetingStarted;
    }

    void Update()
    {
        _workerFSM.UpdateBehaviour();
    }
    void FixedUpdate()
    {
        _workerFSM.FixedUpdateBehaviour();
    }
    public void SetReunionChair(GameObject chair)
    {
        reunionChair = chair;
    }
    public GameObject GetReunionChair() 
    {
        return reunionChair;
    }
    public void MeetingStarted()
    {
        if (working)
        {
            working = false;
            WorldManager.Instance.SetWorkerActivity(false);
        }
        if (GetChair().IsOccupied())
        {
            GetChair().Leave();
        }
        if (GetCurrentChair()!= null)
        {
            if (GetCurrentChair().IsOccupied())
            {
                GetCurrentChair().Leave();
            }
        }

        _workerFSM.State = new MeetingState(_workerFSM, this, (AState)_workerFSM.State);
    }
}
