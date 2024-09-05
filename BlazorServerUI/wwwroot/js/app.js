
console.log("verison 8");


function getLocation(dotNetReference) {
    if (("geolocation" in navigator) === false) {
        dotNetReference.invokeMethodAsync("GetLocationErrorCallback",);
        return;
    }

    navigator.geolocation.getCurrentPosition(
        (position) => {
            dotNetReference.invokeMethodAsync("GetLocationCallback", position.coords.latitude, position.coords.longitude)
        },
        () => {
            dotNetReference.invokeMethodAsync("GetLocationErrorCallback");
        }
    );
}


