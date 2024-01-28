using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateComponentOnEnable : MonoBehaviour
{
    [SerializeField] private Outline m_outLine;

    private void OnEnable()
    {
        if(m_outLine)
            m_outLine.enabled = true;
    }

    private void OnDisable()
    {
        if(m_outLine)
            m_outLine.enabled = false;
    }
}
