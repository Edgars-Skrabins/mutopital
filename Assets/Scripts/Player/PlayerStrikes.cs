using UnityEngine;

public class PlayerStrikes : MonoBehaviour
{
    private int m_maxPlayerStrikesInclusive;

    private int m_playerStrikes;

    public void StrikePlayer(int _strikeDamageAmount)
    {
        m_playerStrikes -= _strikeDamageAmount;
        if (m_playerStrikes >= m_maxPlayerStrikesInclusive)
        {
            KillPlayer();
        }
    }

    private void KillPlayer()
    {
        GameManager.I.LoseGame();
    }
}
