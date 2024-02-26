using Zenject;

namespace FanComposition.UI
{
    public class UIFrameworkInstaller : MonoInstaller<UIFrameworkInstaller>
    {
        public override void InstallBindings()
        {
            Container
                .BindInterfacesAndSelfTo<UIRoot>()
                .FromComponentInNewPrefabResource("UI/UIRootPrefab")
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
