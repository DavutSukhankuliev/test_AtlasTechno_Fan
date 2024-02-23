using UnityEngine;
using Zenject;

namespace FanComposition
{
    public class FanSceneInstaller : MonoInstaller
    {
        [SerializeField] private FanView _prefab;
        
        public override void InstallBindings()
        {
            InstallFan();
            InstallInput();
        }

        private void InstallFan()
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
            
            Container
                .BindInterfacesAndSelfTo<HingeController>()
                .AsSingle();
            
            Container
                .BindInterfacesAndSelfTo<BodyController>()
                .AsSingle();
            
            Container
                .BindInterfacesAndSelfTo<MotorController>()
                .AsSingle();
        }
        private void InstallInput()
        {
            Container
                .BindInterfacesAndSelfTo<KeyboardInput>()
                .AsSingle()
                .NonLazy();
        }
    }
}