﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DG.Tweening;
using LitJson;
using UnityEngine;

namespace JTween.Camera {
    public class JTweenCameraShakePosition : JTweenBase {
        private float m_strength = 0;
        private Vector3 m_strengthVec = Vector3.zero;
        private int m_vibrato = 0;
        private float m_randomness = 0;
        private bool m_fadeOut = false;
        private Vector3 m_begainPosition = Vector3.zero;
        private UnityEngine.Camera m_Camera;

        public JTweenCameraShakePosition() {
            m_tweenType = (int)JTweenCamera.ShakePosition;
            m_tweenElement = JTweenElement.Camera;
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

        public Vector3 BegainPosition {
            get {
                return m_begainPosition;
            }
            set {
                m_begainPosition = value;
                if (m_target != null) {
                    m_target.position = m_begainPosition;
                } // end if
            }
        }

        protected override void Init() {
            if (null == m_target) return;
            // end if
            m_Camera = m_target.GetComponent<UnityEngine.Camera>();
            if (null == m_Camera) return;
            // end if
            m_begainPosition = m_target.position;
        }

        protected override Tween DOPlay() {
            if (null == m_Camera) return null;
            // end if
            if (m_strengthVec == null || m_strengthVec == Vector3.zero) {
                return m_Camera.DOShakePosition(m_duration, m_strength, m_vibrato, m_randomness, m_fadeOut);
            } // end if
            return m_Camera.DOShakePosition(m_duration, m_strengthVec, m_vibrato, m_randomness, m_fadeOut);
        }

        public override void Restore() {
            if (null == m_Camera) return;
            // end if
            m_target.position = m_begainPosition;
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
            if (json.Contains("begainPosition")) BegainPosition = JTweenUtils.JsonToVector3(json["begainPosition"]);
            // end if
        }

        protected override void ToJson(ref JsonData json) {
            json["strength"] = m_strength;
            if (m_strengthVec != null) {
                json["strengthVec"] = JTweenUtils.Vector3Json(m_strengthVec);
            } // end if
            json["vibrato"] = m_vibrato;
            json["randomness"] = m_randomness;
            json["fadeOut"] = m_fadeOut ? 1 : 0;
            if (m_begainPosition != null) {
                json["begainPosition"] = JTweenUtils.Vector3Json(m_begainPosition);
            } // end if
        }

        protected override bool CheckValid(out string errorInfo) {
            if (null == m_Camera) {
                errorInfo = GetType().FullName + " GetComponent<Camera> is null";
                return false;
            } // end if
            errorInfo = string.Empty;
            return true;
        }
    }
}