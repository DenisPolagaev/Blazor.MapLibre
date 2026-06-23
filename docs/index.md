# Blazor MapLibre

## About The Project

This project should help people working on Blazor projects to use maps more easily.

## Getting Started

### Prerequisites

The library and plugins target .NET 8, 9, and 10. Examples and the documentation site use .NET 10 — install the [.NET 10 SDK](https://dotnet.microsoft.com/download) or newer to build and run them.

### Installation

Install the package:

```bash
dotnet add package Community.Blazor.MapLibre
```

Add this to head of your file to load the css for the maps:

```html
<link href="_content/Community.Blazor.MapLibre/maplibre-gl/dist/maplibre-gl.css" rel="stylesheet" />
```

## Usage

After the package is installed you can use it with simple:

```csharp
<MapLibre />
```

You can customize the map more with options using `MapOptions.cs`:

```csharp
<MapLibre Options="_mapOptions"></MapLibre>

@code
{
    private readonly MapOptions _mapOptions = new MapOptions();
}
```
