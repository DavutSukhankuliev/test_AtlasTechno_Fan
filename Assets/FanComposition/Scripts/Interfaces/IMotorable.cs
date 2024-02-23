namespace FanComposition
{
    public interface IMotorable
    {
        void PowerOn();
        void PowerOff();
        void SetMotor(float targetVelocity, float force, bool isFreeSpin = false);
    }
}