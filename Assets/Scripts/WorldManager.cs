using CharactersBehaviour;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

public class WorldManager : MonoBehaviour
{
    public static WorldManager Instance { get; private set; }
    [SerializeField] private float _timeSpeed = 1;
    //Manejo de la basura:
    [SerializeField] private GameObject _trashPrefab;
    //Manejo del baño:
    private List<Chair> _baths = new List<Chair>();
    //Reuniones:
    private int _numWorkersReunion = 0;
    public event Action OnWorkersReady;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(Instance);
        DontDestroyOnLoad(Instance);

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
        //Time.timeScale = _timeSpeed;
        //Lo siguiente sólo está aquí para probar el jefe; hay que eliminarlo más adelante
        DecreaseWorkerInReunion();
        AddWorkerToReunion();
    }

    public void AddSpeed(float speed)
    {
        if (Time.timeScale + speed <= 0) return;
        Time.timeScale += speed;
        MainMenuManager.Instance.SetSpeedText(Time.timeScale.ToString());
    }

    public void GenerateTrash(Vector3 position) //Instancia basura dada una posición
    {
        NavMeshHit navHit;
        NavMesh.SamplePosition(position, out navHit, 20, 1);
        GameObject.Instantiate(_trashPrefab, navHit.position, Quaternion.identity);
    }

    public Chair GetBathroom(IAgent agent) //Devuelve un baño libre que ni está siendo usado ni ha sido seleccionado por un personaje para usarse
    {
        foreach(Chair bath in _baths)
        {
            if (!bath.IsOccupied() && !bath.selected) 
            {
                agent.SetBath(bath);
                bath.selected = true;
                return bath;
            }
        }
        return null;
    }

    public void ReunionNotified()
    {
        //Falta aplicar la lógica para que los trabajadores vayan a la reunión
        //Se debería notificar a los trabajadores para que vayan a la sala de reuniones, busquen una silla y se sienten, solamente
    }

    public void ReunionEnded()
    {
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
        if(_numWorkersReunion == GameObject.FindObjectsOfType<ProgrammerBehaviour>().Length)
        {
            OnWorkersReady?.Invoke();
        }
    }
}
