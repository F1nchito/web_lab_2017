'use strict';

AIRAPP.set('entities.Bonus',['helpers','settings','entities','sprites'], function(helpers,settings,entities,sprites){
var GameObject = entities.GameObject,
    weapons = entities.parts.weapon;

function Bonus(position, bonus) {
    GameObject.apply(this, arguments);
    this.bonus = bonus;
    if(bonus === 'health'){
    this.sprite = sprites.getSprite("health.png",3);
    }else if(bonus instanceof entities.parts.weapon.Weapon){
        this.sprite = sprites.getSprite("weapon.png", 1);
    }
    this.speed = 5;
    this.size = [this.sprite.img.width/this.sprite.numbersOfFrames,this.sprite.img.height];
    this.collisions.player = true;
    this.position = position;
};
Bonus.prototype = Object.create(GameObject.prototype);
Bonus.prototype.constructor = Bonus;
Bonus.prototype.hit = function () {
    this.die();
};
Bonus.prototype.move = function(direction){
    this.move_strategy.move(this, direction);
};
return Bonus;
});