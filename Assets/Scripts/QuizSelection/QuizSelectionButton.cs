using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace QuizSelection
{
    public class QuizSelectionButton: MonoBehaviour
    {
        public delegate void QuizSelected(int quizIndex);
        public event QuizSelected OnQuizSelected;
        
        public TMP_Text quizNameText;
        public Button button;
        
        private int _quizIndex;

        public void Initialize(QuizVariationsScriptableObject quizData, int index)
        {
            quizNameText.text = quizData.name;
            _quizIndex = index;
            button.onClick.AddListener(_OnButtonClicked);
        }

        private void _OnButtonClicked()
        {
            Debug.Log($"Quiz {_quizIndex} selected");
            OnQuizSelected?.Invoke(_quizIndex);
        }
    }
}