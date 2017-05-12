'use strict';
AIRAPP.set('collision_strategy',['entities'],function(entities){
var Collision = function(owner,type) {
    this.type = type;
    this.owner = owner;
}

var Default = function(){
    this.player = false;
    this.enemy = false;
    this.bullet = false;
    this.bonus = false;
};
Default.prototype.checkCollision = function(object){
    throw new Error('checkCollision not overriden')
};

var CasualCollision = function(){
    this.player = true;
    this.enemy = true;
    this.bullet = true;
    this.bonus = true;
};
CasualCollision.prototype = Object.create(Default.prototype);
CasualCollision.prototype.checkCollision = function(object){
    if(object instanceof entities.Player && this.player){
        owner.hit(object);
    }else if(object instanceof entities.Enemy && this.enemy){
        owner.hit(object);
    }else if(object instanceof entities.Bullet && this.bullet){
owner.hit(object);
    }else if(object instanceof entities.Bonus && this.bonus){
        owner.hit(object); 
    }
};


// var TrippleGun = function(){};
// TrippleGun.prototype = Object.create(Default.prototype);
// TrippleGun.prototype.shoot = function(lastFire){
//     return "трррррр";
// };
Collision.prototype.checkCollision = function(object){
    return this.type.checkCollision(object);
};
return{
    Collision: Collision,
    CasualCollision: CasualCollision,
};
});