using Zenject;

namespace FanComposition.Fan
{
    public class FanInstaller : MonoInstaller
    {
        private const string FAN_BUTTONS_DESCRIPTION_CONFIG = "Fan/FanButtonsDescription";
        private const string FAN_CONFIG = "Fan/FanConfigs";
        private const string FAN_PREFAB = "Fan/FanPrefab";
        private const string FAN_GROUP = "Fans";
        
        public override void InstallBindings()
        {
            Container
                .Bind<FanButtonsDescription>()
                .FromScriptableObjectResource(FAN_BUTTONS_DESCRIPTION_CONFIG)
                .AsSingle();
            
            Container
                .Bind<FanConfig>()
                .FromScriptableObjectResource(FAN_CONFIG)
                .AsSingle();
            
            Container
                .BindMemoryPool<FanView, FanView.Pool>()
                .WithMaxSize(5)
                .FromComponentInNewPrefabResource(FAN_PREFAB)
                .UnderTransformGroup(FAN_GROUP)
                .AsSingle();

            Container
                .Bind<FanController>()
                .AsSingle();
        }
    }
}