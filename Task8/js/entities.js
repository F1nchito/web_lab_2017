var GameObject = function (position, size) {
    this.speed = 5;
    this.size = size;
    this.position = [position[0] || 0, position[1] || 0],
        this.size = [size[0] || 0, size[1] || 0];
};

GameObject.prototype.move = function (direction) {
    var newPosition = [];
    switch (direction) {
        case "up":
            newPosition[1] = this.position[1] - this.speed;
            if (newPosition[1] + this.size[0] <= AIRAPP.settings.height && newPosition[1] >= 0) {
                this.position[1] = newPosition[1];
            }
            break;
        case "down":
            newPosition[1] = this.position[1] + this.speed;
            if (newPosition[1] + this.size[0] <= AIRAPP.settings.height && newPosition[1] >= 0) {
                this.position[1] = newPosition[1];
            }
            break;
        case "left":
            newPosition[0] = this.position[0] - this.speed;
            if (newPosition[0] + this.size[0] <= AIRAPP.settings.width && newPosition[0]  >= 0) {
                this.position[0] = newPosition[0];
            }
            break;
        case "right":
            // this.position[0] += this.speed;
            newPosition[0] = this.position[0] + this.speed;
            var w = AIRAPP.settings.width;
            if (newPosition[0] + this.size[0] <= AIRAPP.settings.width && newPosition[0] >= 0) {
                this.position[0] = newPosition[0];
            }
            break;
        default:
            break;
    }
};

function Player(position, size) {
    GameObject.apply(this, arguments);
    this.sprite = AIRAPP.sprites.getSprite("canvas", "img/red.png");
}

Player.prototype = Object.create(GameObject.prototype);
Player.prototype.constructor = Player;
Player.prototype.hit = function () {
    console.log("ай!");
}
Player.prototype.shoot = function () {
    allObj.push(new Bullet([this.position[0] + (this.size[0] / 2), this.position[1]-10], [0,0 ], 'up'));
}


function Enemy(position, size) {
    GameObject.apply(this, arguments);
    this.sprite = AIRAPP.sprites.getSprite("canvas", "img/green.png");
};

Enemy.prototype = Object.create(GameObject.prototype);
Enemy.prototype.constructor = Enemy;
Enemy.prototype.hit = function () {
    delete allObj[allObj.indexOf(this)];
    // allObj.delete(allObj.indexOf(this), 1);
};
Enemy.prototype.shoot = function () {
    allObj.push(new Bullet([this.position[0] - 10, this.position[1]-10], [0, 5], 'up'));
}

function Bullet(position, size, direction) {
    GameObject.apply(this, arguments);
    this.sprite = AIRAPP.sprites.getSprite("canvas", "img/bullet.png");
    this.speed = 15;
    this.direction = direction;
}
Bullet.prototype = Object.create(GameObject.prototype);
Bullet.prototype.constructor = Bullet;
Bullet.prototype.hit = function () {
    delete allObj[allObj.indexOf(this)];
};
Bullet.prototype.move = function (direction) {
    var newPosition = [];
    switch (direction) {
        case "up":
            newPosition[1] = this.position[1] - this.speed;
            if (newPosition[1] + this.size[0] <= AIRAPP.settings.height && newPosition[1] >= 0) {
                this.position[1] = newPosition[1];
            }else{
                allObj.splice(allObj.indexOf(this), 1);
            }
            break;
        case "down":
            newPosition[1] = this.position[1] + this.speed;
            if (newPosition[1] + this.size[0] <= AIRAPP.settings.height && newPosition[1] >= 0) {
                this.position[1] = newPosition[1];
            }
            else{
                allObj.splice(allObj.indexOf(this), 1);
            }
            break;
        case "left":
            newPosition[0] = this.position[0] - this.speed;
            if (newPosition[0] + this.size[0] <= AIRAPP.settings.width && newPosition[0]  >= 0) {
                this.position[0] = newPosition[0];
            }else{
                allObj.splice(allObj.indexOf(this), 1);
            }
            break;
        case "right":
            // this.position[0] += this.speed;
            newPosition[0] = this.position[0] + this.speed;
            var w = AIRAPP.settings.width;
            if (newPosition[0] + this.size[0] <= AIRAPP.settings.width && newPosition[0] >= 0) {
                this.position[0] = newPosition[0];
            }else{
                allObj.splice(allObj.indexOf(this), 1);
            }
            break;
        default:
            break;
    }
};

// GameObject.factory = function (type,position,size) {
//     var constr = type,
//         newObj;
//         if(typeof GameObject[constr] !== "function"){
//             throw{
//                 name: "Error",
//                 message: constr + "doesnt exist"
//             }
//         }
//         if(typeof GameObject[constr].prototype.move !== "function"){
//             GameObject[constr].prototype = new GameObject(position,size);
//         }
//         newObj = new GameObject[constr](position,size);
//         return newObj;
// }
// GameObject.Enemy = function(){
//     this.title = "enemy";
// }
// var q = GameObject.factory("Enemy",[1,1],[2,6]);