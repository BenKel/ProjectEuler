namespace ProjectEuler.Problems
{
    public interface IProblem
    {
        string Description { get; }

        string Title { get; }

        string GetAnswer();
    }
}