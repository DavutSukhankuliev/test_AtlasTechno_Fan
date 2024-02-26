using Zenject;

namespace FanComposition.CustomInput
{
    public class CustomInputInstaller : MonoInstaller<CustomInputInstaller>
    {
        public override void InstallBindings()
        {
            Container
                .Bind<MouseInputSystem>()
                .AsSingle();

            Container
                .Bind<MouseLookSystem>()
                .AsSingle()
                .NonLazy();
        }
    }
}