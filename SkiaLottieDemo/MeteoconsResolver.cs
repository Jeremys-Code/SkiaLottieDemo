namespace SkiaLottieDemo
{
    public static class MeteoconsResolver
    {
        public static async Task<String> ResolveIcon(
            int weatherCode,
            bool isDay,
            string style = "flat")
        {
            // Normalize style
            style = style.ToLowerInvariant();

            // --- WMO / NWS Unified Mapping ---
            // This mapping is deterministic and audit-safe.
            string slug = weatherCode switch
            {
                // --- Clear / Mostly Clear ---
                0 or 1 or 2 => isDay ? "clear-day" : "clear-night",

                // --- Partly Cloudy ---
                3 => isDay ? "partly-cloudy-day" : "partly-cloudy-night",

                // --- Fog / Mist / Haze ---
                45 or 48 => isDay ? "fog-day" : "",

                // --- Drizzle ---
                51 or 53 or 55 => "drizzle",

                // --- Freezing Drizzle ---
                56 or 57 => "drizzle-freezing",

                // --- Rain ---
                61 or 63 or 65 => "rain",

                // --- Freezing Rain ---
                66 or 67 => "rain-freezing",

                // --- Snow (Light/Moderate/Heavy) ---
                71 or 73 or 75 => "snow",

                // --- Snow Grains ---
                77 => "snow-grains",

                // --- Rain + Snow Mix ---
                80 or 81 or 82 => "rain",

                // --- Showers ---
                85 or 86 => "snow",

                // --- Thunderstorms ---
                95 => "thunderstorms",

                // --- Thunderstorms with Hail ---
                96 or 99 => "thunderstorms-hail",

                // --- NWS Special Codes (if you use weather.gov) ---
                1000 => "clear-day",
                1001 => "cloudy",
                1100 => isDay ? "mostly-clear-day" : "mostly-clear-night",
                1101 => isDay ? "partly-cloudy-day" : "partly-cloudy-night",
                1102 => "cloudy",
                2000 => "fog",
                2100 => "fog-light",
                4000 => "drizzle",
                4001 => "rain",
                4200 => "rain-light",
                4201 => "rain-heavy",
                5000 => "snow",
                5001 => "flurries",
                5100 => "snow-light",
                5101 => "snow-heavy",
                6000 => "freezing-drizzle",
                6001 => "freezing-rain",
                6200 => "freezing-rain-light",
                6201 => "freezing-rain-heavy",
                7000 => "ice-pellets",
                7101 => "ice-pellets-heavy",
                7102 => "ice-pellets-light",
                8000 => "thunderstorms",

                // --- Fallback ---
                _ => "unknown"
            };
            var fileName = $"{style}-{slug}.json";
            var app = @"F:\fill\";
            var path = Path.Combine(app, fileName);
            if (File.Exists(path))
            {
                return path;
            }
            else
            {
                using var stream = await FileSystem.OpenAppPackageFileAsync($"{style}-{slug}.json");
                using var reader = new StreamReader(stream);
                var content = reader.ReadToEnd();

                if (content != null)
                {
                    File.WriteAllText(path, content);
                    return path;
                }

                return "unknown";
            }
        }
    }
}
