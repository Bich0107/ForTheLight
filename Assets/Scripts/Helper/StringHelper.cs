using System;

public class StringHelper
{
    public static string SecondToString(float seconds) {
        // minutes
        int minutes = (int)seconds / 60;

        // seconds
        int secs = (int)seconds % 60;

        // milliseconds
        int milliseconds = (int)((seconds - (int)seconds) * 1000);

        // format string mm:ss:msms
        return String.Format("{0:D2}:{1:D2}:{2:D3}", minutes, secs, milliseconds / 10);
    }
}
