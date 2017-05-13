'use strict';

AIRAPP.set('entities.Enemy',['helpers','settings','entities','sprites'], function(helpers,settings,entities,sprites){
var GameObject = entities.GameObject;
var weapon = entities.parts.weapon;
var movement = entities.parts.move_strategy;
function Enemy(position,bonus) {
    GameObject.apply(this, arguments);
    this.sprite = sprites.getSprite("green.png",3);
    this.size = [this.sprite.img.width/this.sprite.numbersOfFrames,this.sprite.img.height];
    this.collisions.player = true;
    this.collisions.bullet = true;
    this.bonus = bonus;
    this.weapon = new weapon.Weapon(new weapon.TrippleGun);
    this.move_strategy = new movement.MoveStrategy(new movement.ChangingMove('left',30));
};

Enemy.prototype = Object.create(GameObject.prototype);
Enemy.prototype.constructor = Enemy;
Enemy.prototype.hit = function () {
    if(this.bonus){
    this.activate("create", new entities.Bonus(this.position, this.bonus));
    }
    this.die();
};
Enemy.prototype.move = function(direction){
    this.move_strategy.move(this,direction);
}
Enemy.prototype.shoot = function (direction) {
    var bullet = this.weapon.shoot(this.position, this.size, direction);
    if(bullet){
    this.activate('create',bullet);
    }
}
return Enemy;
});