// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

public static class ObsoleteEndpointsExtensions
{
    public static IEndpointRouteBuilder MapObsoleteEndpoints(this IEndpointRouteBuilder endpointRouteBuilder)
    {
        var deprecatedRoutes = endpointRouteBuilder.MapGroup("deprecated")
            .WithGroupName("deprecated");

#pragma warning disable CS0612 // Type or member is obsolete
        deprecatedRoutes.MapGet("/model", () => TypedResults.Ok(new DeprecatedModel()));
        deprecatedRoutes.MapGet("/operation", [Obsolete] () => TypedResults.Ok(new DeprecatedModel()));
        deprecatedRoutes.MapGet("/operation-meta", () => TypedResults.Ok(new DeprecatedModel()))
            .WithMetadata(new ObsoleteAttribute());
        //deprecatedRoutes.MapGet("/test", () => TypedResults.Ok(new DeprecatedReference()));
#pragma warning restore CS0612 // Type or member is obsolete

        return deprecatedRoutes;
    }

    [Obsolete]
    public class DeprecatedModel
    {
        [Obsolete]
        public string? Name { get; set; }

        [Obsolete]
        public DeprecatedReference DeprecatedReference { get; set; } = null!;

        public DeprecatedItem DeprecatedItem { get; set; } = null!;

        [Obsolete]
        public DeprecatedReference? NullableDeprecatedReference { get; set; }

        public DeprecatedItem? NullableDeprecatedItem { get; set; }
    }

    public class DeprecatedReference
    {
        public int Id { get; set; }
    }

    [Obsolete]
    public class DeprecatedItem
    {
        public string Text { get; set; } = null!;
    }
}
