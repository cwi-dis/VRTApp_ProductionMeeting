using UnityEngine;
using VRT.Pilots.Common;
using VRT.UserRepresentation.PointCloud;
using VRT.UserRepresentation.Voice;

namespace VRT.Pilots.Traction
{
    public class TractionController : PilotController
    {
        [Tooltip("Disable pointclouds in this scene")]
        public bool disablePointclouds = false;
        [Tooltip("Disable hands in this scene")]
        public bool disableHands = false;
        [Tooltip("Disable audio in this scene")]
        public bool disableAudio = false;
        [Tooltip("Enable fadein/fadeout at beginning and end of scene")]
        public bool enableFade = true;
        [Tooltip("Text to show on the fadein")]
        public string fadeInText = "";
       
        public override void Awake() { base.Awake(); }


        // Start is called before the first frame update
        public override void Start()
        {
            base.Start();
            if (enableFade)
            {
                if (CameraFader.Instance != null)
                {
                    CameraFader.Instance.SetText(fadeInText);
                    StartCoroutine(CameraFader.Instance.FadeIn());
                }
            }

            SessionPlayersManager playerManager = GetComponent<SessionPlayersManager>();
            foreach (PlayerNetworkControllerBase pncb in playerManager.AllUsers)
            {
                PlayerControllerBase pcb = pncb.GetComponentInParent<PlayerControllerBase>();
                if (disablePointclouds)
                {
                    pcb.DisablePointCloud();
                }

                if (disableAudio)
                {
                    pcb.DisableAudio();
                }

                if (disableHands)
                {
                    pcb.DisableHands();
                }
            }

        }

    }
}