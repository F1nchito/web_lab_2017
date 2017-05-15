'use strict';

AIRAPP.set('inputer', ['settings'], function (settings) {
    var pressedKeys = {};

    function setKey(event, status) {
        var code = event.keyCode,
            key;

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
            case 27:
                key = 'ESC';
                break;
            default:
                key = String.fromCharCode(code);
        }

        pressedKeys[key] = status;
    };

    document.addEventListener('keydown', function (e) {
        setKey(e, true);
    });

    document.addEventListener('keyup', function (e) {
        setKey(e, false);
    });

    window.addEventListener('blur', function () {
        pressedKeys = {};
    });

    function isDown(key) {
        return pressedKeys[key.toUpperCase()];
    }

    function handleInput(player) {
        if (isDown('DOWN') || isDown('s')) {
            player.move('down');
        }

        if (isDown('UP') || isDown('w')) {
            player.move('up');
        }

        if (isDown('LEFT') || isDown('a')) {
            player.move('left');
        }

        if (isDown('RIGHT') || isDown('d')) {
            player.move('right');
        }

        if (isDown('SPACE')) {
            player.shoot();
        }
    };
    return {
        isDown: isDown,
        handleInput: handleInput
    };
});