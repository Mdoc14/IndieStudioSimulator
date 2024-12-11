using CharactersBehaviour;
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
    [SerializeField] private GameObject trashPrefab;
    //Manejo del ba�o:
    private List<Chair> _baths = new List<Chair>();
    //Reuniones:
    private int _numWorkersReunion = 0;

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
        Time.timeScale = _timeSpeed;
    }

    public void GenerateTrash(Vector3 position) //Instancia basura dada una posici�n
    {
        NavMeshHit navHit;
        NavMesh.SamplePosition(position, out navHit, 20, 1);
        GameObject.Instantiate(trashPrefab, position, Quaternion.identity);
    }

    public Chair GetBathroom(IAgent agent) //Devuelve un ba�o libre que ni est� siendo usado ni ha sido seleccionado por un personaje para usarse
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

    public void StartReunion()
    {
        //Falta aplicar la l�gica para que los trabajadores vayan a la reuni�n
        //Se deber�a notificar a los trabajadores para que vayan a la sala de reuniones, busquen una silla y se sienten, solamente
    }

    public void EndReunion()
    {
        //L�gica similar a StartReunion
    }

    public void AddWorkerToReunion()
    {
        _numWorkersReunion++;
    }

    public void DecreaseWorkerInReunion()
    {
        _numWorkersReunion--;
    }

    public bool AreWorkersReady()
    {
        if(_numWorkersReunion == GameObject.FindObjectsOfType<ProgrammerBehaviour>().Length)
        {
            return true;
        }
        return false;
    }
}
