using UnityEngine;

namespace Game.Production.Tools
{
    internal interface IResourceLoader
    {
        Sprite LoadSprite(string path);
        GameObject LoadPrefab(string path);
    } 
}

