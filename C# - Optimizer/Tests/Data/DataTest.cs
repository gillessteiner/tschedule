using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TrainScheduler.InputOutputDataModel;

namespace Tests.Data
{
    [TestClass()]
    public class DataTest
    {
        public TestContext TestContext { get; set; }

        [TestMethod()]
        public void ReadFromJsonTest()
        {
            try
            {
                var sampleFile = @"C:\Users\gille\Documents\Development\Cff_Schedule\tschedule\C# - Optimizer\TrainScheduler\Samples\sample_scenario.json";
                var json = File.ReadAllText(sampleFile);

                var sampleScenario = new ProblemInstance();
                sampleScenario.FromJson(json);

                Assert.AreEqual(sampleScenario.Hash, -1254734547);
                Assert.AreEqual(sampleScenario.ServiceIntentions.Count, 2);
                Assert.AreEqual(sampleScenario.Resources.Count, 13);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [TestMethod()]
        public void SectionRequirementTest()
        {
            var json =
                @"{
                    ""sequence_number"": 2,
                    ""section_marker"": ""B"",
                    ""type"": ""halt"",
                    ""min_stopping_time"": ""PT3M"",
                    ""entry_delay_weight"": 1,
                    ""exit_earliest"": ""08:30:00"",
                    ""exit_delay_weight"": 1,
                    ""connections"": null
                  }";

            try
            {
                var obj = new SectionRequirement();
                obj.FromJson(json);
                Assert.AreEqual(obj.SequenceNumber, 2);
                Assert.AreEqual(obj.SectionMarker, "B");
                Assert.AreEqual(obj.MinStoppingTime, IsoDuration.FromString("PT3M"));
                Assert.AreEqual(obj.EntryDelayWeight, 1.0);
                Assert.AreEqual(obj.ExitEarliest, DateTime.Parse("08:30:00"));
                Assert.AreEqual(obj.ExitDelayWeight, 1.0);
                Assert.IsNull(obj.Connections);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [TestMethod()]
        public void RouteSectionTest()
        {
            var json =
                @"{
					""sequence_number"": 1,
                    ""route_alternative_marker_at_exit"": [
                    ""M1""
                        ],
                    ""section_marker"": [
                    ""A""
                        ],
                    ""resource_occupations"": [
                    {
                        ""resource"": ""A1"",
                        ""occupation_direction"": null

                    },
                    {
                        ""resource"": ""AB"",
                        ""occupation_direction"": null
                    }
                    ],
                    ""penalty"": null,
                    ""starting_point"": ""A"",
                    ""minimum_running_time"": ""PT53S"",
                    ""ending_point"": ""A""
                  }";

            try
            {
                var obj = new RouteSection();
                obj.FromJson(json);
                Assert.AreEqual(obj.SequenceNumber, 1);
                Assert.AreEqual(obj.RouteAlternativeMarkersAtExit[0], "M1");
                Assert.AreEqual(obj.SectionMarkers[0], "A");


            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }
    }
}