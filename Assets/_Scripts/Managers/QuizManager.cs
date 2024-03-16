using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class QuizManager : MonoBehaviour
{
    [SerializeField] private TMP_Text questionTextArea;
    [SerializeField] private TMP_Text timerCaption;
    [SerializeField] private TMP_Text lifeCaption;
    [SerializeField] private Image timerProgressBar;
    
    [SerializeField] private List<Question> questions;
    [SerializeField] private List<GameObject> instantiatedChoiceButtons;

    [SerializeField] private GameObject choiceButtonPrefab;
    [SerializeField] private RectTransform choiceSpawningPoint;
    [SerializeField] private Sprite correctSprite;
    [SerializeField] private Sprite wrongSprite;
    [SerializeField] private Sprite normalSprite;

    [SerializeField] private int duration;
    [SerializeField] private int maxLife;

    private int currentQuestionIndex = 0;
    private int score = 0;
    private int currentLifeCount;
    private float remainingDuration;

    private bool IsTimeRunning = true;

    private void Start()
    {
        PopulateQuestions();
        StartQuiz(duration, currentQuestionIndex);
    }

    private void OnEnable() => ChoiceButton.OnChoiceButtonClicked += AnswerSelected;

    private void OnDisable() => ChoiceButton.OnChoiceButtonClicked -= AnswerSelected;

    private void StartQuiz(int seconds, int questionIndex)
    {
        remainingDuration = seconds;
        currentLifeCount = maxLife;
        DisplayQuestionAndAnswers(questionIndex);
        StartCoroutine(UpdateQuizTimer());
    }

    private void ChoiceButtonInitializer()
    {
        for (int i = 0; i < questions[currentQuestionIndex].answers.Count; i++)
        {
            var instantiatedButton = Instantiate(choiceButtonPrefab, choiceSpawningPoint);
            instantiatedButton.transform.SetParent(choiceSpawningPoint);
            instantiatedChoiceButtons.Add(instantiatedButton);
        }
    }

    private IEnumerator UpdateQuizTimer()
    {
        while (remainingDuration >= 0)
        {
            if (IsTimeRunning)
            {
                DisplayTimer(remainingDuration);
                timerProgressBar.fillAmount = Mathf.InverseLerp(0, duration, remainingDuration);
                remainingDuration--;
                yield return new WaitForSeconds(1f);
            }
        }
        //yield return new WaitForSeconds(5f);
        RestartTimer(duration);
        NextQuestion();
    }

    private void RestartTimer(int time)
    {
        remainingDuration = time;
        DisplayTimer(remainingDuration);
        timerProgressBar.fillAmount = 1f;
    }

    private void DisplayTimer(float timeRemaining)
    {
        timerCaption.text = ((int)timeRemaining).ToString();
        TimerProgressBarColor(timerProgressBar, timerProgressBar.fillAmount);
    }

    // Define questions and answers programmatically
    private void PopulateQuestions()
    {
        questions = new List<Question>();

        Question _question1 = new Question();
        _question1.questionText = "What is the capital of France?";
        _question1.answers = new List<Answer>
        {
            new Answer() { answerText = "Paris" },
            new Answer() { answerText = "London" },
            new Answer() { answerText = "Berlin" },
            new Answer() { answerText = "Madrid" }
        };
        _question1.correctAnswerIndex = 0;
        _question1.correctAnswerText = "Paris";
        questions.Add(_question1);

        Question _question2 = new Question();
        _question2.questionText = "What color is the letter G in Google's multicolored logo?";
        _question2.answers = new List<Answer>
        {
            new Answer() { answerText = "Yellow" },
            new Answer() { answerText = "Blue" },
            new Answer() { answerText = "Green" },
            new Answer() { answerText = "Red" }
        };
        _question2.correctAnswerIndex = 1;
        _question2.correctAnswerText = "Blue";
        questions.Add(_question2);

        Question _question3 = new Question();
        _question3.questionText = "Which continent is Kenya located on?";
        _question3.answers = new List<Answer>
        {
            new Answer() { answerText = "Asia" },
            new Answer() { answerText = "Europe" },
            new Answer() { answerText = "North America" },
            new Answer() { answerText = "Africa" }
        };
        _question3.correctAnswerIndex = 3;
        _question3.correctAnswerText = "Africa";
        questions.Add(_question3);
    }

    public void AnswerSelected(int answerIndex)
    {
        if (answerIndex == questions[currentQuestionIndex].correctAnswerIndex)
        {
            score++;
        }
        Debug.Log("The score is: "+ score);
    }

    public void AnswerSelected(string answerText, Button selectedButton)
    {
        var correctAnswer = questions[currentQuestionIndex].correctAnswerText;
        var selectedAnswerText = selectedButton.GetComponentInChildren<TMP_Text>();
        bool IsCorrect = answerText.Equals(correctAnswer);
        selectedButton.image.sprite = IsCorrect ? correctSprite : wrongSprite;

        //if (IsCorrect) score++;

        if (!IsCorrect)
        {
            currentLifeCount--;
            lifeCaption.text = currentLifeCount.ToString();
        }

        Debug.Log("The score is: " + score);
        EnableChoiceButtons(false);
        ChangeChoiceButtonSprite(correctAnswer, selectedAnswerText.text);
    }

    public void EnableChoiceButtons(bool enabled)
    {
        foreach (var buttonOBJ in instantiatedChoiceButtons)
        {
            var button = buttonOBJ.GetComponent<Button>();
            button.enabled = enabled;
        }
    }

    public void ChangeChoiceButtonSprite(string correctAnswer, string selectedAnswer)
    {
        foreach (var buttonOBJ in instantiatedChoiceButtons)
        {
            var button = buttonOBJ.GetComponent<Button>();
            var buttonLabel = button.GetComponentInChildren<TMP_Text>();
            if (buttonLabel.text != correctAnswer && buttonLabel.text != selectedAnswer)
            {
                button.image.sprite = normalSprite;
            }
            if (buttonLabel.text == correctAnswer)
            {
                button.image.sprite = correctSprite;
            }
        }
    }

    public void RestartChoiceButtonSprite()
    {
        foreach (var buttonOBJ in instantiatedChoiceButtons)
        {
            var button = buttonOBJ.GetComponent<Button>();
            button.image.sprite = normalSprite;            
        }
    }

    private void DisplayQuestionAndAnswers(int questionIndex)
    {
        questionTextArea.text = questions[questionIndex].questionText;
        for (int i = 0; i < questions[questionIndex].answers.Count; i++)
        {
            var choiceButtonLabel = instantiatedChoiceButtons[i].GetComponentInChildren<TMP_Text>();
            choiceButtonLabel.text = questions[questionIndex].answers[i].answerText;
        }
    }

    private void NextQuestion()
    {
        currentQuestionIndex++;
        if (currentQuestionIndex < questions.Count)
        {
            DisplayQuestionAndAnswers(currentQuestionIndex);
            StartCoroutine(UpdateQuizTimer());
            EnableChoiceButtons(true);
            RestartChoiceButtonSprite();
        }
        else
        {
            EndQuiz();
        }
        Debug.Log("Current Question Index: "+currentQuestionIndex);
    }

    private void EndQuiz()
    {
        // Display final score and end of quiz message
        Debug.Log("End of the Quiz");
    }

    private void OnTimerStarted(float moveTime)
    {
        bool outOfTimeSoundPlayed = false;
        bool playerTurnSoundPlayed = false;
        //TimerProgressBarColor(timerProgressBar, timerProgressBar.fillAmount);

            //if (_tablesController.GetCurrentUserGUID() == _player.PlayerData.GUID)
            //{
            //    if (!playerTurnSoundPlayed)
            //    {
            //        PlaySound("PlayerTurn");
            //        playerTurnSoundPlayed = true;
            //    }

            //    if (ActivePlayerImage.fillAmount >= 0.75f
            //        && !outOfTimeSoundPlayed)
            //    {
            //        PlaySound("PlayerOutOfTime");
            //        outOfTimeSoundPlayed = true;
            //    }
            //}
    }

    private void TimerProgressBarColor(Image progressBar, float fillProgress)
    {
        Color colorA = ColorUtility.TryParseHtmlString("#45BE26", out Color parsedColorA) ? parsedColorA : Color.white;
        Color colorB = ColorUtility.TryParseHtmlString("#B9AB2F", out Color parsedColorB) ? parsedColorB : Color.white;
        Color colorC = ColorUtility.TryParseHtmlString("#FFA500", out Color parsedColorC) ? parsedColorC : Color.white;
        Color colorD = ColorUtility.TryParseHtmlString("#FF0000", out Color parsedColorD) ? parsedColorD : Color.white;

        if (fillProgress <= 0.25f)
            progressBar.color = colorD;
        else if (fillProgress <= 0.5f)
            progressBar.color = Color.Lerp(colorD, colorC, (fillProgress - 0.25f) / 0.25f);
        else if (fillProgress <= 0.75f)
            progressBar.color = Color.Lerp(colorC, colorB, (fillProgress - 0.5f) / 0.25f);
        else
            progressBar.color = Color.Lerp(colorB, colorA, (fillProgress - 0.75f) / 0.25f);
    }
}
