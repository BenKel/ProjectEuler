using ProjectEuler.Utilities;

namespace ProjectEuler.Problems
{
    public class Problem31 : ProblemBase
    {
        public override string Title => "Coin sums";

        public override string Description => @"
In England the currency is made up of pound, £, and pence, p, and there are eight coins in general circulation:

    1p, 2p, 5p, 10p, 20p, 50p, £1 (100p) and £2 (200p).

It is possible to make £2 in the following way:

    1×£1 + 1×50p + 2×20p + 1×5p + 1×2p + 3×1p

How many different ways can £2 be made using any number of coins?
            ";

        public override string GetAnswer()
        {
            const int targetValue = 200;
            // Start off counting a £2 coin and two £1 coins.
            int count = 2;
            int poundCount = 2;
            int fiftyCount = 0;
            int twentyCount = 0;
            int tenCount = 0;
            int fiveCount = 0;
            int twoCount = 0;
            int oneCount = 0;
            int largestCoinToAdd = 100;

            while (oneCount < targetValue)
            {
                // Remove smallest coin that isn't a 1 and zero all coins smaller than it.
                if (twoCount > 0)
                {
                    // No need to zero the oneCount as the coins will be added straight back on.
                    --twoCount;
                    largestCoinToAdd = 1;
                }
                else if (fiveCount > 0)
                {
                    --fiveCount;
                    twoCount = 0;
                    oneCount = 0;
                    largestCoinToAdd = 2;
                }
                else if (tenCount > 0)
                {
                    --tenCount;
                    fiveCount = 0;
                    twoCount = 0;
                    oneCount = 0;
                    largestCoinToAdd = 5;
                }
                else if (twentyCount > 0)
                {
                    --twentyCount;
                    tenCount = 0;
                    fiveCount = 0;
                    twoCount = 0;
                    oneCount = 0;
                    largestCoinToAdd = 10;
                }
                else if (fiftyCount > 0)
                {
                    --fiftyCount;
                    twentyCount = 0;
                    tenCount = 0;
                    fiveCount = 0;
                    twoCount = 0;
                    oneCount = 0;
                    largestCoinToAdd = 20;
                }
                else if (poundCount > 0)
                {
                    --poundCount;
                    fiftyCount = 0;
                    twentyCount = 0;
                    tenCount = 0;
                    fiveCount = 0;
                    twoCount = 0;
                    oneCount = 0;
                    largestCoinToAdd = 50;
                }

                int coinTotal = Utility.CoinSum(poundCount, fiftyCount, twentyCount, tenCount, fiveCount, twoCount, oneCount);

                // Add coins to get to the total, Starting with the biggest coin
                while (coinTotal < 200)
                {
                    if (largestCoinToAdd >= 50 && coinTotal + 50 <= 200)
                    {
                        coinTotal += 50;
                        ++fiftyCount;
                    }
                    else if (largestCoinToAdd >= 20 && coinTotal + 20 <= 200)
                    {
                        coinTotal += 20;
                        ++twentyCount;
                    }
                    else if (largestCoinToAdd >= 10 && coinTotal + 10 <= 200)
                    {
                        coinTotal += 10;
                        ++tenCount;
                    }
                    else if (largestCoinToAdd >= 5 && coinTotal + 5 <= 200)
                    {
                        coinTotal += 5;
                        ++fiveCount;
                    }
                    else if (largestCoinToAdd >= 2 && coinTotal + 2 <= 200)
                    {
                        coinTotal += 2;
                        ++twoCount;
                    }
                    else if (coinTotal + 1 <= 200)
                    {
                        coinTotal += 1;
                        ++oneCount;
                    }
                }

                ++count;
            }

            return count.ToString();
        }
    }
}