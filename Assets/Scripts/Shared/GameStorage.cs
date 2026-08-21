using UnityEngine;

// Single place that knows the PlayerPrefs keys used by the game.
public static class GameStorage
{
    private const string PersonalHighscoreKey = "PersonalHighscore";
    private const string FinalTimeKey = "finalTime";

    public static float PersonalHighscore
    {
        get { return PlayerPrefs.GetFloat(PersonalHighscoreKey, 0); }
        set { PlayerPrefs.SetFloat(PersonalHighscoreKey, value); }
    }

    public static float FinalTime
    {
        get { return PlayerPrefs.GetFloat(FinalTimeKey, 0); }
        set { PlayerPrefs.SetFloat(FinalTimeKey, value); }
    }

    public static void ClearPersonalHighscore()
    {
        PlayerPrefs.DeleteKey(PersonalHighscoreKey);
    }
}
