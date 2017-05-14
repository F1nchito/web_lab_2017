'use strict';
AIRAPP.set('renderer', ['settings', 'sprites'], function (settings, sprites) {
    var r = {},
        node,
        healthNode,
        levelNode,
        infoNode,
        enemyCountNode,
        context;

    r.updateSize = function () {

    };

    r.newFrame = function () {
        context.clearRect(0, 0, node.width, node.height);
    };

    r.save = function () {
        context.save();
    };

    r.restore = function () {
        context.restore();
    };

    r.reset = function () {
        context.setTransform(1, 0, 0, 1, 0, 0);
    };

    r.translate = function (x, y) {
        context.translate(x, y);
    };

    r.fillStyle = function (color) {
        context.fillStyle = color;
    };

    r.fillRect = function (x, y, w, h) {
        context.fillRect(x, y, w, h);
    };

    r.pattern = function (image, repeating) {
        return context.createPattern(image, repeating);
    };

    r.getTextWidth = function (text) {
        return context.measureText(text).width;
    };

    r.setFont = function (size, font) {
        context.font = size+'px '+font;
    };

    r.text = function (text, x, y) {
        context.fillText(text, x, y);
    };

    r.drawPlayerHealth = function (health) {
        var newHealth = document.createElement('p'),
            healthImg,
            i;

        newHealth.id = 'health';
        for (i = 0; i < health; i++) {
            healthImg = document.createElement('img');
            healthImg.src = './img/heart.png';
            newHealth.appendChild(healthImg);
        }
        healthNode.parentNode.replaceChild(newHealth, healthNode);
        healthNode = document.getElementById('health');
    };
    r.drawLevelCount = function (count) {
        levelNode.innerHTML = count;
    };
    r.drawEnemyAmount = function (count) {
        enemyCountNode.innerHTML = count;
    };
    r.init = function () {
        node = document.getElementById('canvas');
        context = node.getContext('2d');
        infoNode = document.getElementById('info');
        healthNode = document.getElementById('health');
        levelNode = document.getElementById('level');
        enemyCountNode = document.getElementById('enemycount');
        node.width = settings.width;
        node.height = settings.height;
    };

    r.draw = function (img, coordinates, sx, sy, swidth, sheight, width, height) {
        switch (arguments.length) {
            case 2:
                context.drawImage(img, coordinates[0], coordinates[1]);
                break;
            case 4:
                context.drawImage(img, coordinates[0], coordinates[1], sx, sy);
                break;
            case 8:
                context.drawImage(img, sx, sy, swidth, sheight, coordinates[0], coordinates[1], width, height);
                break;
            default:
                break;
        }
    };
    return r;
});