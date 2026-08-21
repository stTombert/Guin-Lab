using System;
using UnityEngine;
using UnityEngine.UI;

// Helpers for the repeated "look up a Text component and write a time into it" flow.
public static class UiText
{
    public const string TimeFormat = "mm\\:ss\\.fff";

    public static Text Find(string gameObjectName)
    {
        GameObject target = GameObject.Find(gameObjectName);
        if (!target) {
            Debug.LogWarning("Es gibt kein GameObject mit dem Namen " + gameObjectName);
            return null;
        }
        return target.GetComponent<Text>();
    }

    public static void DisplayTime(Text target, float timeToDisplay, string format = TimeFormat)
    {
        if (!target) {
            return;
        }
        target.text = TimeSpan.FromSeconds(timeToDisplay).ToString(format);
    }
}
