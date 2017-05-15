'use strict';
AIRAPP.set('sprites', ['renderer', 'resources'], function (renderer, resources) {

    var Sprite = function (src, numbersOfFrames, repeatable) {
        this.img = resources.get(src);
        this.frameIndex = 0;
        this.repeatable = repeatable;
        this.numbersOfFrames = numbersOfFrames;
    };

    Sprite.prototype.draw = function (coordinates) {
        renderer.draw(this.img, coordinates,
            this.frameIndex * (this.img.width / this.numbersOfFrames),
            0,
            this.img.width / this.numbersOfFrames,
            this.img.height,
            this.img.width / this.numbersOfFrames,
            this.img.height
        );
        this.frameIndex++;
        if (this.frameIndex > this.numbersOfFrames - 1) {
            if (this.repeatable === true) {
                this.frameIndex = 0;
            } else {
                return false;
            }
        }
    };
    Sprite.prototype.getSprite = function (painter, src, repeatable) {
        return new Sprite(painter, src, repeatable);
    };
    return {
        draw: Sprite.prototype.draw,
        getSprite: Sprite.prototype.getSprite
    };
});