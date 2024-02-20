using UnityEngine;
using Zenject;

namespace FanConstruction.Fan
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
                .Bind<FanController>()
                .AsSingle();
        }
    }
}