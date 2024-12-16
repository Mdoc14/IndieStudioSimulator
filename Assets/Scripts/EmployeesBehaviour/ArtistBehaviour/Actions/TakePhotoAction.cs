using CharactersBehaviour;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakePhotoAction : ASimpleAction
{
    private float _photoTime;
    public TakePhotoAction(IAgent agent) : base(agent) { }
    public override void Enter()
    {
        base.Enter();
        Debug.Log("Artista está sacando foto...");
        _photoTime = Random.Range(10, 25);
        agent.SetBark("Photo");
        agent.SetAnimation("TakePhoto");
    }
    public override void Exit()
    {
    }
    public override void FixedUpdate()
    {
    }
    public override void Update()
    {
        _photoTime -= Time.deltaTime;

        if (agent.GetAgentVariable((agent as EmployeeBehaviour).Motivation) <= 100f)
        {
            agent.SetAgentVariable((agent as EmployeeBehaviour).Motivation, agent.GetAgentVariable((agent as EmployeeBehaviour).Motivation) + Time.deltaTime);
        }
        else { agent.SetAgentVariable((agent as EmployeeBehaviour).Motivation, 100f); }

        if (_photoTime <= 0)
        {
            agent.SetAgentVariable((agent as EmployeeBehaviour).Boredom, Random.Range(0, 10));
            Debug.Log("Artista ha terminado de sacar foto");
            finished = true;
        }
    }
}
