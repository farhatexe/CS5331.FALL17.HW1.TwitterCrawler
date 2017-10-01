using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using LinqToTwitter;

namespace Twitter.Crawler.Repository
{
    public static class CalculateHighestWordFrequency
    {

        public static async Task<Dictionary<string, int>> GetWordTopResults(IEnumerable<string> textToEvalute, int numerOfResults)
        {
            var fullResults = await GetWordListAsync(textToEvalute);
            var subTopResults = fullResults.OrderByDescending(o => o.Value).Take(numerOfResults).ToDictionary(x => x.Key, x => x.Value);//.Take(numerOfResults).ToDictionary(x=>x.Key, x=>x.Value);
            return subTopResults;
        }
        public static async Task<Dictionary<string, int>> GetWordListAsync(IEnumerable<string> textToEvalute)
        {
            var result = await Task.Run(() => GetWordList(textToEvalute));
            return result;
        }

        public static Dictionary<string, int> GetWordList(IEnumerable<string> textToEvalute)
        {

            var wordDictionary = new Dictionary<string, int>();
            var stopWordHash = new StopWords();
            foreach (var textValue in textToEvalute)
            {
                var normalizedString = textValue.NormalizeString();
                var brokenString = normalizedString.Split(new char[0]);
                foreach (var work in brokenString)
                {
                    try
                    {
                        if(!stopWordHash.StopWordsSet.Contains(work)&&work!=string.Empty)
                            wordDictionary.Add(work, 1);
                    }
                    catch (ArgumentException)
                    {
                        wordDictionary[work] += 1;

                    }
                }
            }
            return wordDictionary;
        }

        public static string NormalizeString(this string stringToClean)
        {
            var startingString = stringToClean;
            //Cut leading and trailing spaces.
            var trimmedString = startingString.Trim();
            //Ensure case is lower
            var stringLowerCase = trimmedString.ToLowerInvariant();
            //Ensure nothing but letters and numbers get through
            var normalizedString = Regex.Replace(stringLowerCase, @"[^\w\s]", "");
          
            return normalizedString;
        }
    }
}