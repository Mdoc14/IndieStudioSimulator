using CharactersBehaviour;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CleanCatBoxAction : ASimpleAction
{
    public CleanCatBoxAction(IAgent agent) : base(agent)
    {
    }

    public override void Enter()
    {
        base.Enter();
        //Iniciar animacion de limpair caja
        Debug.Log("Limpiando caja del gato...");
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
