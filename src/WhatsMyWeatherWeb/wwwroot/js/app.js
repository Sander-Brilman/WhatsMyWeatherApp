console.log("verison 9");


function getLocation(dotNetReference) {
    if (("geolocation" in navigator) === false) {
        dotNetReference.invokeMethodAsync("OnLocationNotSupported",);
        return;
    }

    navigator.geolocation.getCurrentPosition(
        (position) => {
            dotNetReference.invokeMethodAsync("OnLocationReceived", position.coords.latitude, position.coords.longitude)
        },
        () => {
            dotNetReference.invokeMethodAsync("OnLocationDenied");
        }
    );
}


