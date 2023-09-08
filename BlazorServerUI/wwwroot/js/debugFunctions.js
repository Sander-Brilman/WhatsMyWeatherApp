// consts meant for debugging. not used by C# or other javascript code.

const setWeatherTo_Partly_Cloudly = () => {
    setBackgroundWeather({
        cloudGenerationFactor:  100,
        timeBetweenCloudSpawnInMiliseconds:  2000,

        maxCloudSpeedInPixelsPerSecond:  70,
        minCloudSpeedInPixelsPerSecond:  40,
        
        maxCloudElevationInPixels:  300,
        minCloudElevationInPixels:  160,
        
        maxCloudScale:  50,
        minCloudScale:  40,
        
        maxCloudOpacity: 65,
        minCloudOpacity: 50,

        backgroundCssClass:  "Day"
    })
};


const setWeatherTo_Cloudy = () => {
    setBackgroundWeather({
        cloudGenerationFactor:  40,
        timeBetweenCloudSpawnInMiliseconds:  1000,

        maxCloudSpeedInPixelsPerSecond:  70,
        minCloudSpeedInPixelsPerSecond:  40,
        
        maxCloudElevationInPixels:  300,
        minCloudElevationInPixels:  160,
        
        maxCloudScale:  50,
        minCloudScale:  40,
        
        maxCloudOpacity:  65,
        minCloudOpacity:  50,

        backgroundCssClass: "Day"
    })
};


const setWeatherTo_Rain = () => {
    setBackgroundWeather({
        cloudGenerationFactor:  40,
        timeBetweenCloudSpawnInMiliseconds:  1000,

        maxCloudSpeedInPixelsPerSecond:  70,
        minCloudSpeedInPixelsPerSecond:  40,
        
        maxCloudElevationInPixels:  225,
        minCloudElevationInPixels:  175,
        
        maxCloudScale:  60,
        minCloudScale:  50,
        
        maxCloudOpacity: 100,
        minCloudOpacity: 75,

        typeOfPrecipitation: "rain",
        precipitationSpawnIntervalInSeconds:  0.7,
        thunder: false, // Moderate or heavy rain with thunder

        backgroundCssClass: "Day"
    })
};



const setWeatherTo_Rain_with_thunder = () => {
    setBackgroundWeather({
        cloudGenerationFactor: 40,
        timeBetweenCloudSpawnInMiliseconds: 1000,

        maxCloudSpeedInPixelsPerSecond: 70,
        minCloudSpeedInPixelsPerSecond: 40,

        maxCloudElevationInPixels: 225,
        minCloudElevationInPixels: 175,

        maxCloudScale: 60,
        minCloudScale: 50,

        maxCloudOpacity: 100,
        minCloudOpacity: 75,

        typeOfPrecipitation: "rain",
        precipitationSpawnIntervalInSeconds: 0.7,
        thunder: true, // Moderate or heavy rain with thunder

        backgroundCssClass: "DarkDay"
    })
};

const setWeatherTo_Light_Rain = () => {
    setBackgroundWeather({
        cloudGenerationFactor:  40,
        timeBetweenCloudSpawnInMiliseconds:  1000,

        maxCloudSpeedInPixelsPerSecond:  70,
        minCloudSpeedInPixelsPerSecond:  40,
        
        maxCloudElevationInPixels:  225,
        minCloudElevationInPixels:  175,
        
        maxCloudScale:  60,
        minCloudScale:  50,
        
        maxCloudOpacity: 100,
        minCloudOpacity: 75,

        typeOfPrecipitation: "rain",
        precipitationSpawnIntervalInSeconds:  1.4,
        thunder: false, // Patchy light rain with thunder

        backgroundCssClass: "Day"
    })
};


const setWeatherTo_Snow = () => {
    setBackgroundWeather({
        cloudGenerationFactor:  40,
        timeBetweenCloudSpawnInMiliseconds:  1000,

        maxCloudSpeedInPixelsPerSecond:  70,
        minCloudSpeedInPixelsPerSecond:  50,
        
        maxCloudElevationInPixels:  225,
        minCloudElevationInPixels:  175,
        
        maxCloudScale:  60,
        minCloudScale:  50,
        
        maxCloudOpacity:  100,
        minCloudOpacity:  75,

        typeOfPrecipitation: "snow",
        precipitationSpawnIntervalInSeconds:  0.7,
        thunder: false, // Moderate or heavy snow with thunder

        backgroundCssClass: "DarkDay"
    })
};


const setWeatherTo_Light_Snow = () => {
    setBackgroundWeather({
        cloudGenerationFactor:  40,
        timeBetweenCloudSpawnInMiliseconds:  1000,

        maxCloudSpeedInPixelsPerSecond:  70,
        minCloudSpeedInPixelsPerSecond:  40,
        
        maxCloudElevationInPixels:  225,
        minCloudElevationInPixels:  175,
        
        maxCloudScale:  60,
        minCloudScale:  50,
        
        maxCloudOpacity:  100,
        minCloudOpacity:  75,

        typeOfPrecipitation: "snow",
        precipitationSpawnIntervalInSeconds:  1.4,
        thunder:  false, // Patchy light snow with thunder

        backgroundCssClass: "DarkDay"
    })
};


const setWeatherTo_Fog = () => {
    setBackgroundWeather({
        cloudGenerationFactor:  50,
        timeBetweenCloudSpawnInMiliseconds:  3000,

        maxCloudSpeedInPixelsPerSecond:  25,
        minCloudSpeedInPixelsPerSecond:  20,

        maxCloudElevationInPixels:  20,
        minCloudElevationInPixels:  -40,

        maxCloudScale:  75,
        minCloudScale:  55,

        maxCloudOpacity:  40,
        minCloudOpacity:  30,

        backgroundCssClass: "Day"
    });
};


const setWeatherTo_Thunder = () => {
    setBackgroundWeather({
        cloudGenerationFactor:  40,
        timeBetweenCloudSpawnInMiliseconds:  1000,

        maxCloudSpeedInPixelsPerSecond:  70,
        minCloudSpeedInPixelsPerSecond:  40,

        maxCloudElevationInPixels:  300,
        minCloudElevationInPixels:  160,

        maxCloudScale:  85,
        minCloudScale:  70,

        maxCloudOpacity:  99,
        minCloudOpacity:  90,
        thunder:  true,

        backgroundCssClass: "DarkDay"
    })
};


const setWeatherTo_Nothin = () => {
    setBackgroundWeather({
        cloudGenerationFactor:  320,
        timeBetweenCloudSpawnInMiliseconds:  5000,

        maxCloudSpeedInPixelsPerSecond:  90,
        minCloudSpeedInPixelsPerSecond:  65,
        
        maxCloudElevationInPixels:  350,
        minCloudElevationInPixels:  100,
        
        maxCloudScale:  55,
        minCloudScale:  35,
        
        maxCloudOpacity:  65,
        minCloudOpacity:  50,

        backgroundCssClass: "Day"
    })
};


console.log('debug functions initted :)');