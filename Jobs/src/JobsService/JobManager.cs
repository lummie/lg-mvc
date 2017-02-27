using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Jobs.Service
{

    public partial class JobManager
    {
        // Matches A or A=>B with whitespace (^[a-zA-Z]\s?=>\s?[a-zA-Z])|(^[a-zA-Z])
        private const string RX_INPUT_PATTERN = @"(?'links'[a-zA-Z]\s?=>\s?[a-zA-Z])|(?'nodes'[a-zA-Z])";

        // string to represent the root node, only used in debug output
        private const string ROOT = "<R>";

        // job list holds the list of the parent nodes in the Directed Graph
        private List<Job> jobList = new List<Job>();

        // a root node for the graph which is used as the starting point for the DFS
        private Job rootJob = new Job(ROOT);

        // flag for outputting the input, tree and result to the console for debugging
        private readonly  bool debug;

        public JobManager(bool debug = false) {
            this.debug = debug;
            // add a root item
            jobList.Add(rootJob);
        }



        // Looks for a job with the specified id in the joblist and adds a new one if not found
        private Job FindOrAddJob(string id) {
            var found = jobList.Find(j => (j.ID == id));
            if (found == null) { // not found
                // create one and add it to the list
                found = new Job(id);
                jobList.Add(found);
            }
            return found;
        }


        // splits up a job line into child and parent, defaulting the parent to ROOT if one is not supplied
        private Tuple<string,string> SplitPair(string token) {
                string id, parent = ROOT;
                int i = token.IndexOf("=>");
                if (i == -1) { 
                    // not a relationship
                    id = token.Trim();
                } else {
                    // is a relationship
                    id = token.Substring(0,i).Trim();
                    parent = token.Substring(i+2).Trim();
                }
                return new Tuple<string,string>(id,parent);
        }


        public string SortJobs(string joblist){

            // process the passed in job list string and construct parent-child model
            Regex rx = new Regex(RX_INPUT_PATTERN, RegexOptions.Multiline | RegexOptions.IgnoreCase);
            Match m = rx.Match(joblist);
            while (m.Success)
            {
                var pair = SplitPair(m.Value);
                
                var child = FindOrAddJob(pair.Item1);
                var parent = FindOrAddJob(pair.Item2);
                parent.AddChild(child);
                m = m.NextMatch();
            }




            // walk the tree and return string
            StringBuilder sb = new StringBuilder();
            Job.Walk(rootJob, (j,l) => {
                if(j!=rootJob) {
                    sb.Append(j.ID);
                }
                return false; // dont quit walking
            });

            if (debug) {
                // output the tree idented
                Console.WriteLine($"PROCESS:[{joblist}]");
                Job.Walk(rootJob, (j,l) => {
                    Console.WriteLine(new string('-', l*2) + j.ID); 
                    return false; // dont quit walking
                });
                Console.WriteLine("ANS: " + sb.ToString());
            }

            // return the ordered string
            return sb.ToString();
        }

    }
}


