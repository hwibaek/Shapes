using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem;

namespace InGame.Player
{
    public partial class PlayerController : MonoBehaviour
    {
        [Serializable]
        private class InputBool
        {
            /// <summary>
            /// 입력 (키 누름)
            /// </summary>
            [SerializeField] private bool isPerformed;

            public bool IsPerformed
            {
                get
                {
                    var temp = isPerformed;
                    isPerformed = false;
                    return temp;
                }
                set => isPerformed = value;
            }
            /// <summary>
            /// 입력 취소(키 땜)
            /// </summary>
            [SerializeField] private bool isCanceled;

            public bool IsCanceled
            {
                get
                {
                    var temp = isCanceled;
                    isCanceled = false;
                    return temp;
                }
                set => isCanceled = value;
            }
        }
    }

    public partial class PlayerController
    {
        [Header("Variables")]
        
        [SerializeField] private int health;

        public int Health
        {
            get => health;
            set
            {
                if (value > 0)
                {
                    health = value;
                }
                else
                {
                    health = 0;
                }
            }
        }
        
        [SerializeField] private float horizontal;
        [SerializeField] private float vertical;
        [SerializeField] private bool moveLock;

        [SerializeField] private int currentJumpCnt;
        [SerializeField] private float coyoteCnt;
        [SerializeField] private float jumpBufferCnt;
        [SerializeField] private bool airJumping;
        [SerializeField] private bool jumped;

        [SerializeField] private bool canDash;

        [SerializeField] private InputBool jump;
        [SerializeField] private InputBool attack;
        [SerializeField] private InputBool dash;
    }

    public partial class PlayerController
    {
        [Header("References")]
        public PlayerInfo info;

        [SerializeField] private Rigidbody2D rigid;
        public Rigidbody2D Rigid => rigid;

        [SerializeField] private Transform groundChecker;
        public Transform GroundChecker => groundChecker;
    }

    public partial class PlayerController
    {
        public void OnGetHMoveKey(InputAction.CallbackContext context)
        {
            horizontal = context.ReadValue<float>();
        }

        public void OnGetJumpKey(InputAction.CallbackContext context)
        {
            jump.IsPerformed = context.performed;
            jump.IsCanceled = context.canceled;
        }

        public void OnGetVMoveKey(InputAction.CallbackContext context)
        {
            vertical = context.ReadValue<float>();
        }

        public void OnGetAttackKey(InputAction.CallbackContext context)
        {
            attack.IsPerformed = context.performed;
            attack.IsCanceled = context.canceled;
        }

        public void OnGetDashKey(InputAction.CallbackContext context)
        {
            dash.IsPerformed = context.performed;
            dash.IsCanceled = context.canceled;
        }
    }
    
    public partial class PlayerController
    {
        public bool IsGround() =>
            Physics2D.OverlapCapsule(GroundChecker.position, new Vector2(GameManager.Instance.Dictionary.GroundDetectRange, 0.5f),
                CapsuleDirection2D.Horizontal, 0, GameManager.Instance.Dictionary.WallLayer);

        private void Flip()
        {
            var trans = transform;
            var local = trans.localScale;
            local.x = horizontal switch
            {
                > 0 => 1,
                < 0 => -1,
                _ => local.x
            };
            trans.localScale = local;
        }

        private void Move()
        {
            if (moveLock) return;
            Rigid.velocityX = horizontal * info.Speed;
        }

        private void Jump()
        {
            if (currentJumpCnt < info.BonusJumpCount && jumpBufferCnt > 0 && coyoteCnt > 0)
            {
                jumped = true;
                Rigid.velocityY = info.JumpPower;
                jumpBufferCnt = 0;
                canDash = true;
            }
            
            if (jump.IsCanceled)
            {
                currentJumpCnt += 1;
                airJumping = true;
                coyoteCnt = GameManager.Instance.Dictionary.CoyoteTime;
                if (Rigid.velocityY > 0)
                {
                    Rigid.velocityY *= 0.5f;
                }
            }
        }

        private async void Dash()
        {
            if (!canDash) return;
            canDash = false;
            var temp = moveLock;
            moveLock = true;
            Rigid.gravityScale = 0;
            var power = transform.localScale.x * info.Speed * info.DashPower;
            Rigid.velocity = new Vector2(power, 0);
            await UniTask.WaitForSeconds(info.DashDuration);
            moveLock = temp;
            Rigid.gravityScale = 1;
        }

        private void OnGround()
        {
            if (!IsGround()) return;
            
            airJumping = false;
            canDash = true;
            currentJumpCnt = 0;
            if (jumped)
            {
                jumped = false;
            }
        }

        private void CoyoteChecker()
        {
            if (IsGround())
            {
                coyoteCnt = GameManager.Instance.Dictionary.CoyoteTime;
            }
            else if (coyoteCnt > 0 && !airJumping)
            {
                coyoteCnt -= Time.deltaTime;
            }
        }

        private void JumpBufferChecker()
        {
            if (jump.IsPerformed)
            {
                jumpBufferCnt = GameManager.Instance.Dictionary.JumpBufferTimes;
            }
            else if (jumpBufferCnt > 0)
            {
                jumpBufferCnt -= Time.deltaTime;
            }
        }

        private void DashChecker()
        {
            if (dash.IsPerformed)
            {
                Dash();
            }
        }

        private void Init()
        {
            Health = 5;
        }
    }
    
    public partial class PlayerController
    {
        private void Start()
        {
            Init();
        }

        private void Update()
        {
            Flip();
            OnGround();
            DashChecker();
            CoyoteChecker();
            JumpBufferChecker();
        }

        private void FixedUpdate()
        {
            Move();
            Jump();
        }

        private void LateUpdate()
        {
            
        }
    }
}