using Zenject;

namespace FanComposition.CustomInput
{
    public class CustomInputInstaller : MonoInstaller<CustomInputInstaller>
    {
        private const string CUSTOM_INPUT_CONFIG = "CustomInput/CustomInputConfig";
        public override void InstallBindings()
        {
            InstallCameraInput();
            InstallFanInput();
        }

        private void InstallCameraInput()
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
        }

        private void InstallFanInput()
        {
            Container
                .Bind<FanInputSystem>()
                .AsSingle();

            Container
                .Bind<FanInputHandler>()
                .AsSingle()
                .NonLazy();
        }
    }
}