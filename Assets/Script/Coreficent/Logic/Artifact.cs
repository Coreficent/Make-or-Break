namespace Coreficent.Logic
{
    using Coreficent.Utility;
    using System.Collections.Generic;
    using UnityEngine;

    public class Artifact : MonoBehaviour
    {
        [SerializeField] private List<string> Predicates = new List<string>();

        public string CurrentState = "Dormancy";
        public string NextState = "Dormancy";

        // the format is: (CurrentState, [ArtifactA:State, ArtifactA:State]) -> NextState
        private readonly List<Predicate> _predicates = new List<Predicate>();

        private Animator _animator;
        private Artifact _artifact;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _artifact = GetComponent<Artifact>();

            SanityCheck.Check(this, _animator, _artifact);

            DebugLogger.ToDo("error handling in artifact");


            DebugLogger.Log("Parsing Predicates for" + " " + name);

            foreach (string i in Predicates)
            {
                _predicates.Add(new Predicate(i));
            }

            DebugLogger.Log("Finishing Predicates for" + " " + name);

            DebugLogger.Awake(this);
        }

        public bool CanAdvance()
        {
            foreach (Predicate predicate in _predicates)
            {
                if (predicate.CurrentState == CurrentState && predicate.MeetConditions)
                {
                    NextState = predicate.NextState;
                    return true;
                }
            }

            return false;
        }

        public void Advance()
        {
            _animator.SetBool(_artifact.NextState, true);
        }
    }
}

