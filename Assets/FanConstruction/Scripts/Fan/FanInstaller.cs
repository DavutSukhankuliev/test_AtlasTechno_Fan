using UnityEngine;
using Zenject;

namespace FanConstruction
{
    public class FanInstaller : MonoInstaller<FanInstaller>
    {
        [SerializeField] private FanView _prefab;

        public override void InstallBindings()
        {
            Container
                .BindMemoryPool<FanView, FanView.Pool>()
                .WithMaxSize(5)
                .FromComponentInNewPrefab(_prefab)
                .UnderTransformGroup("Fans")
                .AsSingle();

            Container
                .Bind<FanSpawner>()
                .AsSingle();
        }
    }
}