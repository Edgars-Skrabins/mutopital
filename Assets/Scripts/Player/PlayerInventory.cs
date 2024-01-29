using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    [SerializeField] private Transform m_handTF;
    [SerializeField] private Transform m_objectsInHand;


    private void LateUpdate()
    {
        if(GameManager.I.IsGamePaused())
        {
            return;
        }
        if(m_objectsInHand)
            m_objectsInHand.position = m_handTF.position;
    }

    public void PickUpObject(Transform _object)
    {
        if (m_objectsInHand == null)
        {
            m_objectsInHand = _object;
            m_objectsInHand.transform.SetParent(m_handTF);
            m_objectsInHand.position = m_handTF.position;
        }
        else
        {
            DropObject();
            m_objectsInHand = _object;
            m_objectsInHand.SetParent(m_handTF);
            m_objectsInHand.position = m_handTF.position;
        }
    }

    public void DropObject()
    {
        if (m_objectsInHand)
        {
            m_objectsInHand.DetachChildren();
            m_objectsInHand.GetComponent<Rigidbody>().AddForce(transform.forward * 100, ForceMode.Impulse);
            m_objectsInHand = null;
        }
    }
}
