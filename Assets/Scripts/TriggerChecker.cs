using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerChecker : MonoBehaviour
{
    private bool triggerActive;

    public bool TriggerActive { get => triggerActive; set => triggerActive = value; }

    private void OnTriggerEnter(Collider other)
    {
        TriggerActive = true;
    }
    private void OnTriggerExit(Collider other)
    {
        TriggerActive = false;
    }
}
