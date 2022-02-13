window.addEventListener('load', function (e) {

    const output = document.getElementById('messages');

    function writeMessage(message) {
        output.innerText += message + '\r\n';
    }

    const source = new ReconnectingEventSource('/api/subscribe');

    source.onmessage = function (event) {
        console.log('onmessage', event);

        writeMessage(event.data);
    };

    source.onopen = function (event) {
        console.log('onopen', event);

        writeMessage('Connected');
    };

    source.onerror = function (event) {
        console.log('onerror', event);

        writeMessage('Server error occurred');
    };

});
