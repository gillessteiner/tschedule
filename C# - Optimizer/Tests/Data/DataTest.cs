using Microsoft.VisualStudio.TestTools.UnitTesting;
using TrainScheduler.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace TrainScheduler.Data.Tests
{
    [TestClass()]
    public class DataTest
    {
        public TestContext TestContext { get; set; }

        [TestMethod()]
        public void FromToJsonTest()
        {
            var sampleScenario = new ProblemInstance("SBB_challenge_sample_scenario_with_routing_alternatives", -1254734547);
            sampleScenario.ServiceIntentions.Add(new ServiceIntention(111, 111));
            sampleScenario.ServiceIntentions.Add(new ServiceIntention(113, 113));


            TestContext.WriteLine(sampleScenario.ToJson());

            string sampleFile = @"C:\Users\gille\Documents\Development\Cff_Schedule\tschedule\C# - Optimizer\TrainScheduler\Samples\sample_scenario.json";
            string sampleScenarioJson = File.ReadAllText(sampleFile);

            var readSampleScenario = new ProblemInstance();
            readSampleScenario.FromJson(sampleScenarioJson);

            // Compare instances
            Assert.AreEqual(sampleScenario.Label, readSampleScenario.Label);
            Assert.AreEqual(sampleScenario.Hash, readSampleScenario.Hash);
            Assert.AreEqual(sampleScenario.ServiceIntentions.Count, readSampleScenario.ServiceIntentions.Count);
        }
    }
}