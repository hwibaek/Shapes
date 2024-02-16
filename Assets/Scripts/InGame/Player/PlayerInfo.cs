using UnityEngine;
using UnityEngine.Serialization;

namespace InGame.Player
{
    [CreateAssetMenu(menuName = "Player/Info", fileName = "new PlayerInfo")]
    public class PlayerInfo : ScriptableObject
    {
        [SerializeField] private int health;
        public int Health => health;

        [SerializeField] private float speed;
        public float Speed => speed;

        [Header("Jump")]
        [SerializeField] private float jumpPower;
        public float JumpPower => jumpPower;

        [SerializeField] private int bonusJumpCount;
        public int BonusJumpCount => bonusJumpCount;

        [Header("Dash")]
        [SerializeField, Tooltip("대쉬 시 적용되는 속도. 일반 속도에 곱한다.")] private float dashPower;
        public float DashPower => dashPower;

        [SerializeField] private float dashDuration;
        public float DashDuration => dashDuration;

        [Header("Attack")]
        [SerializeField] private float attackDelay;
        public float AttackDelay => attackDelay;

        [SerializeField] private float attackDuration;
        public float AttackDuration => attackDuration;

        [SerializeField] private int attackDamage;
        public int AttackDamage => attackDamage;

        [SerializeField, Tooltip("패링 시 들어가는 데미지. 일반뎀에 곱한다.")] private int parringDamage;
        public int ParringDamage => parringDamage;
    }
}