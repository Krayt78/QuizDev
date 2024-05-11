using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace Quiz
{
    public class QuizCanvasManager: MonoBehaviour
    {
    public delegate void HintButtonClicked();

    public event HintButtonClicked OnHintButtonClicked;

    public delegate void AnswerButtonClicked(int answerIndex);

    public event AnswerButtonClicked OnAnswerButtonClicked;
    
    public GameObject AnswerButtonPrefab;
    public Transform AnswerButtonContainer;

    [Header("Quiz Buttons")] public Button HintButton;
    public Button[] AnswerButtons;
    
    public void Initialize(QuizScriptableObject quiz)
    {
        for (var i = 0; i < quiz.Answers.Length; i++)
        {
            var answerIndex = i;
            var button = Instantiate(AnswerButtonPrefab, AnswerButtonContainer);
            var buttonComponent = button.GetComponent<Button>();
            buttonComponent.onClick.AddListener(() => _OnAnswerButtonClicked(answerIndex));
            button.GetComponentInChildren<TMP_Text>().text = quiz.Answers[i];
        }
        
        HintButton.onClick.AddListener(_OnHintButtonClicked);
    }

    private void _OnHintButtonClicked()
    {
        OnHintButtonClicked?.Invoke();
    }

    private void _OnAnswerButtonClicked(int answerIndex)
    {
        OnAnswerButtonClicked?.Invoke(answerIndex);
    }
    }
}