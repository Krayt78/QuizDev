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

    [Header("Quiz Buttons")] public Button HintButton;
    public Button[] AnswerButtons;
    
    public void Initialize(QuizScriptableObject quiz)
    {
        for (var i = 0; i < AnswerButtons.Length; i++)
        {
            var answerIndex = i;
            AnswerButtons[i].onClick.AddListener(() => _OnAnswerButtonClicked(answerIndex));
        }

        
        HintButton.onClick.AddListener(_OnHintButtonClicked);
    }

    // Start is called before the first frame update
    void Start()
    {
        for (var i = 0; i < AnswerButtons.Length; i++)
        {
            var answerIndex = i;
            AnswerButtons[i].onClick.AddListener(() => _OnAnswerButtonClicked(answerIndex));
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