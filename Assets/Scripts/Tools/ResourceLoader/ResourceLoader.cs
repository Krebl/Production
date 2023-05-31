using System.Collections.Generic;
using UnityEngine;

namespace Game.Production.Tools
{
    internal class ResourceLoader : IResourceLoader
    {
        private Dictionary<string, Sprite> _loadedSprite;

        public Sprite LoadSprite(string path)
        {
            if (_loadedSprite.TryGetValue(path, out Sprite result))
                return result;
            Sprite sprite = Resources.Load<Sprite>(path);
            _loadedSprite[path] = sprite;
            return sprite;
        }
    }
}