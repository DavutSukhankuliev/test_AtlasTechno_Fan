using Zenject;

namespace FanComposition.UI
{
    public class UIFrameworkInstaller : MonoInstaller<UIFrameworkInstaller>
    {
        private const string UI_ROOT_PREFAB = "UI/UIRootPrefab";
        
        public override void InstallBindings()
        {
            Container
                .BindInterfacesAndSelfTo<UIRoot>()
                .FromComponentInNewPrefabResource(UI_ROOT_PREFAB)
                .AsSingle();

            Container
                .BindInterfacesAndSelfTo<UIService>()
                .AsSingle();
            
            Container
                .Bind<UIController>()
                .AsSingle()
                .NonLazy();
        }
    }
}
