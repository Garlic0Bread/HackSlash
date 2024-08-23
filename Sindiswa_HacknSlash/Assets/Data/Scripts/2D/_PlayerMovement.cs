using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OWL
{
    public class PlayerMovement : MonoBehaviour
    {
        [Header("References")]
        public PlayerMovemement_Stats moveStats;
        [SerializeField] private Collider2D feetCollider;
        [SerializeField] private Collider2D bodyCollider;

        private Rigidbody2D rb2;
        //collisions check variables
        private bool isGrounded;
        private bool bumbedHead;
        private RaycastHit2D groundHit;
        private RaycastHit2D headHit;

        //movement variables
        private bool isFacingRight;
        private Vector2 moveVelocity;

        //jumping variables
        public float verticalVelocity {  get; private set; }
        private float fastFallReleasesSpeed;
        private int numberOfJumpsUsed;
        private bool isFastFalling;
        private float fastfallTime;
        private bool isJumping;
        private bool isFalling;

        //apex variables
        private float apexPoint;
        private bool isPastApexThreshold;
        private float timePastApexThreshold;

        //jump buffer variables
        private float jumpBufferTimer;
        private bool jumpReleasesDuringBuffer;

        //coyote time variables
        private float coyoteTimer;

        private void Awake()
        {
            isFacingRight = true;
            rb2 = GetComponent<Rigidbody2D>();
        }
        private void Update()
        {
            CountTimers();
            JumpChecks();
        }
        private void FixedUpdate()
        {
            CollisionChecks();
            Jump();
            if (isGrounded)
            {
                Move(moveStats.GroundAcceleration, moveStats.GroundDeceleration, _InputManager.Movement);
            }
            else
            {
                Move(moveStats.AirAcceleration, moveStats.AirDeceleration, _InputManager.Movement);
            }
        }

        #region Movement
        private void Move(float acceleration, float deceleration, Vector2 moveInput)
        {
            if (moveInput != Vector2.zero) 
            {
                TurnCheck(moveInput);

                Vector2 targetVelocity = Vector2.zero;
                if (_InputManager.runIsHeld)
                {
                    targetVelocity = new Vector2(moveInput.x, 0f) * moveStats.MaxRunSpeed;
                }
                else
                {
                    targetVelocity = new Vector2(moveInput.x, 0f) * moveStats.MaxWalkSpeed;
                }
                moveVelocity = Vector2.Lerp(moveVelocity, targetVelocity, acceleration * Time.fixedDeltaTime);
                rb2.velocity = new Vector2(moveVelocity.x, rb2.velocity.y);
            }
            else if(moveInput == Vector2.zero)
            {
                moveVelocity = Vector2.Lerp(moveVelocity, Vector2.zero, deceleration * Time.fixedDeltaTime);
                rb2.velocity = new Vector2(moveVelocity.x, rb2.velocity.y);
            }
        }

        private void TurnCheck(Vector2 moveInput)
        {
            if(isFacingRight && moveInput.x < 0)
            {
                Turn(false);
            }
            else if (!isFacingRight && moveInput.x > 0)
            {
                Turn(true);
            }
        }
        private void Turn(bool tunrRight)
        {
            if (tunrRight)
            {
                isFacingRight = true;
                transform.Rotate(0f, 180f, 0f);
            }
            else
            {
                isFacingRight = false;
                transform.Rotate(0f, -180f, 0f);
            }
        }

        #endregion

        #region Jumping
        private void JumpChecks()
        {
            //when jump button is pressed
            if (_InputManager.jumpWasPressed)
            {
                jumpBufferTimer = moveStats.jumpBufferTime;
                jumpReleasesDuringBuffer = false;
            }

            //when released
            if (_InputManager.jumpWasReleased)
            {
                if(jumpBufferTimer > 0f)
                {
                    jumpReleasesDuringBuffer = true;
                }
                if(isJumping && verticalVelocity > 0f)
                {
                    if (isPastApexThreshold)
                    {
                        isPastApexThreshold = false;
                        isFastFalling = true;
                        fastfallTime = moveStats.timeForUpwardsCancel;
                        verticalVelocity = 0f;
                    }
                    else
                    {
                        isFastFalling = true;
                        fastFallReleasesSpeed = verticalVelocity;
                    }
                }
            }

            //initiate jump w jump buffering and coyote time
            if(jumpBufferTimer > 0f && !isJumping && (isGrounded || coyoteTimer > 0f))
            {
                InitiateJump(1);
                if (jumpReleasesDuringBuffer)
                {
                    isFastFalling = true;
                    fastFallReleasesSpeed = verticalVelocity;
                }
            }

            //double jump
            else if(jumpBufferTimer > 0f && isJumping && numberOfJumpsUsed < moveStats.numberOfJumpsAllowed)
            {
                isFastFalling = false;
                InitiateJump(1);
            }

            //air jump after coyote time lapsed
            else if(jumpBufferTimer > 0f && isFalling && numberOfJumpsUsed < moveStats.numberOfJumpsAllowed - 1)
            {
                InitiateJump(2);
                isFastFalling = false;
            }

            //landed
            if((isJumping || isFastFalling) && isGrounded && verticalVelocity <= 0f)
            {
                isJumping = false;
                isFalling = false;
                isFastFalling = false;
                fastfallTime = 0f;
                isPastApexThreshold = false;
                numberOfJumpsUsed = 0;

                verticalVelocity = Physics2D.gravity.y;
            }
        }

        private void InitiateJump(int NumberOfJumpsUsed)
        {
            if (!isJumping)
            {
                isJumping = true;
            }
            jumpBufferTimer = 0f;
            numberOfJumpsUsed += NumberOfJumpsUsed;
            verticalVelocity = moveStats.InitialJumpVelocity;
        }
        private void Jump()
        {
            //apply gravity while jumping
            if (isJumping)
            {
                //check for head bump
                if (bumbedHead)
                {
                    isFastFalling = true;
                }
                //gravity on ascending
                if(verticalVelocity >= 0f)
                {
                    //apex controls
                    apexPoint = Mathf.InverseLerp(moveStats.InitialJumpVelocity, 0f, verticalVelocity);

                    if(apexPoint > moveStats.apexThreshold)
                    {
                        if (!isPastApexThreshold)
                        {
                            isPastApexThreshold = true;
                            timePastApexThreshold = 0f;
                        }

                        if (isPastApexThreshold)
                        {
                            timePastApexThreshold += Time.fixedDeltaTime;
                            if(timePastApexThreshold < moveStats.apexHangTime)
                            {
                                verticalVelocity = 0f;
                            }
                            else
                            {
                                verticalVelocity = -0.01f;
                            }
                        }
                    }
                    //gravity on ascending but not pas apex threshold
                    else
                    {
                        verticalVelocity += moveStats.Gravity * Time.fixedDeltaTime;
                        if (isPastApexThreshold)
                        {
                            isPastApexThreshold = false;
                        }
                    }
                }

                //gravity on descending
                else if (!isFastFalling)
                {
                    verticalVelocity += moveStats.Gravity * moveStats.gravityOnReleaseMultiplier * Time.fixedDeltaTime;
                }
                else if(verticalVelocity < 0f)
                {
                    if (!isFalling)
                    {
                        isFalling = true;
                    }
                }
            }

            //jump cut
            if (isFastFalling)
            {
                if(fastfallTime >= moveStats.timeForUpwardsCancel)
                {
                    verticalVelocity += moveStats.Gravity * moveStats.gravityOnReleaseMultiplier * Time.fixedDeltaTime;
                }
                else if(fastfallTime < moveStats.timeForUpwardsCancel)
                {
                    verticalVelocity = Mathf.Lerp(fastFallReleasesSpeed, 0f, (fastfallTime / moveStats.timeForUpwardsCancel));
                }
                fastfallTime += Time.fixedDeltaTime;
            }

            //normal gravity while falling
            if(!isGrounded && !isJumping)
            {
                if (!isFalling)
                {
                    isFalling = true;
                }
                verticalVelocity += moveStats.Gravity * Time.fixedDeltaTime;
            }

            //clamp fall speed
            verticalVelocity = Mathf.Clamp(verticalVelocity, -moveStats.maxFallSpeed, 50f);

            rb2.velocity = new Vector2(rb2.velocity.x, verticalVelocity);

        }
        #endregion

        #region Collision Checks
        private void IsGrounded()
        {
            Vector2 boxCastOrigin = new Vector2(feetCollider.bounds.center.x, feetCollider.bounds.min.y);
            Vector2 boxCastSize = new Vector2(feetCollider.bounds.size.x, moveStats.GroundDetectionRayLength);

            groundHit = Physics2D.BoxCast(boxCastOrigin, boxCastSize, 0f, Vector2.down, moveStats.GroundDetectionRayLength, moveStats.Groundlayer);
            if(groundHit.collider != null)
            {
                isGrounded = true;
            }
            else
            {
                isGrounded = false;
            }
            #region Debug Visualisation
            if (moveStats.debugShowIsgroundedBox)
            {
                Color rayColour;
                if (isGrounded)
                {
                    rayColour = Color.green;
                }
                else { rayColour = Color.red; }

                Debug.DrawRay(new Vector2(boxCastOrigin.x - boxCastSize.x /2, boxCastOrigin.y), Vector2.down * moveStats.GroundDetectionRayLength, rayColour);
                Debug.DrawRay(new Vector2(boxCastOrigin.x + boxCastSize.x /2, boxCastOrigin.y), Vector2.down * moveStats.GroundDetectionRayLength, rayColour);
                Debug.DrawRay(new Vector2(boxCastOrigin.x - boxCastSize.x /2, boxCastOrigin.y - moveStats.GroundDetectionRayLength), Vector2.right * boxCastSize.x, rayColour);
            }
            #endregion
        }
        private void BumpedHead()
        {
            Vector2 boxCastOrigin = new Vector2(feetCollider.bounds.center.x, bodyCollider.bounds.max.y);
            Vector2 boxCastSize = new Vector2(feetCollider.bounds.size.x * moveStats.HeadWidth, moveStats.HeadDetectionRayLength);

            headHit = Physics2D.BoxCast(boxCastOrigin, boxCastSize, 0f, Vector2.up, moveStats.HeadDetectionRayLength, moveStats.Groundlayer);
            if(headHit.collider != null)
            {
                bumbedHead = true;
            }
            else
            {
                bumbedHead= false;
            }

            #region Debug Visualisation
            if (moveStats.debugShowHeadBumpBox)
            {
                float headWidth = moveStats.HeadWidth;

                Color rayColour;
                if (bumbedHead)
                {
                    rayColour = Color.green;
                }
                else
                {
                    rayColour= Color.red;
                    Debug.DrawRay(new Vector2(boxCastOrigin.x - boxCastSize.x / 2 * headWidth, boxCastOrigin.y), Vector2.up * moveStats.HeadDetectionRayLength, rayColour);
                    Debug.DrawRay(new Vector2(boxCastOrigin.x + (boxCastSize.x / 2) * headWidth, boxCastOrigin.y), Vector2.up * moveStats.HeadDetectionRayLength, rayColour);
                    Debug.DrawRay(new Vector2(boxCastOrigin.x - boxCastSize.x / 2 * headWidth, boxCastOrigin.y + moveStats.HeadDetectionRayLength), Vector2.right * boxCastSize.x * headWidth, rayColour);

                }
            }
            #endregion
        }
        private void CollisionChecks()
        {
            IsGrounded();
            BumpedHead();
        }
        #endregion

        #region Timers
        private void CountTimers()
        {
            jumpBufferTimer -= Time.deltaTime;
            if (!isGrounded)
            {
                coyoteTimer -= Time.deltaTime;
            }
            else
            {
                coyoteTimer = moveStats.jumpCoyoteTime;
            }
        }

        #endregion
    }
}
