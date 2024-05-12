using System;
using System.Collections;
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
        public TMP_Text CodeText;
        public TMP_Text QuestionText;
        public HintPopup HintPopup;
        
        public GameObject Background1;
        public GameObject Background2;
        public GameObject Background3;

        public event HintButtonClicked OnHintButtonClicked;
        public event AnswerButtonClicked OnAnswerButtonClicked;
        public event BackButtonClicked OnBackButtonClicked;
        
        private List<GameObject> _answerButtons = new List<GameObject>();

        public void Initialize(QuizScriptableObject quiz, bool isFirstInitialization = false)
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
            
            CodeText.text = "";
            if (!isFirstInitialization)
            {
                StartCoroutine(ComputerTypeEffect(quiz));
            }
            
            QuestionText.text = quiz.Question;

            HintButton.onClick.AddListener(_OnHintButtonClicked);
            BackButton.onClick.AddListener(_OnBackButtonClicked);
        }
        
        public IEnumerator ComputerTypeEffect(QuizScriptableObject quizScriptableObject)
        {
            var amountOfCharacters = quizScriptableObject.Code.Length;
            var timeBetweenLetters = quizScriptableObject.timeToAppear / amountOfCharacters;
            foreach (var codeCharacter in quizScriptableObject.Code)
            {
                CodeText.text += codeCharacter;
                yield return new WaitForSeconds(timeBetweenLetters);
            }
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

        public void HandleBackgrounds(int backgroundIndex)
        {
            switch (backgroundIndex)
            {
                case 0:
                    Background1.SetActive(true);
                    Background2.SetActive(false);
                    Background3.SetActive(false);
                    break;
                case 1:
                    Background1.SetActive(false);
                    Background2.SetActive(true);
                    Background3.SetActive(false);
                    break;
                case 2:
                    Background1.SetActive(false);
                    Background2.SetActive(false);
                    Background3.SetActive(true);
                    break;
                default:
                    throw new Exception("Invalid background index");
            }
        }
    }
}