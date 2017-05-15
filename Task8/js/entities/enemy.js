'use strict';

AIRAPP.set('entities.Enemy', ['helpers', 'settings', 'entities', 'sprites'], function (helpers, settings, entities, sprites) {
    var GameObject = entities.GameObject,
        weapon = entities.parts.weapon,
        movement = entities.parts.move_strategy;

    function Enemy(sprite, position, bonus) {
        GameObject.call(this, position);
        this.sprite = sprites.getSprite(sprite, 3, true);
        this.size = [this.sprite.img.width / this.sprite.numbersOfFrames, this.sprite.img.height];
        this.collisions.player = true;
        this.collisions.bullet = true;
        this.bonus = bonus;
        this.weapon = new weapon.Weapon(new weapon.CasualWeapon);
        this.move_strategy = new movement.MoveStrategy(new movement.BorderMove);
    };

    Enemy.prototype = Object.create(GameObject.prototype);
    Enemy.prototype.constructor = Enemy;
    Enemy.prototype.hit = function () {
        this.health--;
        if (this.health <= 0) {
            this.activate('create', new entities.Explosion(this.position));
            if (this.bonus) {
                this.activate('create', new entities.Bonus(this.position, this.bonus));
            }
            this.die();
        }
    };
    Enemy.prototype.move = function (direction) {
        this.move_strategy.move(this, direction);
    };
    Enemy.prototype.shoot = function (direction) {
        var bullet = this.weapon.shoot(this.position, this.size, direction);

        if (bullet) {
            this.activate('create', bullet);
        }
    };
    return Enemy;
});