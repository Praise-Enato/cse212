using Microsoft.VisualStudio.TestTools.UnitTesting;

// TODO Problem 2 - Write and run test cases and fix the code to match requirements.

[TestClass]
public class PriorityQueueTests
{
    [TestMethod]
    // Scenario: Enqueue three items with different priorities (Low=1, Medium=3,
    //   High=2) then dequeue all three. This verifies that Dequeue always removes
    //   the item with the highest priority, and that the highest-priority item
    //   located at the END of the list is still found (loop-bound check).
    // Expected Result: "Medium" (pri 3), then "High" (pri 2), then "Low" (pri 1).
    // Defect(s) Found: (1) The Dequeue loop used "index < _queue.Count - 1" which
    //   never inspects the last element, so the highest-priority item at the back
    //   was missed. (2) Dequeue never removed the selected item from the list, so
    //   the same value was returned every time and the queue never shrank.
    public void TestPriorityQueue_1()
    {
        var priorityQueue = new PriorityQueue();
        priorityQueue.Enqueue("Low", 1);
        priorityQueue.Enqueue("Medium", 3);
        priorityQueue.Enqueue("High", 2);

        Assert.AreEqual("Medium", priorityQueue.Dequeue());
        Assert.AreEqual("High", priorityQueue.Dequeue());
        Assert.AreEqual("Low", priorityQueue.Dequeue());
    }

    [TestMethod]
    // Scenario: Enqueue several items where multiple share the same highest
    //   priority. Items: A(2), B(5), C(5), D(1), E(5). Dequeue all and confirm
    //   that ties are broken in FIFO order (the one closest to the front first).
    // Expected Result: B (first pri 5), C (next pri 5), E (last pri 5),
    //   then A (pri 2), then D (pri 1).
    // Defect(s) Found: The comparison used ">=" instead of ">", which caused a
    //   later item with an equal priority to replace the earlier one as the
    //   chosen index, breaking the required FIFO ordering for ties.
    public void TestPriorityQueue_2()
    {
        var priorityQueue = new PriorityQueue();
        priorityQueue.Enqueue("A", 2);
        priorityQueue.Enqueue("B", 5);
        priorityQueue.Enqueue("C", 5);
        priorityQueue.Enqueue("D", 1);
        priorityQueue.Enqueue("E", 5);

        Assert.AreEqual("B", priorityQueue.Dequeue());
        Assert.AreEqual("C", priorityQueue.Dequeue());
        Assert.AreEqual("E", priorityQueue.Dequeue());
        Assert.AreEqual("A", priorityQueue.Dequeue());
        Assert.AreEqual("D", priorityQueue.Dequeue());
    }

    [TestMethod]
    // Scenario: Dequeue from an empty queue.
    // Expected Result: An InvalidOperationException is thrown with the message
    //   "The queue is empty."
    // Defect(s) Found: None for this requirement. The empty-queue guard was
    //   already implemented correctly.
    public void TestPriorityQueue_Empty()
    {
        var priorityQueue = new PriorityQueue();

        try
        {
            priorityQueue.Dequeue();
            Assert.Fail("Exception should have been thrown.");
        }
        catch (InvalidOperationException e)
        {
            Assert.AreEqual("The queue is empty.", e.Message);
        }
        catch (AssertFailedException)
        {
            throw;
        }
        catch (Exception e)
        {
            Assert.Fail(
                string.Format("Unexpected exception of type {0} caught: {1}",
                    e.GetType(), e.Message)
            );
        }
    }

    [TestMethod]
    // Scenario: Enqueue items, dequeue all of them, then attempt one more
    //   Dequeue. This proves items are actually REMOVED from the queue (not just
    //   read) because once empty the queue must throw.
    // Expected Result: "first" then "second" are returned, then an
    //   InvalidOperationException ("The queue is empty.") on the third Dequeue.
    // Defect(s) Found: Dequeue did not call RemoveAt, so the item was never
    //   removed; without the fix the queue would never become empty and the
    //   third Dequeue would not throw.
    public void TestPriorityQueue_RemovesItems()
    {
        var priorityQueue = new PriorityQueue();
        priorityQueue.Enqueue("first", 10);
        priorityQueue.Enqueue("second", 5);

        Assert.AreEqual("first", priorityQueue.Dequeue());
        Assert.AreEqual("second", priorityQueue.Dequeue());

        try
        {
            priorityQueue.Dequeue();
            Assert.Fail("Exception should have been thrown.");
        }
        catch (InvalidOperationException e)
        {
            Assert.AreEqual("The queue is empty.", e.Message);
        }
        catch (AssertFailedException)
        {
            throw;
        }
        catch (Exception e)
        {
            Assert.Fail(
                string.Format("Unexpected exception of type {0} caught: {1}",
                    e.GetType(), e.Message)
            );
        }
    }
}
