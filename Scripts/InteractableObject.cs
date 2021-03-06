using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    public bool beingLaunched = false;
    [SerializeField] GameObject roomChecker;
    public void InvokeRePickup() { Invoke("RePickup", 1f); }
    void RePickup() { beingLaunched = false; }
}
