using UnityEngine;
using UnityEngine.Serialization;

namespace InGame
{
    public partial class GameDic : ScriptableObject
    {
        
    }
    
    public partial class GameDic
    {
        [Header("Number")] 
        
        [SerializeField, Tooltip("땅을 감지하는 거리")] private float groundDetectRange;
        public float GroundDetectRange => groundDetectRange;

        [FormerlySerializedAs("jumpBufferFrames")] [SerializeField, Tooltip("땅에 닿기 전 점프 키를 눌렀을 때 보정하는 시간")] private float jumpBufferTimes;
        public float JumpBufferTimes => jumpBufferTimes;

        [SerializeField, Tooltip("땅에서 벗어난 후 점프 상실까지 걸리는 시간")] private float coyoteTime;
        public float CoyoteTime => coyoteTime;
    }
    
    public partial class GameDic
    {
        [Header("Layer")]
        
        [SerializeField] private LayerMask wallLayer;
        public LayerMask WallLayer => wallLayer;

        [SerializeField] private LayerMask enemyLayer;
        public LayerMask EnemyLayer => enemyLayer;
    }
}