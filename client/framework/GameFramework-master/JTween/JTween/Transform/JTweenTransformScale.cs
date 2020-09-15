﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DG.Tweening;
using LitJson;
using UnityEngine;

namespace JTween.Transform {
    public class JTweenTransformScale : JTweenBase {
        public enum ScaleTypeEnum {
            Scale = 0,
            ScaleV = 1,
            ScaleX = 2,
            ScaleY = 3,
            ScaleZ = 4,
        }
        private ScaleTypeEnum m_ScaleType = ScaleTypeEnum.Scale;
        private Vector3 m_beginScale = Vector3.zero;
        private Vector3 m_toScale = Vector3.zero;
        private float m_toScaleV = 0;
        private float m_toScaleX = 0;
        private float m_toScaleY = 0;
        private float m_toScaleZ = 0;
        private UnityEngine.Transform m_Transform;

        public JTweenTransformScale() {
            m_tweenType = (int)JTweenTransform.Scale;
            m_tweenElement = JTweenElement.Transform;
        }

        public ScaleTypeEnum ScaleType {
            get {
                return m_ScaleType;
            }
            set {
                m_ScaleType = value;
            }
        }

        public Vector3 BeginScale {
            get {
                return m_beginScale;
            }
            set {
                m_beginScale = value;
                if (m_beginScale != null) {
                    m_Transform.localScale = m_beginScale;
                } // end if
            }
        }

        public Vector3 ToScale {
            get {
                return m_toScale;
            }
            set {
                m_toScale = value;
            }
        }

        public float ToScaleV {
            get {
                return m_toScaleV;
            }
            set {
                m_toScaleV = value;
            }
        }

        public float ToMoveX {
            get {
                return m_toScaleX;
            }
            set {
                m_toScaleX = value;
            }
        }

        public float ToMoveY {
            get {
                return m_toScaleY;
            }
            set {
                m_toScaleY = value;
            }
        }

        public float ToMoveZ {
            get {
                return m_toScaleZ;
            }
            set {
                m_toScaleZ = value;
            }
        }

        protected override void Init() {
            if (null == m_target) return;
            // end if
            m_Transform = m_target.GetComponent<UnityEngine.Transform>();
            if (null == m_Transform) return;
            // end if
            m_beginScale = m_Transform.localScale;
        }

        protected override Tween DOPlay() {
            if (null == m_Transform) return null;
            // end if
            switch (m_ScaleType) {
                case ScaleTypeEnum.Scale:
                    return m_Transform.DOScale(m_toScale, m_duration);
                case ScaleTypeEnum.ScaleV:
                    return m_Transform.DOScale(m_toScaleV, m_duration);
                case ScaleTypeEnum.ScaleX:
                    return m_Transform.DOScaleX(m_toScaleX, m_duration);
                case ScaleTypeEnum.ScaleY:
                    return m_Transform.DOScaleY(m_toScaleY, m_duration);
                case ScaleTypeEnum.ScaleZ:
                    return m_Transform.DOScaleZ(m_toScaleZ, m_duration);
                default: return null;
            } // end switch
        }

        public override void Restore() {
            if (null == m_Transform) return;
            // end if
            m_Transform.localScale = m_beginScale;
        }

        protected override void JsonTo(JsonData json) {
            if (json.Contains("beginScale")) BeginScale = JTweenUtils.JsonToVector3(json["beginScale"]);
            // end if
            if (json.Contains("scale")) {
                m_ScaleType = ScaleTypeEnum.Scale;
                m_toScale = JTweenUtils.JsonToVector3(json["scale"]);
            } else if (json.Contains("scaleV")) {
                m_ScaleType = ScaleTypeEnum.ScaleV;
                m_toScaleV = (float)json["scaleV"];
            } else if (json.Contains("scaleX")) {
                m_ScaleType = ScaleTypeEnum.ScaleX;
                m_toScaleX = (float)json["scaleX"];
            } else if (json.Contains("scaleY")) {
                m_ScaleType = ScaleTypeEnum.ScaleY;
                m_toScaleY = (float)json["scaleY"];
            } else if (json.Contains("scaleZ")) {
                m_ScaleType = ScaleTypeEnum.ScaleZ;
                m_toScaleZ = (float)json["scaleZ"];
            } else {
                Debug.LogError(GetType().FullName + " JsonTo MoveType is null");
            } // end if
        }

        protected override void ToJson(ref JsonData json) {
            json["beginScale"] = JTweenUtils.Vector3Json(m_beginScale);
            switch (m_ScaleType) {
                case ScaleTypeEnum.Scale:
                    json["scale"] = JTweenUtils.Vector3Json(m_toScale);
                    break;
                case ScaleTypeEnum.ScaleV:
                    json["scaleV"] = m_toScaleV;
                    break;
                case ScaleTypeEnum.ScaleX:
                    json["scaleX"] = m_toScaleX;
                    break;
                case ScaleTypeEnum.ScaleY:
                    json["scaleY"] = m_toScaleY;
                    break;
                case ScaleTypeEnum.ScaleZ:
                    json["scaleZ"] = m_toScaleZ;
                    break;
                default:
                    Debug.LogError(GetType().FullName + " ToJson MoveType is null");
                    break;
            } // end swtich
        }

        protected override bool CheckValid(out string errorInfo) {
            if (null == m_Transform) {
                errorInfo = GetType().FullName + " GetComponent<Transform> is null";
                return false;
            } // end if
            errorInfo = string.Empty;
            return true;
        }
    }
}
