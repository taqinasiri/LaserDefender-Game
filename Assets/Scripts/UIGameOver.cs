using TMPro;
using UnityEngine;

public class UIGameOver : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI ScoreText;
    private ScoreKeeper scoreKeeper;

    private void Awake() => scoreKeeper = FindObjectOfType<ScoreKeeper>();

    private void Start() => ScoreText.text = scoreKeeper.GetScore().ToString("000");
}