﻿namespace Coreficent.Cursor
{
    using Coreficent.Logic;
    using Coreficent.Utility;
    using System.Collections.Generic;
    using System.Linq;
    using UnityEngine;

    public class CursorController : MonoBehaviour
    {
        public static CursorController Singleton;

        [SerializeField] private Texture2D _readyCursor;
        [SerializeField] private Texture2D _busyCursor;

        [SerializeField] private CursorMode cursorMode = CursorMode.Auto;

        private Queue<bool> _averager = new Queue<bool>();
        private int _averageSize = 5;
        private bool _cursorOn = true;
        public bool CursorOn
        {
            get { return _cursorOn; }
            set
            {
                _cursorOn = value;
                DebugLogger.Log("cursor", _cursorOn);
                Cursor.SetCursor(_cursorOn ? _readyCursor : _busyCursor, Vector2.zero, cursorMode);
            }
        }

        protected void Start()
        {
            Singleton = this;

            SanityCheck.Check(this, _readyCursor, _busyCursor, Singleton);

            for (var i = 0; i < _averageSize; ++i)
            {
                _averager.Enqueue(Executor.Singleton.Transitioning);
            }
        }

        protected void Update2222()
        {
            _averager.Dequeue();
            _averager.Enqueue(Executor.Singleton.Transitioning);

            foreach (bool transitioning in _averager)
            {
                if (transitioning)
                {
                    Cursor.SetCursor(_busyCursor, Vector2.zero, cursorMode);
                    return;
                }
            }

            Cursor.SetCursor(_readyCursor, Vector2.zero, cursorMode);
        }
    }

}
