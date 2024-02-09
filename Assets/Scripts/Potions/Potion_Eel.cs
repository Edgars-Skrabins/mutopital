using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potion_Eel : Potion
{
    private Rigidbody m_rigidbody;

    private void Awake()
    {
        m_rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (transform.parent != null)
        {
            m_rigidbody.isKinematic = true;

        }
        else
        {
            m_rigidbody.isKinematic = false;
            CheckForDestruction();
        }
    }
}
