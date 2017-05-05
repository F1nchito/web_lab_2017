/*var canvas = document.getElementById("canvas");
var context = canvas.getContext("2d");

var Sprite = function(drawStrategy) {
    this.drawStrategy = drawStrategy;
}

var DrawStrategy = function(){};

DrawStrategy.prototype.draw = function(coordinates){
    throw new Error('draw not overriden')
};

var DrawPlayer = function(){};
DrawPlayer.prototype = Object.create(Strategy.prototype);
DrawPlayer.prototype.draw = function(coordinates){
    var playerImg = new Image();
    playerImg.src = "img/B-17.png";
    var pattern = context.drawImage(playerImg,coordinates[0],coordinates[1]);
};

Sprite.prototype.draw = function(coordinates){
    return this.drawStrategy.draw(coordinates);
}
var w = new Sprite(new DrawPlayer());


*/