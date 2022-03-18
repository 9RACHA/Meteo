using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System;

// UnityWebRequest.Get example

// Access a website and use UnityWebRequest.Get to download a page.
// Also try to download a non-existing page. Display the error.

[Serializable]
public class MeteoMain : MonoBehaviour
{
    public Dia dia;

    public Prediccion predicion; //Castrapo

    void Start()
    {
        // A correct website page.
        StartCoroutine(GetRequest("https://servizos.meteogalicia.gal/mgrss/predicion/jsonCPrazo.action?dia=0&request_locale=gl"));

        // A non-existing page.
        //StartCoroutine(GetRequest("https://error.html"));
    }

    IEnumerator GetRequest(string uri)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
        {
            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();

            string[] pages = uri.Split('/');
            int page = pages.Length - 1;

            switch (webRequest.result)
            {
                case UnityWebRequest.Result.ConnectionError:
                case UnityWebRequest.Result.DataProcessingError:
                    Debug.LogError(pages[page] + ": Error: " + webRequest.error);
                    break;
                case UnityWebRequest.Result.ProtocolError:
                    Debug.LogError(pages[page] + ": HTTP Error: " + webRequest.error);
                    break;
                case UnityWebRequest.Result.Success:
                    Debug.Log(pages[page] + ":\nReceived: " + webRequest.downloadHandler.text);
                    break;
            }

            dia = JsonUtility.FromJson<Dia>(webRequest.downloadHandler.text);
            predicion = JsonUtility.FromJson<Prediccion>(webRequest.downloadHandler.text);
            Debug.Log(dia.listaPredicions[0]);
            Debug.Log(predicion.dia);
            Debug.Log(predicion.comentario);
            /*Debug.Log(r.results[1].question);
            Debug.Log(r.results[3].correct_answer);
            Debug.Log(r.results[7].difficulty);
            Debug.Log(r.results[2].incorrect_answers[1]);*/
        }
    }
}
