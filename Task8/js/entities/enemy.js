'use strict';

AIRAPP.set('entities.Enemy',['helpers','settings','entities','sprites'], function(helpers,settings,entities,sprites){
var GameObject = entities.GameObject;

function Enemy(position, size) {
    GameObject.apply(this, arguments);
    this.sprite = sprites.getSprite("canvas", "img/green.png");
};

Enemy.prototype = Object.create(GameObject.prototype);
Enemy.prototype.constructor = Enemy;
Enemy.prototype.hit = function () {
    this.die();
    // delete allObj[allObj.indexOf(this)];
    // allObj.delete(allObj.indexOf(this), 1);
};
Enemy.prototype.shoot = function () {
    allObj.push(new Bullet([this.position[0] - 10, this.position[1]-10], [0, 5], 'up'));
}
return Enemy;
});