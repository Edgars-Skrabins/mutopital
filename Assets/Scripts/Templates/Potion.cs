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
}
