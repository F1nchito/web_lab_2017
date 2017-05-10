'use strict';
AIRAPP.set('entities.parts.weapon',['entities'],function(entities){
var Bullet = entities.Bullet;
var Weapon = function(type) {
    this.type = type;
}

var Default = function(){
    this.lastFire = 0;
};
Default.prototype.shoot = function(lastFire){
    throw new Error('shoot not overriden')
};

var CasualWeapon = function(){this.lastFire = 0;};
CasualWeapon.prototype = Object.create(Default.prototype);
CasualWeapon.prototype.shoot = function(position, size, direction){
    var w = this.lastFire;
    if(Date.now() - this.lastFire > 100){
        this.lastFire = Date.now();
        return new Bullet([position[0] - 50, position[1] - 10], [0, 5], 'up');
    }else{
        return null;
    }
};


var TrippleGun = function(){};
TrippleGun.prototype = Object.create(Default.prototype);
TrippleGun.prototype.shoot = function(lastFire){
    return "трррррр";
};
Weapon.prototype.shoot = function(position, size, direction){
    return this.type.shoot(position, size, direction);
};
return{
    Weapon: Weapon,
    CasualWeapon: CasualWeapon,
    TrippleGun: TrippleGun
};
});