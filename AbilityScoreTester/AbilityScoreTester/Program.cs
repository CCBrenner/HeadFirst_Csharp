// See https://aka.ms/new-console-template for more information

class AbilityScoreCalculator 
{
    public int RollResult = 14;
    public double DivideBy = 1.75;
    public int AddAmount = 2;
    public int Minimum = 3;
    public int Score;

    public void CalculateAbilityScore()
    {
        // Divide the roll result by the Result field
        double divided = RollResult / DivideBy;

        // Add AddAmount ot the rsult of that division
        int added = AddAmount += divided;

        // If the result is too small , use Minimum
        if (added < Minimum)
        {
            Score = Minimum;
        }
        else
        {
            Score = added;
        }
    }
}
