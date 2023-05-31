using UnityEngine;

namespace Game.Production.Config
{
    [CreateAssetMenu(
        fileName = "GameConfig",
        menuName = "[Production]/Config/GameConfig")]
    internal class GameConfig : ScriptableObject
    {
        public CostContent CurrencyForWin;

        [Header("Configs")] 
        public CurrencyConfig Currency;
        public GameResourceConfig GameResource;
        public CraftItemsConfig CraftItems;
        public ReceiptsConfig Receipts;
        public BuildingsConfig Buildings;
    }
}

