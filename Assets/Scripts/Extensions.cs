using System.Collections.Generic;
using UnityEngine;

public static partial class Extensions
{
    public static WWWForm Add(this WWWForm form, KeyValuePair<string, string> kvp)
    {
        form.AddField(kvp.Key, kvp.Value);
        return form;
    }
    public static WWWForm Add(this WWWForm form, string key, string value)
    {
        form.AddField(key, value);
        return form;
    }
}