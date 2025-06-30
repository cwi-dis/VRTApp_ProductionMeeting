using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRT.Pilots.Common;

public class HideTraining : MonoBehaviour
{
    public GameObject objectToHide;
    public string triggerKey;
    public NetworkTrigger trigger;
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

    // Update is called once per frame
    void Update()
    {
    }

    public void Hide()
    {
        objectToHide.SetActive(false);
    }
}
