AIRAPP.set('settings', function(){
    var width = 895,
    height = 440;

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