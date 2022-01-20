using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AdLoader : MonoBehaviour
{
    public InputField adUrl;
    public InputField adText;

    public void LoadAd()
    {
        Constant.adUrl = adUrl.text;
        Constant.adText = adText.text;
        SceneManager.LoadScene(1);
    }
}
