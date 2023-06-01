using Game.Production.Model;
using UniRx;

namespace Game.Production.Logic
{
    internal interface IReadOnlyBankLogic
    {
        bool EnoughCurrency(string id, int count);
        public IReadOnlyReactiveDictionary<string, EntityWithCount> Currency { get; }
    }
}

