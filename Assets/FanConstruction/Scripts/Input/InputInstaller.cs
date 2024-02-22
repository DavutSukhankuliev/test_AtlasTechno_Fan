using Zenject;

namespace FanConstruction
{
    public class InputInstaller : MonoInstaller<InputInstaller>
    {
        public override void InstallBindings()
        {
            Container
                .BindInterfacesAndSelfTo<KeyboardInput>()
                .AsSingle()
                .NonLazy();
        }
    }
}