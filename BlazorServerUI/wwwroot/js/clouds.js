//
// constant used to check if clouds should renew themselfs
//


const backgroundWeatherContainer = document.querySelector('#background-weather');

const activeClouds = [];
const cloudsInCue = [];


let cloudSpawnInterval = 0;





function randomInt(min, max) {
    return Math.floor(Math.random() * (max - min + 1)) + min;
}



//
// functions for controlling the weather on the background
//

function spawnCloudFromCloudObject(cloudObject) {



    const maxCloudSpeedInPixelsPerSecond = cloudObject.options.maxCloudSpeedInPixelsPerSecond
    const minCloudSpeedInPixelsPerSecond = cloudObject.options.minCloudSpeedInPixelsPerSecond

    const maxCloudElevationInPixels = cloudObject.options.maxCloudElevationInPixels
    const minCloudElevationInPixels = cloudObject.options.minCloudElevationInPixels

    const maxCloudScale = cloudObject.options.maxCloudScale
    const minCloudScale = cloudObject.options.minCloudScale

    const maxCloudOpacity = cloudObject.options.maxCloudOpacity
    const minCloudOpacity = cloudObject.options.minCloudOpacity

    const typeOfPrecipitation = cloudObject.options.typeOfPrecipitation
    const precipitationIntensityInSeconds = cloudObject.options.precipitationSpawnIntervalInSeconds

    const enableThunder = cloudObject.options.thunder


    // populate object
    cloudObject.AllTimeouts = []
    cloudObject.AllIntervals = [];
    cloudObject.AllElements = [];



    //
    // functions
    //

    const randomSpeedInPixelsPerSecond = () => randomInt(minCloudSpeedInPixelsPerSecond, maxCloudSpeedInPixelsPerSecond);

    const randomElevation = () => randomInt(minCloudElevationInPixels, maxCloudElevationInPixels)

    const randomOpacity = () => randomInt(minCloudOpacity, maxCloudOpacity) / 100;

    const randomScale = () => randomInt(minCloudScale, maxCloudScale) / 100;

    const setCreateThunderTimeout = (cloud, timeoutDurationInMiliSeconds) => {
        timeout = timeoutDurationInMiliSeconds;

        return setTimeout(() => {

            console.log('thunder baby')

            let flashOverlay = document.createElement('div');
            flashOverlay.classList.add('flash', 'overlay');

            // add element to list of cloud-associated elements
            cloudObject.AllElements.push(flashOverlay);


            let thunderElement = document.createElement('div');
            thunderElement.classList.add('thunder');
            thunderElement.innerHTML = `<img width="500" ="-1" src="/svg/thunder2.svg">`;

            thunderElement.style.top = `${cloud.offsetTop + (cloud.offsetHeight / 2)}px`;
            thunderElement.style.left = `${cloud.offsetLeft + (cloud.offsetWidth / 2)}px`;

            // add element to list of cloud-associated elements
            cloudObject.AllElements.push(thunderElement);

            let isFlipped = randomInt(0, 1);

            if (isFlipped) {
                thunderElement.style.transform = `scaleX(-1) translateX(-100%)`;
                thunderElement.style.right = `${(backgroundWeatherContainer.offsetWidth - cloud.offsetLeft) + (cloud.offsetWidth / 2)}px`;
            }

            backgroundWeatherContainer.appendChild(flashOverlay);
            backgroundWeatherContainer.appendChild(thunderElement);

            // activate transitions
            cloudObject.AllTimeouts.push(setTimeout(() => {

                flashOverlay.style.opacity = 0;
                thunderElement.style.opacity = 0;

            }, 100))

            // remove flash
            cloudObject.AllTimeouts.push(setTimeout(() => {
                backgroundWeatherContainer.removeChild(flashOverlay);
            }, 600))

            // remove thunder
            cloudObject.AllTimeouts.push(setTimeout(() => {
                backgroundWeatherContainer.removeChild(thunderElement);
            }, 1100))

        }, timeout);
    }

    const setCreatePrecipitationInterval = (cloud, precipitationIntensityInSeconds, precipitationType) => {

        precipitationIntervalSpeed = (precipitationIntensityInSeconds * 1000);

        let interval = setInterval(() => {

            const raindrop = document.createElement('div');
            raindrop.classList.add('precipitation', precipitationType);

            // add element to list of cloud-associated elements
            cloudObject.AllElements.push(raindrop);

            raindrop.style.left = `${cloud.offsetLeft + (cloud.offsetWidth / 2)}px`;
            raindrop.style.top = `${cloud.offsetTop + (cloud.offsetHeight / 2)}px`;

            backgroundWeatherContainer.appendChild(raindrop);

            // start animation going downwards
            // with a random added downwards location for different speeds
            cloudObject.AllTimeouts.push(setTimeout(() => {
                raindrop.style.top = `calc(100% + 500px + ${randomInt(100, 300)}px)`;
                raindrop.style.opacity = 1;
            }, 100));

            cloudObject.AllTimeouts.push(setTimeout(() => {
                backgroundWeatherContainer.removeChild(raindrop)
            }, 3300))// animation takes 3 seconds

        }, precipitationIntervalSpeed)


        return interval;
    }


    //
    // create cloud, populate object then move it to active array
    //

    let cloud = document.createElement("div");

    cloudObject.AllElements.push(cloud);

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



    // add rain or snow if needed
    if (typeOfPrecipitation === 'snow' || typeOfPrecipitation === 'rain') {

        // start precipitation after a random timeout to ensure out-of-sync 
        cloudObject.AllIntervals.push(setTimeout(() => {

            cloudObject.AllIntervals.push(setCreatePrecipitationInterval(cloud, precipitationIntensityInSeconds, typeOfPrecipitation))

        }, randomInt(500, 1500)));

    }



    // add thunder if its enabled and random chance 
    if (enableThunder && randomInt(0, 5) == 0) {

        cloudObject.AllTimeouts.push( setCreateThunderTimeout(cloud, randomInt(0, transitionDuration * 1000) - 2));

    }

    // activate transition from left to right
    cloudObject.AllTimeouts.push(setTimeout(() => {
        cloud.style.left = '100%';
    }, 100))

    // when the cloud arrives at the right side remove it and put the cloud object back in the que
    cloudObject.AllTimeouts.push(setTimeout(() => {

        // remove cloud
        clearCloudFromObject(cloudObject);

        cloudsInCue.push(cloudObject);

    }, transitionDuration * 1000))



    activeClouds.push(cloudObject);
}

function clearCloudFromObject(cloudObject) {

    cloudObject.AllTimeouts.forEach(timeoutId => {
        clearTimeout(timeoutId);
    })

    cloudObject.AllTimeouts = [];

    // -- 

    cloudObject.AllElements.forEach(element => {
        // try - catch to prevent script from crashing if element is already removed.
        try {
            backgroundWeatherContainer.removeChild(element);
        } catch { }
    });

    cloudObject.AllElements = [];

    // --


    cloudObject.AllIntervals.forEach(intervalId => {
        clearInterval(intervalId);
    })

    cloudObject.AllIntervals = [];

    // -- 

}


function createCloudObject(dotnetOptionsObject) {
    return {
        options: dotnetOptionsObject,

        AllTimeouts: [],
        AllIntervals: [],
        AllElements: [],
    }
}

function spawnCloudFromQue() {
    if (cloudsInCue.length == 0) {
        return;
    }

    let cloudObject = cloudsInCue.shift();

    spawnCloudFromCloudObject(cloudObject);
}

function generateClouds(dotnetOptionsObject) {

    //
    // clear current que and currently active clouds
    //
    clearInterval(cloudSpawnInterval);


    cloudsInCue.forEach(cloudInQue => {
        clearCloudFromObject(cloudInQue);
    })

    cloudsInCue.length = 0;



    activeClouds.forEach(activeCloud => {
        clearCloudFromObject(activeCloud);
    })

    activeClouds.length = 0;




    //
    // set interval to periotically fetch a cloud from the que and spawn it
    // 
    const timeBetweenCloudSpawnInMiliseconds = dotnetOptionsObject.timeBetweenCloudSpawnInMiliseconds

    cloudSpawnInterval = setInterval(() => {

        spawnCloudFromQue();

    }, timeBetweenCloudSpawnInMiliseconds)



    //
    // add X amount of clouds to cue
    //

    const maxAmountOfClouds = dotnetOptionsObject.fixedCloudNumber ?? window.innerWidth / dotnetOptionsObject.cloudGenerationFactor
    for (var i = 0; i < maxAmountOfClouds; i++) {
        cloudsInCue.push(createCloudObject(dotnetOptionsObject));
    }

    spawnCloudFromQue();
}


//
// function for setting the appropirate weather and time of day according to the weather status
//
// uses the weather types from the enum WeatherStatus parsed as strings
//
function setBackgroundWeather(cloudGenerationOptions) {
    setBackground(cloudGenerationOptions.backgroundCssClass);

    generateClouds(cloudGenerationOptions);
}

function setBackground(backgroundClass) {

    if (backgroundWeatherContainer.classList.contains(backgroundClass)) {
        return;
    }

    let overlayElement = document.createElement("div")
    overlayElement.classList.add('overlay', 'background-transition', backgroundWeatherContainer.classList.toString());


    backgroundWeatherContainer.appendChild(overlayElement);

    backgroundWeatherContainer.setAttribute('class', '');
    backgroundWeatherContainer.classList.add(backgroundClass);

    // activate animations
    setTimeout(() => {
        overlayElement.style.opacity = 0;
    }, 100)


    // remove overlay
    setTimeout(() => {
        backgroundWeatherContainer.removeChild(overlayElement);
    }, 5000)
}
