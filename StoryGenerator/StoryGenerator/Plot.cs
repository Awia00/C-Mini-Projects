using System;
using System.Collections.Generic;
using System.Text;
using StoryGenerator.Enums;

namespace StoryGenerator
{
    public class Plot
    {
        private readonly IEnumerable<Adverbium> _actAdverbs;
        private readonly Act _act;
        private readonly IEnumerable<Motivation> _motivations;
        private readonly Character _subject;
        private readonly Character _subjectMatter;
        public Plot()
        {
            _act = EnumHelper.GetRandomValue<Act>();
            _actAdverbs = new List<Adverbium>{EnumHelper.GetRandomValue<Adverbium>()};
            _motivations = new List<Motivation>
            {
                EnumHelper.GetRandomValue<Motivation>(),
                EnumHelper.GetRandomValue<Motivation>()
            };
            _subject = new Character();
            _subjectMatter = new Character();
        }

        public override string ToString()
        {
            var stringBuilder = new StringBuilder();

            stringBuilder.Append(_subject);
            stringBuilder.Append(" ");
            stringBuilder.Append(string.Join(", ", _actAdverbs));
            stringBuilder.Append(" ");
            stringBuilder.Append(_act);
            stringBuilder.Append(" ");
            stringBuilder.Append(_subjectMatter);
            stringBuilder.Append(" because of ");
            stringBuilder.Append(string.Join(" and ", _motivations));
            
            return stringBuilder.ToString();
        }
    }
}
