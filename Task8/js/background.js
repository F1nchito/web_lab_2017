'use strict';
AIRAPP.set('entities.Background',['settings','renderer','resources'], function (settings,renderer, resources) {

var Background = function(src,speed){
 this.img = resources.get(src);
//  this.img.width = settings.width;
//  this.img.height = settings.height; 
 this.speed = speed;
 this.x = 0;
 this.y = 0;
};

Background.prototype.draw = function(){
   var canvasHeight = settings.height,
        canvasWidth = settings.width;
   this.y += this.speed;
		renderer.draw(this.img, [this.x, this.y], canvasWidth, canvasHeight);
		// Draw another image at the top edge of the first image
		renderer.draw(this.img, [this.x, this.y - canvasHeight],canvasWidth,canvasHeight);
		// If the image scrolled off the screen, reset
		if (this.y >= canvasHeight){
			this.y = 0;
	};
};

return Background;
});
