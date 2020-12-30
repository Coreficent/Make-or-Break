﻿namespace Coreficent.Logic
{
    using Coreficent.Utility;
    using System.Collections.Generic;
    using UnityEngine;

    public class Artifact : MonoBehaviour
    {
        [SerializeField] private List<string> Predicates = new List<string>();

        public string CurrentState = "Dormancy";
        public string NextState = "Dormancy";

        public bool Advanced = true;

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
            if (!Advanced)
            {
                foreach (Predicate predicate in _predicates)
                {
                    if (predicate.CurrentState == CurrentState && predicate.MeetConditions)
                    {
                        NextState = predicate.NextState;
                        return true;
                    }
                }
            }

            return false;
        }

        public void Advance()
        {
            Advanced = true;
            Executor.Singleton.Transitioning = true;
            _animator.SetBool(_artifact.NextState, true);
        }

        public void Originate()
        {
            Executor.Singleton.Transitioning = true;
            _animator.SetBool(_artifact.NextState, true);
        }
    }
}

