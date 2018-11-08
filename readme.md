# Email Reply Parser

[![Build status](https://dev.azure.com/decos/Shared/_apis/build/status/EmailReplyParser)](https://dev.azure.com/decos/Shared/_build/latest?definitionId=194) 
[![NuGet version](https://badge.fury.io/nu/EmailReplyParser.NET-Decos.svg)](https://badge.fury.io/nu/EmailReplyParser.NET-Decos)

This is a simple library that can parse plain text email message text into fragments and separate the reply text from the rest of the email thread. **Please note that HTML content is not supported. Converted the HTML content into plain text before using this library.**

This is a fork of [EricJWHuang/EmailReplyParser](https://github.com/EricJWHuang/EmailReplyParser) which in turn is a .NET port of the [original Ruby library created by GitHub](https://github.com/github/email_reply_parser).

### How to use it
- Install the package from [NuGet](https://www.nuget.org/packages/EmailReplyParser.NET-Decos)
- Parse the original reply using
```c#
var parser = new EmailReplyParser.Lib.Parser();
var reply = parser.ParseReply("I get proper rendering as well.\r\n\r\nSent from a magnificent torch of pixels\r\n\r\nOn Dec 16, 2011, at 12:47 PM, Corey Donohoe\r\n<reply@reply.github.com>\r\nwrote:\r\n\r\n> Was this caching related or fixed already?  I get proper rendering here.\r\n>\r\n> ![](https://img.skitch.com/20111216-m9munqjsy112yqap5cjee5wr6c.jpg)\r\n>\r\n> ---\r\n> Reply to this email directly or view it on GitHub:\r\n> https://github.com/github/github/issues/2278#issuecomment-3182418");
```
