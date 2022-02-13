window.addEventListener('load', function (e) {

    const form = document.getElementById('broadcast');
    const messageField = document.getElementById('messagetext');
    const action = form.getAttribute('action');

    form.addEventListener('submit', function (e) {

        e.preventDefault();

        const data = { messageText: messageField.value };

        fetch(action, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify(data),
        })
            .then(response => response.json())
            .then(data => {
                console.log('Success:', data);
            })
            .catch((error) => {
                console.error('Error:', error);
            });
    });

});
