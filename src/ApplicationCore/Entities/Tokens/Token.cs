﻿namespace Vnit.ApplicationCore.Entities.Tokens
{
    public class Token
    {
        public Token(string tokenName, string tokenValue)
        {
            TokenName = tokenName;
            TokenValue = tokenValue;
        }

        public string TokenName { get; set; }

        public string TokenValue { get; set; }
    }
}