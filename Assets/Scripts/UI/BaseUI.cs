using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseUI : MonoBehaviour
{
    [SerializeField] protected GameObject associatedGameObj;

    public virtual void UpdateUI()
    {
        //Debug.Log("BaseUI Base Class [UPDATE UI] Called");
    }

    public virtual void TurnOffObject()
    {
        //Debug.Log("BaseUI Base Class [TURN OFF OBJECT] Called");
    }

    public virtual void TurnOnObject()
    {
        //Debug.Log("BaseUI Base Class [TURN ON OBJECT] Called");
    }
}
