using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreedyAlgorithm
{
    public class Job
    {
        public DateTime startTime;
        public DateTime endTime;
        public double profit;
        
        public Job(DateTime startTime, DateTime endTime, double profit)
        {
            this.startTime = startTime;
            this.endTime = endTime;
            this.profit = profit;
        }

        public static bool CompareJobs(Job job1, Job job2)
        {
            return job1.startTime.CompareTo(job2.startTime) == 1;
        }
    }


}
