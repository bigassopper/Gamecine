﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Sidescroller.Canvas
{

    public class IntroControl : MonoBehaviour
    {

        [SerializeField] Animator introAnimator = null;
        [SerializeField] Camera introCamera = null;

        public void PlayIntro()
        {
            if (!PublicVariablesAvailable())
            {
                Debug.LogAssertion(" Reference missing! ");
                return;
            }

            // Config Animator
            introAnimator.SetBool("Playing", true);

            // Config Camera
            introCamera.enabled = true;
        }

        public void StopIntro()
        {
            if (!PublicVariablesAvailable()) return;

            introAnimator.SetBool("Playing", false);
            
            introCamera.enabled = false;
        }

        bool PublicVariablesAvailable()
        {
            if (introAnimator == null) return false;
            if (introCamera == null) return false;

            return true;
        }
    }
}
