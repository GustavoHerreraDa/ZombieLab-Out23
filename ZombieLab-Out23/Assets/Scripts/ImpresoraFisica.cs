using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using Sfs2X.Entities.Data;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEditor;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Drawing;
using System.Diagnostics;



public class ImpresoraFisica : MonoBehaviour
{
    public GameObject Marco;
    string path = null;

    void Start()
    {
        path = Application.dataPath + "/Ticket.pdf";


    }

    public void GenerateFile()
    {
        if (File.Exists(path))
            File.Delete(path);
        using (var fileStream = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Write))
        {
            var document = new Document(PageSize.A4.Rotate(), 0f, 0f, 0f, 0f);

            var writer = PdfWriter.GetInstance(document, fileStream);

            document.Open();

            document.NewPage();

            if (Marco.GetComponent<MarcoCambiador>().NumeroMark == 1)
            {
                iTextSharp.text.Image Certificado = iTextSharp.text.Image.GetInstance(Application.dataPath + "/CertificadoCompleto1.png");
                Certificado.ScaleAbsoluteWidth(841);
                Certificado.ScaleAbsoluteHeight(595);

                document.Add(Certificado);
            }

            if (Marco.GetComponent<MarcoCambiador>().NumeroMark == 2)
            {
                iTextSharp.text.Image Certificado = iTextSharp.text.Image.GetInstance(Application.dataPath + "/CertificadoCompleto2.png");
                Certificado.ScaleAbsoluteWidth(841);
                Certificado.ScaleAbsoluteHeight(595);

                document.Add(Certificado);
            }

            if (Marco.GetComponent<MarcoCambiador>().NumeroMark == 3)
            {
                iTextSharp.text.Image Certificado = iTextSharp.text.Image.GetInstance(Application.dataPath + "/CertificadoCompleto3.png");
                Certificado.ScaleAbsoluteWidth(841);
                Certificado.ScaleAbsoluteHeight(595);

                document.Add(Certificado);
            }

            if (Marco.GetComponent<MarcoCambiador>().NumeroMark == 4)
            {
                iTextSharp.text.Image Certificado = iTextSharp.text.Image.GetInstance(Application.dataPath + "/CertificadoCompleto4.png");
                Certificado.ScaleAbsoluteWidth(841);
                Certificado.ScaleAbsoluteHeight(595);

                document.Add(Certificado);
            }

            if (Marco.GetComponent<MarcoCambiador>().NumeroMark == 5)
            {
                iTextSharp.text.Image Certificado = iTextSharp.text.Image.GetInstance(Application.dataPath + "/CertificadoCompleto5.png");
                Certificado.ScaleAbsoluteWidth(841);
                Certificado.ScaleAbsoluteHeight(595);

                document.Add(Certificado);
            }

            if (Marco.GetComponent<MarcoCambiador>().NumeroMark == 6)
            {
                iTextSharp.text.Image Certificado = iTextSharp.text.Image.GetInstance(Application.dataPath + "/CertificadoCompleto6.png");
                Certificado.ScaleAbsoluteWidth(841);
                Certificado.ScaleAbsoluteHeight(595);

                document.Add(Certificado);
            }

            if (Marco.GetComponent<MarcoCambiador>().NumeroMark == 7)
            {
                iTextSharp.text.Image Certificado = iTextSharp.text.Image.GetInstance(Application.dataPath + "/CertificadoCompleto7.png");
                Certificado.ScaleAbsoluteWidth(841);
                Certificado.ScaleAbsoluteHeight(595);

                document.Add(Certificado);
            }

            if (Marco.GetComponent<MarcoCambiador>().NumeroMark == 8)
            {
                iTextSharp.text.Image Certificado = iTextSharp.text.Image.GetInstance(Application.dataPath + "/CertificadoCompleto8.png");
                Certificado.ScaleAbsoluteWidth(841);
                Certificado.ScaleAbsoluteHeight(595);

                document.Add(Certificado);
            }



            document.Close();
            writer.Close();
        }



        //StreamWriter writer = new StreamWriter(path, false);
        //writer.WriteLine(string.Format("Ticket Id : {0}",iSFSObject.GetUtfString("TICKET_ID")));
        //var betting = iSFSObject.GetSFSArray("BET_DETAILS");
        //for (int i = 0; i< betting.Count;i++)
        //    writer.WriteLine(string.Format("Bet Number : {0}     BetAmount : {1}", betting.GetSFSObject(i).GetUtfString("BET_NUM"), betting.GetSFSObject(i).GetDouble("BET_AMOUNT")));
        //writer.Close();

        PrintFiles();
    }

    void PrintFiles()
    {
        //Debug.Log(path);
        if (path == null)
            return;

        if (File.Exists(path))
        {
            //Debug.Log("file found");
            //var startInfo = new System.Diagnostics.ProcessStartInfo(path);
            //int i = 0;
            //foreach (string verb in startInfo.Verbs)
            //{
            //    // Display the possible verbs.
            //    Debug.Log(string.Format("  {0}. {1}", i.ToString(), verb));
            //    i++;
            //}
        }
        else
        {
            //Debug.Log("file not found");
            return;
        }
        //System.Diagnostics.Process process = new System.Diagnostics.Process();
        //process.StartInfo.CreateNoWindow = true;
        //process.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Normal;
        //process.StartInfo.UseShellExecute = true;
        //process.StartInfo.FileName = path;
        //process.StartInfo.Verb = "print";

        //process.Start();
        //process.WaitForExit();

        ProcessStartInfo info = new ProcessStartInfo(path);
        info.Verb = "print";
        info.CreateNoWindow = true;
        info.WindowStyle = ProcessWindowStyle.Hidden;

        Process p = new Process();
        p.StartInfo = info;
        p.Start();
        p.WaitForExit();


    }
}