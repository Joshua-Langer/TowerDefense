using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitDestructionDelegate : MonoBehaviour
{
    public delegate void UnitDelegate(GameObject unit);
    public UnitDelegate unitDelegate;

    void OnDestroy()
    {
        if(unitDelegate != null)
        {
            unitDelegate(gameObject);
        }
    }
}
