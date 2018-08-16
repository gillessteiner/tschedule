using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TrainScheduler.Data;
using TrainScheduler.Utils;
using Math = TrainScheduler.Utils.Math;

namespace TrainScheduler.Solver
{
    public class AssignAndCorrectSolver : BaseSolver
    {
        private Solution CurrentSolution { get;  set; }

        public override void Init(ProblemInstance problem)
        {
            Log(HorizontalLine);
            Log("Initializing AssignAndCorrect Solver ...", Utils.Logging.LogEventArgs.MessageType.Info, false);
            CurrentProblem = problem;
            CurrentSolution = new Solution(problem);
            Log(" done.", Utils.Logging.LogEventArgs.MessageType.Success);
        }

        public override Solution Run()
        {
            var usedResources = new UsedResourceCollection();
            Log($"Assign a path for each train ... ", Utils.Logging.LogEventArgs.MessageType.Info, false);
            if (CurrentSolution.AssignPaths()) {
                Log($"done.", Utils.Logging.LogEventArgs.MessageType.Success);
            }
            else {
                Log($"failed.", Utils.Logging.LogEventArgs.MessageType.Error);
            }

            do {
                Log(HorizontalLine);
                Log($"Iteration {++Iteration}");
                
                Log($"Loop on trains and assign entry-exit times ... ");
                foreach (var trainRun in CurrentSolution.TrainRunsDic.Values
                    .OrderBy(tr => tr.Train.MinEntryEarliest).ThenBy(tr => tr.Train.MaxDelayPenalty))
                {
                    for (int k = 0; k < trainRun.TrainRunSections.Count; ++k)
                    {
                        var section = trainRun.TrainRunSections[k];

                        // ------------------------------------------------
                        // Is there a min entry/exit time 
                        var minEntryTime = DateTime.Today;
                        var minExitTime = DateTime.Today;
                        var minStoppingTime = TimeSpan.Zero;

                        if (section.SectionMarker != null &&
                            trainRun.Train.SectionRequirementsDic.ContainsKey(section.SectionMarker)) {
                            if (trainRun.Train.SectionRequirementsDic[section.SectionMarker].EntryEarliest.HasValue) {
                                minEntryTime = trainRun.Train.SectionRequirementsDic[section.SectionMarker].EntryEarliest.Value;
                            }

                            if (trainRun.Train.SectionRequirementsDic[section.SectionMarker].ExitEarliest.HasValue)
                            {
                                minExitTime = trainRun.Train.SectionRequirementsDic[section.SectionMarker].ExitEarliest.Value;
                            }

                            minStoppingTime = trainRun.Train.SectionRequirementsDic[section.SectionMarker].MinStoppingTime;
                        }

                        // ------------------------------------------------
                        // Set section occupation iteratively
                        const int maxIter = 100;
                        int iter = 0;
                        bool shift = false;
                        do {
                            if (iter > 0)
                            {
                                Log($"SubIter {iter}, Train {trainRun.ServiceIntentionId}, section {section.SequenceNumber} adjust entry - exit {section.EntryTime} - {section.ExitTime}");
                            }

                            section.EntryTime = Math.Max(minEntryTime,
                                (k > 0) ? trainRun.TrainRunSections[k - 1].ExitTime : section.EntryTime);

                            section.ExitTime = Math.Max(minExitTime,
                                    section.EntryTime + section.UnderlyingEdge.MinimumRunningTime + minStoppingTime);

                            shift = false;
                           
                            // Check resource occupation 
                            foreach (var resId in section.UnderlyingEdge.ResourceOccupations)
                            {
                             
                                foreach (var usage in usedResources.UsageByOtherThan(resId, trainRun.ServiceIntentionId))
                                {
                                    if (usage.Item1 == "560" && trainRun.ServiceIntentionId == "466" && resId == "MG-TIEF_122" && section.SequenceNumber == 240) {

                                    }
                                    if (Math.Intersect(section.EntryTime, section.ExitTime, usage.Item3, usage.Item4))
                                    {
                                        // Shift entry time
                                        shift = true;
                                        section.EntryTime = usage.Item4;
                                        break;
                                    }
                                }
                            }
                            
                            // Make sure exit time of previous section is consistent with entry time
                            if (k > 0 && trainRun.TrainRunSections[k - 1].ExitTime != section.EntryTime)
                            {
                                shift = true;

                                // Adjust exit time of previous section
                                trainRun.TrainRunSections[k - 1].ExitTime = section.EntryTime;

                                // Adjust resource occupation of previous section
                                foreach (var resId in trainRun.TrainRunSections[k - 1].UnderlyingEdge.ResourceOccupations) {
                                    var resource = CurrentProblem.TryGetResource(resId);
                                    if (resource != null) {
                                        usedResources.Add(resId, trainRun.ServiceIntentionId, 
                                            trainRun.TrainRunSections[k - 1].SequenceNumber, 
                                            trainRun.TrainRunSections[k - 1].EntryTime,
                                            trainRun.TrainRunSections[k - 1].ExitTime + resource.ReleaseTime);
                                    }
                                }
                            }

                        } while (shift && ++iter < maxIter);

                        // ------------------------------------------------
                        // Add current resource occupation
                        foreach (var resId in section.UnderlyingEdge.ResourceOccupations)
                        {
                            var resource = CurrentProblem.TryGetResource(resId);
                            if (resource != null)
                            {
                                usedResources.Add(resId, trainRun.ServiceIntentionId, section.SequenceNumber,
                                    section.EntryTime, section.ExitTime + resource.ReleaseTime);
                            }

                            if ((resId == "MG-TIEF_122") && (trainRun.ServiceIntentionId == "560" || trainRun.ServiceIntentionId == "466"))
                            {

                            }
                        }
                    }
                }

                var validation = CurrentSolution.Validate();
                if (CurrentSolution.IsAdmissible) // Only consider admissible solution 
                {
                    Log($"Compute objective function ... ");
                    CurrentSolution.ComputeObjectiveFunction();
                    CompareWithBest(CurrentSolution);
                }
                else
                {
                    Log(validation);
                }

            } while (Iteration < MaxIteration && ( BestSolution == null || !BestSolution.IsOptimal));

            return BestSolution;
        }
    }
}
