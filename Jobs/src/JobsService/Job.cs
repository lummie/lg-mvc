using System;
using System.Collections.Generic;

namespace Jobs.Service
{
    /**
        Represents a Job that can have child jobs, (jobs that are not as hich priority as this one)
     */
    public class Job
    {
        private readonly string id;
        private List<Job> Children = new List<Job>();

        public Job(string id)
        {
            this.id = id;
        }

        public string ID { get { return this.id; } }
     
        public override string ToString()
        {
            return this.id;
        }

        public void AddChild(Job child) {
            // see if the parent already exists in the child's tree 
            bool found = Job.Walk(child, (node, l) => {
                return (node.ID == this.ID); // if we find it the quit the visiting
            });

            if (found) { // parent found in child' tree so fail as adding this node would cause a cycle
                throw new SelfReferenceException(child);
            }
            // TODO :) - This is potentially an O(n) operation for each insertion
            // it would be better to always allow adding and the scan the tree for cycles after 
            // however with the known simplicty of the inputs I went for the quick solution
            // Add a backlog item to enhance :)

            this.Children.Add(child);
        }


        /**<summary>
        Callback when walking the tree, a visit provides the job and the current depth level in the tree.
        Return true if the walking should be cancelled.
        </summary>*/
        public delegate bool Visit(Job job, int level);

        /**<summary>
        Walks the Jobs child depenedenices raising a Visit for itself and each child
        </summary>*/
        public static bool Walk(Job current, Visit callback, int level=0) {
            if (current != null) {
                if (callback(current, level)) {
                    return true; // quit
                }
            }
            level++;
            foreach(var child in current.Children) {
                if(Walk(child, callback, level)) {
                    return true; //quit
                }
            }   
            level--;     
            return false;    
        }


        // Thrown if the job detects that a cycle has been found
        public class SelfReferenceException : Exception {
            public SelfReferenceException(Job j)  : base($"A Job cannot reference itself or by cyclic {j.ID}") {

            }
        }

    }
}
