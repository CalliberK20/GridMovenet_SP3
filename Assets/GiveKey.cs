using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiveKey : MonoBehaviour
{
    public GateManager parentGateManager;
    
    public void RemoveFromChild()
    {
        parentGateManager.RemoveFromChildAndDesipate(gameObject);
    }
}
