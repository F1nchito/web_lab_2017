'use strict';
AIRAPP.set('entities.parts.move_strategy',['entities'],function(entities){
var Bullet = entities.Bullet;
var MoveStrategy = function(type) {
    this.type = type;
}

var Default = function(){
};
Default.prototype.move = function (object,direction) {
    var newPosition = [];
    switch (direction) {
        case "up":
            newPosition[1] = object.position[1] - object.speed;
            if (newPosition[1] + object.size[1] <= settings.height && newPosition[1] >= 0) {
                object.position[1] = newPosition[1];
            }else{
                return false;
            }
            break;
        case "down":
            newPosition[1] = object.position[1] + object.speed;
            if (newPosition[1] + object.size[1] <= settings.height && newPosition[1] >= 0) {
                object.position[1] = newPosition[1];
            }else{
                return false;
            }
            break;
        case "left":
            newPosition[0] = object.position[0] - object.speed;
            if (newPosition[0] + object.size[0] <= settings.width && newPosition[0]  >= 0) {
                object.position[0] = newPosition[0];
            }else{
                return false;
            }
            break;
        case "right":
            newPosition[0] = object.position[0] + object.speed;
            if (newPosition[0] + object.size[0] <= settings.width && newPosition[0] >= 0) {
                object.position[0] = newPosition[0];
            }else{
                return false;
            }
            break;
        default: return false;
            break;
    }
}

var CasualMove = function(){
};
CasualMove.prototype = Object.create(Default.prototype);
CasualMove.prototype.move = function(object,direction){
if(Default.prototype.move(object, direction) === false){
      object.die(); 
}
};
MoveStrategy.prototype.move = function(object,direction){
    return this.type.move(object,direction);
};
return{
    MoveStrategy: MoveStrategy,
    Default: Default,
    CasualMove: CasualMove
};
});