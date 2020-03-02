﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DG.Tweening;
using LitJson;
using UnityEngine;

namespace JTween.Transform {
    public class JTweenTransformShakeRotation : JTweenBase {
        private float m_strength = 0;
        private Vector3 m_strengthVec = Vector3.zero;
        private int m_vibrato = 0;
        private float m_randomness = 0;
        private bool m_fadeOut = false;
        private Vector3 m_begainRotation = Vector3.zero;
        private UnityEngine.Transform m_Transform;

        public JTweenTransformShakeRotation() {
            m_tweenType = (int)JTweenTransform.ShakeRotation;
            m_tweenElement = JTweenElement.Transform;
        }

        public float Strength {
            get {
                return m_strength;
            }
            set {
                m_strength = value;
            }
        }

        public Vector3 StrengthVec {
            get {
                return m_strengthVec;
            }
            set {
                m_strengthVec = value;
            }
        }

        public float Randomness {
            get {
                return m_randomness;
            }
            set {
                m_randomness = value;
            }
        }

        public int Vibrato {
            get {
                return m_vibrato;
            }
            set {
                m_vibrato = value;
            }
        }

        public bool FadeOut {
            get {
                return m_fadeOut;
            }
            set {
                m_fadeOut = value;
            }
        }

        protected override void Init() {
            if (null == m_target) return;
            // end if
            m_Transform = m_target.GetComponent<UnityEngine.Transform>();
            if (null == m_Transform) return;
            // end if
            m_begainRotation = m_Transform.eulerAngles;
        }

        protected override Tween DOPlay() {
            if (null == m_Transform) return null;
            // end if
            if (m_strengthVec == null || m_strengthVec == Vector3.zero) {
                return m_Transform.DOShakeRotation(m_duration, m_strength, m_vibrato, m_randomness, m_fadeOut);
            } // end if
            return m_Transform.DOShakeRotation(m_duration, m_strengthVec, m_vibrato, m_randomness, m_fadeOut);
        }

        public override void Restore() {
            if (null == m_Transform) return;
            // end if
            m_Transform.eulerAngles = m_begainRotation;
        }

        protected override void JsonTo(JsonData json) {
            if (json.Contains("strength")) m_strength = (float)json["strength"];
            // end if
            if (json.Contains("strengthVec")) m_strengthVec = JTweenUtils.JsonToVector3(json["strengthVec"]);
            // end if
            if (json.Contains("vibrato")) m_vibrato = (int)json["vibrato"];
            // end if
            if (json.Contains("randomness")) m_randomness = (float)json["randomness"];
            // end if
            if (json.Contains("fadeOut")) {
                int fadeOut = (int)json["fadeOut"];
                m_fadeOut = fadeOut == 0 ? false : true;
            } // end if
        }

        protected override void ToJson(ref JsonData json) {
            json["strength"] = m_strength;
            if (m_strengthVec != null && m_strengthVec != Vector3.zero) {
                json["strengthVec"] = JTweenUtils.Vector3Json(m_strengthVec);
            } // end if
            json["vibrato"] = m_vibrato;
            json["randomness"] = m_randomness;
            json["fadeOut"] = m_fadeOut ? 1 : 0;
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