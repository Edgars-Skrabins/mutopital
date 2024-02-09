using UnityEngine;

public abstract class Potion : Interactable
{
    [SerializeField] private string m_potionName;

    public string GetPotionName()
    {
        return m_potionName;
    }
    public void SetPotionName(string _value)
    {
        m_potionName = _value;
    }

    private bool m_isPicked;

    public bool IsPicked()
    {
        return m_isPicked;
    }

    public void SetIsPicked(bool _value)
    {
        m_isPicked = _value;
    }

    public override void Interact()
    {
        PlayerInventory player = FindObjectOfType<PlayerInventory>();

        if (!m_isPicked)
        {
            player.PickUpObject(this.transform);
        }
        else
        {
            m_isPicked = false;
            transform.parent = null;
            player.DropObject();
        }
    }

    [SerializeField] private float m_destroyTime = 5f;
    private float waitTime = 0f;

    public void CheckForDestruction()
    {
        waitTime += Time.deltaTime;

        if(waitTime > m_destroyTime)
        {
            Destroy(gameObject);
        }
    }
}