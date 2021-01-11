namespace Coreficent.Main
{
    using Coreficent.Controller;
    using Coreficent.Interface;
    using Coreficent.Logic;
    using Coreficent.Utility;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.UI;

    public class Main : MonoBehaviour
    {
        public enum GameState
        {
            Initialize,
            Introduce,
            Run
        }

        [SerializeField] private Camera _camera;

        [SerializeField] private float _gameSpeed = 1.0f;
        [SerializeField] private List<Transform> _artifactContainer = new List<Transform>();
        [SerializeField] private List<Button> _buttons = new List<Button>();

        public GameState State = GameState.Initialize;

        bool[] _buttonActivations = null;

        private TimeController _timeController = new TimeController();

        private Vector3 _cameraPositionFinal = new Vector3(0.0f, 0.0f, -15.0f);
        private Vector3 _cameraPositionInitial = Vector3.zero;

        private Quaternion _cameraRotaionInitial = Quaternion.identity;

        private bool _initialized = false;

        // initializer
        private void Start()
        {


            SanityCheck.Check(this, _artifactContainer, _gameSpeed, _camera, _buttons);

            _buttonActivations = new bool[_buttons.Count];

            _cameraPositionInitial = _camera.transform.position;
            _cameraRotaionInitial = _camera.transform.rotation;


            Executor.Singleton.Initialize(_artifactContainer);

            Time.timeScale = _gameSpeed;

            _timeController.Reset();



            

            DebugLogger.Start(this);
        }

        // game loop
        private void Update()
        {
            switch (State)
            {
                case GameState.Initialize:

                    if (!_initialized)
                    {
                        for (int i = 0; i < _buttons.Count; ++i)
                        {
                            _buttons[i].image.color = Color.clear;
                            _buttonActivations[i] = _buttons[i].interactable;
                            _buttons[i].interactable = false;
                        }
                        _initialized = true;
                    }

                    if (Restart.Restarted)
                    {
                        _camera.transform.position = _cameraPositionFinal;
                        _camera.transform.rotation = Quaternion.identity;

                        ActivateButtons();

                        GoTo(GameState.Run);
                    }

                    if (Input.anyKeyDown)
                    {
                        GoTo(GameState.Introduce);
                    }

                    break;

                case GameState.Introduce:
                    float introductionTime = 5.0f;

                    _camera.transform.position = Vector3.Lerp(_cameraPositionInitial, _cameraPositionFinal, _timeController.Progress(introductionTime));

                    _camera.transform.rotation = Quaternion.Lerp(_cameraRotaionInitial, Quaternion.identity, _timeController.Progress(introductionTime));

                    foreach (Button button in _buttons)
                    {
                        button.image.color = Color.Lerp(Color.clear, Color.white, _timeController.Progress(introductionTime) * 2.0f - 1.0f);
                    }

                    if (_timeController.Passed(introductionTime))
                    {
                        ActivateButtons();

                        GoTo(GameState.Run);
                    }

                    break;

                case GameState.Run:
                    break;

                default:
                    DebugLogger.Warn("unexpected game state");
                    break;
            }
        }

        private void GoTo(GameState state)
        {
            _timeController.Reset();
            State = state;
        }

        private void ActivateButtons()
        {
            for (int i = 0; i < _buttons.Count; ++i)
            {
                _buttons[i].image.color = Color.white;
                _buttons[i].interactable = _buttonActivations[i];
            }
        }
    }
}
