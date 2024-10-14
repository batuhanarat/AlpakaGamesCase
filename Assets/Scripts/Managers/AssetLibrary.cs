using Game.Managers;
using UnityEngine;

public class AssetLibrary : MonoBehaviour , IProvidable
{
        [SerializeField] private GameObject DamagePopUp;
        [SerializeField] private GameObject Gem;
        private void Awake()
        {
            ServiceProvider.Register(this);
        }

        public T GetAsset<T>(AssetType assetType, string objectName) where T : MonoBehaviour
        {
            var asset = GetAsset<T>(assetType);
            if (asset == null)
            {
                return null;
            }

            asset.name = objectName;
            return asset;
        }

        public T GetAsset<T>(AssetType assetType) where T : class
        {
            var asset = GetAsset(assetType);
            return asset == null ? null : asset.GetComponent<T>();
        }

        private GameObject GetAsset(AssetType assetType)
        {
            return assetType switch
            {
                AssetType.DAMAGEPOPUP => Instantiate(DamagePopUp),
                AssetType.GEM => Instantiate(Gem),
                _ => null
            };
        }
}