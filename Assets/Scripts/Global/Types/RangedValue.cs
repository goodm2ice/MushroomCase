using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class RangedValue : ValueRange
{
    [SerializeField]
    private float _cur;
    public float cur
    {
        get => _cur;
        set => _cur = Normalize(value);
    }


    public RangedValue(float min = 0, float max = 1, float cur = 0): base(min, max)
    {
        this.cur = cur;
    }
}
