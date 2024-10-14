using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Managers
{
    public static class ServiceProvider
    {
        public static GameState GameState;
        private static readonly Dictionary<Type, IProvidable> _registerDictionary = new();
        public static AudioManager AudioManager => GetManager<AudioManager>();
        public static GameManager GameManager => GetManager<GameManager>();
        public static ScenesManager ScenesManager => GetManager<ScenesManager>();
        public static AssetLibrary AssetLib => GetManager<AssetLibrary>();
        public static UIManager UIManager => GetManager<UIManager>();
        public static Pool Pool => GetManager<Pool>();

        public static EnemySpawner EnemySpawner => GetManager<EnemySpawner>();

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        public static void InitializeServiceProvider()
        {
        }

        private static T GetManager<T>() where T : class, IProvidable
        {
            if (_registerDictionary.ContainsKey(typeof(T)))
            {
                return (T)_registerDictionary[typeof(T)];
            }

            return null;
        }

        public static T Register<T>(T target) where T : class, IProvidable
        {
            _registerDictionary[typeof(T)] = target;
            return target;
        }
    }
    public interface IProvidable {

    }
}