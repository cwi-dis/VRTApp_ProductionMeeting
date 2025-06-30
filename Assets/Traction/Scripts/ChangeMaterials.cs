using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeMaterials : MonoBehaviour
{
    [SerializeField] private Material wall;
    [SerializeField] private Material purple, pink, mint, lime;


    private GameObject[] lightObjects;
    private GameObject[] colorObjects;

    int selected = 0;
    public GameObject[] highlighters;

    private void Start()
    {
        if (colorObjects == null) colorObjects = GameObject.FindGameObjectsWithTag("ColorWall");
        if (lightObjects == null) lightObjects = GameObject.FindGameObjectsWithTag("ColorLight");
        UpdateHighlighters();
    }

    private void GetChildRecursive(GameObject obj)
    {
        List<GameObject> lightList = new List<GameObject>();

        if (null == obj)
            return;

        foreach (Transform child in obj.transform)
        {
            if (null == child)
                continue;
            //child.gameobject contains the current child you can do whatever you want like add it to an array
            if (child.tag == "ColorLight")
                lightList.Add(child.gameObject);
            GetChildRecursive(child.gameObject);
        }

        lightObjects = lightList.ToArray();
    }

    private void Update()
    {
    }

    private void ChangeLights(Color c) //find all the colorlights
    {
        foreach (GameObject lightObject in lightObjects)
        {
            Light lt = lightObject.GetComponent<Light>();
            lt.color = LerpColor(lt.color, c);
        }
            
    }

    private void ChangeWalls(Color albedo, Color emission)
    {
        foreach (GameObject colorObject in colorObjects)
        {
            Renderer ren = colorObject.GetComponent<Renderer>();
            ren.material.SetColor("_Color", albedo);
            ren.material.SetColor("_EmissionColor", emission);
        }
    }

    // ------------------------------------------------------------

    public void ToPurple()
    {
        ChangeLights(purple.color);
        ChangeWalls(purple.color, new Color(0.242f, 0.134f, 0.604f));
        selected = 0;
        UpdateHighlighters();
    }

    public void ToPink()
    {
        ChangeLights(pink.color);
        ChangeWalls(pink.color, new Color(0.547f, 0.266f, 0.266f));
        selected = 1;
        UpdateHighlighters();
    }
    public void ToMint()
    {
        ChangeLights(mint.color);
        ChangeWalls(mint.color, new Color(0.256f, 0.623f, 0.575f));
        selected = 2;
        UpdateHighlighters();
    }
    public void ToLime()
    {
        ChangeLights(lime.color);
        ChangeWalls(lime.color, new Color(0.306f, 0.585f, 0.290f));
        selected = 3;
        UpdateHighlighters();
    }

    // ------------------------------------------------------------
    private Color LerpColor(Color c1, Color c2)
    {
        return Color.Lerp(c1, c2, 3.0f);
    }

    private void UpdateHighlighters()
    {
        int i = 0;
        foreach (var highlighter in highlighters) {
            if (i == selected) {
                highlighter.SetActive(true);
            }
            else {
                highlighter.SetActive(false);
            }
            i++;
        }
    }
}
