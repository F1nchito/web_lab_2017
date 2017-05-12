;
(function () {
    var pressedKeys = {};
    lastFire = 0,
    settings = AIRAPP.settings;

    function setKey(event, status) {
        var code = event.keyCode;
        var key;

        switch (code) {
            case 32:
                key = 'SPACE';
                break;
            case 37:
                key = 'LEFT';
                break;
            case 38:
                key = 'UP';
                break;
            case 39:
                key = 'RIGHT';
                break;
            case 40:
                key = 'DOWN';
                break;
            default:
                key = String.fromCharCode(code);
        }

        pressedKeys[key] = status;
    }

    document.addEventListener('keydown', function (e) {
        setKey(e, true);
    });

    document.addEventListener('keyup', function (e) {
        setKey(e, false);
    });

    window.addEventListener('blur', function () {
        pressedKeys = {};
    });

    window.input = {
        isDown: function (key) {
            return pressedKeys[key.toUpperCase()];
        }
    };
})();

function handleInput(player) {
    if (input.isDown('DOWN') || input.isDown('s')) {
        player.move("down");
    }

    if (input.isDown('UP') || input.isDown('w')) {
        player.move("up");
    }

    if (input.isDown('LEFT') || input.isDown('a')) {
        player.move("left");
    }

    if (input.isDown('RIGHT') || input.isDown('d')) {
        player.move("right");
    }

    if (input.isDown('SPACE')) {
        player.shoot();
    }
};