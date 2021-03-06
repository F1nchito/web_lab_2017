'use strict';
AIRAPP.set('entities.parts.move_strategy', ['settings', 'entities'], function (settings, entities) {
    var MoveStrategy,
        ChangingMove,
        BorderMove,
        PatrolMove,
        Default;

    MoveStrategy = function (type) {
        this.type = type;
    };

    Default = function () {};
    Default.prototype.move = function (object, direction) {
        var newPosition = [];

        switch (direction) {
            case 'up':
                newPosition[1] = object.position[1] - object.speed;
                if (newPosition[1] + object.size[1] <= settings.height && newPosition[1] >= 0) {
                    object.position[1] = newPosition[1];
                } else {
                    return false;
                }
                break;
            case 'down':
                newPosition[1] = object.position[1] + object.speed;
                if (newPosition[1] + object.size[1] <= settings.height && newPosition[1] >= 0) {
                    object.position[1] = newPosition[1];
                } else {
                    return false;
                }
                break;
            case 'left':
                newPosition[0] = object.position[0] - object.speed;
                if (newPosition[0] + object.size[0] <= settings.width && newPosition[0] >= 0) {
                    object.position[0] = newPosition[0];
                } else {
                    return true;
                }
                break;
            case 'right':
                newPosition[0] = object.position[0] + object.speed;
                if (newPosition[0] + object.size[0] <= settings.width && newPosition[0] >= 0) {
                    object.position[0] = newPosition[0];
                } else {
                    return true;
                }
                break;
            default:
                return false;
        }
    };

    function changeDirection(direction) {
        var newDirection;

        if (direction === 'right') {
            newDirection = 'left';
        } else if (direction === 'left') {
            newDirection = 'right';
        }
        return newDirection;
    }

    ChangingMove = function (initDirectory, maxSideMove) {
        this.lastDir = initDirectory,
            this.count = 0;
        this.maxCount = maxSideMove;
    };
    ChangingMove.prototype = Object.create(Default.prototype);
    ChangingMove.prototype.move = function (object, direction) {
        var moveOnField = Default.prototype.move(object, direction);

        if (this.count > this.maxCount || Default.prototype.move(object, this.lastDir) === true) {
            this.lastDir = changeDirection(this.lastDir);
            Default.prototype.move(object, this.lastDir);
            this.count = 0;
        } else {
            this.count++;
        }
        if (moveOnField === false) {
            object.die();
        }
    };

    PatrolMove = function (initDirectory) {
        this.lastDir = initDirectory;
    };
    PatrolMove.prototype = Object.create(Default.prototype);
    PatrolMove.prototype.move = function (object) {
        var moveOnField = Default.prototype.move(object, this.lastDir);

        if (moveOnField === true) {
            this.lastDir = changeDirection(this.lastDir);
            Default.prototype.move(object, this.lastDir);
        }
    };

    BorderMove = function () {};
    BorderMove.prototype = Object.create(Default.prototype);
    BorderMove.prototype.move = function (object, direction) {
        if (Default.prototype.move(object, direction) === false) {
            object.die();
        }
    };

    MoveStrategy.prototype.move = function (object, direction) {
        return this.type.move(object, direction);
    };

    return {
        PatrolMove: PatrolMove,
        MoveStrategy: MoveStrategy,
        Default: Default,
        BorderMove: BorderMove,
        ChangingMove: ChangingMove
    };
});