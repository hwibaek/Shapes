using UnityEngine;

namespace InGame.Boss
{
    [CreateAssetMenu(menuName = "Boss/Info", fileName = "new BossInfo")]
    public abstract class BossInfo : ScriptableObject
    {
        [SerializeField] private string bossName;
        public string BossName => bossName;
        
        [SerializeField] private int health;
        public int Health => health;
    }
}