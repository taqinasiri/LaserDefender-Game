using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private float sceneLoadDelay = 1;
    private ScoreKeeper scoreKeeper;

    private void Awake()
    {
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
    }

    public void LoadGame()
    {
        scoreKeeper.ResetScore();
        SceneManager.LoadScene("Game");
    }

    public void LoadMainMenu() => SceneManager.LoadScene("MainMenu");

    public void LoadGameOver() => StartCoroutine(WaitAndLoad("GameOver",sceneLoadDelay));

    public void QuitGame() => Application.Quit();

    private IEnumerator WaitAndLoad(string sceneName,float dealy)
    {
        yield return new WaitForSeconds(dealy);
        SceneManager.LoadScene(sceneName);
    }
}