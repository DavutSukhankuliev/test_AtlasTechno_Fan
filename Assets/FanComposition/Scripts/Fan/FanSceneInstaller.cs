using Zenject;

namespace FanComposition.Fan
{
    public class FanSceneInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container
                .Bind<FanConfig>()
                .FromScriptableObjectResource("Fan/FanConfigs")
                .AsSingle();
            
            Container
                .BindMemoryPool<FanView, FanView.Pool>()
                .WithMaxSize(5)
                .FromComponentInNewPrefabResource("Fan/FanPrefab")
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