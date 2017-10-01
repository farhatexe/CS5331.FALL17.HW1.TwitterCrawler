using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twitter.Crawler.Access;
using Twitter.Crawler.Helpers;
using Twitter.Crawler.Model.Entities;
using Twitter.Crawler.Repository;

namespace CS5331.FALL17.HW1.TwitterCrawler.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            //var twitterConnection = new ConnectToTwitter();
            //Console.WriteLine("Application Complete");
            var test = new LinqtoTwitter("AccessCredentials.xml");
            //test.DoSearchAsync().Wait();
            //string results = test.DoSearchReturnResultsAsync().Result;//.Wait();
            //results.SaveToFile("Results.xml");
            //var results = test.DoSearchReturnTweetsAsync();
            var repo = new TwitterStatusRepository();
            ///var maxValue = (ulong)repo.GetMaxStatusId();
            ///var results = test.DoSearchReturnTweetsToMaxAsync(ulong.MaxValue, maxValue).Result;//.Wait();
            //results.SaveToFile("ResultTweets.xml");
            ///if (results.Count>0) 
            ///    repo.CreateOrUpdateStatus(results);
            //Console.WriteLine("Max Status: {0}", repo.GetMaxStatusId());
            //Console.WriteLine("Min Status: {0}", repo.GetMinStatusId());
            //Console.WriteLine(repo.RemoveDuplicates());
            //repo.GetAllTags();
            //repo.RemoveDuplicateClasses();
            //repo.UpdateCount();
            //repo.FlagStopWords();
            //repo.GetClassesSortedByRanking();

            var testSet = repo.GetListOfTweetsUnderTest();
            var trueTopWords = CalculateHighestWordFrequency.GetWordTopResults(testSet[true], 10).Result;
            var falseTopWords = CalculateHighestWordFrequency.GetWordTopResults(testSet[false], 10).Result;
            List<string> trueWords = trueTopWords.Select(x=>x.Key).ToList();
            List<string> falseWords = falseTopWords.Select(x=>x.Key).ToList();

            foreach (var resultWord in trueTopWords)
            {
                Console.WriteLine("Top True Class Results: {0} #{1}", resultWord.Key, resultWord.Value);
                if (falseWords.Contains(resultWord.Key))
                {
                    trueWords.Remove(resultWord.Key);
                    falseWords.Remove(resultWord.Key);
                }
            }
            for (int i = 1; i < 10; i++)
            {
                var results = ConfusionResults(falseWords, trueWords, testSet, i);
                var accuracy = Math.Round(Accuracy(results), 4);
                var precision = Math.Round(Precision(results), 4);
                var recall = Math.Round(Recall(results), 4);
                var fMeasure = Math.Round(FMeasure(recall,precision), 4);
                Console.WriteLine("For K={0} Accuracy:{1} Precision:{2} Recall:{3} FMeasure: {4}",i,accuracy,precision,recall,fMeasure);
            }
            
            

            //Console.WriteLine("", );

            Console.ReadLine();
            //test.GetLimits();

        }

        private static List<ConfusionResultEntity> ConfusionResults(List<string> falseWords, List<string> trueWords,  Dictionary<bool,List<string>> listToTest, int kValue)
        {
            var finalResultList = new List<ConfusionResultEntity>();
            foreach (var testValue in listToTest[true])
            {
                var resultList = new List<ConfusionResultEntity>();
                foreach (var value in trueWords)
                {
                    //resultList.Add(new ConfusionResultEntity(true));
                    var confResult = new ConfusionResultEntity(true);
                    confResult.DistanceValue = Distance.LevenshteinDistance(value, testValue);
                    resultList.Add(confResult);
                    //Console.WriteLine("LevTrue: {0}", distance);
                }
                foreach (var value in falseWords)
                {
                    var confResult = new ConfusionResultEntity(false);
                    confResult.DistanceValue = Distance.LevenshteinDistance(value, testValue);
                    resultList.Add(confResult);
                }

                var nearestNeighborList = resultList.OrderBy(x => x.DistanceValue).Take(kValue);
                var numberTrue = nearestNeighborList.Count(x => x.ExpectedValue);
                var numberFalse = nearestNeighborList.Count(x => !x.ExpectedValue);
                foreach (var value in resultList)
                {
                    value.ActualResult = numberTrue > numberFalse;
                }
                var decisionResult =
                    new ConfusionResultEntity(true) {ActualResult = numberTrue > numberFalse, ExpectedValue = true};
                finalResultList.Add(decisionResult);
            }
            foreach (var testValue in listToTest[false])
            {
                var resultList = new List<ConfusionResultEntity>();
                foreach (var value in trueWords)
                {
                    //resultList.Add(new ConfusionResultEntity(true));
                    var confResult = new ConfusionResultEntity(true);
                    confResult.DistanceValue = Distance.LevenshteinDistance(value, testValue);
                    resultList.Add(confResult);
                    //Console.WriteLine("LevTrue: {0}", distance);
                }
                foreach (var value in falseWords)
                {
                    var confResult = new ConfusionResultEntity(false);
                    confResult.DistanceValue = Distance.LevenshteinDistance(value, testValue);
                    resultList.Add(confResult);
                }

                var nearestNeighborList = resultList.OrderBy(x => x.DistanceValue).Take(kValue);
                var numberTrue = nearestNeighborList.Count(x => x.ExpectedValue);
                var numberFalse = nearestNeighborList.Count(x => !x.ExpectedValue);
                foreach (var value in resultList)
                {
                    value.ActualResult = numberTrue > numberFalse;
                }
                var decisionResult =
                    new ConfusionResultEntity(false) { ActualResult = numberTrue > numberFalse, ExpectedValue = false };
                finalResultList.Add(decisionResult);
            }
            return finalResultList;
        }

        private static double Accuracy(List<ConfusionResultEntity> confustionResults)
        {
            double truePositive = confustionResults.Count(x => x.IsTruePositive() == true);
            double falsePositive = confustionResults.Count(x => x.IsFalsePositive() == true);
            double trueNegative = confustionResults.Count(x => x.IsTrueNegative() == true);
            double falseNegative = confustionResults.Count(x => x.IsFalseNegative() == true);
            return (truePositive + trueNegative) / (falseNegative + falsePositive + trueNegative + truePositive);
        }

        private static double Precision(List<ConfusionResultEntity> confustionResults)
        {
            double truePositive = confustionResults.Count(x => x.IsTruePositive() == true);
            double falsePositive = confustionResults.Count(x => x.IsFalsePositive() == true);
            double trueNegative = confustionResults.Count(x => x.IsTrueNegative() == true);
            double falseNegative = confustionResults.Count(x => x.IsFalseNegative() == true);
            return (truePositive) / (falsePositive + truePositive);
        }
        private static double Recall(List<ConfusionResultEntity> confustionResults)
        {
            double truePositive = confustionResults.Count(x => x.IsTruePositive() == true);
            double falsePositive = confustionResults.Count(x => x.IsFalsePositive() == true);
            double trueNegative = confustionResults.Count(x => x.IsTrueNegative() == true);
            double falseNegative = confustionResults.Count(x => x.IsFalseNegative() == true);
            return (truePositive) / (truePositive + falseNegative);
        }
        private static double FMeasure(double recall, double precision)
        {
            return (2 * recall * precision) / (recall + precision);
        }


    }
}
