var Shoot = function(strategy) {
    this.strategy = strategy;
}

var Strategy = function(){};

Strategy.prototype.shoot = function(){
    throw new Error('shoot not overriden')
};

var FastShootStrategy = function(){};
FastShootStrategy.prototype = Object.create(Strategy.prototype);
FastShootStrategy.prototype.shoot = function(){
    return "пиу";
};

Shoot.prototype.shoot = function(){
    return this.strategy.shoot();
}
var q = new Shoot(new FastShootStrategy());
q.shoot();
