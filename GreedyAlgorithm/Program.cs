
using System.Diagnostics;
using System.Numerics;
using System.Runtime.InteropServices;
using System.Linq;


namespace GreedyAlgorithm
{
    class Program
    {
        static void Main(string[] args)
        {
            //Job job1 = new Job(
            //    new DateTime(1, 1, 1, hour: 10, minute: 0, second: 0), 
            //    new DateTime(1, 1, 1, hour: 12, minute: 0, second: 0),
            //    100);
            //Job job2 = new Job(
            //    new DateTime(1, 1, 1, hour: 11, minute: 0, second: 0),
            //    new DateTime(1, 1, 1, hour: 12, minute: 0, second: 0),
            //    200);
            //Job job3 = new Job(
            //    new DateTime(1, 1, 1, hour: 11, minute: 30, second: 0),
            //    new DateTime(1, 1, 1, hour: 12, minute: 30, second: 0),
            //    400);
            //Job job4 = new Job(
            //    new DateTime(1, 1, 1, hour: 12, minute: 0, second: 0),
            //    new DateTime(1, 1, 1, hour: 13, minute: 0, second: 0),
            //    500);

            //List<Job> jobs = new List<Job>();
            //jobs.Add(job1);
            //jobs.Add(job2);
            //jobs.Add(job3);
            //jobs.Add(job4);

            Job job1 = new Job(
    new DateTime(1, 1, 1, hour: 10, minute: 0, second: 0),
    new DateTime(1, 1, 1, hour: 14, minute: 0, second: 0),
    1000);
            Job job2 = new Job(
                new DateTime(1, 1, 1, hour: 7, minute: 0, second: 0),
                new DateTime(1, 1, 1, hour: 11, minute: 0, second: 0),
                900);
            Job job3 = new Job(
                new DateTime(1, 1, 1, hour: 13, minute: 0, second: 0),
                new DateTime(1, 1, 1, hour: 17, minute: 0, second: 0),
                800);

            List<Job> jobs = new List<Job>();
            jobs.Add(job1);
            jobs.Add(job2);
            jobs.Add(job3);
            List<Job> profitableJobs = new List<Job>();
            //profitableJobs = GetProfitableJobs(jobs);
            profitableJobs = GetProfitableJobsGreedy(jobs);
            Console.WriteLine(GetMaxProfit(jobs));
            //double profit = 0;
            //Console.WriteLine("Подмножество работ, обеспечивающих максимальную прибыль");
            //foreach (Job job in profitableJobs)
            //{
            //    Console.WriteLine(job.toString());
            //    profit += job.value;
            //}
            //Console.WriteLine($"Суммарная прибыль = {profit}");

        }

        //public static List<Job> GetProfitableJobs(List<Job> jobs)
        //{
        //    double maxProfit = 0;
        //    double currentProfit;
        //    DateTime currentTime;

        //    List<Job> jobsAnswer = [];
        //    List<Job> profitableJobs = [];
        //    Stack<Job> stack = new();

        //    jobs.Sort((x, y) => DateTime.Compare(x.startTime, y.startTime));



        //    for (int jobIndex = 0;  jobIndex < jobs.Count; jobIndex++)
        //    {
        //        stack.Push(jobs[jobIndex]);
        //        profitableJobs[0] = jobs[jobIndex];
        //        currentProfit = jobs[jobIndex].profit;
        //        currentTime = jobs[jobIndex].endTime;

        //        while (stack.Count > 0)
        //        {
        //            for (int nextJobIndex = jobIndex + 1;  nextJobIndex < jobs.Count; nextJobIndex++)
        //            {
        //                if (jobs[nextJobIndex].startTime > currentTime)
        //                {
        //                    stack.Push(jobs[nextJobIndex]);
        //                    profitableJobs.Add(jobs[nextJobIndex]);
        //                    currentProfit += jobs[nextJobIndex].profit;
        //                    currentTime = jobs[nextJobIndex].endTime;
        //                    break;
        //                }
        //                else
        //                {
        //                    if (currentProfit > maxProfit)
        //                    {
        //                        maxProfit = currentProfit;
        //                        jobsAnswer = profitableJobs;
        //                    }
        //                    stack.Pop();
        //                }
        //            }
        //        }
        //    }
        //    return profitableJobs;
        //}

        public static List<Job> GetProfitableJobsGreedy(List<Job> jobs)
        {
            List<Job> jobsByTime = new List<Job>(jobs);
            List<Job> jobsAnswer = [];

            jobsByTime = jobs.OrderBy(job => job.startTime).ToList();
            jobs = jobs.OrderByDescending(job => job.profit).ToList();


            Job currentJob;
            bool flag;
            for (int i = 0; i < jobs.Count; i++)
            {
                currentJob = jobs[i];
                flag = true;
                for (int j = 0;  j < jobsAnswer.Count; j++)
                {
                    if (!(currentJob.endTime <= jobsAnswer[j].startTime) && !(jobsAnswer[j].endTime <= currentJob.startTime))
                    {
                        flag = false; break;
                    }
                }
                if (flag == true)
                {
                    jobsAnswer.Add(currentJob);
                }
            }

            return jobsAnswer;
        }


        public static double GetMaxProfit(List<Job> jobs)
        {
            jobs = jobs.OrderBy(job => job.startTime).ToList();
            return GetMaxProfitRecursive(jobs, 0, new List<Job>());
        }

        private static double GetMaxProfitRecursive(List<Job> jobs, int currentIndex, List<Job> selectedJobs)
        {
            if (currentIndex >= jobs.Count)
            {
                return selectedJobs.Sum(job => job.value);
            }

            double profitWithoutCurrent = GetMaxProfitRecursive(jobs, currentIndex + 1, selectedJobs);

            bool canIncludeCurrent = true;
            Job currentJob = jobs[currentIndex];
            foreach (var job in selectedJobs)
            {
                if (!(currentJob.endTime <= job.startTime || job.endTime <= currentJob.startTime))
                {
                    canIncludeCurrent = false;
                    break;
                }
            }

            double profitWithCurrent = 0;
            if (canIncludeCurrent)
            {
                List<Job> newSelectedJobs = new List<Job>(selectedJobs) { currentJob };
                profitWithCurrent = GetMaxProfitRecursive(jobs, currentIndex + 1, newSelectedJobs);
            }

            return Math.Max(profitWithoutCurrent, profitWithCurrent);
        }
    }
}