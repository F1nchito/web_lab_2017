AIRAPP.set('resources', function() {
    'use strict';

    var resourcesCache = {},
        callback = null,
        capacity = 0,
        loaded = 0,
        assets = {
            image:'./img/'
        };

    function load(urls) {
        capacity = urls.length;
        urls.forEach(function(url) {
            _load(url);
        });
    };

    function _load(url) {
        if(resourcesCache.hasOwnProperty(url)){
            return resourcesCache[url];
        }else{
        var img = new Image();
        img.src = assets.image + url;
        img.onload = function(){
            resourcesCache[url] = img;
            if(Object.keys(resourcesCache).length === capacity){
                callback();
            }
        };
        }
    };

    function get(url) {
        return resourcesCache[url];
    };
    
    function onReady(func) {
        callback = func;
    };

    return  {
        load : load,
        get : get,
        onReady: onReady
    };
});