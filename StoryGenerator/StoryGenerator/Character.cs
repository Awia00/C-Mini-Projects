using System;
using System.Collections.Generic;
using System.Text;
using StoryGenerator.Enums;

namespace StoryGenerator
{
    public class Character
    {
        private readonly Actor _actor;
        private readonly IEnumerable<ActorAdjective> _adjectives;

        public Character()
        {
            _actor = EnumHelper.GetRandomValue<Actor>();
            _adjectives = new List<ActorAdjective>
            {
                EnumHelper.GetRandomValue<ActorAdjective>(),
            };
        }

        public override string ToString()
        {
            return "The " + string.Join(", ", _adjectives) + " " + _actor;
        }
    }
}
