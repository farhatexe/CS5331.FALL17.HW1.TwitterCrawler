using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Twitter.Crawler.Access;

namespace Twitter.Crawler.Test.TestFileIo
{
    [TestClass]
    public class TestAccessCredentials
    {
        private const string TestConsumerKey = "testConsumerKeyValue";
        private const string TestConsumerSecret = "testConsumerSecretValue";
        private const string TestAccessToken = "testAccessTokenValue";
        private const string AccessTokenSecret = "testAccessTokenSecretValue";

        [TestMethod]
        public void SaveXmlFile()
        {
            var accessCredentialsObjectUnderTest = new AccessCredentials(TestConsumerKey, TestConsumerSecret, TestAccessToken, AccessTokenSecret);
            Assert.AreEqual(TestAccessToken, accessCredentialsObjectUnderTest.AccessToken);
            Assert.AreEqual(AccessTokenSecret, accessCredentialsObjectUnderTest.AccessTokenSecret);
            Assert.AreEqual(TestConsumerKey, accessCredentialsObjectUnderTest.ConsumerKey);
            Assert.AreEqual(TestConsumerSecret, accessCredentialsObjectUnderTest.ConsumerSecret);
        }

        [TestMethod]
        public void LoadCredentialsFromXmlFile()
        {
            const string fileName = "TestLoad.xml";
            
            //Save the file
            var accessSaveCredentialsObjectUnderTest = new AccessCredentials(TestConsumerKey, TestConsumerSecret, TestAccessToken, AccessTokenSecret);
            accessSaveCredentialsObjectUnderTest.Save(fileName);
            
            //Load from file, new object
            var accessCredentialsObjectUnderTest = new AccessCredentials(fileName);
            accessCredentialsObjectUnderTest.Load(fileName);

            //Assert
            Assert.AreEqual(accessCredentialsObjectUnderTest.AccessToken, accessSaveCredentialsObjectUnderTest.AccessToken);
            Assert.AreEqual(accessCredentialsObjectUnderTest.AccessTokenSecret, accessSaveCredentialsObjectUnderTest.AccessTokenSecret);
            Assert.AreEqual(TestConsumerKey, accessCredentialsObjectUnderTest.ConsumerKey);
            Assert.AreEqual(accessCredentialsObjectUnderTest.ConsumerSecret, accessSaveCredentialsObjectUnderTest.ConsumerSecret);

        }
    }
}
