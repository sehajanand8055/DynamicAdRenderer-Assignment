using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Dto;
using ApiHandler;
using AdComponents;

public class AdHandler : MonoBehaviour
{
    public string url;

    #region PRIVATE VARIABLES

    private Uri _adDataUri;
    private AdFrame _adFrame;
    private AdText _adText;
    
    #endregion

    #region MONOBEHAVIOUR METHODS
    
    private void OnEnable()
    {
        _adFrame = GetComponent<AdFrame>();
        _adText = GetComponent<AdText>();
    }
    
    private void Start()
    {
        Debug.Log($"adurl is {Constant.adUrl}");    
        _adDataUri = new Uri(Constant.adUrl);
        RenderAd();
    }
    
    #endregion

    #region PUBLIC METHODS
    
    public void RenderAd()
    {
        RequestAPI.Instance.SendGetRequest(_adDataUri,delegate(string response)
        {
            if (string.IsNullOrEmpty(response))
            {
                Debug.Log("No reponse");
            } else
            {
                Debug.Log("yess ! got reponse");

                APIData apidata = JsonUtility.FromJson<APIData>(response);
                
                Debug.Log($"Api data is {Newtonsoft.Json.JsonConvert.SerializeObject(apidata)}");
                RenderAdPostProcessing(apidata);
            }
        });

    }
    
    #endregion
        
    #region PRIVATE METHODS

    // Recieved data will be placed , operations will be applied.
    private void RenderAdPostProcessing(APIData apiData)
    {
        SetText(_adText);
        foreach (var layer in apiData.layers)
        {
            LayerType layerType = ResolveLayerType(layer.type);
            Position position = layer.placement[0].position;
            
            if(layerType is LayerType.Unknown)
                continue;
            if (layerType is LayerType.Frame)
            {
                Uri textureUri = new Uri(layer.path);
                SetAdFrameTexture(textureUri);
                
                SetLayerPlacement(_adFrame,position);
                
                PerformLayerOperations(_adFrame,layer.operations);

            } else if (layerType is LayerType.Text)
            {

                SetLayerPlacement(_adText,position);
                
                PerformLayerOperations(_adText,layer.operations);
            }
        }
    }

    void SetText(AdText adText)
    {
        adText.SetText(Constant.adText);
    }


    private void PerformLayerOperations(IAdComponent adComponent,List<Operation> layerOperations)
    {
        foreach (var operation in layerOperations)
        {
            OperationType operationType = ResolveOperationType(operation.name);
            if (operationType is OperationType.Unknown)
                continue;
            if (operationType is OperationType.Color)
            {
                Color color = ResolveColorFromHashCode(operation.argument);
                adComponent.SetColor(color);
            }
        }
    }

    private void SetAdFrameTexture(Uri uri)
    {
        RequestAPI.Instance.DownloadImageTexture(uri, delegate (Texture texture)
        {
            if(texture != null)
                _adFrame.SetFrameTexture(texture);
        });
    }
    

    private void SetLayerPlacement(IAdComponent adComponent,Position pos)
    {
        Vector2 componentPosition = new Vector2(pos.x, pos.y);
        Vector2 componentSize = new Vector2(pos.width, pos.height);
                
        adComponent.SetSize(componentSize);
        adComponent.SetPosition(componentPosition);
    }
    
    #endregion

    #region UTILITY METHODS

    private LayerType ResolveLayerType(string layerType)
    {
        if (layerType.Equals(Constant.frameLayer))
        {
            return LayerType.Frame;
        } else if (layerType.Equals(Constant.textLayer))
        {
            return LayerType.Text;
        }
        return LayerType.Unknown;
    }

    private OperationType ResolveOperationType(string operationType)
    {
        if (operationType.Equals(Constant.colorOperation))
        {
            return OperationType.Color;
        }
        return OperationType.Unknown;
    }

    private Color ResolveColorFromHashCode(string hashCode)
    {
        Color color = new Color();
        ColorUtility.TryParseHtmlString(hashCode,out color);

        color.a = 1;
        
        return color;
    }
    
    #endregion
}
