using ProjectEuler.Problems;

namespace ProjectEuler.App
{
    public interface IProblemInstantiator
    {
        IProblem GetProblemInstance(int problemNumber);
    }
}