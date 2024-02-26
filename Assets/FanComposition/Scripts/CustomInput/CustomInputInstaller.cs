using Zenject;

namespace FanComposition.CustomInput
{
    public class CustomInputInstaller : MonoInstaller<CustomInputInstaller>
    {
        private const string CUSTOM_INPUT_CONFIG = "CustomInput/CustomInputConfig";
        public override void InstallBindings()
        {
            Container
                .Bind<CustomInputConfig>()
                .FromScriptableObjectResource(CUSTOM_INPUT_CONFIG)
                .AsSingle();
            
            Container
                .Bind<MouseInputSystem>()
                .AsSingle();

            Container
                .Bind<KeyboardInputSystem>()
                .AsSingle();

            Container
                .Bind<CameraInputHandler>()
                .AsSingle()
                .NonLazy();
            
            Container
                .Bind<FanInputHandler>()
                .AsSingle()
                .NonLazy();
        }
    }
}