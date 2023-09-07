//
// constant used to check if clouds should renew themselfs
//

let WeatherIdentifier = '';

const backgroundWeatherContainer = document.querySelector('#background-weather');


function randomInt(min, max) {
    return Math.floor(Math.random() * (max - min + 1)) + min;
}


//
// functions for controlling the weather on the background
//

function generateClouds(dotnetOptionsObject) {

    console.log(`generating ${dotnetOptionsObject.maxAmountOfClouds} clouds with options: `, dotnetOptionsObject)

    //
    // get configuration from parameter
    //

    const weather = WeatherIdentifier;

    const maxAmountOfClouds = window.innerWidth / dotnetOptionsObject.CloudGenerationFactor
    const timeBetweenCloudSpawnInMiliseconds = dotnetOptionsObject.TimeBetweenCloudSpawnInMiliseconds

    const maxCloudSpeedInPixelsPerSecond = dotnetOptionsObject.MaxCloudSpeedInPixelsPerSecond
    const minCloudSpeedInPixelsPerSecond = dotnetOptionsObject.MinCloudSpeedInPixelsPerSecond

    const maxCloudElevationInPixels = dotnetOptionsObject.MaxCloudElevationInPixels
    const minCloudElevationInPixels = dotnetOptionsObject.MinCloudElevationInPixels

    const maxCloudScale = dotnetOptionsObject.MaxCloudScale
    const minCloudScale = dotnetOptionsObject.MinCloudScale

    const maxCloudOpacity = dotnetOptionsObject.MaxCloudOpacity
    const minCloudOpacity = dotnetOptionsObject.MinCloudOpacity

    const typeOfPrecipitation = dotnetOptionsObject.TypeOfPrecipitation
    const precipitationIntensityInSeconds = dotnetOptionsObject.PrecipitationSpawnIntervalInSeconds

    const enableThunder = dotnetOptionsObject.Thunder


    //
    // functions
    //

    const randomSpeedInPixelsPerSecond = () => randomInt(minCloudSpeedInPixelsPerSecond, maxCloudSpeedInPixelsPerSecond);

    const randomElevation = () => randomInt(minCloudElevationInPixels, maxCloudElevationInPixels)

    const randomOpacity = () => randomInt(minCloudOpacity, maxCloudOpacity) / 100;

    const randomScale = () => randomInt(minCloudScale, maxCloudScale) / 100;



    const setCreateThunderTimeout = (cloud, timeoutDurationInMiliSeconds) => {
        timeout = timeoutDurationInMiliSeconds

        return setTimeout(() => {

            console.log('thunder baby')

            let flashOverlay = document.createElement('div');
            flashOverlay.classList.add('flash');

            let thunderElement = document.createElement('div');
            thunderElement.classList.add('thunder');
            thunderElement.innerHTML = `<img width="500" ="-1" src="/svg/thunder2.svg">`;

            thunderElement.style.top =  `${cloud.offsetTop + (cloud.offsetHeight / 2)}px`;
            thunderElement.style.left = `${cloud.offsetLeft + (cloud.offsetWidth / 2)}px`;

            let isFlipped = randomInt(0, 1);

            if (isFlipped) {
                thunderElement.style.transform = `scaleX(-1) translateX(-100%)`;
                thunderElement.style.right = `${cloud.offsetLeft + (cloud.offsetWidth / 2)}px`;
            }

            backgroundWeatherContainer.appendChild(flashOverlay);
            backgroundWeatherContainer.appendChild(thunderElement);

            // activate transitions
            setTimeout(() => {

                flashOverlay.style.opacity = 0;
                thunderElement.style.opacity = 0;

            }, 100)

            // remove flash
            setTimeout(() => {
                backgroundWeatherContainer.removeChild(flashOverlay);
            }, 600)

            // remove thunder
            setTimeout(() => {
                backgroundWeatherContainer.removeChild(thunderElement);
            }, 1100)

        }, timeout);
    }

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
                raindrop.style.opacity = 1;
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
        let thunderTimeout;

        // add rain or snow if needed
        if (typeOfPrecipitation === 'snow' || typeOfPrecipitation === 'rain') {

            // start precipitation after a random timeout to ensure out-of-sync 
            setTimeout(() => {
                precipitationInterval = setCreatePrecipitationInterval(cloud, precipitationIntensityInSeconds, typeOfPrecipitation)
            }, randomInt(500, 1500));
        }



        // add thunder if its enabled and random chance 
        if (enableThunder && randomInt(0, 5) == 0) {
            thunderTimeout = setCreateThunderTimeout(cloud, randomInt(0, transitionDuration * 1000) - 2);
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
            clearInterval(thunderTimeout);

            // create new cloud if the weather type is the same
            if (weather == WeatherIdentifier) {
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


//
// function for setting the appropirate weather and time of day according to the weather status
//
// uses the weather types from the enum WeatherStatus parsed as strings
//
function setBackgroundWeather(cloudGenerationOptions) {
    WeatherIdentifier = JSON.stringify(cloudGenerationOptions);

    document.querySelectorAll('.cloud').forEach(cloud => {

        backgroundWeatherContainer.removeChild(ck)

    })

    setBackground(cloudGenerationOptions.Background);

    generateClouds(cloudGenerationOptions);
}

function setBackground(backgroundclass) {

    if (backgroundWeatherContainer.classList.contains(backgroundclass)) {
        return;
    }

    console.log('changing background', backgroundWeatherContainer.classList, backgroundclass);

    backgroundWeatherContainer.setAttribute('class', '');
    backgroundWeatherContainer.classList.add(backgroundclass);
}