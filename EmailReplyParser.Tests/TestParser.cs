using System.IO;
using System.Reflection;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EmailReplyParser.Tests
{
    [TestClass]
    public class TestParser
    {
        private string LoadFile(string resourceName)
        {
            var assembly = Assembly.GetExecutingAssembly();

            using (var stream = assembly.GetManifestResourceStream(resourceName))
            {
                if (stream == null)
                    throw new FileNotFoundException($"Unable to find the manifest resource stream. Did you mark it as an embedded resource? Name: {resourceName}", resourceName);

                using (var reader = new StreamReader(stream))
                {
                    return reader.ReadToEnd();
                }
            }
        }

        [DataTestMethod]
        [DataRow("correct_sig.txt")]
        [DataRow("email_1_1.txt")]
        [DataRow("email_1_2.txt")]
        [DataRow("email_1_3.txt")]
        [DataRow("email_1_4.txt")]
        [DataRow("email_1_5.txt")]
        [DataRow("email_1_6.txt")]
        [DataRow("email_1_7.txt")]
        [DataRow("email_1_8.txt")]
        [DataRow("email_2_1.txt")]
        [DataRow("email_2_2.txt")]
        [DataRow("email_BlackBerry.txt")]
        [DataRow("email_bullets.txt")]
        [DataRow("email_iPhone.txt")]
        [DataRow("email_multi_word_sent_from_my_mobile_device.txt")]
        [DataRow("email_one_is_not_on.txt")]
        [DataRow("email_sent_from_my_not_signature.txt")]
        [DataRow("email_sig_delimiter_in_middle_of_line.txt")]
        [DataRow("greedy_on.txt")]
        [DataRow("gmail_nl.txt")]
        [DataRow("outlook_2016.txt")]
        [DataRow("outlook_2016_nl.txt")]
        //[DataRow("pathological.txt")]
        public void VerifyParsedReply(string fileName)
        {
            var email = LoadFile(string.Format("EmailReplyParser.Tests.TestEmails.{0}", fileName));
            var expectedReply = LoadFile(string.Format("EmailReplyParser.Tests.TestEmailResults.{0}", fileName)).Replace("\r\n", "\n");

            var parser = new Lib.Parser();
            var reply = parser.ParseReply(email);

            Assert.AreEqual(expectedReply, reply);
        }
    }
}