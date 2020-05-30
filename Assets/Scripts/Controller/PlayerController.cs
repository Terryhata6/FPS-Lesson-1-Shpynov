namespace Game
{
    public class PlayerController : BaseController, IExecute
    {
        #region PlayerController
        private readonly IMotor _motor;

        public PlayerController(IMotor motor)
        {
            _motor = motor;
        }
        #endregion
        #region IExecute
        public void Execute()
        {
            if (!IsActive) { return; }
            _motor.Move();
        }
        #endregion
    }
}