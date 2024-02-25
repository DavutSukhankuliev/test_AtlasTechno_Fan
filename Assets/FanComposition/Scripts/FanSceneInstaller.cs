using UnityEngine;
using Zenject;

namespace FanComposition
{
    public class FanSceneInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container
                .BindMemoryPool<FanView, FanView.Pool>()
                .WithMaxSize(5)
                .FromComponentInNewPrefabResource("FanPrefab")
                .UnderTransformGroup("Fans")
                .AsSingle();

            Container
                .Bind<FanController>()
                .AsSingle();

            Container
                .Bind<FanInputHandler>()
                .AsSingle()
                .NonLazy();
        }
    }
}