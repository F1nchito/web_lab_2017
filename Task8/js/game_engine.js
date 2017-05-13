;
AIRAPP.set('game_engine', ['settings','manager_collision', 'entities', 'renderer', 'publisher'], function (settings,manager_collision, entities, renderer, publisher) {
    var objectsArr = [],
        player, enemy, background,
        enemyArr = [],
        bulletArr = [];

    function gameLoop() {
        renderer.newFrame();
        background.draw()
        handleInput(player);
        objectsArr.forEach(function (element) {
            if (element instanceof entities.Bullet) {
                element.move();
            }else if(element instanceof entities.Enemy){
                element.move("down");
                element.shoot("down");
            }else if(element instanceof entities.Bonus){
                element.move("down");
            }
            element.sprite.draw([element.position[0], element.position[1]]);
        }, this);
        manager_collision(objectsArr);
        requestAnimationFrame(gameLoop);
    };

    function startGame() {
        player = new entities.Player([200, 150]),
        enemy = new entities.Enemy([300, 0], new entities.parts.weapon.Weapon(new entities.parts.weapon.TrippleGun));
        background = new entities.Background('background.png', 5);
        renderer.init();
        addElem(player);
        addElem(enemy);
        addElem(new entities.Enemy([0, 0],'health'));
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