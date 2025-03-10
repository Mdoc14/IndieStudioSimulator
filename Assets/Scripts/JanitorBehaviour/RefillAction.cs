using CharactersBehaviour;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RefillAction : ASimpleAction
{
    float timer = 0f;
    float refillTotalTime = 5f;
    Room currentRoom;

    public RefillAction(IAgent agent, Room currentRoom) : base(agent)
    {
        this.currentRoom = currentRoom;
    }

    public override void Enter()
    {
        base.Enter();
        //Iniciar aniamcion de reponer
        Debug.Log("Reponiendo maquina...");
        agent.SetBark("Supply");
        agent.SetAnimation("Refill");
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
            currentRoom.GetVendingMachine().RefillMachine();

        }
    }
}
