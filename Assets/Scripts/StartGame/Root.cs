using UnityEngine;
using Game.Production.Config;

namespace Game.Production
{
    internal class Root : MonoBehaviour
    {
        [SerializeField] private GameConfig _gameConfig;
        [SerializeField] private Transform _uiContainer;
    }
}

