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
    //Manejo de la simulaci�n: 
    [SerializeField] private float _productivityFactor = 0.2f; //La productividad aumenta cada segundo * n�mero de trabajadores trabajando
    [SerializeField] private float _productivityDecreaseFactor = 1; //La productividad baja cada segundo
    private Slider _productivitySlider;
    private float _activeWorkers = 0;
    private float _productivity = 500; //0 -> bancarrota; 1000 -> �xito total, se van de vacaciones
    //Manejo de la basura:
    [SerializeField] private GameObject _trashPrefab;
    //Manejo del ba�o:
    private List<Chair> _baths = new List<Chair>();
    //Reuniones:
    private int _numWorkersReunion = 0;
    public event Action OnWorkersReady;

    void Start()
    {
        Instance = this;

        _productivitySlider = MainMenuManager.Instance.transform.GetComponentInChildren<Slider>();

        foreach(GameObject bathroom in GameObject.FindGameObjectsWithTag("Bath"))
        {
            _baths.Add(bathroom.GetComponent<Chair>());
        }

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
            _productivity = 50; //Para que no se llame m�ltiples veces al GameEnded()
            ForceSimulationSpeed(1);
            GameObject.FindObjectOfType<AudioListener>().enabled = false;
            MainMenuManager.Instance.GameEnded(_productivity >= 1000);
        }
        _productivitySlider.value = _productivity;
        //Time.timeScale = _timeSpeed;
        //Lo siguiente s�lo est� aqu� para probar el jefe; hay que eliminarlo m�s adelante
        DecreaseWorkerInReunion();
        AddWorkerToReunion();
    }

    public void SetWorkerActivity(bool active)
    {
        _activeWorkers += active? 1 : -1;
    }

    public void ChangeSimulationSpeed(bool add)
    {
        if ((Time.timeScale == 0.25f && !add) || (Time.timeScale == 64 && add)) return;
        if (add) Time.timeScale *= 2;
        else Time.timeScale /= 2;
        MainMenuManager.Instance.SetSpeedText(Time.timeScale.ToString());
    }

    public void ForceSimulationSpeed(float speed)
    {
        Time.timeScale = speed;
        MainMenuManager.Instance.SetSpeedText(Time.timeScale.ToString());
    }

    public void GenerateTrash(Vector3 position) //Instancia basura dada una posici�n
    {
        NavMeshHit navHit;
        NavMesh.SamplePosition(position, out navHit, 20, 1);
        GameObject.Instantiate(_trashPrefab, navHit.position, Quaternion.identity);
    }

    public Chair GetBathroom(IAgent agent) //Devuelve un ba�o libre que ni est� siendo usado ni ha sido seleccionado por un personaje para usarse
    {
        foreach(Chair bath in _baths)
        {
            if (!bath.IsOccupied() && !bath.selected) 
            {
                agent.SetCurrentChair(bath);
                bath.selected = true;
                return bath;
            }
        }
        return null;
    }

    #region REUNIONES

    public void ReunionNotified()
    {
        //Falta aplicar la l�gica para que los trabajadores vayan a la reuni�n
        //Se deber�a notificar a los trabajadores para que vayan a la sala de reuniones, busquen una silla y se sienten, solamente
    }

    public void ReunionEnded()
    {
        //L�gica similar a ReunionNotified
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
        if(_numWorkersReunion == GameObject.FindObjectsOfType<ProgrammerBehaviour>().Length)
        {
            OnWorkersReady?.Invoke();
        }
    }
    #endregion
}
