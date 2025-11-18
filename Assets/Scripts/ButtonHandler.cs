using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonHandler : MonoBehaviour, IPointerClickHandler, IPointerDownHandler, IPointerUpHandler
{
    private Button button;
    private QuizManager quizManager;
    public int answerIndex;

    void Start()
    {
        button = GetComponent<Button>();
        quizManager = FindObjectOfType<QuizManager>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (button == null)
            return;

        if (!button.interactable)
            return;

        if (quizManager == null)
            return;

        quizManager.HandleButtonClick(answerIndex);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
    }

    public void OnPointerUp(PointerEventData eventData)
    {
    }
}
