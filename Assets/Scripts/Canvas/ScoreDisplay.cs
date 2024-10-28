using UnityEngine;
using TMPro;

public class ScoreDisplay : MonoBehaviour
{
    [SerializeField] private TMP_Text scoreText;

    private void Start()
    {
        UpdateScoreText();
    }

    private void OnEnable()
    {
        GameManager.Instance.OnScoreChanged += UpdateScoreText;
    }

    private void OnDisable()
    {
        GameManager.Instance.OnScoreChanged -= UpdateScoreText;
    }

    private void UpdateScoreText()
    {
        scoreText.text = "Score: 0 " + GameManager.Instance.money.ToString();
    }
}
