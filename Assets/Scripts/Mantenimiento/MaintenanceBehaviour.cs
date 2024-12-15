using CharactersBehaviour;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using System;


namespace CharactersBehaviour
{
    public class MaintenanceBehaviour : AgentBehaviour
    {
        //Maquina de estado FSM1
        private StateMachine FSM1;
        [SerializeField] private float _maxCansancio = 1;
        public event Action OnIncidence;
        private AInteractable _currentIncidence = null;

        //Estados de FSM1 SOLO TENGO QUE CREAR EL ESTADO INCIAL YA EL RESTO SE VAN CREANDO SOBRE A MARCHA
        //private CharactersBehaviour.State_AcariciarGato acariciarGatoState;
        //private CharactersBehaviour.State_ArreglarLuz arreglarLuzState;

        // Referencia al interruptor de luz
        [SerializeField] private LightSwitch lightSwitch;

        void Awake()
        {
            agentVariables.Add("cansancio", 0);
            agentVariables.Add("lastState", 0);
            agentVariables.Add("maxCansancio", _maxCansancio);
            lightSwitch.LightsOut += OnLightsOut;
        }

        void Start()
        {
            FSM1 = new StateMachine();
            FSM1.State = new State_FSM2(FSM1, this);
        }
        private void OnLightsOut()
        {
            Debug.Log("¡Se han ido las luces! El personal de mantenimiento va a areglarlo.");
            FSM1.State = new State_ArreglarLuz(FSM1, this);
        }

        //Update is called once per frame
        void Update()
        {
            FSM1.UpdateBehaviour();
        }

        public AInteractable GetCurrentIncidence()
        {
            return _currentIncidence;
        }

        public void SetCurrentIncidence(AInteractable incidence)
        {
            _currentIncidence = incidence;
            if (incidence != null) OnIncidence?.Invoke();
        }
    }
}