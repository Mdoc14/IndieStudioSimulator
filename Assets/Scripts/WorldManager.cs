using CharactersBehaviour;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class WorldManager : MonoBehaviour
{
    public static WorldManager Instance { get; private set; }
    //Manejo de la simulación: 
    [SerializeField] private float _productivityFactor = 0.2f; //La productividad aumenta cada segundo * número de trabajadores trabajando
    [SerializeField] private float _productivityDecreaseFactor = 1; //La productividad baja cada segundo
    private Slider _productivitySlider;
    private float _activeWorkers = 0;
    private float _productivity = 500; //0 -> bancarrota; 1000 -> éxito total, se van de vacaciones
    //Manejo de la basura:
    [SerializeField] private GameObject _trashPrefab;
    //Reuniones:
    private int _numWorkersReunion = 0;
    public event Action OnWorkersReady;
    public event Action OnNotifyEmployeesStart;
    public event Action OnNotifyEmployeesEnd;

    void Awake()
    {
        Instance = this;

        _productivitySlider = MainMenuManager.Instance.transform.GetComponentInChildren<Slider>();

        foreach(GameObject c in GameObject.FindGameObjectsWithTag("ReunionChair")) 
        { 
            c.GetComponent<Chair>().OnSit += AddWorkerToReunion; 
            c.GetComponent<Chair>().OnLeave += DecreaseWorkerInReunion; 
        }
    }

    private void Update()
    {
        _productivity -= _productivityDecreaseFactor * Time.deltaTime;
        _productivity += _activeWorkers * _productivityFactor * Time.deltaTime;
        if (_productivity <= 0 || _productivity >= 1000)
        {
            _productivityDecreaseFactor = _productivityFactor = 0;
            _productivity = 50; //Para que no se llame múltiples veces al GameEnded()
            ForceSimulationSpeed(1);
            GameObject.FindObjectOfType<AudioListener>().enabled = false;
            MainMenuManager.Instance.GameEnded(_productivity >= 1000);
        }
        _productivitySlider.value = _productivity;


        //Lo siguiente sólo está aquí para probar el jefe, HAY QUE ELIMINARLO MÁS ADELANTE
        DecreaseWorkerInReunion();
        AddWorkerToReunion();
    }

    public void SetWorkerActivity(bool active)
    {
        _activeWorkers += active? 1 : -1;
    }

    public void ChangeSimulationSpeed(bool add)
    {
        if ((Time.timeScale == 0.25f && !add) || (Time.timeScale == 16 && add)) return;
        if (add) Time.timeScale *= 2;
        else Time.timeScale /= 2;
        MainMenuManager.Instance.SetSpeedText(Time.timeScale.ToString());
    }

    public void ForceSimulationSpeed(float speed)
    {
        Time.timeScale = speed;
        MainMenuManager.Instance.SetSpeedText(Time.timeScale.ToString());
    }

    public void GenerateTrash(Vector3 position) //Instancia basura dada una posición
    {
        NavMeshHit navHit;
        NavMesh.SamplePosition(position, out navHit, 20, 1);
        GameObject.Instantiate(_trashPrefab, navHit.position, Quaternion.identity);
    }

    #region REUNIONES

    public void ReunionNotified()
    {
        OnNotifyEmployeesStart?.Invoke();
        //Falta aplicar la lógica para que los trabajadores vayan a la reunión
        //Se debería notificar a los trabajadores para que vayan a la sala de reuniones, busquen una silla y se sienten, solamente
    }

    public void ReunionEnded()
    {
        OnNotifyEmployeesEnd?.Invoke();
        //Lógica similar a ReunionNotified
    }

    public void AddWorkerToReunion()
    {
        _numWorkersReunion++;
        AreWorkersReady();
    }

    public void DecreaseWorkerInReunion()
    {
        _numWorkersReunion--;
    }

    public void AreWorkersReady()
    {
        if(_numWorkersReunion == GameObject.FindObjectsOfType<EmployeeBehaviour>().Length)
        {
            OnWorkersReady?.Invoke();
        }
    }
    #endregion
}
