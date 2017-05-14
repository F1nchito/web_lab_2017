'use strict';
AIRAPP.set('entities.parts.weapon', ['entities'], function (entities) {
    var Bullet = entities.Bullet,
        movement = entities.parts.move_strategy,
        bulletMargin = 15;
    var Weapon = function (type) {
        this.type = type;
    }

    var Default = function () {
        this.lastFire = 0;
    };
    Default.prototype.shoot = function (lastFire) {
        throw new Error('shoot not overriden');
    };

    var CasualWeapon = function () {
        this.lastFire = 0;
    };
    CasualWeapon.prototype = Object.create(Default.prototype);
    CasualWeapon.prototype.shoot = function (position, size, direction) {
        var now = Date.now();
        if (now - this.lastFire > 700) {
            this.lastFire = now;
            if (direction === 'up') {
                return new Bullet([position[0] + size[0] / 2, position[1] - bulletMargin], direction);
            } else if (direction === 'down') {
                return new Bullet([position[0] + size[0] / 2, position[1] + size[1] + bulletMargin], direction);
            }
        } else {
            return null;
        }
    };
    var ZigzagWeapon = function () {
        this.count = 0;
        this.lastFire = 0;
    };
    ZigzagWeapon.prototype = Object.create(Default.prototype);
    ZigzagWeapon.prototype.shoot = function (position, size, direction) {
        var now = Date.now(),
            bullet;

        if (this.count < 3) {
            this.lastFire = now;
            this.count++;
            if (direction === 'up') {
                bullet = new Bullet([position[0] + size[0] / 2, position[1] - bulletMargin], direction);
                if (this.count === 1) {
                    bullet.move_strategy = new movement.MoveStrategy(new movement.ChangingMove('left'));
                } else if (this.count === 2) {
                    bullet.move_strategy = new movement.MoveStrategy(new movement.ChangingMove('right'));
                }
                return bullet;
            } else if (direction === 'down') {
                bullet = new Bullet([position[0] + size[0] / 2, position[1] + size[1] + bulletMargin], direction);
                if (this.count === 1) {
                    bullet.move_strategy = new movement.MoveStrategy(new movement.ChangingMove('left'));
                } else if (this.count === 2) {
                    bullet.move_strategy = new movement.MoveStrategy(new movement.ChangingMove('right'));
                }
                return bullet;
            }
        } else if (now - this.lastFire > 700) {
            this.count = 0;
        } else {
            return null;
        }
    };

    var TrippleGun = function () {
        this.count = 0;
        this.lastFire = 0;
    };
    TrippleGun.prototype = Object.create(Default.prototype);
    TrippleGun.prototype.shoot = function (position, size, direction) {
        var now = Date.now();
        if (this.count < 3) {
            this.lastFire = now;
            this.count++;
            if (direction === 'up') {
                return new Bullet([position[0] + size[0] / 2, position[1] - bulletMargin], direction);
            } else if (direction === 'down') {
                return new Bullet([position[0] + size[0] / 2, position[1] + size[1] + bulletMargin], direction);
            }
        } else if (now - this.lastFire > 400) {
            this.count = 0;
        } else {
            return null;
        }
    };
    Weapon.prototype.shoot = function (position, size, direction) {
        return this.type.shoot(position, size, direction);
    };
    return {
        Weapon: Weapon,
        CasualWeapon: CasualWeapon,
        TrippleGun: TrippleGun,
        ZigzagWeapon:ZigzagWeapon
    };
});