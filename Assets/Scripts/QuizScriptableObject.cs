using UnityEngine;

[CreateAssetMenu(fileName = "QuizScriptableObject", menuName = "ScriptableObjects/Quiz", order = 1)]
public class QuizScriptableObject: ScriptableObject
{
    public string Question;
    public string Code;
    public string[] Answers;
    public float timeToAppear = 2f;
    public int CorrectAnswerIndex;
    public string Hint;
}