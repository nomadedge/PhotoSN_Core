using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace PhotoSN.Data.Services
{
    public static class HashtagService
    {
        public static ICollection<string> GetHashtags(string description)
        {
            var symbolsList = new List<char>();
            foreach (var symbol in Environment.NewLine.ToCharArray())
            {
                symbolsList.Add(symbol);
            }
            symbolsList.Add(' ');

            var symbolsArray = symbolsList.ToArray();

            var words = description.Split(symbolsArray).ToList();
            var regex = new Regex("^[a-zA-Z0-9_]*");
            var hashtags = new List<string>();

            foreach (var word in words)
            {
                var match = regex.Match(word);
                if (match.Success)
                {
                    var hashtag = match.Groups[1].ToString().ToLower();
                    if (string.IsNullOrEmpty(hashtag))
                    {
                        hashtags.Add(hashtag);
                    }
                }
            }

            return hashtags;
        }
    }
}
