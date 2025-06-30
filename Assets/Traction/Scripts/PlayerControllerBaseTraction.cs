using UnityEngine;
using System.Reflection;
using TMPro;
using VRT.Core;
using System;
using VRT.UserRepresentation.Voice;

namespace VRT.Pilots.Common
{
	// xxxjack this functionality somehow needs to be merged into the Traction player
	// controllers. Unsure about the best way to do this.
    static public class PlayerControllerBaseTraction
    {

        public static void EnableAudio(this PlayerControllerBase _this)
        {
            // _this.voice.SetActive(true);
        }

        public static void DisableAudio(this PlayerControllerBase _this)
        {
            // _this.voice.SetActive(false);
        }

        public static void EnablePointCloud(this PlayerControllerBase _this)
        {
            // _this.pointcloud.SetActive(true);
        }

        public static void DisablePointCloud(this PlayerControllerBase _this)
        {
            // _this.pointcloud.SetActive(false);
        }

        public static void EnableHands(this PlayerControllerBase _this)
        {
            // xxxNacho enable hands
        }

        public static void DisableHands(this PlayerControllerBase _this)
        {
            // xxxNacho disable hands
        }





    }
}