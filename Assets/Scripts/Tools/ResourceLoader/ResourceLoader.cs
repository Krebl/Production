using System.Collections.Generic;
using UnityEngine;

namespace Game.Production.Tools
{
    internal class ResourceLoader : IResourceLoader
    {
        private readonly Dictionary<string, Sprite> _loadedSprite = new Dictionary<string, Sprite>();

        public Sprite LoadSprite(string path)
        {
            if (_loadedSprite.TryGetValue(path, out Sprite result))
                return result;
            Sprite sprite = Resources.Load<Sprite>(path);
            _loadedSprite[path] = sprite;
            return sprite;
        }

        public GameObject LoadPrefab(string path)
        {
            return Resources.Load<GameObject>(path);
        }
    }
}