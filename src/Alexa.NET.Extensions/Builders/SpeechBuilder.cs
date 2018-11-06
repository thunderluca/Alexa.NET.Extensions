using Alexa.NET.Extensions.Types;
using Alexa.NET.Response;
using Alexa.NET.Response.Ssml;
using System;
using System.Collections.Generic;

namespace Alexa.NET.Extensions.Builders
{
    public static class SpeechBuilder
    {
        public static PlainTextOutputSpeech CreatePlainTextOutputSpeech(string text)
        {
            return new PlainTextOutputSpeech { Text = text };
        }

        public static SsmlOutputSpeech CreateSsmlOutputSpeech(IEnumerable<ISsml> elements, int breakMilliseconds = 300)
        {
            if (elements == null)
            {
                throw new ArgumentNullException(nameof(elements));
            }

            var speech = new Speech();

            speech.Elements.AddRange(elements);

            var ssml = speech.ToXml();

            return new SsmlOutputSpeech { Ssml = ssml };
        }

        public static SsmlOutputSpeech CreateSsmlOutputSpeech(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
            {
                throw new ArgumentNullException(nameof(text));
            }

            var speech = new Speech();

            speech.Elements.Add(new PlainText(text));

            var ssml = speech.ToXml();

            return new SsmlOutputSpeech { Ssml = ssml };
        }

        public static Emphasis CreateSsmlEmphasis(string text, string emphasisLevel = EmphasisLevel.Moderate)
        {
            return new Emphasis(text) { Level = emphasisLevel };
        }

        public static Break CreateSsmlBreak(int time, string timeUnit = TimeUnit.Milliseconds)
        {
            return new Break { Time = time + timeUnit };
        }
    }
}
