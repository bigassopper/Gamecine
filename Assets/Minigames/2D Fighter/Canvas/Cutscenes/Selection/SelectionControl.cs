﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sidescroller.StateMachine;
using Sidescroller.Control;

namespace Sidescroller.Canvas
{
    public class SelectionControl : MonoBehaviour
    {
        //[SerializeField] Animator animator = null;
        [SerializeField] Camera canvasCamera = null;

        [SerializeField] ControllerMode selectedMode = ControllerMode.SINGLE_PLAYER;
        [SerializeField] int modeID = 0;

        [SerializeField] GameObject optionSingle = null;
        [SerializeField] GameObject optionMulti = null;
        [SerializeField] GameObject optionAuto = null;

        [SerializeField] GameObject canvasSingle = null;
        [SerializeField] GameObject canvasMulti = null;
        [SerializeField] GameObject canvasAuto = null;

        [SerializeField] GameObject arrowLeft = null;
        [SerializeField] GameObject arrowRight = null;

        bool modeIsSelected = false;

        private void Start()
        {
            selectedMode = (ControllerMode)modeID;

            DisplayOptionsScreen(selectedMode);
        }


        private void Update()
        {
            // All input are Key Down; else return
            if (!Input.anyKeyDown) return;

            // Check for new selection
            if (!modeIsSelected)
            {

                float inputAxis = Input.GetAxis("Horizontal");

                if (inputAxis > 0f)
                {
                    //Max Value
                    if (modeID == (int)ControllerMode.COUNT - 1) return;

                    //Toggle Mode
                    modeID = (modeID + 1);

                    selectedMode = (ControllerMode)modeID;
                }

                else if (inputAxis < 0f)
                {
                    //Min Value
                    if (modeID == 0) return;

                    //Toggle Mode
                    modeID = (modeID - 1);

                    selectedMode = (ControllerMode)modeID;
                }
                
                // Update the display
                DisplayOptionsScreen(selectedMode);
            }

            // Activate current selected mode
            if (Input.GetButtonDown("Submit"))
            {
                
                if (!modeIsSelected)
                {
                    DisplayHowToScreen(selectedMode);
                    modeIsSelected = true;
                }

                else
                {
                    // Config Controllers
                    ConfigFightersInput(selectedMode);

                    // Start Sequence in State Machine
                    Fighter2DMinigameStateMachine.current.StartGameplaySequence();
                }
            }

            else if (Input.GetButtonDown("Cancel"))
            {
                // If mode is selected then go back
                if (modeIsSelected)
                {
                    modeIsSelected = false;

                    DisplayOptionsScreen(selectedMode);
                }
            }


        }


        public void Display()
        {
            if (!PublicVariablesAvailable())
            {
                Debug.LogAssertion(" Reference missing! ");
                return;
            }

            // Config Animator
            //animator.SetTrigger("Play");

            // Config Camera
            canvasCamera.enabled = true;
        }

        public void Hide()
        {
            if (!PublicVariablesAvailable()) return;

            //introAnimator.SetTrigger("Stop");
            canvasCamera.enabled = false;
        }

        // Show Mode Selection


        bool PublicVariablesAvailable()
        {
            //if (animator == null) return false;
            if (canvasCamera == null) return false;

            return true;
        }


        void ConfigFightersInput(ControllerMode mode)
        {
            // State Machine
            Fighter2DMinigameStateMachine sm = Fighter2DMinigameStateMachine.current;

            // Single Player
            if (mode == ControllerMode.SINGLE_PLAYER)
            {
                //Fighter1 - Single
                sm.ConfigFighterControllers(1, ControllerMode.SINGLE_PLAYER);

                //Fighter2 - AI
                sm.ConfigFighterControllers(2, ControllerMode.AI_PLAYER);
                
            }

            // Multiplayer
            else if (mode == ControllerMode.MULTI_PLAYER)
            {
                //Fighter1 - Multi
                sm.ConfigFighterControllers(1, ControllerMode.MULTI_PLAYER);

                //Fighter2 - Multi
                sm.ConfigFighterControllers(2, ControllerMode.MULTI_PLAYER);
            }

            // Expectator 
            else if (mode == ControllerMode.AI_PLAYER)
            {
                //Fighter1 - Single
                sm.ConfigFighterControllers(1, ControllerMode.AI_PLAYER);

                //Fighter2 - AI
                sm.ConfigFighterControllers(2, ControllerMode.AI_PLAYER);
            }

        }

        void DisplayOptionsScreen(ControllerMode mode)
        {

            //Hide all screens
            DeactivateAllScreens();

            // Hide all Arrows
            DeactivateAllArrows();

            // Single Player
            if (mode == ControllerMode.SINGLE_PLAYER)
            {
                optionSingle.SetActive(true);

                //Arrows
                arrowRight.SetActive(true);
            }

            // Multiplayer
            else if (mode == ControllerMode.MULTI_PLAYER)
            {
                optionMulti.SetActive(true);

                //Arrows
                arrowRight.SetActive(true);
                arrowLeft.SetActive(true);
            }

            else if (mode == ControllerMode.AI_PLAYER)
            {
                optionAuto.SetActive(true);

                //Arrows
                arrowLeft.SetActive(true);
            }
        }

        void DisplayHowToScreen(ControllerMode mode)
        {
            //Hide all screens
            DeactivateAllScreens();

            // Hide all Arrows
            DeactivateAllArrows();

            // Single Player
            if (mode == ControllerMode.SINGLE_PLAYER)
            {
                canvasSingle.SetActive(true);
            }

            // Multiplayer
            else if (mode == ControllerMode.MULTI_PLAYER)
            {
                canvasMulti.SetActive(true);
            }

            else if (mode == ControllerMode.AI_PLAYER)
            {
                canvasAuto.SetActive(true);
            }
        }

        void DeactivateAllScreens()
        {
            canvasSingle.SetActive(false);
            canvasMulti.SetActive(false);
            canvasAuto.SetActive(false);

            optionSingle.SetActive(false);
            optionMulti.SetActive(false);
            optionAuto.SetActive(false);
        }

        void DeactivateAllArrows()
        {
            arrowLeft.SetActive(false);
            arrowRight.SetActive(false);
        }
    }
}