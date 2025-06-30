using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowPanel : MonoBehaviour
{
    public GameObject Panel;
    public bool showPanelOnInit;
    int occupancy = 0;
    // Start is called before the first frame update
    void Start()
    {
        Panel.SetActive(showPanelOnInit);
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnTriggerEnter(Collider collision)
    {
        occupancy++;
        Panel.SetActive(true);
        //Debug.Log("Object Entered the trigger, Occupancy ="+occupancy);
    }
    private void OnTriggerStay(Collider collision)
    {
        //Debug.Log("Object is within the trigger, Occupancy =" + occupancy);
    }
    private void OnTriggerExit(Collider collision)
    {
        occupancy--;
        if (occupancy == 0)
        {
            Panel.SetActive(false);
        }
        //Debug.Log("Object Exited the trigger, Occupancy =" + occupancy);
    }
}
