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
        
        // Tambahan validasi
        if (button == null)
            Debug.LogError($"❌ Button component not found on {gameObject.name}");
        
        if (quizManager == null)
            Debug.LogError("❌ QuizManager not found!");
        
        Debug.Log($"✅ ButtonHandler [{answerIndex}] initialized on {gameObject.name}");
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log($"🖱️ CLICK detected! Answer: {answerIndex}, Button: {gameObject.name}");
        
        if (button == null)
        {
            Debug.LogError("❌ Button is NULL!");
            return;
        }
        
        if (!button.interactable)
        {
            Debug.LogWarning("⚠️ Button not interactable!");
            return;
        }
        
        if (quizManager == null)
        {
            Debug.LogError("❌ QuizManager is NULL!");
            return;
        }
        
        quizManager.HandleButtonClick(answerIndex);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log($"👇 DOWN: {gameObject.name} (Answer {answerIndex})");
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Debug.Log($"👆 UP: {gameObject.name} (Answer {answerIndex})");
    }
}