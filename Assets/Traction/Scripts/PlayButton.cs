using UnityEngine;
using VRT.Pilots.Common;
using System.Collections.Generic;

namespace VRT.Pilots.Pilot0
{
	public class PlayButton : MonoBehaviour
	{
		public NetworkTrigger PlayButtonTrigger;

		public float TimeOutBetweenTriggers = 1f;
		private float _PlayButtonLastTriggered;


		private void OnTriggerEnter(Collider other)
		{
			if (Time.realtimeSinceStartup - _PlayButtonLastTriggered > TimeOutBetweenTriggers)
			{
				string layer = LayerMask.LayerToName(other.gameObject.layer);
				if (layer != "TouchCollider")
				{
					return;
				}

				Debug.Log($"[PlayButton] Triggered by {other.name} on layer {other.gameObject.layer}");

				PlayButtonTrigger.Trigger();

				_PlayButtonLastTriggered = Time.realtimeSinceStartup;

			}
		}

	}
}