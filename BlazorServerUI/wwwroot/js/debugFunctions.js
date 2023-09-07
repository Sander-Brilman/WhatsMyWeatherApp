// consts meant for debugging. not used by C# or other javascript code.

const setWeatherTo_Partly_Cloudly = () => {
    setBackgroundWeather({
        CloudGenerationFactor:  100,
        TimeBetweenCloudSpawnInMiliseconds:  2000,

        MaxCloudSpeedInPixelsPerSecond:  70,
        MinCloudSpeedInPixelsPerSecond:  40,

        MaxCloudElevationInPixels:  300,
        MinCloudElevationInPixels:  160,

        MaxCloudScale:  50,
        MinCloudScale:  40,

        MaxCloudOpacity: 65,
        MinCloudOpacity: 50,

        Background:  "daylight"
    })
};


const setWeatherTo_Cloudy = () => {
    setBackgroundWeather({
        CloudGenerationFactor:  40,
        TimeBetweenCloudSpawnInMiliseconds:  1000,

        MaxCloudSpeedInPixelsPerSecond:  70,
        MinCloudSpeedInPixelsPerSecond:  40,

        MaxCloudElevationInPixels:  300,
        MinCloudElevationInPixels:  160,

        MaxCloudScale:  50,
        MinCloudScale:  40,

        MaxCloudOpacity:  65,
        MinCloudOpacity:  50,

        Background:  "daylight",
    })
};


const setWeatherTo_Rain = () => {
    setBackgroundWeather({
        CloudGenerationFactor:  40,
        TimeBetweenCloudSpawnInMiliseconds:  1000,

        MaxCloudSpeedInPixelsPerSecond:  70,
        MinCloudSpeedInPixelsPerSecond:  40,

        MaxCloudElevationInPixels:  225,
        MinCloudElevationInPixels:  175,

        MaxCloudScale:  60,
        MinCloudScale:  50,

        MaxCloudOpacity: 100,
        MinCloudOpacity: 75,

        TypeOfPrecipitation: "rain",
        PrecipitationSpawnIntervalInSeconds:  0.7,
        Thunder: false, // Moderate or heavy rain with thunder

        Background:  "daylight",
    })
};


const setWeatherTo_Light_Rain = () => {
    setBackgroundWeather({
        CloudGenerationFactor:  40,
        TimeBetweenCloudSpawnInMiliseconds:  1000,

        MaxCloudSpeedInPixelsPerSecond:  70,
        MinCloudSpeedInPixelsPerSecond:  40,

        MaxCloudElevationInPixels:  225,
        MinCloudElevationInPixels:  175,

        MaxCloudScale:  60,
        MinCloudScale:  50,

        MaxCloudOpacity: 100,
        MinCloudOpacity: 75,

        TypeOfPrecipitation: "rain",
        PrecipitationSpawnIntervalInSeconds:  1.4,
        Thunder: false, // Patchy light rain with thunder

        Background:  "daylight"
    })
};


const setWeatherTo_Snow = () => {
    setBackgroundWeather({
        CloudGenerationFactor:  40,
        TimeBetweenCloudSpawnInMiliseconds:  1000,

        MaxCloudSpeedInPixelsPerSecond:  70,
        MinCloudSpeedInPixelsPerSecond:  50,

        MaxCloudElevationInPixels:  225,
        MinCloudElevationInPixels:  175,

        MaxCloudScale:  60,
        MinCloudScale:  50,

        MaxCloudOpacity:  100,
        MinCloudOpacity:  75,

        TypeOfPrecipitation: "snow",
        PrecipitationSpawnIntervalInSeconds:  0.7,
        Thunder: false, // Moderate or heavy snow with thunder

        Background:  "daylight",
    })
};


const setWeatherTo_Light_Snow = () => {
    setBackgroundWeather({
        CloudGenerationFactor:  40,
        TimeBetweenCloudSpawnInMiliseconds:  1000,

        MaxCloudSpeedInPixelsPerSecond:  70,
        MinCloudSpeedInPixelsPerSecond:  40,

        MaxCloudElevationInPixels:  225,
        MinCloudElevationInPixels:  175,

        MaxCloudScale:  60,
        MinCloudScale:  50,

        MaxCloudOpacity:  100,
        MinCloudOpacity:  75,

        TypeOfPrecipitation: "snow",
        PrecipitationSpawnIntervalInSeconds:  1.4,
        Thunder:  false, // Patchy light snow with thunder

        Background:  "daylight"
    })
};


const setWeatherTo_Fog = () => {
    setBackgroundWeather({
        CloudGenerationFactor:  50,
        TimeBetweenCloudSpawnInMiliseconds:  3000,

        MaxCloudSpeedInPixelsPerSecond:  25,
        MinCloudSpeedInPixelsPerSecond:  20,

        MaxCloudElevationInPixels:  20,
        MinCloudElevationInPixels:  -40,

        MaxCloudScale:  75,
        MinCloudScale:  55,

        MaxCloudOpacity:  40,
        MinCloudOpacity:  30,

        Background:  "daylight"
    });
};


const setWeatherTo_Thunder = () => {
    setBackgroundWeather({
        CloudGenerationFactor:  40,
        TimeBetweenCloudSpawnInMiliseconds:  1000,

        MaxCloudSpeedInPixelsPerSecond:  70,
        MinCloudSpeedInPixelsPerSecond:  40,

        MaxCloudElevationInPixels:  300,
        MinCloudElevationInPixels:  160,

        MaxCloudScale:  85,
        MinCloudScale:  70,

        MaxCloudOpacity:  99,
        MinCloudOpacity:  90,
        Thunder:  true,

        Background:  "darkdaylight",
    })
};


const setWeatherTo_Nothin = () => {
    setBackgroundWeather({
        CloudGenerationFactor:  320,
        TimeBetweenCloudSpawnInMiliseconds:  5000,

        MaxCloudSpeedInPixelsPerSecond:  90,
        MinCloudSpeedInPixelsPerSecond:  65,

        MaxCloudElevationInPixels:  350,
        MinCloudElevationInPixels:  100,

        MaxCloudScale:  55,
        MinCloudScale:  35,

        MaxCloudOpacity:  65,
        MinCloudOpacity:  50,

        Background:  "daylight"
    })
};


console.log('debug functions initted :)');