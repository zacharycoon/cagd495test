using UnityEngine;
using System.Collections;


namespace Assets.Scripts.Components
{




	public class RelicManager: CustomComponentBase {
		public bool jumpRelic = true;
		public bool wallJumpRelic = true;
		public bool dashRelic = true;
		public bool slashRelic = true;

	

		public void ToggleJump (bool setTo){
			if (setTo) {
				Jumping.maxJumps = 2;
			} else {
				Jumping.maxJumps = 1;
			}
		}
		public void ToggleWallJump (bool setTo){
			if (setTo) {
				Jumping.maxWallJumps = 999f;
			} else {
				Jumping.maxWallJumps = 1f;
			}
		}
		public void ToggleDash (bool setTo){
			if (setTo) {
				Dash.DashUnlocked = true;
			} else {
				Dash.DashUnlocked = false;
			}
		}
		public void ToggleSlash (bool setTo){
			Slash.canSlash = setTo;
		}

		public void AbilityManager(){
			//temp manipulation of the toggle functions for prototyping
			ToggleJump(jumpRelic);
			ToggleWallJump (wallJumpRelic);
			ToggleDash (dashRelic);
			ToggleSlash (slashRelic);

		}

	}
}