'use strict';
AIRAPP.set('settings', function () {
    var windowWidth = Math.max(document.documentElement.clientWidth, window.innerWidth || 0),
        windowHeight = Math.max(document.documentElement.clientHeight, window.innerHeight || 0),
        width = windowWidth * 0.9,
        height = windowHeight * 0.8,
        infoWidth = windowWidth * 0.9,
        infoHeight = windowHeight * 0.15,
        enemiesOnLvl = 5,
        weaponCd = 1300,
        enemyHealth = 1,
        maxHealth = 5;

    function getWidth() {
        return width;
    };

    function getHeight() {
        return height;
    }
    return {
        enemyHealth: enemyHealth,
        weaponCd: weaponCd,
        enemiesOnLvl: enemiesOnLvl,
        windowWidth: windowWidth,
        windowHeight: windowHeight,
        maxHealth: maxHealth,
        infoWidth: infoWidth,
        infoHeight: infoHeight,
        width: getWidth(),
        height: getHeight()
    };
});