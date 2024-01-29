using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potion_Eel : Potion
{
    private Rigidbody m_rigidbody;
    [SerializeField] private bool isPicked = false;

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
        }
    }

    public override void Interact()
    {
        PlayerInventory player = FindObjectOfType<PlayerInventory>();
        if (!isPicked)
        {
            player.PickUpObject(this.transform);
            isPicked = true;
        }
        else
        {
            isPicked = false;
            transform.parent = null;
            player.DropObject();
        }

    }
}
