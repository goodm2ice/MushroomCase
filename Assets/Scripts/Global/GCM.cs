using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GCM : MonoBehaviour
{
    [System.Serializable]
    struct Constant
    {
        public string name, value;
    }

    public static Dictionary<string, string> consts = new Dictionary<string, string>();
    public static string DefaultString = "Undefined";

    [SerializeField]
    private List<Constant> _consts;


    void Awake()
    {
        foreach (Constant c in _consts)
            consts[c.name] = c.value;
    }

    public static string Get(string name) => consts[name] ?? DefaultString;

    public static T Get<T>(string name, T _default = default(T)) => GMM.ParseOr<T>(Get(name), _default);
}
