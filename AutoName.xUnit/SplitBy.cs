using System;

namespace AutoName.xUnit
{
    [Flags]
    public enum SplitBy
    {
        Underscore = 1 << 0,
        Uppercase = 1 << 1,
        Number = 1 << 2
    }
}
