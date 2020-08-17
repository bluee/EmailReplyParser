using System;
using System.IO;
using System.Reflection;
using Xunit;

namespace EmailReplyParserCore.Tests
{
    
    public class TestParser
    {
        private string LoadFile(string resourceName)
        {
            var codeBaseUrl = new Uri(Assembly.GetExecutingAssembly().CodeBase);
            var codeBasePath = Uri.UnescapeDataString(codeBaseUrl.AbsolutePath);
            var dirPath = Path.GetDirectoryName(codeBasePath);
            return Path.Combine(dirPath, resourceName);

            return File.ReadAllText(resourceName);
            //var assembly = Assembly.GetExecutingAssembly();

            //using (var stream = assembly.GetManifestResourceStream(resourceName))
            //{
            //    if (stream == null)
            //        throw new FileNotFoundException($"Unable to find the manifest resource stream. Did you mark it as an embedded resource? Name: {resourceName}", resourceName);

            //    using (var reader = new StreamReader(stream))
            //    {
            //        return reader.ReadToEnd();
            //    }
            //}
        }

        [Theory]
        [InlineData("correct_sig.txt")]
        [InlineData("email_1_1.txt")]
        [InlineData("email_1_2.txt")]
        [InlineData("email_1_3.txt")]
        [InlineData("email_1_4.txt")]
        [InlineData("email_1_5.txt")]
        [InlineData("email_1_6.txt")]
        [InlineData("email_1_7.txt")]
        [InlineData("email_1_8.txt")]
        [InlineData("email_2_1.txt")]
        [InlineData("email_2_2.txt")]
        [InlineData("email_BlackBerry.txt")]
        [InlineData("email_bullets.txt")]
        [InlineData("email_iPhone.txt")]
        [InlineData("email_multi_word_sent_from_my_mobile_device.txt")]
        [InlineData("email_one_is_not_on.txt")]
        [InlineData("email_sent_from_my_not_signature.txt")]
        [InlineData("email_sig_delimiter_in_middle_of_line.txt")]
        [InlineData("greedy_on.txt")]
        [InlineData("gmail_nl.txt")]
        [InlineData("outlook_2016.txt")]
        [InlineData("outlook_2016_nl.txt")]
        //[InlineData("pathological.txt")]
        public void VerifyParsedReply(string fileName)
        {
            var email = LoadFile($"TestEmails/EmailReplyParser.Tests.TestEmails.{fileName}");
            var expectedReply = LoadFile($"TestEmailResults/EmailReplyParser.Tests.TestEmailResults.{fileName}").Replace("\r\n", "\n");

            var parser = new EmailReplyParserCore.Parser();
            var reply = parser.ParseReply(email);

            Assert.Equal(expectedReply, reply);
        }
    }
}