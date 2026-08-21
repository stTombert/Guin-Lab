using System;
using System.Collections.Generic;
using UnityEngine;

// Cached access to the scene wide services, so the individual scripts do not
// have to run their own FindObjectOfType lookups and null checks.
public static class Services
{
    private static readonly Dictionary<Type, MonoBehaviour> cache = new Dictionary<Type, MonoBehaviour>();

    public static GameEngineService GameEngine
    {
        get { return Get<GameEngineService>(); }
    }

    public static TimerService Timer
    {
        get { return Get<TimerService>(); }
    }

    // Returns null when the service is not part of the current scene.
    public static T Get<T>() where T : MonoBehaviour
    {
        MonoBehaviour cached;
        if (cache.TryGetValue(typeof(T), out cached) && cached) {
            return (T)cached;
        }

        T service = UnityEngine.Object.FindObjectOfType<T>();
        cache[typeof(T)] = service;
        return service;
    }

    // Like Get, but logs an error that names the missing service and the
    // consequence of it being missing.
    public static T Require<T>(UnityEngine.Object context, string consequence) where T : MonoBehaviour
    {
        T service = Get<T>();
        if (!service) {
            Debug.LogError($"{typeof(T).Name} not found in the scene, {consequence}.", context);
        }
        return service;
    }
}
