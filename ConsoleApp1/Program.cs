using Microsoft.VisualBasic;
using System;
using System.Buffers.Text;
using System.Collections.Generic;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Diagnostics.Contracts;
using System.Diagnostics;
using System.Net.NetworkInformation;
using System.Reflection.PortableExecutable;
using System.Security.Cryptography;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using static System.Net.Mime.MediaTypeNames;

class Program
{
    static void Main()
    {
        string longText = "The offline-to-online (O2O) is a robotic teaching method that combine the online teaching and offline teaching method. " +
            "This method utilizes an offline software to generate the trajectory to be used as a reference trajectory. Then, the reference trajectory is combined with the online " +
            "method to improve the performance and accuracy of the offline trajectory to perform the tasks. The online teaching method implements a haptic teleoperation system to " +
            "assist the operator to control the robot arm. This image shows the overview of the O2O system: https://eorf.short.gy/9YCGoS, https://eorf.short.gy/9Yrtret, https://eorf.short.gy/9Yfgfgg. " +
            "the edge uniformly at random from the remaining edges. Each iteration reduces the number of vertices in the graph by one.After n − 2 iterations, " +
            "the graph consists of two vertices.The algorithm outputs the set of edges connecting the two remaining vertices. The offline-to-online (O2O) " +
            "is a robotic teaching method that combine the online teaching and offline teaching method. This method utilizes an offline software to generate" +
            " the trajectory to be used as a reference trajectory. Then, the reference trajectory is combined with the online method to improve the performance" +
            " and accuracy of the offline trajectory to perform the tasks. The online teaching method implements a haptic teleoperation system" +
            " to assist the operator to control the robot arm. The image shows the overview of the O2O system: https://eorf.short.gy/4sfsfdfd.";

        List<string> listLink = ParseLinksToList(longText);
        // Print each part of the split text
        int ix = 0;
        foreach (var part in listLink)
        {
            ix++;
            Console.WriteLine(ix + ") " + part);
        }
        Console.WriteLine("\n\n");

        List<Tuple<string, string>> listsentence = ParseIntroductionSentenceAndLinks(longText,"image",":");
        // Print each part of the split text
        int iy = 0;
        foreach (var part in listsentence)
        {
            iy++;
            Console.WriteLine(iy + ") " + part.Item1+", "+part.Item2);
        }
    }

    static List<string> ParseLinksToList(string text)
    {
        List<string> linkList = new List<string>();

        var links = new Regex(@"\b(?:https?://|www\.)\S+\b", RegexOptions.Compiled | RegexOptions.IgnoreCase);
        MatchCollection matches = links.Matches(text);

        foreach(Match match in matches)
        {
            linkList.Add(match.Value);
        }
        return linkList;
    }
    static List<Tuple<string, string>> ParseIntroductionSentenceAndLinks(string text, string startWord, string endWord)
    {
        var results = new List<Tuple<string, string>>();
        var sentencePattern = $@"\b{Regex.Escape(startWord)}\b.*?{endWord}";// @"\b{startWord}\b(.*?:)";// ([^.]*)([:])";
        var regexSentence = new Regex(sentencePattern, RegexOptions.IgnoreCase);

        var linkPattern = @"\b(?:https?://|www\.)\S+\b";

        var sentences = regexSentence.Matches(text);
        var links = Regex.Matches(text, linkPattern);

        foreach (Match link in links)
        {
            string foundSentence = "";
            int linkPosition = link.Index;
            foreach (Match sentence in sentences)
            {
                if (sentence.Index + sentence.Length < linkPosition)
                {
                    foundSentence = sentence.Value;
                }
                else
                {
                    break;
                }
            }
            results.Add(new Tuple<string, string>("The "+foundSentence.Trim(), link.Value));
        }

        return results;
    }
}
