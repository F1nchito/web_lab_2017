'use strict';
AIRAPP.set('sprites',['renderer','resources'], function (renderer, resources) {

var Sprite = function(painter, src){
 this.painter = painter;
//  this.img = new Image();
//  this.img.src = src;
 this.img = resources.get(src);
 this.frameIndex = 0;
 this.numbersOfFrames = 3;
};

Sprite.prototype.draw = function(coordinates){
    renderer.draw(this,coordinates,
    this.frameIndex*(this.img.width/(this.numbersOfFrames)),
    0,
    this.img.width/(this.numbersOfFrames),
    this.img.height,
    this.img.width/(this.numbersOfFrames),
    this.img.height
    );
    this.frameIndex++
    if(this.frameIndex > this.numbersOfFrames-1){
        this.frameIndex = 0;
    }
};
Sprite.prototype.getSprite = function (painter, src) {
    return new Sprite(painter, src);
};
return {
    draw : Sprite.prototype.draw,
    getSprite : Sprite.prototype.getSprite
}; 
});


