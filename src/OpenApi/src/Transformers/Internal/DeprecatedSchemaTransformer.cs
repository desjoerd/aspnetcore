// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System.Linq;

namespace Microsoft.AspNetCore.OpenApi.Transformers.Internal;

internal class DeprecatedTransformer : IOpenApiSchemaTransformer, IOpenApiOperationTransformer
{
    public Task TransformAsync(OpenApiSchema schema, OpenApiSchemaTransformerContext context, CancellationToken cancellationToken)
    {
        var isDeprecated = context.JsonPropertyInfo is { }
            ? context.JsonPropertyInfo.AttributeProvider?.GetCustomAttributes(typeof(ObsoleteAttribute), true).Any() == true
            : context.JsonTypeInfo.Type.GetCustomAttributes(typeof(ObsoleteAttribute), true).Any() == true;

        if (isDeprecated)
        {
            if (schema.IsComponentized() && context.JsonPropertyInfo is { })
            {
                schema.Metadata ??= new Dictionary<string, object>();
                schema.Metadata[OpenApiConstants.RefDeprecatedAnnotation] = true;
            }
            else
            {
                schema.Deprecated = true;
            }
        }

        return Task.CompletedTask;
    }

    public Task TransformAsync(OpenApiOperation operation, OpenApiOperationTransformerContext context, CancellationToken cancellationToken)
    {
        var isDeprecated = context.Description.ActionDescriptor.EndpointMetadata.OfType<ObsoleteAttribute>().Any();
        if (isDeprecated)
        {
            operation.Deprecated = true;
        }

        return Task.CompletedTask;
    }
}
