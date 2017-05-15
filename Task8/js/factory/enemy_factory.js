'use strict';

AIRAPP.set('factory.enemy_factory', ['collision_strategy', 'game_engine', 'settings', 'entities', 'sprites'], function (collision_strategy, game_engine, settings, entities, sprites) {
    var Enemy = entities.Enemy,
        weapon = entities.parts.weapon,
        move = entities.parts.move_strategy,
        Enemies = {
            enemyCasual: function (position) {
                var enemy = new Enemy('easy.png', position, null);

                return enemy;
            },
            enemyTrippleGun: function (position) {
                var gun = new weapon.Weapon(new weapon.TrippleGun),
                    enemy = new Enemy('BF-109E.png', position, gun);

                enemy.weapon = gun;
                enemy.speed = 3;
                return enemy;
            },
            enemyBig: function (position) {
                var enemy = new Enemy('JU-87B2.png', position, 'health');

                enemy.health = 3;
                return enemy;
            },
            enemyChanging: function (position) {
                var enemy = new Enemy('TBM3.png', position, null),
                    movement = new move.MoveStrategy(new move.ChangingMove('left'));

                enemy.move_strategy = movement;
                return enemy;
            },
            enemyBoss: function (position, level) {
                var gun = new weapon.Weapon(new weapon.ZigzagWeapon),
                    enemy = new Enemy('boss.png', position, gun),
                    movement = new move.MoveStrategy(new move.PatrolMove('left', 10));

                enemy.move_strategy = movement;
                enemy.health = level * 10;
                enemy.weapon = gun;
                return enemy;
            }
        };

    function getRandomNumber(min, max) {
        min = Math.ceil(min);
        max = Math.floor(max);
        return Math.floor(Math.random() * (max - min)) + min;
    }

    function randomizeEnemy() {
        var typeNum = getRandomNumber(0, 100),
            position = [getRandomNumber(0, settings.width - 200), 0];

        if (typeNum % 7 === 0) {
            return Enemies.enemyBig(position);
        } else if (typeNum % 5 === 0) {
            return Enemies.enemyTrippleGun(position);
        } else if (typeNum % 4 === 0) {
            return Enemies.enemyChanging(position);
        }
        return Enemies.enemyCasual(position);
    };

    function EnemyFactory(enemyAmount) {
        this.maxCount = enemyAmount;
        this.count = 0;
    }
    EnemyFactory.prototype.nextEnemy = function (level) {
        if (this.count === this.maxCount) {
            this.count++;
            return Enemies.enemyBoss([getRandomNumber(0, settings.width - 200), 0], level);
        } else if (this.count < this.maxCount) {
            var enemy = randomizeEnemy();

            this.count++;
            return enemy;
        }
        return null;
    };
    return EnemyFactory;
});