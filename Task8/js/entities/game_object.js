'use strict';

AIRAPP.set('entities.GameObject', ['entities', 'helpers', 'settings'], function (entities, helpers, settings) {
    var publisher = helpers.publisher,
        movement = entities.parts.move_strategy,
        GameObject = function (position) {
            publisher.apply(this, []);
            this.health = settings.enemyHealth;
            this.maxHealth = settings.maxHealth;
            this.speed = 1;
            this.collisions = {
                player: false,
                enemy: false,
                bullet: false,
                bonus: false
            };
            this.position = [position[0] || 0, position[1] || 0];
            this.size = [0, 0];
            this.move_strategy = new movement.MoveStrategy(new movement.BorderMove());
        };

    helpers.inherit.inherit(GameObject, publisher);

    GameObject.prototype.die = function () {
        this.activate('died', this);
    };

    GameObject.prototype.gainBonus = function (object) {
        if (object.bonus === 'health') {
            if (this.health < this.maxHealth) {
                this.health++;
                this.activate('health', this);
            }
        } else if (object.bonus instanceof entities.parts.weapon.Weapon) {
            this.weapon = object.bonus;
        }
    };

    GameObject.prototype.collisionWith = function (object) {
        if (object instanceof entities.Player && this.collisions.player) {
            this.hit(object);
        }
        if (object instanceof entities.Enemy && this.collisions.enemy) {
            this.hit(object);
        }
        if (object instanceof entities.Bullet && this.collisions.bullet) {
            this.hit(object);
        }
        if (object instanceof entities.Bonus && this.collisions.bonus) {
            this.gainBonus(object);
        }
    };
    return GameObject;
});