using Zenject;

namespace FanComposition
{
    public class KeyboardInput : ITickable
    {
        private readonly FanSpawner _spawner;

        public KeyboardInput(FanSpawner spawner)
        {
            _spawner = spawner;
        }

        public void Tick()
        {
            
        }
    }
}