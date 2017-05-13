'use strict';
AIRAPP.set('sprites',['renderer','resources'], function (renderer, resources) {

var Sprite = function(src,numbersOfFrames){
 this.img = resources.get(src);
 this.frameIndex = 0;
 this.numbersOfFrames = numbersOfFrames;
};

Sprite.prototype.draw = function(coordinates){
    renderer.draw(this.img,coordinates,
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


