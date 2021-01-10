using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GMM : MonoBehaviour
{
    public static T ParseOr<T>(string val, T _default)
    {
        try
        {
            return (T)System.Convert.ChangeType(val, typeof(T));
        }
        catch
        {
            return _default;
        }
    }
}
