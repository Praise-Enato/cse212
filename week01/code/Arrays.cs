public static class Arrays
{
    /// <summary>
    /// This function will produce an array of size 'length' starting with 'number' followed by multiples of 'number'.  For 
    /// example, MultiplesOf(7, 5) will result in: {7, 14, 21, 28, 35}.  Assume that length is a positive
    /// integer greater than 0.
    /// </summary>
    /// <returns>array of doubles that are the multiples of the supplied number</returns>
    public static double[] MultiplesOf(double number, int length)
    {
        // Plan:
        // 1. Create a new array of doubles with size equal to 'length' to hold the results.
        // 2. Loop with an index i going from 0 up to (but not including) 'length'.
        // 3. The k-th multiple of 'number' (where k starts at 1) is number * k.
        //    Since i starts at 0, the value to store at position i is number * (i + 1).
        // 4. After the loop finishes, return the filled array.

        // 1. Allocate the result array.
        double[] multiples = new double[length];

        // 2-3. Fill each position with the corresponding multiple of 'number'.
        for (int i = 0; i < length; i++)
        {
            multiples[i] = number * (i + 1);
        }

        // 4. Return the completed array.
        return multiples;
    }

    /// <summary>
    /// Rotate the 'data' to the right by the 'amount'.  For example, if the data is 
    /// List<int>{1, 2, 3, 4, 5, 6, 7, 8, 9} and an amount is 3 then the list after the function runs should be 
    /// List<int>{7, 8, 9, 1, 2, 3, 4, 5, 6}.  The value of amount will be in the range of 1 to data.Count, inclusive.
    ///
    /// Because a list is dynamic, this function will modify the existing data list rather than returning a new list.
    /// </summary>
    public static void RotateListRight(List<int> data, int amount)
    {
        // Plan:
        // Rotating a list to the right by 'amount' is the same as taking the last
        // 'amount' items and moving them to the front, while keeping their order.
        //
        // Example: data = {1,2,3,4,5,6,7,8,9}, amount = 3
        //   - The last 3 items are {7, 8, 9}.
        //   - Move them to the front -> {7, 8, 9, 1, 2, 3, 4, 5, 6}.
        //
        // Step by step using List<int> range methods:
        //   1. Calculate startIndex = data.Count - amount.
        //      This is the position where the "tail" of length 'amount' begins.
        //   2. Use data.GetRange(startIndex, amount) to copy the tail into a new list.
        //   3. Use data.RemoveRange(startIndex, amount) to remove that tail from data.
        //   4. Use data.InsertRange(0, tail) to put the tail at the front of data.
        //   5. The original list is now rotated in place; nothing needs to be returned.
        //
        // Edge case: when amount == data.Count, the entire list is the tail. We
        // remove everything, then insert it back at index 0, leaving the list
        // unchanged — which is the correct behavior for a full rotation.

        // 1. Find where the last 'amount' items begin.
        int startIndex = data.Count - amount;

        // 2. Copy out the last 'amount' items.
        List<int> tail = data.GetRange(startIndex, amount);

        // 3. Remove those items from their current position at the end of the list.
        data.RemoveRange(startIndex, amount);

        // 4. Insert the saved tail at the front of the list.
        data.InsertRange(0, tail);
    }
}
