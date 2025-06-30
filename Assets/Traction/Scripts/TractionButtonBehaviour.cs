using UnityEngine;
using VRT.Pilots.Common;

namespace VRT.Pilots.Traction
{
	public class TractionButtonBehaviour : MonoBehaviour
	{
		public NetworkTrigger TractionButtonTrigger;

		public float TimeOutBetweenTriggers = 1f;
		private float _ButtonLastTriggered;

		private void OnTriggerEnter(Collider other)
		{
			if (Time.realtimeSinceStartup - _ButtonLastTriggered > TimeOutBetweenTriggers)
			{
				string layer = LayerMask.LayerToName(other.gameObject.layer);
				if (layer != "TouchCollider")
				{
					return;
				}

				Debug.Log($"[TractionButtonBehaviour] Triggered by {other.name} on layer {other.gameObject.layer}");

				TractionButtonTrigger.Trigger();

				_ButtonLastTriggered = Time.realtimeSinceStartup;
			}
		}
	}
}