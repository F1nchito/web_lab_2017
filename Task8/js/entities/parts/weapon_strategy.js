'use strict';
AIRAPP.set('entities.parts.weapon',['entities'],function(entities){
var Bullet = entities.Bullet,
    bulletMargin = 15;
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
    var now = Date.now();
    if(now - this.lastFire > 400){
        this.lastFire = now;
        if(direction === 'up'){
        return new Bullet([position[0]+ size[0]/2, position[1]-bulletMargin], direction);
        }else if(direction ==='down'){
          return new Bullet([position[0]+ size[0]/2, position[1]+size[1]+bulletMargin], direction);  
        }
    }else{
        return null;
    }
};


var TrippleGun = function(){
    this.count = 0;
    this.lastFire = 0;
};
TrippleGun.prototype = Object.create(Default.prototype);
TrippleGun.prototype.shoot = function(position, size, direction){
    var now = Date.now();
    if( this.count < 3){
        this.lastFire = now;
        this.count++;
        if(direction === 'up'){
        return new Bullet([position[0]+ size[0]/2, position[1]-bulletMargin], direction);
        }else if(direction ==='down'){
          return new Bullet([position[0]+ size[0]/2, position[1]+size[1]+bulletMargin], direction);  
        }
    }else if(now - this.lastFire > 400){
        this.count = 0;
    }else{
        return null;
    }
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