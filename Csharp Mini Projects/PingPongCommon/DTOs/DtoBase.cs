using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PingPongCommon.DTOs
{
    public enum DtoType
    {
        Move,
        Play,
        Pause,
        Restart
    }

    public abstract class DtoBase
    {
        public readonly DtoType DtoType;

        protected DtoBase(DtoType dtoType)
        {
            DtoType = dtoType;
        }
    }
}
