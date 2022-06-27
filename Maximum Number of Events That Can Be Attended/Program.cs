using System;
using System.Collections.Generic;

namespace Maximum_Number_of_Events_That_Can_Be_Attended
{
  class Program
  {
    static void Main(string[] args)
    {
      int[][] events = new int[5][] { new int[] { 1, 2 }, new int[] { 3, 4 }, new int[] { 1, 6 }, new int[] { 2, 3 }, new int[] { 1, 1 } };
      Solution s = new Solution();
      var answer = s.MaxEvents(events);
      Console.WriteLine(answer);
    }
  }

  public class Solution
  {
    public int MaxEvents(int[][] events)
    {
      // Sort ased on start time
      Array.Sort(events, (a, b) => { return a[0] - b[0]; });

      // MIn heap, this will hold the ending day of an event in asc order
      // the shortest ending event will be at the top
      PriorityQueue<int, int> pq = new PriorityQueue<int, int>();

      int i = 0; // looping variable for events
      int count = 0; // for final answer
      int n = events.Length;
      // for each day will check how many events are starting on day d
      for (int d = 1; d <= 100000; d++)
      {
        // on day d add all the events start at day d in PQ
        while (i < n && events[i][0] == d)
        {
          int endingDay = events[i][1];
          pq.Enqueue(endingDay, endingDay);
          i++;
        }

        // remove all the completed events before day d.
        while (pq.Count > 0 && pq.Peek() < d)
        {
          pq.Dequeue();
        }

        // attend the event, if multiple events start at the same day then will be attending the event which is end early
        // (1, 1), (1, 2), (1, 6)
        // all events are started at 1, but will be attending the one which is ending soon i.e the first one (1,1)
        // those which are ending later we have still time to attend them
        if (pq.Count > 0)
        {
          pq.Dequeue();
          count++;
        }

        // break the loop when no more events to process and all events are already attended
        if (pq.Count == 0 && i >= n)
        {
          break;
        }
      }

      return count;
    }
  }
}
