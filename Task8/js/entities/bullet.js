'use strict';

AIRAPP.set('entities.Bullet', ['helpers', 'settings', 'entities', 'sprites'], function (helpers, settings, entities, sprites) {
    var GameObject = entities.GameObject;

    function Bullet(position, direction) {
        GameObject.apply(this, arguments);
        this.sprite = sprites.getSprite('bullet.png', 3, true);
        this.speed = 15;
        this.size = [this.sprite.img.width / this.sprite.numbersOfFrames, this.sprite.img.height];
        this.direction = direction;
        this.collisions.enemy = true;
        this.collisions.player = true;
        this.position = [position[0] - this.size[0] / 2, position[1]];
    };
    Bullet.prototype = Object.create(GameObject.prototype);
    Bullet.prototype.constructor = Bullet;
    Bullet.prototype.hit = function () {
        this.die();
    };
    Bullet.prototype.move = function () {
        this.move_strategy.move(this, this.direction);
    };
    return Bullet;
});