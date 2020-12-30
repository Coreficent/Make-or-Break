namespace Coreficent.Interface
{
    using Coreficent.Utility;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.EventSystems;

    public class SpriteButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        private Animator _animator;

        private void Start()
        {
            _animator = GetComponent<Animator>();

            SanityCheck.Check(this, _animator);
        }

        private void Update()
        {

        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            DebugLogger.Log("on enter sprite button");
            _animator.SetBool("Enter", true);
            _animator.SetBool("Exit", false);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            DebugLogger.Log("on exit sprite button");
            _animator.SetBool("Enter", false);
            _animator.SetBool("Exit", true);
        }
    }
}
