using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using VRT.Pilots.Common;
using Cwipc;

public class StartMovie : MonoBehaviour
{
    public NetworkTrigger trigger;
    public UnityEngine.Video.VideoPlayer videoPlayer;
    public string triggerKey = "p";

    private void Start()
    {
        UnityEngine.InputSystem.Keyboard.current.onTextInput +=
            inputText => {
                if (inputText.ToString() == triggerKey)
                {
                    trigger.Trigger();
                }
            };
    }

    public void PlayVideo()
    {
#if VRT_WITH_STATS
        Cwipc.Statistics.Output("StartMovie", " Starting movie in cinema room");
#endif
        videoPlayer.Play();
    }
}
