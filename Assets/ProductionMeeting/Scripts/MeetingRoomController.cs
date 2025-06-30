using UnityEngine;
using VRT.Pilots.Common;
using VRT.Core;

namespace VRT.Pilots.ProductionMeeting
{
    public class MeetingRoomController : PilotController
    {
        //public string sceneName = ""; // Name of the scene to load
        public GameObject[] sceneChangeButton; // Reference to the button that will trigger the scene load

        public void LoadSceneName(string sceneName)
        {

            // Load the portal scene when the user clicks the button
            LoadNewScene(sceneName);
        }

    } 


}

