'use strict';

AIRAPP.set('entities.Bullet',['helpers','settings','entities','sprites'], function(helpers,settings,entities,sprites){
var GameObject = entities.GameObject;

function Bullet(position, size, direction) {
    GameObject.apply(this, arguments);
    this.sprite = sprites.getSprite("canvas", "img/bullet.png");
    this.speed = 15;
    this.direction = direction;
}
Bullet.prototype = Object.create(GameObject.prototype);
Bullet.prototype.constructor = Bullet;
Bullet.prototype.hit = function () {
    this.die();
};
Bullet.prototype.move = function (direction) {
    if(!GameObject.prototype.move.call(this, direction)){
        this.die();
}
};
return Bullet;
});