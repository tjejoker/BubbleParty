using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class AssetLoder
{
    public static T AssetLoad<T>(string path) where T : Object
    {
        return Resources.Load<T>(path);
    }

}

