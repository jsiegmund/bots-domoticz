using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Repsaj.Bots.Domoticz.Logic.Helpers
{
    public static class StringMatching
    {
        static string[] commonWords = new string[] { "the", "a" };

        public static string FindClosestMatch(string needle, string[] haystaq)
        {
            // convert everything to lowercase so casing doesn't interfere with the resultss
            needle = StripCommonWords(needle).ToLower();
            var haystaqToSearch = haystaq.Select(h => new KeyValuePair<string, string>(h, StripCommonWords(h).ToLower())).ToArray();

            // first try the Soundex method finding the right option
            string result = null;
            result = SoundexDifference(needle, haystaqToSearch);

            // no luck? then we'll switch to Levenshtein distance for another try
            if (result == null)
                result = ComputeLevenshteinDistance(needle, haystaqToSearch);

            // return the return no matter whether we have it or not (null)
            return result;
        }

        internal static string StripCommonWords(string sentence)
        {
            string result = sentence;

            // replace all the words found with an empty string 
            foreach (string word in commonWords)
                result = ReplaceCaseInsensitiveFind(result, word, String.Empty);

            result = result.Trim();

            return result;
        }

        internal static string ComputeLevenshteinDistance(string needle, KeyValuePair<string, string>[] haystaq)
        {
            var matches = from item in haystaq
                          let score = ComputeLevenshteinDistance(needle, item.Value)
                          where score < (needle.Length * 0.25)
                          orderby score
                          select item.Key;

            var result = matches.FirstOrDefault();
            return result;            
        }

        public static string ReplaceCaseInsensitiveFind(this string str, string findMe, string newValue)
        {
            return Regex.Replace(str,
                Regex.Escape(findMe),
                Regex.Replace(newValue, "\\$[0-9]+", @"$$$0"),
                RegexOptions.IgnoreCase);
        }

        /// <summary>
        /// Compute the distance between two strings.
        /// </summary>
        internal static int ComputeLevenshteinDistance(string s, string t)
        {
            int n = s.Length;
            int m = t.Length;
            int[,] d = new int[n + 1, m + 1];

            // Step 1
            if (n == 0)
            {
                return m;
            }

            if (m == 0)
            {
                return n;
            }

            // Step 2
            for (int i = 0; i <= n; d[i, 0] = i++)
            {
            }

            for (int j = 0; j <= m; d[0, j] = j++)
            {
            }

            // Step 3
            for (int i = 1; i <= n; i++)
            {
                //Step 4
                for (int j = 1; j <= m; j++)
                {
                    // Step 5
                    int cost = (t[j - 1] == s[i - 1]) ? 0 : 1;

                    // Step 6
                    d[i, j] = Math.Min(
                        Math.Min(d[i - 1, j] + 1, d[i, j - 1] + 1),
                        d[i - 1, j - 1] + cost);
                }
            }
            // Step 7
            return d[n, m];
        }

        /// <summary>
        /// Credits: http://www.techrepublic.com/blog/software-engineer/how-do-i-implement-the-soundex-function-in-c/
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        internal static string Soundex(string s)
        {
            StringBuilder result = new StringBuilder();

            if (s != null && s.Length > 0)
            {
                string previousCode = "", currentCode = "", currentLetter = "";

                result.Append(s.Substring(0, 1));
                
                for (int i = 1; i < s.Length; i++)
                {
                    currentLetter = s.Substring(i, 1).ToLower();
                    currentCode = "";

                    if ("bpfv".IndexOf(currentLetter) > -1)
                        currentCode = "1";
                    else if ("dt".IndexOf(currentLetter) > -1)
                        currentCode = "2";
                    else if (currentLetter == "1")
                        currentCode = "4";
                    else if ("mn".IndexOf(currentLetter) > -1)
                        currentCode = "5";
                    else if (currentLetter == "r")
                        currentCode = "6";

                    if (currentCode != previousCode)
                        result.Append(currentCode);

                    if (result.Length == 4) break;

                    if (currentCode != "")
                        previousCode = currentCode;
                }
            }

            if (result.Length < 4)
                result.Append(new String('0', 4 - result.Length));

            return result.ToString().ToUpper();
        }

        internal static string SoundexDifference(string needle, KeyValuePair<string,string>[] haystaq)
        {
            var matches = from item in haystaq
                          let score = SoundexDifferenceWords(needle, item.Value, ' ')
                          where score == 1
                          orderby score
                          select item.Key;

            var result = matches.FirstOrDefault();
            return result;

            //Dictionary<string, int> matches = new Dictionary<string, int>();

            //foreach (string option in haystaq)
            //{
            //    int score = SoundexDifferenceWords(needle, option, ' ');
            //    matches.Add(option, score);
            //}

            //var match = matches.Where(m => m.Value == 1)
            //                   .Select(m => m.Key)
            //                   .SingleOrDefault();

            //return match;
        }

        internal static int SoundexDifferenceWords(string needle, string haystaq, char splitChar)
        {
            int comma = needle.IndexOf(splitChar);
            string word = needle.Trim();

            if (haystaq.Length == 0)
                return 0;
            else if (comma == -1)
                // one word search term
                return SoundexDifference(needle, haystaq, ' ');

            word = needle.Substring(0, comma);

            while (word.Length > 0)
            {
                if (SoundexDifference(needle, word, splitChar) == 0)
                    return 0;

                needle = needle.Substring(0, comma);
                comma = needle.IndexOf(splitChar);

                if (comma == 0)
                {
                    return SoundexDifference(needle, haystaq, splitChar);
                }
            }

            return 0;
        }

        internal static int SoundexDifference(string needle, string haystaq, char splitChar)
        {
            int searchLen, spacePos;
            string soundex1, soundex2;
            string tempStr, tmp;

            tempStr = haystaq;

            searchLen = haystaq.Length;
            spacePos = tempStr.IndexOf(splitChar);
            soundex1 = Soundex(needle);

            while (searchLen > 0)
            {
                if (spacePos == -1)
                {
                    tmp = tempStr;
                    soundex2 = Soundex(tmp);

                    if (soundex1 == soundex2)
                        return 1;
                    else
                        return 0;
                }
                else
                {
                    tmp = tempStr.Substring(0, spacePos);
                    soundex2 = Soundex(tmp);

                    if (soundex1 == soundex2)
                        return 1;

                    tempStr = tempStr.Substring(spacePos + 1);
                    searchLen = tempStr.Length;
                }

                spacePos = tempStr.IndexOf(splitChar);
            }

            return 0;
        }
        /*
        public static int SoundexDifference(string data1, string data2, char separator)
        {
            int result = 0;
            string soundex1 = Soundex(data1);
            string soundex2 = Soundex(data2);

            if (soundex1 == soundex2)
                result = 4;
            else
            {
                string sub1 = soundex1.Substring(1, 3);
                string sub2 = soundex1.Substring(2, 2);
                string sub3 = soundex1.Substring(1, 2);
                string sub4 = soundex1.Substring(1, 1);
                string sub5 = soundex1.Substring(2, 1);
                string sub6 = soundex1.Substring(3, 1);

                if (soundex2.IndexOf(sub1) > -1)
                    result = 3;
                else if (soundex2.IndexOf(sub2) > -1)
                    result = 2;
                else if (soundex2.IndexOf(sub3) > -1)
                    result = 2;
                else
                {
                    if (soundex2.IndexOf(sub4) > -1)
                        result++;

                    if (soundex2.IndexOf(sub5) > -1)
                        result++;

                    if (soundex2.IndexOf(sub6) > -1)
                        result++;
                }

                if (soundex1.Substring(0, 1) ==
                    soundex2.Substring(0, 1))
                    result++;
            }

            return (result == 0) ? 1 : result;
       }*/
    }

}
