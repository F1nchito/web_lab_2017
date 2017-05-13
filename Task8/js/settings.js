AIRAPP.set('settings', function(){
    var width = Math.max(document.documentElement.clientWidth, window.innerWidth || 0)*0.9,
        height = Math.max(document.documentElement.clientHeight, window.innerHeight || 0)*0.95;
    //  var width = 894,
    //  height = 440;   

    function getWidth() {
        return width;
    };
    function getHeight() {
        return height;
    }
    return{
        width: getWidth(),
        height: getHeight()
    };
});