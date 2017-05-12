'use strict';

AIRAPP.set('entities.GameObject',['entities','helpers','settings'], function(entities,helpers,settings){
var publisher = helpers.publisher;
var movement = entities.parts.move_strategy;
var GameObject = function (position) {
    publisher.apply(this,[]);
    this.health = 1;   
    this.speed = 5;
    this.collisions = {
        player : false,
        enemy : false,
        bullet : false,
        bonus : false
    };
    this.position = [position[0] || 0, position[1] || 0],
        // this.size = [size[0] || 0, size[1] || 0];
        this.move_strategy = new movement.MoveStrategy(new movement.CasualMove);
        this.size = [0,0];
};

helpers.inherit.inherit(GameObject,publisher);

GameObject.prototype.die = function () {
    this.activate('died',this);
}

GameObject.prototype.collisionWith = function (object){
    if(object instanceof entities.Player && this.collisions.player){
        this.hit(object);
    }else if(object instanceof entities.Enemy && this.collisions.enemy){
        this.hit(object);
    }else if(object instanceof entities.Bullet && this.collisions.bullet){
        this.hit(object);
    }/*else if(object instanceof entities.Bonus && this.collisions.bonus){
        this.hit(object); 
    }*/
}
/*GameObject.prototype.move = function (direction) {
    var newPosition = [];
    switch (direction) {
        case "up":
            newPosition[1] = this.position[1] - this.speed;
            if (newPosition[1] + this.size[1] <= settings.height && newPosition[1] >= 0) {
                this.position[1] = newPosition[1];
                return true;
            }else{
                return false;
            }
            break;
        case "down":
            newPosition[1] = this.position[1] + this.speed;
            if (newPosition[1] + this.size[1] <= settings.height && newPosition[1] >= 0) {
                this.position[1] = newPosition[1];
                return true;
            }else{
                return false;
            }
            break;
        case "left":
            newPosition[0] = this.position[0] - this.speed;
            if (newPosition[0] + this.size[0] <= settings.width && newPosition[0]  >= 0) {
                this.position[0] = newPosition[0];
            return true;
            }else{
                return false;
            }
            break;
        case "right":
            // this.position[0] += this.speed;
            newPosition[0] = this.position[0] + this.speed;
            if (newPosition[0] + this.size[0] <= settings.width && newPosition[0] >= 0) {
                this.position[0] = newPosition[0];
            return true;
            }else{
                return false;
            }
            break;
        default: return false;
            break;
    }
}*/
return GameObject;
});