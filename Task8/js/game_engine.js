;
AIRAPP.set('game_engine', ['inputer', 'factory', 'settings', 'manager_collision', 'entities', 'renderer', 'publisher'], function (inputer, factory, settings, manager_collision, entities, renderer, publisher) {
    var objectsArr = [],
        gameState,
        enemiesOnLvl,
        pausePlanned = false,
        Factory = factory.enemy_factory,
        enemyFactory,
        spawnCount = 3,
        player,
        enemy,
        background,
        level = 1,
        cycle = 1,
        enemyArr = [],
        loopID,
        bulletArr = [];

    function gameInit() {
        menu();
    }

    function menu() {
        document.getElementById('gamecontainer').style.display = 'none';
        document.getElementById('menucontainer').style.display = 'flex';
        gameState = 'menu';
        document.getElementById('startgame').addEventListener('click', startGame, false);
    }

    function gameLoop() {
        renderer.newFrame();
        if (gameState === 'game_over') {
            gameOver();
            if (inputer.isDown('SPACE')) {
                menu();
                return;
            }
        }
        if (inputer.isDown('ESC')) {
            pausePlanned = true;
        }
        if (pausePlanned && !inputer.isDown('ESC')) {
            pauseGame();
            pausePlanned = false;
        }
        inputer.handleInput(player);
        if (gameState === 'paused') {
            renderer.fillStyle('#333');
            renderer.fillRect(0, 0, settings.width, settings.height);
            renderer.setFont(26, 'Arial');
            renderer.fillStyle('#eee');
            renderer.text('paused', settings.width / 2, settings.height / 2);
        } else if (gameState === 'changelvl') {
            renderer.fillStyle('#333');
            renderer.fillRect(0, 0, settings.width, settings.height);
            renderer.setFont(26, 'Arial');
            renderer.fillStyle('#eee');
            renderer.text('Level ' + level, settings.width / 2, settings.height / 2);
            if (cycle > 200) {
                startGame(null, level);
                return;
            }
            cycle++;
        } else if (gameState === 'ingame' || gameState === 'boss') {
            if ((cycle % 500 === 0||objectsArr.length === 1) && gameState === 'ingame') {
                for (var i = 0; i < spawnCount; i++) {
                    enemy = enemyFactory.nextEnemy(level);
                    if (enemy) {
                        addElem(enemy);
                        enemiesOnLvl--;
                        updateEnemyAmount(enemiesOnLvl);
                    } else {
                        level++;
                        gameState = 'boss';
                        break;
                    }
                }
                cycle = 0;
            } else if (gameState === 'boss' && objectsArr.length === 1) {
                gameState = 'changelvl';
                cycle = 0;
                objectsArr = [];
            };
            background.draw();
            objectsArr.forEach(function (element) {
                if (element instanceof entities.Bullet) {
                    element.move();
                } else if (element instanceof entities.Enemy) {
                    element.move('down');
                    element.shoot('down');
                } else if (element instanceof entities.Bonus) {
                    element.move('down');
                }
                if (element.sprite.draw([element.position[0], element.position[1]]) === false) {
                    element.activate('died', element);
                };
            }, this);
            manager_collision(objectsArr);
            cycle++;
        }
        requestAnimationFrame(gameLoop);
    };

    function pauseGame(params) {
        if (gameState === 'paused') {
            gameState = 'ingame';
        } else if (gameState === 'ingame') {
            gameState = 'paused';
        }
    }

    function gameOver(element) {
        gameState = 'game_over';
        renderer.fillStyle('#333');
        renderer.fillRect(0, 0, settings.width, settings.height);
        renderer.setFont(26, 'Arial');
        renderer.fillStyle('#eee');
        renderer.text('GAME OVER', settings.width / 2, settings.height / 2);
        renderer.text('press space', settings.width / 2, settings.height / 2 + 50);
    }

    function startGame(e, lvl) {
        if (!lvl) {
            lvl = 1;
        }
        document.getElementById('menucontainer').style.display = 'none';
        document.getElementById('gamecontainer').style.display = 'flex';
        enemyFactory = new Factory(settings.enemiesOnLvl * lvl);
        renderer.init();
        updateLevelCount(lvl);
        enemiesOnLvl = settings.enemiesOnLvl * lvl;
        updateEnemyAmount(enemiesOnLvl);
        player = new entities.Player([settings.width / 2, settings.height - 100]);
        background = new entities.Background('background.png', 5);
        addElem(player);
        addElem(enemyFactory.nextEnemy(level));
        player.subscribe('health', updatePlayerHealth, player);
        player.subscribe('died', gameOver, player);
        inputer.handleInput(player);
        gameState = 'ingame';
        updatePlayerHealth(player);
        window.arr = objectsArr;
        gameLoop();
    };

    function updateEnemyAmount(count) {
        renderer.drawEnemyAmount(count);
    }

    function updateLevelCount(count) {
        renderer.drawLevelCount(count);
    }

    function updatePlayerHealth(player) {
        renderer.drawPlayerHealth(player.health);
    }


    function removeElem(element) {
        objectsArr.splice(objectsArr.indexOf(element), 1);
    }

    function addElem(element) {
        element.subscribe('died', removeElem, element);
        element.subscribe('create', addElem, element);
        objectsArr.push(element);
    }
    return {
        gameInit: gameInit,
        startGame: startGame,
        removeElem: removeElem,
        addElem: addElem,
        gameLoop: gameLoop,
        pauseGame: pauseGame
    };
});