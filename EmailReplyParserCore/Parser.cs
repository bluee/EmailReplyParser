﻿namespace EmailReplyParserCore
{
    /// <summary>
    /// A text parser to extract reply from email body.
    /// </summary>
    public class Parser
    {
        /// <summary>
        /// Extra reply from <paramref name="text">email body</paramref>
        /// </summary>
        /// <param name="text"></param>
        /// <returns>reply</returns>
        public string ParseReply(string text)
        {
            var email = new Email(text);
            email.Parse();

            return email.Reply;
        }
    }
}
