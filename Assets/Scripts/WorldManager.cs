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
    public int ActiveWorkers { get; private set; } = 0;
    private float _productivity = 500; //0 -> bancarrota; 1000 -> éxito total, se van de vacaciones
    //Manejo de la basura:
    [SerializeField] private GameObject _trashPrefab;
    [SerializeField] private GameObject _catBallPrefab;
    //Reuniones:
    private int _numWorkersReunion = 0;
    public bool reunionNotified = false;
    public event Action OnWorkersReady;
    public event Action OnNotifyEmployeesStart;
    public event Action OnNotifyEmployeesEnd;

    void Awake()
    {
        if (Instance == this) Destroy(this.gameObject);
        DontDestroyOnLoad(this.gameObject);
        Instance = this;

        _productivitySlider = GameObject.Find("@MenuManager").transform.GetComponentInChildren<Slider>();

        foreach(GameObject c in GameObject.FindGameObjectsWithTag("ReunionChair")) 
        { 
            c.GetComponent<Chair>().OnSit += AddWorkerToReunion; 
            c.GetComponent<Chair>().OnLeave += DecreaseWorkerInReunion; 
        }
    }

    private void Update()
    {
        _productivity -= _productivityDecreaseFactor * Time.deltaTime;
        _productivity += ActiveWorkers * _productivityFactor * Time.deltaTime;
        if (_productivity <= 0 || _productivity >= 1000)
        {
            _productivityDecreaseFactor = _productivityFactor = 0;
            _productivity = 500; //Para que no se llame múltiples veces al GameEnded()
            ForceSimulationSpeed(1);
            GameObject.FindObjectOfType<AudioListener>().enabled = false;
            MainMenuManager.Instance.GameEnded(!(_productivity < 900));
        }
        _productivitySlider.value = _productivity;
    }

    public void SetWorkerActivity(bool active)
    {
        ActiveWorkers += active? 1 : -1;
        Debug.Log("TRABAJADORES ACTIVOS: " + ActiveWorkers);
    }

    public void SetActiveWorkers(int num)
    {
        ActiveWorkers = num;
    }

    public void ChangeSimulationSpeed(bool add)
    {
        if ((Time.timeScale == 0.25f && !add) || (Time.timeScale == 8 && add)) return;
        if (add) Time.timeScale *= 2;
        else Time.timeScale /= 2;
        MainMenuManager.Instance.SetSpeedText(Time.timeScale.ToString());
    }

    public void ForceSimulationSpeed(float speed)
    {
        Time.timeScale = speed;
        MainMenuManager.Instance.SetSpeedText(Time.timeScale.ToString());
    }

    public void GenerateTrash(Vector3 position, bool isCatPurging = false) //Instancia basura dada una posición
    {
        NavMeshHit navHit;
        NavMesh.SamplePosition(position, out navHit, 20, 1);
        if (isCatPurging) GameObject.Instantiate(_catBallPrefab, navHit.position, Quaternion.identity);
        else GameObject.Instantiate(_trashPrefab, navHit.position, Quaternion.identity);
    }

    #region REUNIONES

    public void ReunionNotified()
    {
        OnNotifyEmployeesStart?.Invoke();
        reunionNotified = true;
        //Falta aplicar la lógica para que los trabajadores vayan a la reunión
        //Se debería notificar a los trabajadores para que vayan a la sala de reuniones, busquen una silla y se sienten, solamente
    }

    public void ReunionEnded()
    {
        OnNotifyEmployeesEnd?.Invoke();
        reunionNotified = false;
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
