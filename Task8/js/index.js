var canvas = document.getElementById("canvas");
var context = canvas.getContext("2d");
canvas.width = 895;
canvas.height = 900;
var player = new Player([240, 200], [100, 100], 'green');
var enemy = new Enemy([50, 50], [100, 100], 'green');
var enemy1 = new Enemy([160, 60], [100, 100], 'green');

allObj = [player, enemy,enemy1];
var velocity = 100;
var bgImage = new Image();
var lastRepaintTime = window.performance.now();
bgImage.addEventListener("load", drawplayer, false);
bgImage.src = "img/Sprite_background_effects_0013.png";

function drawBackground(time) {
    var framgap = time - lastRepaintTime;
    lastRepaintTime = time;
    var translateY = velocity * (framgap / 1000);
    context.clearRect(0, 0, canvas.width, canvas.height);
    var pattern = context.createPattern(bgImage, "repeat-y");
    context.fillStyle = pattern;
    context.rect(0, translateY, bgImage.width, bgImage.height);
    context.fill();
    context.translate(0, translateY);
    player.sprite.draw([player.position[0], player.position[1]]);
    requestAnimationFrame(drawBackground);
}

function drawplayer(time) {
    context.clearRect(0, 0, canvas.width, canvas.height);
    var framgap = time - lastRepaintTime;
    lastRepaintTime = time;
    var translateY = velocity * (framgap / 1000);
    context.clearRect(0, 0, canvas.width, canvas.height);
    var pattern = context.createPattern(bgImage, "repeat-y");
    context.fillStyle = pattern;
    context.rect(0, translateY, bgImage.width, bgImage.height);
    context.fill();
    // context.translate(0,translateY);
    // handleInput(enemy);
    handleInput(player);
    allObj.forEach(function (element) {
        if (element instanceof Bullet) {
            element.move(element.direction);
        }
        allObj.forEach(function(element) {
        Collision(element);
          
        }, this);
        element.sprite.draw([element.position[0], element.position[1]])
    }, this);
    // allObj.forEach(function(element) {
    //     Collision(element);
    // }, this);
    requestAnimationFrame(drawplayer);
}