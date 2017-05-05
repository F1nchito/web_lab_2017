var AIRAPP = AIRAPP || {};
AIRAPP.namespace = function (ns_name) {
    var parts = ns_name.split('.'),
        parent = AIRAPP,
        i;

    if(parts[0] === "AIRAPP"){
        parts = parts.slice(1);
    }

    for(i = 0; i < parts.length; i++){
        if(typeof parent[parts[i] === "undefined"]){
            parent[parts[i]] = {};
        }
        parent = parent[parts[i]];
    }
    return parent;
};