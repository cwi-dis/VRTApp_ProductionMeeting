using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using System.Net;
using System.Net.Mail;

public class MailBox : MonoBehaviour
{
    //public GameObject pc;
    // Start is called before the first frame update
    public AudioSource send_sound;

    public bool debug = false;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //// code used for debug
        ////get the input
        //var input = Input.inputString;
        ////ignore null input to avoid unnecessary computation
        //if (!string.IsNullOrEmpty(input))
        //    //Debug.Log("Pressed char: " + Input.inputString);
        //    if (Input.inputString == "m")
        //    {
        //        GameObject pc = GameObject.FindGameObjectsWithTag("Picture")[0];
        //        pc.GetComponent<Collider>().enabled = false;
        //        AnimatePicture(pc);
        //        SavePicture(pc);
        //    }
    }

    public void SavePicture(GameObject go)
    {
        SaveRTToFile((Texture2D)go.GetComponent<Renderer>().material.mainTexture, go.name);
    }

    public string SaveRTToFile(Texture2D tex, string filename)
    {
        byte[] bytes;
        bytes = tex.EncodeToPNG();

        String folderPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyPictures), "TractionLobby");
        Directory.CreateDirectory(folderPath);
        String fullFilename = Path.Combine(folderPath, filename + ".png");
        System.IO.File.WriteAllBytes(fullFilename, bytes);
        if (debug) Debug.Log("MailBox: Saved picture to " + fullFilename);
        return fullFilename;
    }

    public void AnimatePicture(GameObject go)
    {
        if (debug) Debug.Log("MailBox: Animating picture");

        //setting picture as a child of the mailbox to change the position relative to it
        GameObject mb = GameObject.FindGameObjectsWithTag("MailBox")[0];
        go.transform.SetParent(mb.transform);
        go.transform.rotation = mb.transform.rotation;
        go.transform.Rotate(0, 90.0f, 0, Space.World);

        //enabling and launching animation and mailbox sound
        Animator anim = go.GetComponent<Animator>();
        anim.enabled = true;
        anim.Play("MoveDown");
        send_sound.PlayDelayed(1.3f);

    }

    private void OnTriggerEnter(Collider collision)
    {
        GameObject go = collision.gameObject;
        if (collision.CompareTag("Picture")) //go.tag == "Picture")
        {
            if (debug) Debug.Log("MailBox: A picture collided with the mailbox");

            //disable collider to stop holding the picture in the hand
            go.GetComponent<Collider>().enabled = false;

            //disable grabbable script so it does not return parent to null
            go.GetComponent<VRT.Pilots.Common.VRTGrabbableController>().enabled = false;

            //run animation
            AnimatePicture(go);
            SavePicture(go);
        }
    }


    private bool SendByEmail(string picpath)
    {
        try
        {
            if (debug) Debug.Log($"MailBox.SendByMail: sending {picpath}");
            MailMessage mail = new MailMessage();

            mail.From = new MailAddress("cwidistest@gmail.com");
            mail.To.Add("cwidistest@gmail.com");
            mail.Subject = "Test";
            mail.Body = "testing pics sending";
            mail.Attachments.Add(new Attachment(picpath, @"image/png"));

            SmtpClient smtpServer = new SmtpClient("smtp.gmail.com");
            smtpServer.Port = 587;
            smtpServer.Credentials = new System.Net.NetworkCredential("cwidistest@gmail.com", "xxx") as ICredentialsByHost;
            smtpServer.EnableSsl = true;
            ServicePointManager.ServerCertificateValidationCallback =
                delegate (object s, System.Security.Cryptography.X509Certificates.X509Certificate certificate, System.Security.Cryptography.X509Certificates.X509Chain chain, System.Net.Security.SslPolicyErrors sslPolicyErrors)
                {
                    return true;
                };
            smtpServer.Send(mail);
            if (debug) Debug.Log("Mailbox.SendByMail: success");
        }
        catch (Exception ex)
        {
            Debug.LogError("MailBox: Error sending email: " + ex.ToString());
            return false;
        }
        return true;
    }
}
