using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeSeason : MonoBehaviour
{
    [SerializeField] private Material floor;
    [SerializeField] private Material summer, autumn, winter, spring;
    [SerializeField] private Texture grass_Albedo, grass_Normal, wood_Albedo, wood_Normal, snow_Albedo, snow_Normal;
    [SerializeField] private Texture spring_Tex, summer_Tex, autumn_Tex, winter_Tex;

    private bool isSnowing;
    private bool isBlooming;
    private bool isFogging;

    private GameObject[] treeObjects;
    private GameObject[] snowObjects;
    private GameObject[] groundObjects;
    private GameObject[] flowerObjects;
    private GameObject[] fogObjects;

    int selected = 0;
    public GameObject[] highlighters;


    /* change 1) tree color (leaves), 
              2) ground color, 
              3) additional effects - snows for winter, flowers for Summer

    */

    private void Start() 
    {
        if (treeObjects == null) treeObjects = GameObject.FindGameObjectsWithTag("Tree");
        if (snowObjects == null) snowObjects = GameObject.FindGameObjectsWithTag("Snow");
        if (groundObjects == null) groundObjects = GameObject.FindGameObjectsWithTag("Ground");
        if (flowerObjects == null) flowerObjects = GameObject.FindGameObjectsWithTag("Flower");
        if (fogObjects == null) fogObjects = GameObject.FindGameObjectsWithTag("Fog");

        ToSpring();
        UpdateHighlighters();

    }

    private void Update()
    {
    }



    private void ChangeLeaves(Texture tex, Color col)
    {
        foreach (GameObject treeObject in treeObjects)
        {
            Renderer ren = treeObject.GetComponent<Renderer>();
            ren.material.SetTexture("_MainTex", tex);
            ren.material.SetColor("_Color", col);
        }
    }


    private void ChangeGrounds(Texture albedo, Texture normal)
    {
        foreach (GameObject groundObject in groundObjects)
        {
            Renderer ren = groundObject.GetComponent<Renderer>();
            ren.material.EnableKeyword("_NORMALMAP");

            ren.material.SetTexture("_MainTex", albedo);
            ren.material.SetTexture("_BumpMap", normal);

        }
    }

    private void Snowing(bool bl)
    {
  
        if (bl && !isSnowing) {
            isSnowing = true;
            foreach (GameObject snow in snowObjects)
            {
                snow.GetComponent<ParticleSystem>().Play();
            }    
        }

        if (!bl && isSnowing) {
            isSnowing = false;
            foreach (GameObject snow in snowObjects) {
                snow.GetComponent<ParticleSystem>().Stop();
            }
        }
    
    }

    private void Blooming(bool bl)
    {

        if (bl && !isBlooming) {
            isBlooming = true;
            foreach (GameObject flower in flowerObjects)
            {
                flower.SetActive(true);
            }
        }

        if (!bl && isBlooming) {
            isBlooming = false;
            foreach (GameObject flower in flowerObjects)
            {
                flower.SetActive(false);
            }
        }
    }

    private void Fogging(bool bl)
    {
  
        if (bl && !isFogging) {
            isFogging = true;
            foreach (GameObject fog in fogObjects)
            {
                fog.GetComponent<ParticleSystem>().Play();
            }    
        }

        if (!bl && isFogging) {
            isFogging = false;
            foreach (GameObject fog in fogObjects) {
                fog.GetComponent<ParticleSystem>().Stop();
            }
        }
    
    }
    // ------------------------------------------------------------
    public void ToSpring()
    {
        ChangeLeaves(spring_Tex, spring.color);
        ChangeGrounds(grass_Albedo, grass_Normal);
        Snowing(false);
        Blooming(true);
        Fogging(false);
        selected = 0;
        UpdateHighlighters();
    }
    public void ToSummer()
    {
        ChangeLeaves(summer_Tex, summer.color);
        ChangeGrounds(grass_Albedo, grass_Normal);
        Snowing(false);
        Blooming(false);
        Fogging(false);
        selected = 1;
        UpdateHighlighters();
    }
    public void ToAutumn()
    {
        ChangeLeaves(autumn_Tex, autumn.color);
        ChangeGrounds(wood_Albedo, wood_Normal);
        Snowing(false);
        Blooming(false);
        Fogging(true);
        selected = 2;
        UpdateHighlighters();
    }
    public void ToWinter()
    {
        ChangeLeaves(winter_Tex, winter.color);
        ChangeGrounds(snow_Albedo, snow_Normal);
        Snowing(true);
        Blooming(false);
        Fogging(true);
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
