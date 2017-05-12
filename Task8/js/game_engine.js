;
AIRAPP.set('game_engine', ['settings','manager_collision', 'entities', 'renderer', 'publisher'], function (settings,manager_collision, entities, renderer, publisher) {
    var objectsArr = [],
        player, enemy,
        enemyArr = [],
        bulletArr = [];

    function gameLoop() {
        renderer.newFrame();
        handleInput(player);
        objectsArr.forEach(function (element) {
            if (element instanceof entities.Bullet) {
                element.move();
            }else if(element instanceof entities.Enemy){
                element.move("down");
                element.shoot("down");
            }
            element.sprite.draw([element.position[0], element.position[1]]);
        }, this);
        manager_collision(objectsArr);
        requestAnimationFrame(gameLoop);
    };

    function startGame() {
        renderer.init();
        player = new entities.Player([500, 200]);
        addElem(player);
        enemy = new entities.Enemy([50, 50], [100, 100]);
        addElem(enemy);
        addElem(new entities.Enemy([160, 60], [100, 100]));
        handleInput(player);
        window.arr = objectsArr;
        gameLoop();
    };

    function removeElem(element) {
        objectsArr.splice(objectsArr.indexOf(element), 1);
    }

    function addElem(element) {
        element.subscribe('died', removeElem, element);
        element.subscribe('create',addElem, element)
        objectsArr.push(element);
    }
    return {
        startGame: startGame,
        removeElem: removeElem,
        addElem: addElem,
        gameLoop: gameLoop
    }
});