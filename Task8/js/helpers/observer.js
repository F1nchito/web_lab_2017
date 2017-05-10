"use strict";
AIRAPP.set('helpers.publisher', function(){
    
    function Publisher() {
        this.subscribers = {
            any: []
        };
    };

    function subscribe(type, fn, context) {
        type = type || 'any';
        fn = typeof fn === "function" ? fn : context[fn];
        if (typeof this.subscribers[type] === "undefined") {
            this.subscribers[type] = [];
        }
        this.subscribers[type].push({fn: fn,context: context || this});
    };

    function unsubscribe(type,fn, context) {
        console.log('unsub');
        visitSubs('unsub',type, fn,context);
    };

    function activate(type,publication) {
        visitSubs.call(this,'publish',type,publication);
    };

    function visitSubs(action, type, arg, context) {
        var pubtype = type || 'any',
        subscribers = this.subscribers[pubtype],
        max = subscribers ? subscribers.length : 0,
        i;

        for (i = 0; i < max; i++) {
            if(action === 'publish'){
                subscribers[i].fn.call(subscribers[i].context,arg);
            }else{
                if(subscribers[i].fn === arg && subscribers[i].context === context){
                    subscribers.splice(i,1);
                }
            }            
        }
    };

    Publisher.prototype = {
        subscribe: subscribe,
        unsubscribe: unsubscribe,
        activate: activate
    };

    return Publisher;
});
