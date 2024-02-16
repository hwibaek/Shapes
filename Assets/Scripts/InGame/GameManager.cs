using InGame.Player;
using UnityEngine;
using Util;

namespace InGame
{
    public partial class GameManager : Singleton<GameManager>
    {
        
    }

    public partial class GameManager
    {
        
    }
    
    public partial class GameManager
    {
        [SerializeField] private GameDic dictionary;
        public GameDic Dictionary => dictionary;

        [SerializeField] private PlayerController playerPrefab;
        public PlayerController PlayerPrefab => playerPrefab;
    }
    
    public partial class GameManager
    {
        public override void Awake()
        {
            Instance = this;
        }
    }
}