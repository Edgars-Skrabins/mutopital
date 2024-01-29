using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MedBayManager : MonoBehaviour
{
    [SerializeField] private List<MedBayController> m_medBaysAll;
    [SerializeField] private Transform m_exitPointTF, m_waitPointTF;

    private void Start()
    {
        InitializeMedBays();
    }
    public List<MedBayController> GetAllMedBays()
    {
        return m_medBaysAll;
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
                    _medBay.m_IsMedbayOccupied = true;
                    return _medBay;
                }
            }
        }
        return null;
    }

    public Vector3 GetFarthestOccupiedMedBayEdge(Vector3 _patientPos)
    {
        if (m_medBaysAll.Capacity > 0)
        {
            Transform nearestMedBay = null;
            float farthestDistance= 0f;

            foreach (MedBayController _medBay in m_medBaysAll)
            {
                if (_medBay.IsMedbayOccupied())
                {
                    float medBayDistance = Vector3.Distance(_patientPos, _medBay.transform.position);

                    if (medBayDistance > farthestDistance)
                    {
                        nearestMedBay = _medBay.transform;
                        farthestDistance = medBayDistance;
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

        AddMedBayManagerToControllers();
    }

    public void AddMedBayManagerToControllers()
    {
        foreach (MedBayController _medBayController in m_medBaysAll)
        {
            _medBayController.SetMedBayManager(this);
        }
    }

    public bool IsMedBayAvailable()
    {
        int patientsInTransition = 0;

        foreach (PatientController _patient in FindObjectsOfType<PatientController>())
        {
            if (_patient.m_inTrasitionToMedBay)
                patientsInTransition += 1;
        }

        if (m_medBaysAll.Count == patientsInTransition)
            return false;
        else
            return true;
    }
}
