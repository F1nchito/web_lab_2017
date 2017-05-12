'use strict';

AIRAPP.set('entities.Enemy',['helpers','settings','entities','sprites'], function(helpers,settings,entities,sprites){
var GameObject = entities.GameObject;
var weapon = entities.parts.weapon;
var movement = entities.parts.move_strategy;
function Enemy(position) {
    GameObject.apply(this, arguments);
    this.sprite = sprites.getSprite("canvas", "green.png");
    this.size = [this.sprite.img.width/this.sprite.numbersOfFrames,this.sprite.img.height];
    this.collisions.player = true;
    this.collisions.bullet = false;
    this.weapon = new weapon.Weapon(new weapon.CasualWeapon);
    this.move_strategy = new movement.MoveStrategy(new movement.CasualMove);
};

Enemy.prototype = Object.create(GameObject.prototype);
Enemy.prototype.constructor = Enemy;
Enemy.prototype.hit = function () {
    this.die();
};
Enemy.prototype.move = function(direction){
    this.move_strategy.move(this,direction);
}
Enemy.prototype.shoot = function (direction) {
    var bullet = this.weapon.shoot(this.position, this.size,direction);
    if(bullet){
    this.activate('create',bullet);
    }
}
return Enemy;
});