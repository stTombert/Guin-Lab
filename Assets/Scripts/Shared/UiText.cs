using System;
using UnityEngine;
using UnityEngine.UI;

// Helpers for the repeated "look up a Text component and write a time into it" flow.
public static class UiText
{
    public const string TimeFormat = "mm\\:ss\\.fff";

    // Returns null and logs an error when the GameObject or its Text component is missing.
    public static Text Find(string gameObjectName, UnityEngine.Object context = null)
    {
        GameObject target = GameObject.Find(gameObjectName);
        if (!target) {
            Debug.LogError($"GameObject '{gameObjectName}' not found in the scene.", context);
            return null;
        }

        Text text = target.GetComponent<Text>();
        if (!text) {
            Debug.LogError($"GameObject '{gameObjectName}' has no {nameof(Text)} component.", target);
        }
        return text;
    }

    public static void DisplayTime(Text target, float timeToDisplay, string format = TimeFormat)
    {
        if (!target) {
            Debug.LogError("No text target given, cannot display the time.");
            return;
        }
        target.text = TimeSpan.FromSeconds(timeToDisplay).ToString(format);
    }
}
