'use strict';

AIRAPP.set('entities.Bullet',['helpers','settings','entities','sprites'], function(helpers,settings,entities,sprites){
var GameObject = entities.GameObject;

function Bullet(position, direction) {
    GameObject.apply(this, arguments);
    this.sprite = sprites.getSprite("canvas", "bullet.png");
    this.speed = 15;
    this.size = [this.sprite.img.width/this.sprite.numbersOfFrames,this.sprite.img.height];
    this.direction = direction;
    this.collisions.player = true;
};
Bullet.prototype = Object.create(GameObject.prototype);
Bullet.prototype.constructor = Bullet;
Bullet.prototype.hit = function () {
    this.die();
};
Bullet.prototype.move = function(){
    this.move_strategy.move(this,this.direction);
};
// Bullet.prototype.move = function (direction) {
//     if(!GameObject.prototype.move.call(this, direction)){
//         this.die();
// }
// };
return Bullet;
});