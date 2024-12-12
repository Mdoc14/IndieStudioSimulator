using CharactersBehaviour;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JanitorLookAction : ASimpleAction
{
    float timer = 0f;
    float lookTotalTime = 2f; //Coincide con la duracion de la animacion Looking

    public JanitorLookAction(IAgent agent) : base(agent)
    {
    }

    public override void Enter()
    {
        base.Enter();
        Debug.Log("Accion: observar habitacion");
        base.Enter();
        //Iniciar animacion de mirar
        agent.SetAnimation("Looking");
        agent.SetBark("Look");
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

        if (timer >= lookTotalTime)
        {
            timer = 0f;
            finished = true;
        }
    }
}
