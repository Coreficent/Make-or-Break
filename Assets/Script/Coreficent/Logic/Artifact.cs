namespace Coreficent.Logic
{
    using Coreficent.Cursor;
    using Coreficent.Utility;
    using System.Collections.Generic;
    using UnityEngine;

    public class Artifact : MonoBehaviour
    {
        public List<string> Predicates = new List<string>();

        public string CurrentState = "HideLoop";
        public string NextState = "HideLoop";

        public bool Transitioning = false;
        public bool Advanced = true;

        // world coordinates
        public float LocationOffsetMinimum = 0.0f;
        public float LocationOffsetMaximum = 0.0f;

        // degrees
        public float AngleOffsetMinimum = 0.0f;
        public float AngleOffsetMaximum = 0.0f;

        // normalized vector
        public float GroundOffsetMinimum = 0.0f;
        public float GroundOffsetMaximum = 0.0f;

        // scale
        public float SizeOffsetMinimum = 0.0f;
        public float SizeOffsetMaximum = 0.0f;


        // the format is: (CurrentState, [ArtifactA:State, ArtifactA:State]) -> NextState
        private readonly List<Predicate> _predicates = new List<Predicate>();

        private Animator _animator;
        private Artifact _artifact;

        private float _planetRadius = 5.0f;

        private void Start()
        {
            DebugLogger.ToDo("remove following line after removing conceptual items");

            if (transform.Find("Display") && transform.Find("Display").GetComponent<Animator>())
            {
                _animator = transform.Find("Display").GetComponent<Animator>();
            }
            else
            {
                _animator = GetComponent<Animator>();
            }

            _artifact = GetComponent<Artifact>();

            SanityCheck.Check(this, _animator, _artifact);

            DebugLogger.ToDo("error handling in artifact");


            DebugLogger.Log("Parsing Predicates for" + " " + name);

            foreach (string i in Predicates)
            {
                _predicates.Add(new Predicate(i));
            }

            DebugLogger.Log("Finishing Predicates for" + " " + name);



            // location
            transform.position += new Vector3(Random.Range(LocationOffsetMinimum, LocationOffsetMaximum), Random.Range(LocationOffsetMinimum, LocationOffsetMaximum), Random.Range(LocationOffsetMinimum, LocationOffsetMaximum));

            // direction
            Vector3 to = (transform.position - Vector3.zero).normalized;
            transform.rotation = Quaternion.FromToRotation(transform.up.normalized, to) * transform.rotation;
            transform.eulerAngles += new Vector3(Random.Range(AngleOffsetMinimum, AngleOffsetMaximum), Random.Range(AngleOffsetMinimum, AngleOffsetMaximum), Random.Range(AngleOffsetMinimum, AngleOffsetMaximum));

            // position
            transform.position = transform.position.normalized * _planetRadius * (1.0f + Random.Range(GroundOffsetMinimum, GroundOffsetMaximum));

            // scale
            float scale = Random.Range(SizeOffsetMinimum + 1.0f, SizeOffsetMaximum + 1.0f);
            transform.localScale = new Vector3(scale, scale, scale);

            DebugLogger.Start(this);
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
            Transitioning = true;
            CursorController.Singleton.CursorOn = false;
            _animator.SetBool(_artifact.NextState, true);
        }

        public void Finish()
        {
            DebugLogger.Log("finish");
            CurrentState = NextState;
            Transitioning = false;

            if (Executor.Singleton.Transitioning)
            {
                Executor.Singleton.Run();
            }
            else
            {
                CursorController.Singleton.CursorOn = true;
            }

            //Executor.Singleton.Run();
            //if (!Executor.Singleton.Transitioning)
            //{
            //    CursorController.Singleton.CursorOn = true;
            //}
        }
    }
}

