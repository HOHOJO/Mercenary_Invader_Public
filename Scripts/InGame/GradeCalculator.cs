using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GradeCalculator : MonoBehaviour
{
    [SerializeField]
    private Dictionary<string, int> scoreRanges = new Dictionary<string, int>
    {
        { "F", 0 },
        { "D", 100 },
        { "C", 200 },
        { "B", 300 },
        { "A", 400 },
        { "S", 500 },
    };

    [SerializeField]
    private Dictionary<string, int> gradeToReward = new Dictionary<string, int>
    {
        { "F", 10 },
        { "D", 20 },
        { "C", 30 },
        { "B", 40 },
        { "A", 50 },
        { "S", 60 },
    };

    public string CalculateGrade(int score)
    {
        foreach (var pair in scoreRanges)
        {
            if (score <= pair.Value)
            {
                return pair.Key;
            }
        }

        return scoreRanges.Last().Key;
    }

    public int CalculateRewards(string grade)
    {
        if (gradeToReward.ContainsKey(grade))
        {
            return gradeToReward[grade];
        }

        return 0;
    }
}
