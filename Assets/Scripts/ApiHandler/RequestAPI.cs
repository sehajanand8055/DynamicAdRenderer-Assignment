using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

namespace ApiHandler
{
    public class RequestAPI : MonoBehaviour
    {
        #region SINGLETON

        private static RequestAPI _instance;

        public static RequestAPI Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindObjectOfType<RequestAPI>();
                }

                return _instance;
            }
        }

        #endregion

        #region ACTION METHODS

        public void SendGetRequest(Uri url, Action<string> onResponse)
        {
            StartCoroutine(GetRequest(url, onResponse));
        }

        public void DownloadImageTexture(Uri textureUri, Action<Texture> onResponse)
        {
            Debug.Log("Downloading image texture...");
            StartCoroutine(DownloadTexture(textureUri, onResponse));
        }

        #endregion

        #region NETWORK COROUTINES

        IEnumerator GetRequest(Uri url, Action<string> onResponse)
        {
            using (UnityWebRequest webRequest = UnityWebRequest.Get(url))
            {
                yield return webRequest.SendWebRequest();

                if (webRequest.isNetworkError)
                {
                    onResponse?.Invoke(null);
                }
                else
                {
                    Debug.Log("Web response is " + webRequest.downloadHandler.text);
                    onResponse?.Invoke(webRequest.downloadHandler.text);
                }
            }
        }


        IEnumerator DownloadTexture(Uri textureUri, Action<Texture> onResponse)
        {
            using (UnityWebRequest webRequest = UnityWebRequestTexture.GetTexture(textureUri))
            {
                yield return webRequest.SendWebRequest();

                if (webRequest.isNetworkError)
                {
                    onResponse?.Invoke(null);
                }
                else
                {
                    Debug.Log("Web response is " + webRequest.downloadHandler.text);
                    onResponse?.Invoke(((DownloadHandlerTexture) webRequest.downloadHandler).texture);
                }
            }
        }

        #endregion
    }
}