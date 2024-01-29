using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    private Transform m_cam;

    private void Awake()
    {
        m_cam = Camera.main.transform;
    }

    private void LateUpdate()
    {
        transform.LookAt(m_cam);
    }
}
