using System;
using Xunit;
using Jobs.Service;

namespace uk.lummie.Tests
{
    public class JobTest
    {
        [Fact]
        public void TestID()
        {
            Job j = new Job("ID1");
            Assert.Equal("ID1", j.ID);
        }

        [Fact]
        public void TestParentSetGet()
        {
            Job j = new Job("ID1");
            Job j2 = new Job("ID2");
        }

    }

    public class JobServiceTests
    {

        [Fact]
        public void TestEmpty()
        {
            JobManager jm = new JobManager();
            string input = "";
            string expected = "";

            Assert.Equal(expected, jm.SortJobs(input));
        }

        [Fact]
        public void TestSingle()
        {
            JobManager jm = new JobManager();
            string input = "Q";
            string expected = "Q";

            Assert.Equal(expected, jm.SortJobs(input));
        }

        [Fact]
        public void TestWords()
        {
            JobManager jm = new JobManager();
            string input = "XYZ";
            string expected = "XYZ";

            Assert.Equal(expected, jm.SortJobs(input));
        }


        [Fact]
        public void TestAB()
        {
            JobManager jm = new JobManager();
            string input = @"A B";
            string expected = "AB";
            Assert.Equal(expected, jm.SortJobs(input));
        }


        [Fact]
        public void TestABOnDifferentLines()
        {
            JobManager jm = new JobManager();
            string input = @"H
            I";
            string expected = "HI";
            Assert.Equal(expected, jm.SortJobs(input));
        }


        [Fact]
        public void TestABCOnDifferentLines()
        {
            JobManager jm = new JobManager();
            string input = @"J
            K
            L";
            string expected = "JKL";
            Assert.Equal(expected, jm.SortJobs(input));
        }

        [Fact]
        public void TestDependency()
        {
            JobManager jm = new JobManager();
            string input = @"B => C";
            string expected = ""; // nothing to tie to root.
            Assert.Equal(expected, jm.SortJobs(input));
        }

        [Fact]
        public void TestDependencyCase1()
        {
            JobManager jm = new JobManager();
            string input = @"A
            B => C
            C
            ";
            string expected = "ACB";
            Assert.Equal(expected, jm.SortJobs(input));
        }

        [Fact]
        public void TestDependencyCase2()
        {
            JobManager jm = new JobManager();
            string input = @"A
            B => C
            C => F
            D => A
            E => B
            F
            ";
            string expected = "ADFCBE";
            Assert.Equal(expected, jm.SortJobs(input));
        }

        [Fact]
        public void TestDependencyCaseSelfShouldFail()
        {
            JobManager jm = new JobManager();
            string input = @"
            A
            B
            C => C
            ";
            string expected = "ADFCBE";
            try
            {
                Assert.Equal(expected, jm.SortJobs(input));
            }
            catch (Job.SelfReferenceException)
            {
            }
            catch (Exception)
            {
                Assert.False(true);
            }
        }

        [Fact]
        public void TestSimpleCycle()
        {
            JobManager jm = new JobManager();
            string input = @"
            A
            B => A
            A => B
            ";
            string expected = "AB";
            try
            {
                Assert.Equal(expected, jm.SortJobs(input));
            }
            catch (Job.SelfReferenceException)
            {
            }
            catch (Exception)
            {
                Assert.False(true);
            }
        }


        [Fact]
        public void TestDependencyCycle()
        {
            JobManager jm = new JobManager();
            string input = @"
            A
            B => C
            C => F
            D => A
            E 
            F => B
            ";
            string expected = "ADFCBE";
            try
            {
                Assert.Equal(expected, jm.SortJobs(input));
            }
            catch (Job.SelfReferenceException)
            {
            }
            catch (Exception)
            {
                Assert.False(true);
            }
        }
    }
}
