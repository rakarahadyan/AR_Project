using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;

public class QuizManager : MonoBehaviour
{
    [Header("UI References - Assign Manual")]
    public TextMeshProUGUI questionText;
    public TextMeshProUGUI scoreText;
    public Button buttonA;
    public Button buttonB;
    public Button buttonC;
    public Button buttonD;
    public Canvas quizCanvas;

    [Header("Filter Prefabs")]
    public GameObject correctFilterPrefab;
    public GameObject wrongFilterPrefab;

    [Header("Quiz Settings")]
    public float nextQuestionDelay = 2.5f;
    public int pointsPerCorrectAnswer = 10;

    // Game State
    private int score = 0;
    private int currentQuestionIndex = 0;
    private GameObject currentFilter;
    private bool isAnswering = false;
    private Transform faceTransform;

    private Question[] questions = new Question[]
    {
        new Question("Berapa hasil 5 + 3?", new string[]{"6", "7", "8", "9"}, 2),
        new Question("Ibu kota Indonesia?", new string[]{"Bandung", "Jakarta", "Surabaya", "Medan"}, 1),
        new Question("Berapa hasil 10 - 4?", new string[]{"5", "6", "7", "8"}, 1),
        new Question("Planet terdekat dengan matahari?", new string[]{"Venus", "Merkurius", "Mars", "Bumi"}, 1),
        new Question("Berapa sisi pada segitiga?", new string[]{"2", "3", "4", "5"}, 1),
        new Question("Hasil dari 12 x 2?", new string[]{"20", "22", "24", "26"}, 2),
        new Question("Warna daun pada umumnya?", new string[]{"Merah", "Hijau", "Biru", "Kuning"}, 1),
        new Question("Berapa jumlah benua di dunia?", new string[]{"5", "6", "7", "8"}, 2),
        new Question("Hewan yang bisa terbang?", new string[]{"Kucing", "Burung", "Ikan", "Kuda"}, 1),
        new Question("1 + 1 = ?", new string[]{"0", "1", "2", "3"}, 2)
    };

    void Start()
    {
        if (ValidateReferences())
        {

            faceTransform = quizCanvas.transform.parent;

            if (quizCanvas != null)
                quizCanvas.gameObject.SetActive(false);

        }
    }

    void Update()
    {
        if (quizCanvas != null && !quizCanvas.gameObject.activeSelf)
        {
            if (faceTransform != null && faceTransform.gameObject.activeInHierarchy)
            {
                quizCanvas.gameObject.SetActive(true);
                LoadQuestion();
                UpdateScoreUI();
            }
        }
    }

    // PUBLIC method yang dipanggil dari ButtonHandler
    public void HandleButtonClick(int answerIndex)
    {
        CheckAnswer(answerIndex);
    }

    bool ValidateReferences()
    {
        bool valid = true;
        if (questionText == null) { Debug.LogError("QuestionText not assigned!"); valid = false; }
        if (scoreText == null) { Debug.LogError("ScoreText not assigned!"); valid = false; }
        if (buttonA == null) { Debug.LogError("ButtonA not assigned!"); valid = false; }
        if (buttonB == null) { Debug.LogError("ButtonB not assigned!"); valid = false; }
        if (buttonC == null) { Debug.LogError("ButtonC not assigned!"); valid = false; }
        if (buttonD == null) { Debug.LogError("ButtonD not assigned!"); valid = false; }
        if (quizCanvas == null) { Debug.LogError("QuizCanvas not assigned!"); valid = false; }
        return valid;
    }

    void LoadQuestion()
    {
        if (currentQuestionIndex >= questions.Length)
        {
            ShowQuizComplete();
            return;
        }

        Question q = questions[currentQuestionIndex];

        questionText.text = q.questionText;
        buttonA.GetComponentInChildren<TextMeshProUGUI>().text = "A. " + q.answers[0];
        buttonB.GetComponentInChildren<TextMeshProUGUI>().text = "B. " + q.answers[1];
        buttonC.GetComponentInChildren<TextMeshProUGUI>().text = "C. " + q.answers[2];
        buttonD.GetComponentInChildren<TextMeshProUGUI>().text = "D. " + q.answers[3];

        isAnswering = false;
        EnableButtons();

    }

    void CheckAnswer(int selectedAnswer)
    {

        if (isAnswering)
        {
            return;
        }

        isAnswering = true;
        DisableButtons();

        Question q = questions[currentQuestionIndex];

        if (currentFilter != null)
            Destroy(currentFilter);

        if (selectedAnswer == q.correctAnswerIndex)
        {
            score += pointsPerCorrectAnswer;
        }
        else
        {
            ShowFilter(wrongFilterPrefab);
        }

        UpdateScoreUI();
        StartCoroutine(NextQuestionDelayed());
    }

    void ShowFilter(GameObject filterPrefab)
    {
        if (filterPrefab == null || faceTransform == null) return;

        currentFilter = Instantiate(filterPrefab, faceTransform);
        currentFilter.transform.localPosition = Vector3.zero;
        currentFilter.transform.localRotation = Quaternion.identity;
        currentFilter.transform.localScale = Vector3.one;
    }

    IEnumerator NextQuestionDelayed()
    {
        yield return new WaitForSeconds(nextQuestionDelay);

        if (currentFilter != null)
            Destroy(currentFilter);

        currentQuestionIndex++;
        LoadQuestion();
    }

    void ShowQuizComplete()
    {
        questionText.text = $"Quiz Selesai!\n\nScore: {score}/{questions.Length * pointsPerCorrectAnswer}";
        DisableButtons();
    }

    void UpdateScoreUI()
    {
        scoreText.text = $"Score: {score}";
    }

    void EnableButtons()
    {
        if (buttonA != null) buttonA.interactable = true;
        if (buttonB != null) buttonB.interactable = true;
        if (buttonC != null) buttonC.interactable = true;
        if (buttonD != null) buttonD.interactable = true;
    }

    void DisableButtons()
    {
        if (buttonA != null) buttonA.interactable = false;
        if (buttonB != null) buttonB.interactable = false;
        if (buttonC != null) buttonC.interactable = false;
        if (buttonD != null) buttonD.interactable = false;
    }
}

[System.Serializable]
public class Question
{
    public string questionText;
    public string[] answers;
    public int correctAnswerIndex;

    public Question(string question, string[] ans, int correct)
    {
        questionText = question;
        answers = ans;
        correctAnswerIndex = correct;
    }
}