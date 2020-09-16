﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DG.Tweening;
using LitJson;
using UnityEngine;

namespace JTween.Transform {
    public class JTweenTransformLookAt : JTweenBase {
        private Vector3 m_beginRotate = Vector3.zero;
        private Vector3 m_towards = Vector3.zero;
        private AxisConstraint m_axisConstraint = AxisConstraint.None;
        private Vector3 m_up = Vector3.up;
        private UnityEngine.Transform m_Transform;

        public JTweenTransformLookAt() {
            m_tweenType = (int)JTweenTransform.LookAt;
            m_tweenElement = JTweenElement.Transform;
        }

        public Vector3 BeginRotate {
            get {
                return m_beginRotate;
            }
            set {
                m_beginRotate = value;
            }
        }

        public Vector3 Towards {
            get {
                return m_towards;
            }
            set {
                m_towards = value;
            }
        }

        public AxisConstraint AxisConstraint {
            get {
                return m_axisConstraint;
            }
            set {
                m_axisConstraint = value;
            }
        }
        public Vector3 Up {
            get {
                return m_up;
            }
            set {
                m_up = value;
            }
        }

        protected override void Init() {
            if (null == m_target) return;
            // end if
            m_Transform = m_target.GetComponent<UnityEngine.Transform>();
            if (null == m_Transform) return;
            // end if
            m_beginRotate = m_Transform.rotation.eulerAngles;
        }

        protected override Tween DOPlay() {
            if (null == m_Transform) return null;
            // end if
            return m_Transform.DOLookAt(m_towards, m_duration, m_axisConstraint, m_up);
        }

        public override void Restore() {
            if (null == m_Transform) return;
            // end if
            m_Transform.rotation = Quaternion.Euler(m_beginRotate);
        }

        protected override void JsonTo(JsonData json) {
            if (json.Contains("beginRotate")) BeginRotate = JTweenUtils.JsonToVector3(json["beginRotate"]);
            // end if
            if (json.Contains("towards")) m_towards = JTweenUtils.JsonToVector3(json["towards"]);
            // end if
            if (json.Contains("axis")) m_axisConstraint = (AxisConstraint)(int)json["axis"];
            // end if
            if (json.Contains("up")) m_up = JTweenUtils.JsonToVector3(json["up"]);
            // end if
            Restore();
        }

        protected override void ToJson(ref JsonData json) {
            json["beginRotate"] = JTweenUtils.Vector3Json(m_beginRotate);
            json["towards"] = JTweenUtils.Vector3Json(m_towards);
            json["axis"] = (int)m_axisConstraint;
            json["up"] = JTweenUtils.Vector3Json(m_up);
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
