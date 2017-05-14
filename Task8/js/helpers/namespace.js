'use strict';
var AIRAPP = AIRAPP || {};

AIRAPP.set = (function (ns_name) {
    function setDefinition(modules, relations, definition) {
        var parts = modules.split('.'),
            parent = ns_name,
            relatedModules,
            i;

        if (typeof relations === 'function') {
            definition = relations;
            relatedModules = [];
        } else {
            relatedModules = getModules(relations);
        }

        if (parts[0] === 'AIRAPP') {
            parts = parts.slice(1);
        }

        for (i = 0; i < parts.length - 1; i++) {
            if (typeof parent[parts[i]] === 'undefined') {
                parent[parts[i]] = {};
            }
            parent = parent[parts[i]];
        }
        parent[parts[i]] = definition.apply(null, relatedModules);
    };

    function getModules(moduleNames) {
        var modules = [],
            i;

        for (i = 0; i < moduleNames.length; i++) {
            if (!ns_name[moduleNames[i]]) {
                ns_name[moduleNames[i]] = {};
            }
            modules[i] = ns_name[moduleNames[i]];
        }
        return modules;
    }

    return setDefinition;
}(AIRAPP));