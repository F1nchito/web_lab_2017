var allObj = [];
function MacroCollision(obj1,obj2){
  var XColl=false;
  var YColl=false;

  if ((obj1.position[0] + obj1.size[0] >= obj2.position[0]) && (obj1.position[0] <= obj2.position[0] + obj2.size[0])) XColl = true;
  if ((obj1.position[1] + obj1.size[1] >= obj2.position[1]) && (obj1.position[1] <= obj2.position[1] + obj2.size[1])) YColl = true;

  if (XColl&YColl){return true;}
  return false;
}
function Collision(obj) {
    for (var i = 0; i < allObj.length; i++) {
        if(allObj[i]!= obj && typeof allObj[i]!="undefined" && typeof obj!="undefined"){
            if(MacroCollision(obj,allObj[i])){
                obj.hit();
                allObj[i].hit();
            }
        }
    }
    for (var index = 0; index < allObj.length; index++) {
        if(typeof allObj[index]=="undefined"){
            allObj.splice(index, 1);
        }
    }
}