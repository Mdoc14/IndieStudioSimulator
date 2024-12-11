using CharactersBehaviour;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatBehaviour : AgentBehaviour
{
    string _boredom = "boredom";
    string _tiredness = "tiredness";

    [SerializeField] List<Chair> _catBeds = new List<Chair>();

    StateMachine sm = new StateMachine();
    UtilitySystem us;

    public string Boredom { get => _boredom; }
    public List<Chair> CatBeds { get => _catBeds; }
    public string Tiredness { get => _tiredness; }

    // Start is called before the first frame update
    void Start()
    {
        agentVariables[_boredom] = 0f;
        agentVariables[_tiredness] = 0f;

        sm.State = new WanderingState(sm, this);

        //us = new UtilitySystem(, this);
    }

    // Update is called once per frame
    void Update()
    {
        sm.UpdateBehaviour();
        //us.UpdateBehaviour();
    }
}
