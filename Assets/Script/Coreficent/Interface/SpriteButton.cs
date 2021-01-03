namespace Coreficent.Interface
{
    using Coreficent.Utility;
    using UnityEngine;
    using UnityEngine.EventSystems;
    using UnityEngine.UI;

    public class SpriteButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
    {
        protected Animator _animator;
        protected Button _button;
        
        protected virtual void Start()
        {
            _animator = GetComponent<Animator>();
            _button = GetComponent<Button>();

            SanityCheck.Check(this, _animator);

            DebugLogger.Start(this);
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (_button.interactable)
            {
                DebugLogger.Log("on enter sprite button");
                EnterAnimation();
            }
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            DebugLogger.Log("on exit sprite button");
            ExitAnimation();
        }

        public virtual void OnPointerClick(PointerEventData eventData)
        {
            switch (eventData.button)
            {
                case (PointerEventData.InputButton.Left):
                    Debug.Log("Left click");
                    ExitAnimation();
                    _button.interactable = false;
                    break;
                case (PointerEventData.InputButton.Middle):
                    Debug.Log("Middle click");
                    break;
                case (PointerEventData.InputButton.Right):
                    Debug.Log("Right click");
                    break;
                default:
                    Debug.Log("Unexpected mouse click");
                    break;
            }
        }

        private void EnterAnimation()
        {
            _animator.SetBool("Enter", true);
            _animator.SetBool("Exit", false);
        }

        private void ExitAnimation()
        {
            _animator.SetBool("Enter", false);
            _animator.SetBool("Exit", true);
        }
    }
}
