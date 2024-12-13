using CharactersBehaviour;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CleanCatBoxAction : ASimpleAction
{
    float timer = 0f;
    float cleanTotalTime = 1.167f;

    public CleanCatBoxAction(IAgent agent) : base(agent)
    {
    }

    public override void Enter()
    {
        base.Enter();
        //Iniciar animacion de limpair caja
        Debug.Log("Limpiando caja del gato...");
        agent.SetBark("CleanBathroom");
        agent.SetAnimation("Take");
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

        if (timer >= cleanTotalTime)
        {
            timer = 0f;
            finished = true;
        }
    }

}
