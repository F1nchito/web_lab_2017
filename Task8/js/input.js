;(function() {
    var pressedKeys = {};
        lastFire = 0;
    function setKey(event, status) {
        var code = event.keyCode;
        var key;

        switch(code) {
        case 32:
            key = 'SPACE'; break;
        case 37:
            key = 'LEFT'; break;
        case 38:
            key = 'UP'; break;
        case 39:
            key = 'RIGHT'; break;
        case 40:
            key = 'DOWN'; break;
        default:
            // Convert ASCII codes to letters
            key = String.fromCharCode(code);
        }

        pressedKeys[key] = status;
    }

    document.addEventListener('keydown', function(e) {
        setKey(e, true);
    });

    document.addEventListener('keyup', function(e) {
        setKey(e, false);
    });

    window.addEventListener('blur', function() {
        pressedKeys = {};
    });

    window.input = {
        isDown: function(key) {
            return pressedKeys[key.toUpperCase()];
        }
    };
})();
function handleInput(player) {
    if(input.isDown('DOWN') || input.isDown('s')) {
        // player.position[1] += player.move() ;
        player.move("down");
    }

    if(input.isDown('UP') || input.isDown('w')) {
        // player.position[1]-= player.move();
        player.move("up");
    }

    if(input.isDown('LEFT') || input.isDown('a')) {
        // player.position[0] -= player.move();
        player.move("left");
    }

    if(input.isDown('RIGHT') || input.isDown('d')) {
        // player.position[0] +=player.move();
        player.move("right");
    }

    if(input.isDown('SPACE'))
    { 
    player.shoot();
        // if(Date.now() - lastFire > 100){
        // lastFire = Date.now();

        // }
        /*&&
       !isGameOver &&
       Date.now() - lastFire > 100) {
        var x = player.pos[0] + player.sprite.size[0] / 2;
        var y = player.pos[1] + player.sprite.size[1] / 2;

        bullets.push({ pos: [x, y],
                       dir: 'forward',
                       sprite: new Sprite('img/sprites.png', [0, 39], [18, 8]) });
        bullets.push({ pos: [x, y],
                       dir: 'up',
                       sprite: new Sprite('img/sprites.png', [0, 50], [9, 5]) });
        bullets.push({ pos: [x, y],
                       dir: 'down',
                       sprite: new Sprite('img/sprites.png', [0, 60], [9, 5]) });


        lastFire = Date.now();*/
    }
};