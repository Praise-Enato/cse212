using System.Collections;

public static class Recursion
{
    /// <summary>
    /// #############
    /// # Problem 1 #
    /// #############
    /// Using recursion, find the sum of 1^2 + 2^2 + 3^2 + ... + n^2
    /// and return it.  Remember to both express the solution 
    /// in terms of recursive call on a smaller problem and 
    /// to identify a base case (terminating case).  If the value of
    /// n <= 0, just return 0.   A loop should not be used.
    /// </summary>
    public static int SumSquaresRecursive(int n)
    {
        // Base case: nothing to add once n drops to 0 or below.
        if (n <= 0)
        {
            return 0;
        }

        // Recursive case: add n^2 to the sum of squares for everything below n.
        return n * n + SumSquaresRecursive(n - 1);
    }

    /// <summary>
    /// #############
    /// # Problem 2 #
    /// #############
    /// Using recursion, insert permutations of length
    /// 'size' from a list of 'letters' into the results list.  This function
    /// should assume that each letter is unique (i.e. the 
    /// function does not need to find unique permutations).
    ///
    /// In mathematics, we can calculate the number of permutations
    /// using the formula: len(letters)! / (len(letters) - size)!
    ///
    /// For example, if letters was [A,B,C] and size was 2 then
    /// the following would the contents of the results array after the function ran: AB, AC, BA, BC, CA, CB (might be in 
    /// a different order).
    ///
    /// You can assume that the size specified is always valid (between 1 
    /// and the length of the letters list).
    /// </summary>
    public static void PermutationsChoose(List<string> results, string letters, int size, string word = "")
    {
        // Base case: once the in-progress word reaches the requested size,
        // save it and stop recursing.
        if (word.Length == size)
        {
            results.Add(word);
            return;
        }

        // Recursive case: try each remaining letter as the next character.
        // For each choice we recurse on the letters list with that letter
        // removed, so no letter is reused within a single permutation.
        for (int i = 0; i < letters.Length; i++)
        {
            string remainingLetters = letters[..i] + letters[(i + 1)..];
            PermutationsChoose(results, remainingLetters, size, word + letters[i]);
        }
    }

    /// <summary>
    /// #############
    /// # Problem 3 #
    /// #############
    /// Imagine that there was a staircase with 's' stairs.  
    /// We want to count how many ways there are to climb 
    /// the stairs.  If the person could only climb one 
    /// stair at a time, then the total would be just one.  
    /// However, if the person could choose to climb either 
    /// one, two, or three stairs at a time (in any order), 
    /// then the total possibilities become much more 
    /// complicated.  If there were just three stairs,
    /// the possible ways to climb would be four as follows:
    ///
    ///     1 step, 1 step, 1 step
    ///     1 step, 2 step
    ///     2 step, 1 step
    ///     3 step
    ///
    /// With just one step to go, the ways to get
    /// to the top of 's' stairs is to either:
    ///
    /// - take a single step from the second to last step, 
    /// - take a double step from the third to last step, 
    /// - take a triple step from the fourth to last step
    ///
    /// We don't need to think about scenarios like taking two 
    /// single steps from the third to last step because this
    /// is already part of the first scenario (taking a single
    /// step from the second to last step).
    ///
    /// These final leaps give us a sum:
    ///
    /// CountWaysToClimb(s) = CountWaysToClimb(s-1) + 
    ///                       CountWaysToClimb(s-2) +
    ///                       CountWaysToClimb(s-3)
    ///
    /// To run this function for larger values of 's', you will need
    /// to update this function to use memoization.  The parameter
    /// 'remember' has already been added as an input parameter to 
    /// the function for you to complete this task.
    /// </summary>
    public static decimal CountWaysToClimb(int s, Dictionary<int, decimal>? remember = null)
    {
        // Base Cases
        if (s == 0)
            return 0;
        if (s == 1)
            return 1;
        if (s == 2)
            return 2;
        if (s == 3)
            return 4;

        // Initialize the memoization dictionary on the first (outer) call.
        remember ??= new Dictionary<int, decimal>();

        // If we already computed the answer for this 's', return it instantly.
        // This is what turns the naive O(3^n) recursion into roughly O(n).
        if (remember.ContainsKey(s))
        {
            return remember[s];
        }

        // Solve using recursion. Pass the same 'remember' dictionary down so
        // every sub-problem populates and benefits from the same cache.
        decimal ways = CountWaysToClimb(s - 1, remember)
                     + CountWaysToClimb(s - 2, remember)
                     + CountWaysToClimb(s - 3, remember);

        // Cache the result before returning so future calls can reuse it.
        remember[s] = ways;
        return ways;
    }

    /// <summary>
    /// #############
    /// # Problem 4 #
    /// #############
    /// A binary string is a string consisting of just 1's and 0's.  For example, 1010111 is 
    /// a binary string.  If we introduce a wildcard symbol * into the string, we can say that 
    /// this is now a pattern for multiple binary strings.  For example, 101*1 could be used 
    /// to represent 10101 and 10111.  A pattern can have more than one * wildcard.  For example, 
    /// 1**1 would result in 4 different binary strings: 1001, 1011, 1101, and 1111.
    ///	
    /// Using recursion, insert all possible binary strings for a given pattern into the results list.  You might find 
    /// some of the string functions like IndexOf and [..X] / [X..] to be useful in solving this problem.
    /// </summary>
    public static void WildcardBinary(string pattern, List<string> results)
    {
        // Find the first wildcard in the pattern.
        int wildcardIndex = pattern.IndexOf('*');

        // Base case: no more wildcards means the pattern is a finished
        // binary string (or the empty string), so add it to the results.
        if (wildcardIndex == -1)
        {
            results.Add(pattern);
            return;
        }

        // Recursive case: replace the first '*' once with '0' and once with
        // '1', then recurse on each resulting string. Every subsequent
        // wildcard will be expanded the same way as the recursion unwinds.
        string before = pattern[..wildcardIndex];
        string after = pattern[(wildcardIndex + 1)..];

        WildcardBinary(before + "0" + after, results);
        WildcardBinary(before + "1" + after, results);
    }

    /// <summary>
    /// Use recursion to insert all paths that start at (0,0) and end at the
    /// 'end' square into the results list.
    /// </summary>
    public static void SolveMaze(List<string> results, Maze maze, int x = 0, int y = 0, List<ValueTuple<int, int>>? currPath = null)
    {
        // If this is the first time running the function, then we need
        // to initialize the currPath list.
        if (currPath == null) {
            currPath = new List<ValueTuple<int, int>>();
        }

        // Record the current square on the path. This must happen before the
        // recursive calls so that IsValidMove can see we have been here and
        // refuse to revisit (no circles).
        currPath.Add((x, y));

        if (maze.IsEnd(x, y))
        {
            // We have reached the goal — save a snapshot of the current path.
            results.Add(currPath.AsString());
        }
        else
        {
            // Try each of the four cardinal directions. IsValidMove handles
            // boundary, wall, and already-visited checks for us.
            if (maze.IsValidMove(currPath, x + 1, y))
            {
                SolveMaze(results, maze, x + 1, y, currPath);
            }
            if (maze.IsValidMove(currPath, x - 1, y))
            {
                SolveMaze(results, maze, x - 1, y, currPath);
            }
            if (maze.IsValidMove(currPath, x, y + 1))
            {
                SolveMaze(results, maze, x, y + 1, currPath);
            }
            if (maze.IsValidMove(currPath, x, y - 1))
            {
                SolveMaze(results, maze, x, y - 1, currPath);
            }
        }

        // Backtrack: remove the current square so the caller can explore
        // other paths that do not include this position.
        currPath.RemoveAt(currPath.Count - 1);
    }
}