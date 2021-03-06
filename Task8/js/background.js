'use strict';

AIRAPP.set('entities.Background', ['settings', 'renderer', 'resources'], function (settings, renderer, resources) {
    var Background = function (src, speed) {
        this.img = resources.get(src);
        this.speed = speed;
        this.x = 0;
        this.y = 0;
    };

    Background.prototype.draw = function () {
        var canvasHeight = settings.height,
            canvasWidth = settings.width;

        this.y += this.speed;
        renderer.draw(this.img, [this.x, this.y], canvasWidth, canvasHeight);
        renderer.draw(this.img, [this.x, this.y - canvasHeight], canvasWidth, canvasHeight);
        if (this.y >= canvasHeight) {
            this.y = 0;
        };
    };

    return Background;
});