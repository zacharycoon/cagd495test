using System.Collections.Generic;

namespace Assets.Scripts.Character{
	public class CharStates : Componenets.StatesBase<CharStates.States>
	{
		public enum States{
			Idle,
			Moving,
			Jumping,
			WallGrab,
			WallJumping,
			DashingNoInput,
			DashingWithInput

		}

		public override States currentState { get; protected set; }
		public List<States> moveStates = new List<States>{ States.Idle, States.Moving };

	}

}