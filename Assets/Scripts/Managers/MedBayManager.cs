using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MedBayManager : MonoBehaviour
{
    private List<MedBayController> m_medBaysAll;
    [SerializeField] private Transform m_exitPointTF, m_waitPointTF;

    private void Start()
    {
        InitializeMedBays();
    }

    public Vector3 GetWaitPoint()
    {
        return m_waitPointTF.position;
    }

    public Vector3 GetExitPoint()
    {
        return m_exitPointTF.position;
    }

    public MedBayController GetUnoccupiedMedBay()
    {
        if(m_medBaysAll.Capacity > 0)
        {
            foreach (MedBayController _medBay in m_medBaysAll)
            {
                if(!_medBay.IsMedbayOccupied())
                {
                    return _medBay;
                }
            }
        }
        return null;
    }

    public Vector3 GetNearestOccupiedMedBayEdge(Vector3 _patientPos)
    {
        if (m_medBaysAll.Capacity > 0)
        {
            Transform nearestMedBay = null;
            float closestDistance= 100f;

            foreach (MedBayController _medBay in m_medBaysAll)
            {
                if (_medBay.IsMedbayOccupied())
                {
                    float medBayDistance = Vector3.Distance(_patientPos, _medBay.transform.position);

                    if (medBayDistance < closestDistance)
                    {
                        nearestMedBay = _medBay.transform;
                        closestDistance = medBayDistance;
                    }
                    return nearestMedBay.position;
                }
            }
        }
        return Vector3.zero;
    }

    private void InitializeMedBays()
    {
        m_medBaysAll = new List<MedBayController>();
        m_medBaysAll.AddRange(FindObjectsOfType<MedBayController>());
    }
}
