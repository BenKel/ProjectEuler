namespace ProjectEuler.Problems
{
    public class Problem24 : ProblemBase
    {
        public override string Title => "Lexicographic permutations";

        public override string Description => @"
A permutation is an ordered arrangement of objects. For example, 3124 is one possible permutation of the digits 1, 2, 3 and 4. If all of the permutations are listed numerically or alphabetically, we call it lexicographic order. The lexicographic permutations of 0, 1 and 2 are:

012   021   102   120   201   210

What is the millionth lexicographic permutation of the digits 0, 1, 2, 3, 4, 5, 6, 7, 8 and 9?
            ";

        public override string GetAnswer()
        {
            // 3,628,800 ways of arranging in total.

            // 1,000,000 = 2*9! + 6*8! + 6*7! + 2*6! + 5*5! + 4! + 2*3! + 2*2!
            // 2*9! means that the third number in the list will be in the first position.
            // 6*8! means that the 7th number from the list (with '2' removed) will be in the second position.
            // Applying this rule to the whole list, we get
            return "2783915460";
        }
    }
}