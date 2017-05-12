'use strict';

AIRAPP.set('entities.Player',['collision_strategy','game_engine','settings','entities','sprites'], function(collision_strategy,game_engine,settings,entities,sprites){
var GameObject = entities.GameObject;
var weapon = entities.parts.weapon;
var movement = entities.parts.move_strategy;
function Player(position) {
    GameObject.apply(this, arguments);
    this.weapon = new weapon.Weapon(new weapon.CasualWeapon);  
    this.sprite = sprites.getSprite("canvas", "B-17.png");
    this.health = 3;
    this.size = [this.sprite.img.width/this.sprite.numbersOfFrames,this.sprite.img.height];
    this.collisions.bullet = false;
    this.move_strategy = new movement.MoveStrategy(new movement.Default);
};

Player.prototype = Object.create(GameObject.prototype);
Player.prototype.constructor = Player;
Player.prototype.hit = function () {
    this.health--;
    if(this.health < 1){
    this.die();
}
};
Player.prototype.move = function(direction){
    this.move_strategy.move(this,direction);
};
Player.prototype.shoot = function () {
    var bullet = this.weapon.shoot(this.position, this.size,'up');
    if(bullet){
    this.activate('create',bullet);
}
};
return Player;
});