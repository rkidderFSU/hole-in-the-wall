using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    private CartManager m;
    private Coroutine feedbackCoroutine;
    private Coroutine correctCoroutine;
    private Coroutine incorrectCoroutine;
    private Coroutine unsoldCoroutine;
    [SerializeField] private TextMeshProUGUI requestText;
    [SerializeField] private TextMeshProUGUI feedbackTextCorrect;
    [SerializeField] private TextMeshProUGUI feedbackTextIncorrect;
    [SerializeField] private TextMeshProUGUI feedbackTextGeneral;
    [SerializeField] private TextMeshProUGUI feedbackTextUnsold;
    [SerializeField] private TextMeshProUGUI scoreText;


    private void Start()
    {
        m = GameObject.Find("Game Manager").GetComponent<CartManager>();
    }

    private void Update()
    {
        UpdateScoreText();
    }

    public IEnumerator UpdateRequestText(string text)
    {
        requestText.text = text;
        yield return new WaitForSeconds(5);
        requestText.text = "";
    }

    // Code for displaying correct items sold
    public void SetCorrectText(string text)
    {
        // Stop the current coroutine if it's running
        if (correctCoroutine != null)
        {
            StopCoroutine(correctCoroutine);
        }
        // Start a new coroutine to display the feedback immediately
        correctCoroutine = StartCoroutine(DisplayCorrect(text));
    }
    public IEnumerator DisplayCorrect(string text)
    {
        feedbackTextCorrect.text = text;
        yield return new WaitForSeconds(5);
        feedbackTextCorrect.text = "";
    }

    // Code for displaying incorrect items sold
    public void SetIncorrectText(string text)
    {
        // Stop the current coroutine if it's running
        if (incorrectCoroutine != null)
        {
            StopCoroutine(incorrectCoroutine);
        }
        // Start a new coroutine to display the feedback immediately
        incorrectCoroutine = StartCoroutine(DisplayIncorrect(text));
    }
    public IEnumerator DisplayIncorrect(string text)
    {
        feedbackTextIncorrect.text = text;
        yield return new WaitForSeconds(5);
        feedbackTextIncorrect.text = "";
    }

    // Code for displaying items added to/removed from cart and items sold
    public void SetFeedbackText(string text)
    {
        if (feedbackCoroutine != null)
        {
            StopCoroutine(feedbackCoroutine);
        }
        feedbackCoroutine = StartCoroutine(DisplayFeedback(text));
    }
    public IEnumerator DisplayFeedback(string text)
    {
        feedbackTextGeneral.text = text;
        yield return new WaitForSeconds(2);
        feedbackTextGeneral.text = "";
    }

    public void SetUnsoldText(string text)
    {
        if (unsoldCoroutine != null)
        {
            StopCoroutine(unsoldCoroutine);
        }
        unsoldCoroutine = StartCoroutine(DisplayUnsold(text));
    }
    public IEnumerator DisplayUnsold(string text)
    {
        feedbackTextUnsold.text = text;
        yield return new WaitForSeconds(5);
        feedbackTextUnsold.text = "";
    }

    public void UpdateScoreText()
    {
        scoreText.text = "Score: " + m.score;
    }
}
