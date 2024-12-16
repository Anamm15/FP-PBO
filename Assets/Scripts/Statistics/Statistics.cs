using UnityEngine;

public class Statistics
{
    private static int _killRecord = 0;

    private const string KillRecordKey = "KillRecord";
    private const string TimeRecordKey = "TimeRecord";

    public static int KillRecord
    {
        get { return _killRecord; }
    }

    public static int LoadKillRecord()
    {
        return PlayerPrefs.GetInt(KillRecordKey);
    }

    public static int LoadTimeRecord()
    {
        return PlayerPrefs.GetInt(TimeRecordKey); 
    }

    public static string HighestTimeRecord
    {
        get 
        { 
            int timeInSeconds = LoadTimeRecord();
            int minutes = timeInSeconds / 60;
            int seconds = timeInSeconds % 60;
            return $"{minutes:00}:{seconds:00}";
        }
    }

    public static void SaveStats(int playTime)
    {
        if (_killRecord > LoadKillRecord())
        {
            PlayerPrefs.SetInt(KillRecordKey, _killRecord);
        }

        if (playTime > LoadTimeRecord())
        {
            PlayerPrefs.SetInt(TimeRecordKey, playTime);
        }

        PlayerPrefs.Save();
    }

    public static void IncrementKillRecord()
    {
        _killRecord++;
    }


    public static void ResetStats()
    {
        _killRecord = 0;
        PlayerPrefs.SetInt(KillRecordKey, 0);
        PlayerPrefs.SetInt(TimeRecordKey, 0);
        PlayerPrefs.Save();
    }
}
