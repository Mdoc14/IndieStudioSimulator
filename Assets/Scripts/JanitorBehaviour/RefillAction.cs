using CharactersBehaviour;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RefillAction : ASimpleAction
{
    float timer = 0f;
    float refillTotalTime = 3f;

    public RefillAction(IAgent agent) : base(agent)
    {
    }

    public override void Enter()
    {
        base.Enter();
        //Iniciar aniamcion de reponer
        Debug.Log("Reponiendo maquina...");
        agent.SetBark("Supply");
    }

    public override void Exit()
    {
    }

    public override void FixedUpdate()
    {
    }

    public override void Update()
    {
        timer += Time.deltaTime;

        if (timer >= refillTotalTime)
        {
            timer = 0f;
            finished = true;
        }
    }

    // Start is called before the first frame update
    void Start()
    {

    }
}
