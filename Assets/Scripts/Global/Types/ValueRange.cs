using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ValueRange {
    public float min = 0;
    public float max = 1;

    public ValueRange(float min = 0, float max = 1)
    {
        this.min = min;
        this.max = max;
    }

    public float Normalize(float val) {
        return Math.Min(Math.Max(val, min), max);
    }
}
