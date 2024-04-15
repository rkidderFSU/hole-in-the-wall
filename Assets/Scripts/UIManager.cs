using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    private CartManager m;
    private Coroutine feedbackCoroutine;
    [SerializeField] private TextMeshProUGUI requestText;
    [SerializeField] private TextMeshProUGUI feedbackTextCorrect;
    [SerializeField] private TextMeshProUGUI feedbackTextIncorrect;
    [SerializeField] private TextMeshProUGUI feedbackTextGeneral;
    [SerializeField] private TextMeshProUGUI scoreText;


    private void Start()
    {
        m = GameObject.Find("Game Manager").GetComponent<CartManager>();
    }

    private void Update()
    {
        UpdateScoreText();
    }

    public void UpdateRequestText(string text)
    {
        requestText.text = text;
    }

    public void SetCorrectText(string text)
    {
        feedbackTextCorrect.text = text;
    }

    public void SetIncorrectText(string text)
    {
        feedbackTextIncorrect.text = text;
    }

    public void SetFeedbackText(string text)
    {
        // Stop the current coroutine if it's running
        if (feedbackCoroutine != null)
        {
            StopCoroutine(feedbackCoroutine);
        }
        // Start a new coroutine to display the feedback immediately
        feedbackCoroutine = StartCoroutine(DisplayFeedback(text));
    }

    public IEnumerator DisplayFeedback(string text)
    {
        feedbackTextGeneral.text = text;
        yield return new WaitForSeconds(2);
        feedbackTextGeneral.text = "";
    }

    public void UpdateScoreText()
    {
        scoreText.text = "Score: " + m.score;
    }
}
