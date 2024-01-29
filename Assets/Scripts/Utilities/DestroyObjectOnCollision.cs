using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObjectOnCollision : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent<PatientController>(out PatientController _pc))
        { Destroy(other.gameObject); }
    }
}
