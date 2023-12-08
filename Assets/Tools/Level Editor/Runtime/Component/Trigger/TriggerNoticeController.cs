using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Tools.Level_Editor.Runtime.Component.Trigger
{
    public enum TriggerNoticeState
    {
        None,
        Far,
        Near
    }
    
    public class TriggerNoticeController : MonoBehaviour
    {
        [SerializeField] private CanvasGroup farNotice;
        [SerializeField] private CanvasGroup nearNotice;

        private TriggerNoticeState state;
        
        public TriggerNoticeState State
        {
            get => state;
            set
            {
                if (state != value)
                {
                    state = value;
                    
                    farNotice.DOKill();
                    nearNotice.DOKill();
                    
                    switch (value)
                    {
                        case TriggerNoticeState.None:
                            farNotice.DOFade(0f, 0.2f);
                            nearNotice.DOFade(0f, 0.2f);
                            break;
                        case TriggerNoticeState.Far:
                            farNotice.DOFade(1f, 0.2f);
                            nearNotice.DOFade(0f, 0.2f);
                            break;
                        case TriggerNoticeState.Near:
                            farNotice.DOFade(0f, 0.2f);
                            nearNotice.DOFade(1f, 0.2f);
                            break;
                    }
                }
            }
        }

        private void OnDisable()
        {
            farNotice.DOKill();
            nearNotice.DOKill();
        }
    }
}
