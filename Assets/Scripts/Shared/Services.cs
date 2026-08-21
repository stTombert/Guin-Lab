using UnityEngine;

// Cached access to the scene wide services, so the individual scripts do not
// have to run their own FindObjectOfType lookups.
public static class Services
{
    private static GameEngineService gameEngine;
    private static TimerService timer;

    public static GameEngineService GameEngine
    {
        get { return Resolve(ref gameEngine); }
    }

    public static TimerService Timer
    {
        get { return Resolve(ref timer); }
    }

    private static T Resolve<T>(ref T cached) where T : MonoBehaviour
    {
        if (cached == null) {
            cached = Object.FindObjectOfType<T>();
        }
        return cached;
    }
}
