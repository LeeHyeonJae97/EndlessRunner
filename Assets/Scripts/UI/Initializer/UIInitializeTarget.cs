using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using NaughtyAttributes;
using Extension;

namespace UIExtension
{
    public class UIInitializeTarget : MonoBehaviour
    {
        private enum ActivateTarget { Canvas, Self }
        private enum CanvasActivateType { Enable, SetActive }

        [SerializeField] private bool _overrideInitPos;
        [ShowIf("_overrideInitPos")]
        [Indent(1)]
        [SerializeField] private Vector2 _anchoredPos;
        [SerializeField] private bool _setActiveOnAwake;
        [ShowIf("_setActiveOnAwake")]
        [Indent(1)]
        [SerializeField] private bool _active;
        [ShowIf("_setActiveOnAwake")]
        [Indent(1)]
        [SerializeField] private ActivateTarget _activateTarget;
        [ShowIf(EConditionOperator.And, "_setActiveOnAwake", "IsActivateTargetCanvas")]
        [Indent(1)]
        [SerializeField] private CanvasActivateType _activateType;

        private bool IsActivateTargetCanvas => _activateTarget == ActivateTarget.Canvas;

        private void OnValidate()
        {
            if (!_overrideInitPos)
                _anchoredPos = Vector2.zero;

            if (!_setActiveOnAwake)
            {
                _active = false;
                _activateTarget = ActivateTarget.Canvas;
            }
        }

        public void Init()
        {
            GetComponent<RectTransform>().anchoredPosition = _anchoredPos;
            if (_setActiveOnAwake)
            {
                switch (_activateTarget)
                {
                    case ActivateTarget.Canvas:
                        if (_activateType == CanvasActivateType.Enable)
                        {
                            GetComponentsInParent<Canvas>(true)[0].enabled = _active;
                        }
                        else if (_activateType == CanvasActivateType.SetActive)
                        {
                            GetComponentsInParent<Canvas>(true)[0].SetActive(_active);
                        }
                        break;

                    case ActivateTarget.Self:
                        gameObject.SetActive(_active);
                        break;
                }
            }

            Destroy(this);
        }

        [Button("Load", EButtonEnableMode.Editor, 10)]
        public void Load()
        {
            Undo.RecordObject(gameObject.transform, "Load");
            GetComponent<RectTransform>().anchoredPosition = _anchoredPos;
        }

        [ShowIf("_overrideInitPos")]
        [Button("Save", EButtonEnableMode.Editor)]
        public void Save()
        {
            Undo.RecordObject(this, "Save");
            _anchoredPos = GetComponent<RectTransform>().anchoredPosition;
        }
    }
}
