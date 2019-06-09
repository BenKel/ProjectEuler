namespace ProjectEuler.Problems
{
    public abstract class ProblemBase : IProblem
    {
        public virtual string Description => "No Description";

        public virtual string Title => "No Title";

        public abstract string GetAnswer();
    }
}