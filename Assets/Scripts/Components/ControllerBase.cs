using UnityEngine;

namespace Assets.Scripts.Components
{

	public abstract class ControllerBase: CustomComponentBase
	{
		protected override void Awake()
		{
			base.Awake ();

		}

		void InitializeCustomComponents()
		{
			Components.CustomComponentBase[] components = GetComponents<Components.CustomComponentBase> ();
			foreach (Components.CustomComponentBase component in components) {
				component.Load (_parent.gameObject);
			}

		}

		public T GetCustomComponent<T>()
		{
			return GetComponent<T> ();
		}

	}
}