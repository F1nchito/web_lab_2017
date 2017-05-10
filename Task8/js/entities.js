// 'use strict';
// AIRAPP.set('ent')


// function Player(position, size) {
//     GameObject.apply(this, arguments);
//     this.weapon = new Weapon(new CasualWeapon); 
//     this.sprite = AIRAPP.sprites.getSprite("canvas", "img/red.png");
// }

// Player.prototype = Object.create(GameObject.prototype);
// Player.prototype.constructor = Player;
// Player.prototype.hit = function () {
//     this.die();
// }
// Player.prototype.shoot = function () {
//     var bullet = this.weapon.shoot(this.position,[1,1],'up');
//     bullet ? allObj.push(bullet) : null;
//     // allObj.push(new Bullet([this.position[0] + (this.size[0] / 2), this.position[1]-10], [0,0 ], 'up'));
// }


// function Enemy(position, size) {
//     GameObject.apply(this, arguments);
//     this.sprite = AIRAPP.sprites.getSprite("canvas", "img/green.png");
// };

// Enemy.prototype = Object.create(GameObject.prototype);
// Enemy.prototype.constructor = Enemy;
// Enemy.prototype.hit = function () {
//     this.die();
//     delete allObj[allObj.indexOf(this)];
//     // allObj.delete(allObj.indexOf(this), 1);
// };
// Enemy.prototype.shoot = function () {
//     allObj.push(new Bullet([this.position[0] - 10, this.position[1]-10], [0, 5], 'up'));
// }

// function Bullet(position, size, direction) {
//     GameObject.apply(this, arguments);
//     this.sprite = AIRAPP.sprites.getSprite("canvas", "img/bullet.png");
//     this.speed = 15;
//     this.direction = direction;
// }
// Bullet.prototype = Object.create(GameObject.prototype);
// Bullet.prototype.constructor = Bullet;
// Bullet.prototype.hit = function () {
//     delete allObj[allObj.indexOf(this)];
// };
// Bullet.prototype.move = function (direction) {
//     if(!Object.getPrototypeOf(Object.getPrototypeOf(this)).move.call(this,direction)){
//         allObj.splice(allObj.indexOf(this), 1);
//     }
// };

// GameObject.factory = function (type,position,size) {
//     var constr = type,
//         newObj;
//         if(typeof GameObject[constr] !== "function"){
//             throw{
//                 name: "Error",
//                 message: constr + "doesnt exist"
//             }
//         }
//         if(typeof GameObject[constr].prototype.move !== "function"){
//             GameObject[constr].prototype = new GameObject(position,size);
//         }
//         newObj = new GameObject[constr](position,size);
//         return newObj;
// }
// GameObject.Enemy = function(){
//     this.title = "enemy";
// }
// var q = GameObject.factory("Enemy",[1,1],[2,6]);