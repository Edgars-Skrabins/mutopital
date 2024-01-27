using UnityEngine;

public abstract class PatientHealth : MonoBehaviour
{
    protected virtual void Die()
    {
        Destroy(gameObject);
    }
}
