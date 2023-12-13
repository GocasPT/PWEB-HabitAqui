function generateCoordinates() {
    var street = document.getElementById("Rua").value;
    var distrito = document.getElementById("Distrito").value;
    var concelho = document.getElementById("Concelho").value;
    var pais = document.getElementById("Pais").value;

    if (
        street.trim() === "" ||
        distrito.trim() === "" ||
        concelho.trim() === "" ||
        pais.trim() === ""
    ) {
        document.getElementById("latitude").value = "";
        document.getElementById("longitude").value = "";
        return;
    }

    var apiKey = '880649e9c3144bcdb157c18a67200d26';
    var apiUrl = 'https://api.opencagedata.com/geocode/v1/json?q=' +
        encodeURIComponent(street + ', ' + concelho + ', ' + distrito + ', ' + pais) +
        '&key=' + apiKey;

    fetch(apiUrl)
        .then(response => response.json())
        .then(data => {
            if (data && data.results && data.results.length > 0) {
                var latitude = data.results[0].geometry.lat;
                var longitude = data.results[0].geometry.lng;
                document.getElementById("Latitude").value = latitude;
                document.getElementById("Longitude").value = longitude;
            } else {
                document.getElementById("Latitude").value = "";
                document.getElementById("Longitude").value = "";
            }
        })
        .catch(error => {
            document.getElementById("Latitude").value = "";
            document.getElementById("Longitude").value = "";
        });
}