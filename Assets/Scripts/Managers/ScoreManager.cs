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
        if (PlayerPrefs.HasKey("HighScore"))
        {
            m_highScore = PlayerPrefs.GetInt("HighScore");
            return;
        }

        m_highScore = 0;
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
