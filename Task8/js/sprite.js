'use strict';
AIRAPP.set('sprites', function () {
   var canvas = document.getElementById("canvas");
var context = canvas.getContext("2d");

var Sprite = function(painter, src){
 this.painter = painter;
 this.img = new Image();
 this.img.src = src;
};

Sprite.prototype.draw = function(coordinates){
    context.drawImage(this.img,coordinates[0],coordinates[1]);
};
Sprite.prototype.getSprite = function (painter, src) {
    return new Sprite(painter, src);
};
return {
    draw : Sprite.prototype.draw,
    getSprite : Sprite.prototype.getSprite
}; 
});


