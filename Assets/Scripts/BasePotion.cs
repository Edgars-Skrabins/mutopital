using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasePotion : Interactable
{
    public override void Interact()
    {
        transform.SetParent(FindObjectOfType<PlayerInteract>().transform);
    }
}
