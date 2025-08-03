using UnityEngine;


public class MiscHelper {
    
    public static string FormatScore(long num) {
        
        if(num >= 1_000_000_000) // 1B+
            return (num / 1_000_000_000f).ToString("0.#") + "B";
        if(num >= 1_000_000) // 1M+
            return (num / 1_000_000f).ToString("0.#") + "M";
        if(num >= 1_000) // 1K+
            return (num / 1_000f).ToString("0.#") + "K";

        return num.ToString(); // Less than 1K, show full number
    }

}