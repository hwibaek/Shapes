using Cysharp.Threading.Tasks;
using InGame.Player;
using UnityEngine;

namespace InGame.Boss
{
    public abstract class BossPatternBase : ScriptableObject
    {
        public abstract UniTask Execute(BossController boss, PlayerController player);
    }
}