using UnityEngine;

public class Player : Singleton<Player>
{
    [SerializeField] private PlayerStrikes m_playerStrikesCS;

    protected override void Awake()
    {
        Initialize();
    }

    private void Initialize()
    {
        m_playerStrikesCS = GetComponent<PlayerStrikes>();
    }

    public PlayerStrikes GetPlayerStrikesCS()
    {
        return m_playerStrikesCS;
    }
}
