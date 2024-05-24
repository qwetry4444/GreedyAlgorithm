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
        public double value;
        public double profit;

        public Job(DateTime startTime, DateTime endTime, double value)
        {
            this.startTime = startTime;
            this.endTime = endTime;
            this.value = value;
            this.profit = value / (endTime - startTime).TotalSeconds;
        }

        public static bool CompareJobs(Job job1, Job job2)
        {
            return job1.startTime.CompareTo(job2.startTime) == 1;
        }

        public string toString()
        {
            return $"Время начала: {startTime}; Время конца: {endTime}; Прибыль: {value}";
        }
    }


}
