'use strict';

AIRAPP.set('manager_collision',function(){

function macroCollision(obj1,obj2){
  var XColl=false;
  var YColl=false;

  if ((obj1.position[0] + obj1.size[0] >= obj2.position[0]) && (obj1.position[0] <= obj2.position[0] + obj2.size[0])) XColl = true;
  if ((obj1.position[1] + obj1.size[1] >= obj2.position[1]) && (obj1.position[1] <= obj2.position[1] + obj2.size[1])) YColl = true;

  if (XColl&YColl){return true;}
  return false;
}

function checkCollisions(arr){
    var collisions = [],
        objArr = arr,
        i,j,count;
    for( i=0, count = arr.length; i < count; i++){
        for( j=0; j<count; j++){
            if(arr[i]!== arr[j] && macroCollision(arr[i],arr[j])){
                collisions.push([arr[i],arr[j]]);
                }
            }
        }
        return collisions;
    };
function collision(arr) {
    var i, collisions;    
  collisions = checkCollisions(arr);
  for(i = 0; i < collisions.length; i++){
      collisions[i][0].collisionWith(collisions[i][1]);
  }
}
return collision;
});
