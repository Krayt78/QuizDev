using System.Collections.Generic;
using QuizSelection;
using UnityEngine;
using UnityEngine.UI;

public class SelectQuizManager: MonoBehaviour
{
    public delegate void QuizSelected(int quizIndex);
    public event QuizSelected OnQuizSelected;
    
    public List<Button> quizSelectionButtons;
    public GameObject quizSelectionButtonPrefab;
    public Transform quizSelectionButtonContainer;
    
    public void Initialize(List<QuizScriptableObject> quizes)
    {
        for (var i = 0; i < quizes.Count; i++)
        {
            var quizSelectionButton = Instantiate(quizSelectionButtonPrefab, quizSelectionButtonContainer);
            var quizComponent = quizSelectionButton.GetComponent<QuizSelectionButton>();
            
            quizComponent.Initialize(quizes[i], i);
            quizComponent.OnQuizSelected += _OnQuizSelected;
        }
    }
    
    private void _OnQuizSelected(int index)
    {
        OnQuizSelected?.Invoke(index);
    }
}