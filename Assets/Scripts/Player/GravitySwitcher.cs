﻿using System;
using BallJump.Core;
using UnityEngine;

namespace BallJump.Player
{
    [RequireComponent(typeof(Rigidbody2D), typeof(GroundChecker))]
    public class GravitySwitcher : MonoBehaviour
    {
        //On tap, changes gravity direction on opposite.

        [SerializeField] [Range(0f, 500f)] private float gravityScale = 5f;
        private GroundChecker groundChecker;

        private Rigidbody2D rb;

        private void Awake()
        {
            TryGetComponent(out rb);
            TryGetComponent(out groundChecker);
            rb.gravityScale = gravityScale;
        }

        private void Update()
        {
            if (Input.touchCount > 0)
            {
                var touch = Input.GetTouch(0);

                if (touch.phase == TouchPhase.Began) SwitchGravity();
            }

            if (Input.GetKeyDown(KeyCode.Space)) SwitchGravity();
        }

        public event Action OnGroundLeave;

        private void SwitchGravity()
        {
            if (groundChecker.isGrounded && !PauseGame.IsPaused)
            {
                rb.gravityScale = -Math.Sign(rb.gravityScale) * DifficultyModifier.CurrentDifMod * gravityScale;
                OnGroundLeave?.Invoke();
            }
        }
    }
}