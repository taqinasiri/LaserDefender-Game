using UnityEngine;

public class ScoreKeeper : MonoBehaviour
{
    private int score;

    private static ScoreKeeper instance;

    private void Awake()
    {
        ManageSingleton();
    }

    private void ManageSingleton()
    {
        if(instance is not null)

        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public int GetScore() => score;

    public void AddScore(int value = 1)
    {
        score += value;
        Mathf.Clamp(score,0,int.MaxValue);
    }

    public void ResetScore() => score = 0;
}