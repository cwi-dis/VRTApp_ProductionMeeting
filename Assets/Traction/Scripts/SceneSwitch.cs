using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using VRT.Pilots.Common;
using VRT.Pilots.Traction;
using Cwipc;

public class SceneSwitch : MonoBehaviour
{
    public NetworkTrigger trigger;
    public string triggerKey;
    // Start is called before the first frame update
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

    void Update()
    {
    }

}
