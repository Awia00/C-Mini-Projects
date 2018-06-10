using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StoryGenerator.Enums;

namespace StoryGenerator
{

    public class Setting
    {
        private readonly Location _location;
        private readonly IEnumerable<LocationAdjective> _adjectives;
        public Setting()
        {
            _location = EnumHelper.GetRandomValue<Location>();
            _adjectives = new List<LocationAdjective>
            {
                EnumHelper.GetRandomValue<LocationAdjective>(),
                EnumHelper.GetRandomValue<LocationAdjective>()
            };
        }

        public override string ToString()
        {
            return "A " + string.Join(", ", _adjectives) + " " + _location;
        }
    }
}
