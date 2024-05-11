using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Quiz
{
    public class QuizCanvasManager : MonoBehaviour
    {
        public delegate void AnswerButtonClicked(int answerIndex);

        public delegate void BackButtonClicked();

        public delegate void HintButtonClicked();

        public GameObject AnswerButtonPrefab;
        public Transform AnswerButtonContainer;

        [Header("Quiz Buttons")] public Button BackButton;

        public Button HintButton;
        public TMP_Text QuestionText;

        public event HintButtonClicked OnHintButtonClicked;

        public event AnswerButtonClicked OnAnswerButtonClicked;
        public event BackButtonClicked OnBackButtonClicked;

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

            QuestionText.text = quiz.Question;

            HintButton.onClick.AddListener(_OnHintButtonClicked);
            BackButton.onClick.AddListener(_OnBackButtonClicked);
        }

        private void _OnBackButtonClicked()
        {
            OnBackButtonClicked?.Invoke();
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