using System.Collections.Generic;

[System.Serializable]
public class Question
{
    public string questionText;
    public List<Answer> answers;
    public int correctAnswerIndex;
    public string correctAnswerText;
}
