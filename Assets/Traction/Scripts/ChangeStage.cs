using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeStage : MonoBehaviour
{

    private GameObject[] benchStage;
    private GameObject[] busStage;
    private GameObject[] cafeStage;

    int selected = 0;
    public GameObject[] highlighters;

    private void Start() 
    {
        if (benchStage == null) benchStage = GameObject.FindGameObjectsWithTag("Bench");
        if (busStage == null) busStage = GameObject.FindGameObjectsWithTag("Bus");
        if (cafeStage == null) cafeStage = GameObject.FindGameObjectsWithTag("Cafe");

        ToBench();
        UpdateHighlighters();
    }

    private void Update()
    {
    }


    // ------------------------------------------------------------

    public void ToBench()
    {
        foreach (GameObject obj in benchStage)
        {
            obj.SetActive(true);
        }
        foreach (GameObject obj in busStage)
        {
            obj.SetActive(false);
        }
        foreach (GameObject obj in cafeStage)
        {
            obj.SetActive(false);
        }
        selected = 0;
        UpdateHighlighters();
        
    }
    public void ToBus()
    {
        foreach (GameObject obj in benchStage)
        {
            obj.SetActive(false);
        }
        foreach (GameObject obj in busStage)
        {
            obj.SetActive(true);
        }
        foreach (GameObject obj in cafeStage)
        {
            obj.SetActive(false);
        }
        selected = 1;
        UpdateHighlighters();
    }
    public void ToCafe()
    {
        foreach (GameObject obj in benchStage)
        {
            obj.SetActive(false);
        }
        foreach (GameObject obj in busStage)
        {
            obj.SetActive(false);
        }
        foreach (GameObject obj in cafeStage)
        {
            obj.SetActive(true);
        }
        selected = 2;
        UpdateHighlighters();
    }
    private void UpdateHighlighters()
    {
        int i = 0;
        foreach (var highlighter in highlighters)
        {
            if (i == selected)
            {
                highlighter.SetActive(true);
            }
            else
            {
                highlighter.SetActive(false);
            }
            i++;
        }
    }
}
