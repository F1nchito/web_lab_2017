'use strict';
;AIRAPP.set('renderer',['settings','sprites'], function(settings, sprites){
    var r = {},
        node = undefined,
        context = undefined;

    r.updateSize = function(){

    };

    r.newFrame = function(){
        context.clearRect(0, 0, node.width, node.height);
    }
    r.save = function(){
        context.save();
    };

    r.restore = function(){
        context.restore();
    };

    r.reset = function(){
        context.setTransform(1,0,0,1,0,0);
    };

    r.translate = function(x, y){
        context.translate(x, y);
    };

    r.fillStyle = function(color){
        context.fillStyle = color;
    };

    r.fillRect = function(x, y, w, h){
        context.fillRect(x, y, w, h);
    };

    r.pattern = function(image, repeating){
        return context.createPattern(image, repeating);
    };
    
    r.getTextWidth = function(text){
        return context.measureText(text).width;
    };
    
    r.setFont = function(size, font){
        context.font = '{0}px {1}'.format(size, font);
    };

    r.text = function(text, x, y){
        context.fillText(text, x, y);
    };

    r.init = function(){
        node = document.getElementById('canvas');
        context = node.getContext('2d');
        node.width = settings.width;
        node.height = settings.height;
    };

    r.draw = function(sprite,coordinates,sx,sy,swidth,sheight,width,height){
        switch (arguments.length) {
            case 2:
        context.drawImage(sprite.img, coordinates[0], coordinates[1]);
                break;
            case 8:
                context.drawImage(sprite.img,sx,sy,swidth,sheight,coordinates[0],coordinates[1],width,height);
                break;
            default:
                break;
        }
    };
    return r;
});