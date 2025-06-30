using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRT.Pilots;
using VRT.Pilots.Common;
using VRT.Pilots.Traction;
using VRT.UserRepresentation.PointCloud;

public class TractionExtras : MonoBehaviour
{
    public GameObject PointcloudGO;

    // Start is called before the first frame update
    void Start()
    {
        PlayerControllerBase playerManager = GetComponent<PlayerControllerBase>();
        if (playerManager == null) playerManager = GetComponentInParent<PlayerControllerBase>();
        if (!((TractionController)TractionController.Instance).disablePointclouds)
        {
            PointCloudPipelineBase pcPipeline = PointcloudGO?.GetComponent<PointCloudPipelineBase>();
            pcPipeline?.PausePlayback(false);
        }
        else
        {
            PointCloudPipelineBase pcPipeline = PointcloudGO?.GetComponent<PointCloudPipelineBase>();
            pcPipeline?.PausePlayback(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
