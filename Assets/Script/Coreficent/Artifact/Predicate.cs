namespace Coreficent.Artifact
{
    using Coreficent.Utility;
    using System;
    using System.Collections.Generic;

    public class Predicate
    {
        public readonly string CurrentState;
        public readonly List<Tuple<string, string>> Conditions;
        public readonly string NextState;

        public bool MeetConditions
        {
            get
            {
                bool meetConditions = true;

                foreach (Tuple<string, string> condition in Conditions)
                {
                    string artifact = condition.Item1;
                    string state = condition.Item2;

                    DebugLogger.Log("condition artifact", artifact);
                    DebugLogger.Log("condition state", state);

                    DebugLogger.ToDo("error handling in artifact");

                    if (!Artifact.ArtifactLookup.ContainsKey(artifact) || Artifact.ArtifactLookup[artifact].CurrentState != state)
                    {
                        meetConditions = false;
                    }
                }
                return meetConditions;
            }
        }

        public Predicate(string predicate)
        {
            string trim = predicate.Replace(" ", string.Empty);
            DebugLogger.Log("predicate", trim);

            string currentState = trim.Substring(trim.IndexOf('(') + 1, trim.IndexOf(',') - trim.IndexOf('(') - 1);
            DebugLogger.Log("currentState", currentState);

            string conditionList = trim.Substring(trim.IndexOf('[') + 1, trim.IndexOf(']') - trim.IndexOf('[') - 1);

            List<Tuple<string, string>> conditions = new List<Tuple<string, string>>();

            DebugLogger.Log("conditions" + ":");

            foreach (string pair in conditionList.Split(','))
            {
                if (pair != string.Empty)
                {
                    string[] arrayPair = pair.Split(':');
                    Tuple<string, string> tuple = new Tuple<string, string>(arrayPair[0], arrayPair[1]);

                    DebugLogger.Log(tuple.Item1 + ":" + tuple.Item2);
                    conditions.Add(tuple);
                }
            }

            string nextState = trim.Substring(trim.IndexOf('>') + 1);
            DebugLogger.Log("nextState", nextState);

            CurrentState = currentState;
            Conditions = conditions;
            NextState = nextState;
        }
    }
}
