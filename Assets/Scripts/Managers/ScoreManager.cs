using UnityEngine;

public class ScoreManager : Singleton<ScoreManager>
{
    private int m_highScore;
    private int m_score;

    protected override void Awake()
    {
        base.Awake();
        Initialize();
    }

    private void Initialize()
    {
        InitializeHighScore();
    }

    private void InitializeHighScore()
    {
        if(PlayerPrefs.HasKey("HighScore"))
        {
            m_highScore = PlayerPrefs.GetInt("HighScore");
            return;
        }

        m_highScore = 0;
        PlayerPrefs.SetInt("HighScore", m_highScore);
    }

    public void ClearScore()
    {
        m_score = 0;
        EventManager.I.OnScoreUpdate_Invoke(m_score);
    }

    public void IncreaseScore(int _incrementAmount)
    {
        m_score += _incrementAmount;
        if(m_score > m_highScore)
        {
            UpdateHighScore(m_score);
        }

        EventManager.I.OnScoreUpdate_Invoke(m_score);
    }

    private void UpdateHighScore(int _newHighScore)
    {
        m_highScore = _newHighScore;
        PlayerPrefs.SetInt("HighScore", m_highScore);
    }

    public int GetScore()
    {
        return m_score;
    }

    public int GetHighScore()
    {
        return m_highScore;
    }
}
