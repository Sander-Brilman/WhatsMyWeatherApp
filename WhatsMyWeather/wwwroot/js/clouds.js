//
// constant used to check if clouds should renew themselfs
//

let currentWeather = '';

const backgroundWeatherContainer = document.querySelector('.background-weather');


function randomInt(min, max) {
    return Math.floor(Math.random() * (max - min + 1)) + min;
}


//
// functions for controlling the weather on the background
//

function generateClouds(optionsObject) {

    console.log(`generating ${optionsObject.maxAmountOfClouds} clouds with options: `, optionsObject)

    //
    // get configuration from parameter
    //

    const weather = currentWeather;

    const maxAmountOfClouds = optionsObject.maxAmountOfClouds
    const timeBetweenCloudSpawnInMiliseconds = optionsObject.timeBetweenCloudSpawnInMiliseconds

    const maxCloudSpeedInPixelsPerSecond = optionsObject.maxCloudSpeedInPixelsPerSecond
    const minCloudSpeedInPixelsPerSecond = optionsObject.minCloudSpeedInPixelsPerSecond

    const maxCloudElevationInPixels = optionsObject.maxCloudElevationInPixels
    const minCloudElevationInPixels = optionsObject.minCloudElevationInPixels

    const maxCloudScale = optionsObject.maxCloudScale
    const minCloudScale = optionsObject.minCloudScale

    const maxCloudOpacity = optionsObject.maxCloudOpacity
    const minCloudOpacity = optionsObject.minCloudOpacity

    const typeOfPrecipitation = optionsObject.typeOfPrecipitation
    const precipitationIntensityInSeconds = optionsObject.precipitationIntensityInSeconds


    //
    // functions
    //

    const randomSpeedInPixelsPerSecond = () => randomInt(minCloudSpeedInPixelsPerSecond, maxCloudSpeedInPixelsPerSecond);

    const randomElevation = () => randomInt(minCloudElevationInPixels, maxCloudElevationInPixels)

    const randomOpacity = () => randomInt(minCloudOpacity, maxCloudOpacity) / 100;

    const randomScale = () => randomInt(minCloudScale, maxCloudScale) / 100;

    const setCreatePrecipitationInterval = (cloud, precipitationIntensityInSeconds, precipitationType) => {

        precipitationIntervalSpeed = (precipitationIntensityInSeconds * 1000);

        return setInterval(() => {

            const raindrop = document.createElement('div');
            raindrop.classList.add('precipitation', precipitationType);

            raindrop.style.left = `${cloud.offsetLeft + (cloud.offsetWidth / 2)}px`;
            raindrop.style.top = `${cloud.offsetTop + (cloud.offsetHeight / 2)}px`;

            backgroundWeatherContainer.appendChild(raindrop);

            // start animation going downwards
            // with a random added downwards location for different speeds
            setTimeout(() => {
                raindrop.style.top = `calc(100% + 500px + ${randomInt(100, 300)}px)`;
            }, 100);

            setTimeout(() => {
                backgroundWeatherContainer.removeChild(raindrop)
            }, 3300)// animation takes 3 seconds

        }, precipitationIntervalSpeed)

    }

    const createCloud = () => {
        let cloud = document.createElement("div");

        cloud.classList.add('cloud');
        cloud.innerHTML = '<img src="/svg/cloud.svg" alt="decorative cloud" tabindex="-1" >';


        // set random style
        let transitionDuration = window.innerWidth / randomSpeedInPixelsPerSecond();

        cloud.style.transitionDuration = `${transitionDuration}s`;
        cloud.style.bottom = `${randomElevation()}px`;

        cloud.style.scale = randomScale();
        cloud.style.opacity = randomOpacity();


        // apply cloud to background-weather container
        backgroundWeatherContainer.appendChild(cloud);


        let precipitationInterval;

        // add rain or snow if needed
        if (typeOfPrecipitation === 'snow' || typeOfPrecipitation === 'rain') {

            // start precipitation after a random timeout to ensure out-of-sync 
            setTimeout(() => {
                precipitationInterval = setCreatePrecipitationInterval(cloud, precipitationIntensityInSeconds, typeOfPrecipitation)
            }, randomInt(500, 1500));
        }


        // activate transition from left to right
        setTimeout(() => {
            cloud.style.left = '100%';
        }, 100)

        setTimeout(() => {

            // remove cloud
            backgroundWeatherContainer.removeChild(cloud);

            // remove precipitation interval
            clearInterval(precipitationInterval);

            // create new cloud if the weather type is the same
            if (weather == currentWeather) {
                createCloud();
            }

        }, transitionDuration * 1000)
    }


    let delay = 0;
    for (var i = 0; i < maxAmountOfClouds; i++) {

        setTimeout(() => {
            createCloud();
        }, delay);

        delay += timeBetweenCloudSpawnInMiliseconds;
    }
}


function setTimeOfDay(timeInHours) {
    let timeClass = '';

    if (timeInHours > 5 && timeInHours < 9) {
        timeClass = 'morning'
    }
    else if (timeInHours > 9 && timeInHours < 19) {
        timeClass = 'daylight'
    }
    else {
        timeClass = 'midnight'
    }

    backgroundWeatherContainer.classList.remove('morning', 'daylight', 'midnight');
    backgroundWeatherContainer.classList.add(timeClass)
}


//
// function for setting the appropirate weather and time of day according to the weather status
//
// uses the weather types from the enum WeatherStatus parsed as strings
//
function setBackgroundWeather(weatherType, timeInHours) {
    currentWeather = weatherType;

    setTimeOfDay(timeInHours);

    let cloudGenerationOptions;

    switch (currentWeather) {
        case 'Clear':
            cloudGenerationOptions = {
                maxAmountOfClouds: window.innerWidth / 320,
                timeBetweenCloudSpawnInMiliseconds: 5000,

                maxCloudSpeedInPixelsPerSecond: 90,
                minCloudSpeedInPixelsPerSecond: 65,

                maxCloudElevationInPixels: 350,
                minCloudElevationInPixels: 100,

                maxCloudScale: 55,
                minCloudScale: 35,

                maxCloudOpacity: 65,
                minCloudOpacity: 50,
            }
            break;

        case 'Rain':
            cloudGenerationOptions = {
                maxAmountOfClouds: window.innerWidth / 40,
                timeBetweenCloudSpawnInMiliseconds: 1000,

                maxCloudSpeedInPixelsPerSecond: 70,
                minCloudSpeedInPixelsPerSecond: 50,

                maxCloudElevationInPixels: 225,
                minCloudElevationInPixels: 175,

                maxCloudScale: 60,
                minCloudScale: 50,

                maxCloudOpacity: 100,
                minCloudOpacity: 75,

                typeOfPrecipitation: 'rain',
                precipitationIntensityInSeconds: 0.7,
            }
            break;

        case 'Snow':
            cloudGenerationOptions = {
                maxAmountOfClouds: window.innerWidth / 40,
                timeBetweenCloudSpawnInMiliseconds: 1000,

                maxCloudSpeedInPixelsPerSecond: 70,
                minCloudSpeedInPixelsPerSecond: 50,

                maxCloudElevationInPixels: 225,
                minCloudElevationInPixels: 175,

                maxCloudScale: 60,
                minCloudScale: 50,

                maxCloudOpacity: 100,
                minCloudOpacity: 75,

                typeOfPrecipitation: 'snow',
                precipitationIntensityInSeconds: 0.7,
            }
            break;

        case 'Wind':
            cloudGenerationOptions = {
                maxAmountOfClouds: window.innerWidth / 150,
                timeBetweenCloudSpawnInMiliseconds: 2000,

                maxCloudSpeedInPixelsPerSecond: 170,
                minCloudSpeedInPixelsPerSecond: 130,

                maxCloudElevationInPixels: 350,
                minCloudElevationInPixels: -60,

                maxCloudScale: 55,
                minCloudScale: 35,

                maxCloudOpacity: 65,
                minCloudOpacity: 50,
            }
            break;

        case 'Fog':
            cloudGenerationOptions = {
                maxAmountOfClouds: window.innerWidth / 50,
                timeBetweenCloudSpawnInMiliseconds: 3000,

                maxCloudSpeedInPixelsPerSecond: 25,
                minCloudSpeedInPixelsPerSecond: 20,

                maxCloudElevationInPixels: 20,
                minCloudElevationInPixels: -40,

                maxCloudScale: 75,
                minCloudScale: 55,

                maxCloudOpacity: 40,
                minCloudOpacity: 30,
            }
            break;

        case 'Cloudy':
            cloudGenerationOptions = {
                maxAmountOfClouds: window.innerWidth / 40,
                timeBetweenCloudSpawnInMiliseconds: 1000,

                maxCloudSpeedInPixelsPerSecond: 70,
                minCloudSpeedInPixelsPerSecond: 40,

                maxCloudElevationInPixels: 300,
                minCloudElevationInPixels: 160,

                maxCloudScale: 50,
                minCloudScale: 40,

                maxCloudOpacity: 65,
                minCloudOpacity: 50,
            }
            break;

        case 'PartlyCloudy':
            cloudGenerationOptions = {
                maxAmountOfClouds: window.innerWidth / 100,
                timeBetweenCloudSpawnInMiliseconds: 2000,

                maxCloudSpeedInPixelsPerSecond: 70,
                minCloudSpeedInPixelsPerSecond: 40,

                maxCloudElevationInPixels: 300,
                minCloudElevationInPixels: 160,

                maxCloudScale: 50,
                minCloudScale: 40,

                maxCloudOpacity: 65,
                minCloudOpacity: 50,
            }
            break;

        default:
    }

    generateClouds(cloudGenerationOptions);
}