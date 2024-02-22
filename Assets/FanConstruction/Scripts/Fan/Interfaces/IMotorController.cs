namespace FanConstruction
{
    public interface IMotorController
    {
        void TogglePower();
        void SetMotor(float targetVelocity, float force, bool isFreeSpin = false);
    }
}