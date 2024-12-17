using CharactersBehaviour;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BathroomWaitingLine : MonoBehaviour
{
    [SerializeField] private Chair[] _baths;
    public bool maleBathroom = true;

    public Chair GetBathroom(IAgent agent) //Devuelve un baño libre que ni está siendo usado ni ha sido seleccionado por un personaje para usarse
    {
        foreach (Chair bath in _baths)
        {
            if (!bath.IsOccupied() && !bath.selected)
            {
                agent.SetCurrentBath(bath);
                bath.selected = true;
                return bath;
            }
        }
        return null;
    }
}
