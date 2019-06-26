using System;
using System.Collections.Generic;
using System.Linq;

namespace DependencyChallenge
{
    public static class Program
    {
        static void Main()
        {
            // definitions
            var dependencyGraph = new HashSet<Tuple<string, string>>();
            var jobList = new HashSet<string>();
            bool isSelfDependent = false;

            // fetch the jobs to be added
            Console.WriteLine("Enter the no. of Jobs to be performed");
            int count = Convert.ToInt32(Console.ReadLine());

            try
            {
                // iterate to fetch the job names and dependencies
                for (int i = 0; i < count; i++)
                {
                    Console.WriteLine($"Enter the Job name  for Job[{i + 1}]: ");
                    string jobName = Convert.ToString(Console.ReadLine());

                    Console.WriteLine($"Enter the dependency  for Job : {jobName} ");
                    string jobDependency = Convert.ToString(Console.ReadLine());

                    jobList.Add(jobName);                    

                    // validate for selg dependency
                    if (!string.IsNullOrEmpty(jobName) && (jobName.Equals(jobDependency)))
                    {
                        isSelfDependent = true;
                        Console.WriteLine("Job cannot depend on themselves");
                        break;
                    }
                    if (!string.IsNullOrEmpty(jobDependency))
                    {
                        dependencyGraph.Add(Tuple.Create(jobName, jobDependency));
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception : { ex.Message}");
                throw ex;
            }
            if (!isSelfDependent)
            {
                // fetch the dependency list
                var result = GetDependencyList(jobList, dependencyGraph);
                if (result != null)
                {
                    Console.WriteLine($"Job Sequence : {string.Join(" => ", result)}");
                }
                else
                {
                    Console.WriteLine($"Result : Jobs can’t have circular dependencies.");
                }
            }
            Console.ReadLine();
        }

        /// <summary>
        /// GetDependencyList
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="jobs">List of Jobs to be performed</param>
        /// <param name="dependencyJobGraph"> Dependecy Mapping of Jobs</param>
        /// <returns> sequence of dependencies </returns>
        public static List<T> GetDependencyList<T>(HashSet<T> jobs, HashSet<Tuple<T, T>> dependencyJobGraph) where T : IEquatable<T>
        {
            var dependencyJobsList = new List<T>();

            //fetch all the non dependent jobs
            var nonDependencyJobs = new HashSet<T>(jobs.Where(n => dependencyJobGraph.All(j => j.Item1.Equals(n) == false)));
            while (nonDependencyJobs.Any())
            {
                var n = nonDependencyJobs.First();
                nonDependencyJobs.Remove(n);
                dependencyJobsList.Add(n);
                
                // iterate over the dependencies
                foreach (var current in dependencyJobGraph.Where(g => g.Item2.Equals(n)).ToList())
                {
                    var dependentOn = current.Item1;
                    dependencyJobGraph.Remove(current);

                    // if not more in dependencies add to non dependent list
                    if (dependencyJobGraph.All(x => x.Item1.Equals(dependentOn) == false))
                    {
                        nonDependencyJobs.Add(dependentOn);
                    }
                }
            }

            // circular dependencies
            if (dependencyJobGraph.Any())
            {
                return null;
            }
            return dependencyJobsList;
        }
    }
}