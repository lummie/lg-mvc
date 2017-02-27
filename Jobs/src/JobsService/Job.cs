using System;
using System.Collections.Generic;

namespace Jobs.Service
{
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
            if (child.ID == this.ID) {
                throw new SelfReferenceException(child);
            }
            this.Children.Add(child);
        }


        // callback when walking the tree
        public delegate void Visit(Job job, int level);

        public static void Walk(Job current, Visit callback, int i=0) {
            if (current != null) {
                callback(current, i);
            }
            i++;
            foreach(var child in current.Children) {
                Walk(child, callback, i);
            }   
            i--;         
        }



        public class SelfReferenceException : Exception {
            public SelfReferenceException(Job j)  : base($"A Job cannot reference itself {j.ID}") {

            }
        }

    }
}
