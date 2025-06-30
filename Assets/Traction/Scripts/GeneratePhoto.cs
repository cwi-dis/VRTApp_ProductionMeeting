using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;
using VRT.Pilots.Common;
using VRT.Orchestrator.Wrapping;

public class GeneratePhoto : NetworkInstantiator
{
   
    public Camera cameraMe;
    public GameObject endPoint;
    public GameObject CDText;
    public MeshRenderer BtnRenderer;
    public AudioSource countSound;
    public AudioSource shotSound;
    public AudioSource printSound;
    private int resWidth = 3200;
    private int resHeight = 2400;
    Vector3 displacement = new Vector3(0,0,0);
    Vector3 increment = new Vector3(((float)(Math.Tan((Math.PI / 180.0f)) * 24.0f)*-0.44f), 0, -0.44f);
    private bool takeshot = false;
    private float timeRemaining = 3;
    private bool timerIsRunning = false;
    TextMeshPro counterText;
    [SerializeField] private bool debug = true;
 
    private void Start()
    {
        CDText.SetActive(false);
        counterText = CDText.GetComponent<TextMeshPro>();
        counterText.SetText("3");

    }

    private string Name()
    {
        return "GeneratePhoto"; // xxxjack should really include identity
    }

    void Update()
    {
        if (takeshot)
        {
            if (debug) Debug.Log($"{Name()}: Start taking picture");
            shotSound.Play();
            TakeShot();
            takeshot = false;
        }
        if (timerIsRunning)
        {
            timeRemaining -= Time.deltaTime;
            if (timeRemaining > 1)
            {
                string newText = (Math.Truncate(timeRemaining)).ToString();
                if (counterText.text != newText) {
                    counterText.SetText(newText);
                }
            }
            else
            {
                if (debug) Debug.Log($"{Name()}: Timer has run out");
                timeRemaining = 0;
                timerIsRunning = false;
                BtnRenderer.material.color = new Color(0, 1, 0);
                CDText.SetActive(false);
                takeshot = true;
            }
        }
    }

    public void Run()
    {
        if (debug) Debug.Log($"{Name()}: Run() called ");
        // Turn btn into red
        BtnRenderer.material.color = new Color(1, 0, 0);

        //setup timer
        counterText.SetText("3");
        CDText.SetActive(true);
        timeRemaining = 3.99f;
        timerIsRunning = true;
        countSound.Play();
    }

    protected override GameObject InstantiateTemplateObject()
    {
        if (debug) Debug.Log($"{Name()}: InstantiateTemplateObject() ");

        RenderTexture original = cameraMe.targetTexture;
        resWidth = original.width;
        resHeight = original.height;
        Texture2D screenShot = new Texture2D(resWidth, resHeight, TextureFormat.RGB24, false);

        RenderTexture.active = original;

        // Make a new texture and read the active Render Texture into it.
        screenShot.ReadPixels(new Rect(0, 0, resWidth, resHeight), 0, 0);
        screenShot.Apply();

        RenderTexture.active = null; // JC: added to avoid errors

        //generate new photos
        GameObject newPhoto = Instantiate(templateObject, location.position, location.rotation);
        newPhoto.name = "photo_" + ((DateTimeOffset)System.DateTime.Now).ToUnixTimeSeconds();
        newPhoto.GetComponent<Renderer>().material.mainTexture = screenShot;
        newPhoto.tag = "Picture";

        if (debug) Debug.Log($"{Name()}: Starting animation to final position");
        displacement += increment;
        PrintAnimation pa = new PrintAnimation(location, endPoint.transform, newPhoto, 2.0f, printSound);
        StartCoroutine(pa.LaunchPrintAnimation());

        return newPhoto;
    }

    public void TakeShot()
    {
        if (OrchestratorController.Instance.UserIsMaster)
        {
            if (debug) Debug.Log($"{Name()}: master user, calling Trigger() to have picture taken");
            Trigger();
        } else {
            if (debug) Debug.Log($"{Name()}: not master user, let them take the picture.");
        }
    }
}

public class PrintAnimation
{
    private Transform init;
    private Transform end;
    private GameObject go;
    private float duration;
    private bool animating = false;
    private float target;
    private float value;
    private float steps;
    private Vector3 dPos;
    private Vector3 dRot;
    public AudioSource printSound;
    private bool debug;


    public PrintAnimation(Transform _init, Transform _end, GameObject _go, float _duration, AudioSource _printSound)
    {
        init = _init;
        end = _end;
        go = _go;
        duration = _duration;
        printSound = _printSound;
    }

    public IEnumerator LaunchPrintAnimation()
    {
        printSound.Play();
        go.transform.position = init.position;
        go.transform.rotation = init.rotation;
        animating = true;
        value = 0.0f;
        steps = duration / Time.deltaTime;
        Vector3 v = (end.position - init.position);
        target = v.magnitude;
        float step = target / steps;

        Vector3 dPos = v / steps;
        Vector3 dRot = (end.rotation.eulerAngles - init.rotation.eulerAngles) / steps ;

        Debug.Log($"PrintAnimation: Print init={init.position} end={end.position} v={v}");


        while (animating)
        {
            value += step;

            if (value >= target)
            {
                animating = false;
            }
            go.transform.Translate(dPos,Space.World);
            go.transform.Rotate(dRot);
            if (debug) Debug.Log("PrintAnimation: Printing pos=" + go.transform.position.ToString("F4"));
            yield return null;
        }
        Debug.Log("PrintAnimation: done");
    }
}
