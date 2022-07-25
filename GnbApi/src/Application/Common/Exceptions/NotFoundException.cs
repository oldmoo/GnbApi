namespace GnbApi.Application.Common.Exceptions;

using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;
using Enums;

[Serializable]
[ExcludeFromCodeCoverage]
public class NotFoundException : Exception
{
    public NotFoundException(string message)
        : base(message)
    {
    }

    protected NotFoundException(SerializationInfo info, StreamingContext context)
        : base(info, context)
    {
    }

    public static void ThrowIfNull(object argument, EntityType entityType)
    {
        if (argument is null)
        {
            Throw(entityType);
        }
    }
    public static void Throw(EntityType entityType)
    {
        throw new NotFoundException($"The {entityType} with the supplied id was not found.");
    }
}
