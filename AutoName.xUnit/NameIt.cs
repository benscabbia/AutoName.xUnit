using System;

namespace AutoName.xUnit
{
    [Flags]
    public enum NameIt
    {
        AbsolutePath = 1,
        AbsolutePathWithoutExtension = 2,
        NameSpace = 4,
        FileName = 8,
        FileNameWithoutExtension = 16,
        MethodName = 32
    }
}
