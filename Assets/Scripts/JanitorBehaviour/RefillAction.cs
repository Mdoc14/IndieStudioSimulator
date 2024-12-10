using CharactersBehaviour;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RefillAction : ASimpleAction
{
    public RefillAction(IAgent agent) : base(agent)
    {
    }

    public override void Enter()
    {
        base.Enter();
        //Iniciar aniamcion de reponer
        Debug.Log("Reponiendo maquina...");
    }

    public override void Exit()
    {
    }

    public override void FixedUpdate()
    {
    }

    public override void Update()
    {
    }

    // Start is called before the first frame update
    void Start()
    {

    }
}
