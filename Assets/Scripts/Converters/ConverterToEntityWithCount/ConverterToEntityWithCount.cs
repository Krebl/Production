using System.Collections.Generic;
using System.Linq;
using Game.Production.Config;
using Game.Production.Model;

namespace Game.Production.Converters
{
    internal class ConverterToEntityWithCount : IConverterToEntityWithCount
    {
        public struct Ctx
        {
            public IReadOnlyList<ContentWithIcon> currency;
            public IReadOnlyList<ContentWithIcon> resource;
        }

        public ConverterToEntityWithCount(Ctx ctx)
        {
            _ctx = ctx;
        }

        private readonly Ctx _ctx;
        
        public List<EntityWithCount> ConvertToList(List<ContentWithIcon> list)
        {
            List<EntityWithCount> result = new List<EntityWithCount>();
            foreach (var content in list)
            {
                result.Add(ConvertToEntity(content));
            }

            return result;
        }

        public List<EntityWithCount> ConvertToList(CostContent[] list)
        {
            List<EntityWithCount> result = new List<EntityWithCount>();
            foreach (var content in list)
            {
                result.Add(ConvertToEntity(content));
            }

            return result;
        }

        public EntityWithCount ConvertToEntity(ContentWithIcon content)
        {
            return new EntityWithCount
            {
                Id = content.Id,
                Name = content.Name,
                IconPath = content.IconPath,
                Count = 0
            };
        }
        
        public EntityWithCount ConvertToEntity(CostContent content)
        {
            ContentWithIcon contentWithIcon = _ctx.currency.FirstOrDefault(currency => currency.Id == content.Id);
            if (contentWithIcon == default)
                contentWithIcon = _ctx.resource.FirstOrDefault(resource => resource.Id == content.Id);
            if (contentWithIcon == default)
                return null;
            return new EntityWithCount
            {
                Id = content.Id,
                Name = contentWithIcon.Name,
                IconPath = contentWithIcon.IconPath,
                Count = content.Count
            };
        }
    }  
}

