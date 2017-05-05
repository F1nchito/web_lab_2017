;AIRAPP.namespace('sprites');
AIRAPP.sprites = (function () {
    'use strict'
   var canvas = document.getElementById("canvas");
var context = canvas.getContext("2d");

var Sprite = function(painter, src){
 this.painter = painter;
 this.src = src;
};

Sprite.prototype.draw = function(coordinates){
    var img = new Image();
    img.src = this.src;
    context.drawImage(img,coordinates[0],coordinates[1]);
};
Sprite.prototype.getSprite = function (painter, src) {
    return new Sprite(painter, src);
};
return {
    draw : Sprite.prototype.draw,
    getSprite : Sprite.prototype.getSprite
}; 
})();


