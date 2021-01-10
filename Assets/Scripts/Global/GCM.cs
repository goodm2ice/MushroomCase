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

    [SerializeField]
    private List<Constant> _consts;

    private static Dictionary<string, string> consts = new Dictionary<string, string>();

    void Awake()
    {
        foreach (Constant c in _consts)
            consts[c.name] = c.value;
    }

    public static string Get(string name) {
        return consts[name] ?? "Undefined";
    }
}
