'use strict';

AIRAPP.set('entities.Explosion', ['helpers', 'settings', 'entities', 'sprites'], function (helpers, settings, entities, sprites) {
    var GameObject = entities.GameObject;

    function Explosion(position) {
        GameObject.apply(this, arguments);
        this.sprite = sprites.getSprite('explosion.png', 12, false);
        this.size = [this.sprite.img.width / this.sprite.numbersOfFrames, this.sprite.img.height];
        this.position = position;
    };
    Explosion.prototype = Object.create(GameObject.prototype);
    Explosion.prototype.constructor = Explosion;
    Explosion.prototype.hit = function () {
        this.die();
    };
    return Explosion;
});