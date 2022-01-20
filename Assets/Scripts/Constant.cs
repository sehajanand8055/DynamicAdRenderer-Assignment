using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum LayerType
{
    Frame,
    Text,
    Unknown
}

public enum OperationType
{
    Color,
    Unknown
}

public static class Constant
{

    internal static string adUrl;
    internal static string adText;

    #region LAYERS
    
    internal static string frameLayer = "frame";
    internal static string textLayer = "text";
    
    #endregion

    #region OPERATIONS
    
    internal static string colorOperation = "color";
    
    #endregion
    
}
