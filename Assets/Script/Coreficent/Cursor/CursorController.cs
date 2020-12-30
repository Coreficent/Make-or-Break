namespace Coreficent.Cursor
{
    using Coreficent.Logic;
    using Coreficent.Utility;
    using UnityEngine;

    public class CursorController : MonoBehaviour
    {
        [SerializeField] private Texture2D _readyCursor;
        [SerializeField] private Texture2D _busyCursor;

        [SerializeField] private CursorMode cursorMode = CursorMode.Auto;

        protected void Start()
        {
            SanityCheck.Check(this, _readyCursor, _busyCursor);
        }

        protected void Update()
        {
            Cursor.SetCursor(Executor.Singleton.TransitionComplete ? _readyCursor : _busyCursor, Vector2.zero, cursorMode);
        }
    }

}
