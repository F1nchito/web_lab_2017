'use strict';

AIRAPP.set('entities.Player',['helpers','settings','entities','sprites'], function(helpers,settings,entities,sprites){
var GameObject = entities.GameObject;
var weapon = entities.parts.weapon;
function Player(position, size) {
    GameObject.apply(this, arguments);
    this.weapon = new weapon.Weapon(new weapon.CasualWeapon); 
    this.sprite = sprites.getSprite("canvas", "img/red.png");
    this.health = 3;
};

Player.prototype = Object.create(GameObject.prototype);
Player.prototype.constructor = Player;
Player.prototype.hit = function () {
    this.health--;
    if(this.health < 1){
    this.die();
}
};
Player.prototype.shoot = function () {
    var bullet = this.weapon.shoot(this.position,[1,1],'up');
    // bullet.subscribe('died', removeElem ,this);
    bullet ? allObj.push(bullet) : null;
    // allObj.push(new Bullet([this.position[0] + (this.size[0] / 2), this.position[1]-10], [0,0 ], 'up'));
};
return Player;
});