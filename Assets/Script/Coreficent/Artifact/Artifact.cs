namespace Coreficent.Artifact
{
    using Coreficent.Utility;
    using System;
    using System.Collections.Generic;
    using UnityEngine;

    public class Artifact : MonoBehaviour
    {
        public List<string> Predicates = new List<string>();

        public bool Transitioning = false;
        public string CurrentState = "Origin";
        public string NextState = "Origin";

        // a list where each entry is the current state, required conditions, and the next state
        private List<Tuple<string, List<Tuple<string, string>>, string>> _logic = new List<Tuple<string, List<Tuple<string, string>>, string>>();

        public void ParsePredicates()
        {
            // TODO error handling
            // the format is: (CurrentState, [ArtifactA:State, ArtifactA:State]) -> NextState

            DebugLogger.Log("Parsing Predicates for" + " " + name);

            foreach (string i in Predicates)
            {
                string trim = i.Replace(" ", string.Empty);
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

                _logic.Add(new Tuple<string, List<Tuple<string, string>>, string>(currentState, conditions, nextState));
            }

            DebugLogger.Log("Finishing Predicates for" + " " + name);
        }

        public bool CanAdvance(Dictionary<string, GameObject> _artifactLookup)
        {
            foreach (Tuple<string, List<Tuple<string, string>>, string> i in _logic)
            {
                string currentState = i.Item1;
                if (currentState == CurrentState)
                {
                    bool satisifiedAll = true;
                    List<Tuple<string, string>> conditions = i.Item2;
                    foreach (Tuple<string, string> condition in conditions)
                    {
                        string artifact = condition.Item1;
                        string state = condition.Item2;

                        DebugLogger.Log("condition artifact", artifact);
                        DebugLogger.Log("condition state", state);
                        // TODO error handling
                        if (!_artifactLookup.ContainsKey(artifact) || _artifactLookup[artifact].GetComponent<Artifact>().CurrentState != state)
                        {
                            satisifiedAll = false;
                        }
                    }
                    if (satisifiedAll)
                    {
                        string nextState = i.Item3;
                        NextState = nextState;
                        return true;
                    }
                }
            }
            return false;
        }

        public void Advance()
        {
            Transitioning = true;
            GetComponent<Animator>().SetBool(GetComponent<Artifact>().NextState, true);
            Debug.Log("advance!");
        }
    }
}

