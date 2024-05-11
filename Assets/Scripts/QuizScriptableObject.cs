using UnityEngine;

[CreateAssetMenu(fileName = "QuizScriptableObject", menuName = "ScriptableObjects/Quiz", order = 1)]
public class QuizScriptableObject: ScriptableObject
{
    public string Question;
    public string[] Answers;
    public int CorrectAnswerIndex;
    public string Hint;
}