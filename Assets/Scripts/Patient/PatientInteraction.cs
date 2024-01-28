using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatientInteraction : Interactable
{
    public override void Interact()
    {
        GetComponent<PatientStats>().Heal();
    }
}
