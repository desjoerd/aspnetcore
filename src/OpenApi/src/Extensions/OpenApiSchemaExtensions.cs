// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace Microsoft.AspNetCore.OpenApi;

internal static class OpenApiSchemaExtensions
{
    private static readonly OpenApiSchema _nullSchema = new() { Type = JsonSchemaType.Null };

    public static IOpenApiSchema CreateOneOfNullableWrapper(this IOpenApiSchema originalSchema)
    {
        return new OpenApiSchema
        {
            OneOf =
            [
                _nullSchema,
                originalSchema
            ]
        };
    }

    public static bool IsComponentized(this IOpenApiSchema schema)
    {
        if (schema is OpenApiSchemaReference)
        {
            return true;
        }

        if (schema is OpenApiSchema actualSchema
            && actualSchema.Metadata is { }
            && actualSchema.Metadata.TryGetValue(OpenApiConstants.SchemaId, out var schemaId)
            && !string.IsNullOrEmpty(schemaId as string))
        {
            return true;
        }

        return false;
    }
}
