using System;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Controller.UI
{
    public class ResponseButton : MonoBehaviour,
        IPointerEnterHandler,
        IPointerExitHandler,
        IPointerClickHandler
    {
        public TMP_Text text;
        public Image background;
        public CanvasGroup rootCanvasGroup;
        public CanvasGroup ringWhite;
        public CanvasGroup glow;

        [HideInInspector]
        public UnityEvent onClick = new UnityEvent();

        private bool enable = true;
        private Sequence sequence;

        private void OnEnable()
        {
            enable = true;
            
            ringWhite.alpha = 0f;
            glow.alpha = 0f;

            rootCanvasGroup.alpha = 0f;
            rootCanvasGroup.DOFade(1f, 0.3f);
        }

        private void OnDisable()
        {
            rootCanvasGroup.DOKill();
            background.DOKill();
            ringWhite.DOKill();
            glow.DOKill();
            
            sequence.Kill();
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (!enable)
            {
                return;
            }
            
            sequence.Kill();
            ringWhite.DOKill();
            
            ringWhite.DOFade(1f, 0.1f); 
            sequence = DOTween.Sequence()
                .Append(glow.DOFade(1f, 1f))
                .Append(glow.DOFade(0f, 1f))
                .SetLoops(-1); 
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (!enable)
            {
                return;
            }
            
            sequence.Kill();
            ringWhite.DOKill();
            glow.DOKill();
            
            ringWhite.DOFade(0f, 0.1f);
            glow.DOFade(0f, 0.1f);
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (!enable)
            {
                return;
            }

            enable = false;
            
            sequence.Kill();
            ringWhite.DOKill();
            glow.DOKill();
            background.DOKill();
            
            ringWhite.DOFade(0f, 0.1f);
            glow.DOFade(0f, 0.1f);

            sequence = DOTween.Sequence()
                .Append(background.DOColor(new Color(1f, 1f, 1f, 200f), 0.2f))
                .AppendCallback(() => onClick?.Invoke())
                .Append(background.DOColor(new Color(0f, 0f, 0f, 200f), 0.2f));
        }
    }
}