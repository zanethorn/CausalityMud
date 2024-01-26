namespace Causality.Mud.Common.Values;

public interface IValue:
    IComparable, IFormattable, IConvertible, IComparable<int>, IEquatable<int>
{
    string? Description { get; }
    
    int Min { get; }
    int Max { get; }
    int Avg { get; }

    int Value { get; }

    
    bool IConvertible.ToBoolean(IFormatProvider? provider)
    {
        return ((IConvertible)this.ToInt32(provider)).ToBoolean(provider);
    }

    byte IConvertible.ToByte(IFormatProvider? provider)
    {
        return ((IConvertible)this.ToInt32(provider)).ToByte(provider);
    }

    char IConvertible.ToChar(IFormatProvider? provider)
    {
        return ((IConvertible)this.ToInt32(provider)).ToChar(provider);
    }

    DateTime IConvertible.ToDateTime(IFormatProvider? provider)
    {
        return ((IConvertible)this.ToInt32(provider)).ToDateTime(provider);
    }

    decimal IConvertible.ToDecimal(IFormatProvider? provider)
    {
        return ((IConvertible)this.ToInt32(provider)).ToDecimal(provider);
    }

    double IConvertible.ToDouble(IFormatProvider? provider)
    {
        return ((IConvertible)this.ToInt32(provider)).ToDouble(provider);
    }

    short IConvertible.ToInt16(IFormatProvider? provider)
    {
        return ((IConvertible)this.ToInt32(provider)).ToInt16(provider);
    }
    
    int IConvertible.ToInt32(IFormatProvider? provider)
    {
        return Value;
    }

    long IConvertible.ToInt64(IFormatProvider? provider)
    {
        return ((IConvertible)this.ToInt32(provider)).ToInt64(provider);
    }

    sbyte IConvertible.ToSByte(IFormatProvider? provider)
    {
        return ((IConvertible)this.ToInt32(provider)).ToSByte(provider);
    }

    float IConvertible.ToSingle(IFormatProvider? provider)
    {
        return ((IConvertible)this.ToInt32(provider)).ToSingle(provider);
    }

    object IConvertible.ToType(Type conversionType, IFormatProvider? provider)
    {
        return ((IConvertible)this.ToInt32(provider)).ToType(conversionType,provider);
    }

    ushort IConvertible.ToUInt16(IFormatProvider? provider)
    {
        return ((IConvertible)this.ToInt32(provider)).ToUInt16(provider);
    }

    uint IConvertible.ToUInt32(IFormatProvider? provider)
    {
        return ((IConvertible)this.ToInt32(provider)).ToUInt32(provider);
    }

    ulong IConvertible.ToUInt64(IFormatProvider? provider)
    {
        return ((IConvertible)this.ToInt32(provider)).ToUInt64(provider);
    }
    
    string IFormattable.ToString(string? format, IFormatProvider? formatProvider)
    {
        return ToString(formatProvider);
    }
    
    string IConvertible.ToString(IFormatProvider? provider)
    {
        return this.ToString();
    }
    
    TypeCode IConvertible.GetTypeCode()
    {
        return TypeCode.Int32;
    }
    
    int IComparable<int>.CompareTo(int other)
    {
        return Value.CompareTo(other);
    }

    bool IEquatable<int>.Equals(int other)
    {
        return Value.Equals(other);
    }
    
    int IComparable.CompareTo(object? obj)
    {
        return Value.CompareTo(obj);
    }
    
    static IValue operator +(IValue left, IValue right) => new AddValue(left, right);
    static IValue operator -(IValue left, IValue right) => new SubtractValue(left, right);
    static IValue operator *(IValue left, IValue right) => new MultiplyValue(left, right);
    static IValue operator /(IValue left, IValue right) => new DivideValue(left, right);
    static IValue operator -(IValue term) => new NegateValue(term);
}