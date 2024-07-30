using System;

namespace BKey.Util.Encode.Encodings;
[AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
sealed class EncoderAttribute : Attribute
{
    public string Name { get; }

    public EncoderAttribute(string name)
    {
        Name = name;
    }
}
