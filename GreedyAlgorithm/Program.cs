
using System.Diagnostics;
using System.Numerics;

namespace GreedyAlgorithm
{
    class Program
    {
        static void Main(string[] args)
        {
            Job job1 = new Job(
                new DateTime(1, 1, 1, hour: 10, minute: 0, second: 0), 
                new DateTime(1, 1, 1, hour: 12, minute: 0, second: 0),
                100);
            Job job2 = new Job(
                new DateTime(1, 1, 1, hour: 11, minute: 0, second: 0),
                new DateTime(1, 1, 1, hour: 12, minute: 0, second: 0),
                200);
            Job job3 = new Job(
                new DateTime(1, 1, 1, hour: 11, minute: 30, second: 0),
                new DateTime(1, 1, 1, hour: 12, minute: 13, second: 0),
                400);
            Job job4 = new Job(
                new DateTime(1, 1, 1, hour: 12, minute: 0, second: 0),
                new DateTime(1, 1, 1, hour: 13, minute: 0, second: 0),
                500);

            List<Job> jobs = new List<Job>();
            jobs.Add(job1);
            jobs.Add(job2);
            jobs.Add(job3);
            jobs.Add(job4);
            List<Job> profitableJobs = new List<Job>(); 
            profitableJobs = GetProfitableJobs(jobs);

        }

        public static List<Job> GetProfitableJobs(List<Job> jobs)
        {
            double maxProfit = 0;
            double currentProfit;
            DateTime currentTime;

            List<Job> jobsAnswer = [];
            List<Job> profitableJobs = [];
            Stack<Job> stack = new();

            jobs.Sort((x, y) => DateTime.Compare(x.startTime, y.startTime));



            for (int jobIndex = 0;  jobIndex < jobs.Count; jobIndex++)
            {
                stack.Push(jobs[jobIndex]);
                profitableJobs[0] = jobs[jobIndex];
                currentProfit = jobs[jobIndex].profit;
                currentTime = jobs[jobIndex].endTime;

                while (stack.Count > 0)
                {
                    for (int nextJobIndex = jobIndex + 1;  nextJobIndex < jobs.Count; nextJobIndex++)
                    {
                        if (jobs[nextJobIndex].startTime > currentTime)
                        {
                            stack.Push(jobs[nextJobIndex]);
                            profitableJobs.Add(jobs[nextJobIndex]);
                            currentProfit += jobs[nextJobIndex].profit;
                            currentTime = jobs[nextJobIndex].endTime;
                            break;
                        }
                        else
                        {
                            if (currentProfit > maxProfit)
                            {
                                maxProfit = currentProfit;
                                jobsAnswer = profitableJobs;
                            }
                            stack.Pop();
                        }
                    }
                }
            }
            return profitableJobs;
        }
    }

 
}