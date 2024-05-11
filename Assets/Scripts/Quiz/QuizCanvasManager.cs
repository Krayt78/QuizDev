using System;
using System.Collections.Generic;
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
        public TMP_Text TitleText;
        public HintPopup HintPopup;

        public event HintButtonClicked OnHintButtonClicked;
        public event AnswerButtonClicked OnAnswerButtonClicked;
        public event BackButtonClicked OnBackButtonClicked;
        
        private List<GameObject> _answerButtons = new List<GameObject>();

        public void Initialize(QuizScriptableObject quiz)
        {
            ClearPreviousAnswers();
            
            for (var i = 0; i < quiz.Answers.Length; i++)
            {
                var answerIndex = i;
                var button = Instantiate(AnswerButtonPrefab, AnswerButtonContainer);
                _answerButtons.Add(button);
                var answerButton = button.GetComponent<AnswerButton>();
                answerButton.AnswerButtonComponent.onClick.AddListener(() => _OnAnswerButtonClicked(answerIndex));
                answerButton.SetText(quiz.Answers[i]);

                switch (i)
                {
                    case 0:
                        answerButton.SetColor(Utils.HexToColor(Constants.FIRST_ANSWER_BUTTON_COLOUR));
                        break;
                    case 1:
                        answerButton.SetColor(Utils.HexToColor(Constants.SECOND_ANSWER_BUTTON_COLOUR));
                        break;
                    case 2:
                        answerButton.SetColor(Utils.HexToColor(Constants.THIRD_ANSWER_BUTTON_COLOUR));
                        break;
                    case 3:
                        answerButton.SetColor(Utils.HexToColor(Constants.FOURTH_ANSWER_BUTTON_COLOUR));
                        break;
                    default:
                        throw new Exception("Invalid answer button index");
                    
                }
            }

            QuestionText.text = quiz.Question;
            TitleText.text = quiz.name;

            HintButton.onClick.AddListener(_OnHintButtonClicked);
            BackButton.onClick.AddListener(_OnBackButtonClicked);
        }
        
        private void ClearPreviousAnswers()
        {
            foreach (var answerButton in _answerButtons)
            {
                Destroy(answerButton);
            }
            
            _answerButtons.Clear();
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