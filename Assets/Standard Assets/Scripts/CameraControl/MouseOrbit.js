var target : Transform;
var distance = 10.0;

var minDist = 4.0;
var maxDist = 15.0;

var xSpeed = 250.0;
var ySpeed = 120.0;

var yMinLimit = -20;
var yMaxLimit = 80;

private var x = 0.0;
private var y = 0.0;

function Start () {
    var angles = transform.eulerAngles;
    x = angles.y;
    y = angles.x;

	// Make the rigid body not change rotation
   	if (GetComponent.<Rigidbody>())
		GetComponent.<Rigidbody>().freezeRotation = true;
}

function LateUpdate () {
    if (target) {
		
		
		
        if(Input.GetMouseButton(1)){
			x += Input.GetAxis("Mouse X") * xSpeed * 0.02;
			y -= Input.GetAxis("Mouse Y") * ySpeed * 0.02;
		}
 		
 		y = ClampAngle(y, yMinLimit, yMaxLimit);
 		      
        var rotation = Quaternion.Euler(y, x, 0);
        var position = rotation * Vector3(0.0, 0.0, -distance) + target.position;
        
		
		transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 0.09);
        transform.position = Vector3.Slerp(transform.position, position, 0.09);
    }
	
	zoomCamera();
}

function zoomCamera() {
	if(Input.GetAxis("Mouse ScrollWheel") < 0){
		if(distance != maxDist)
			distance ++;
	}
	else if(Input.GetAxis("Mouse ScrollWheel") > 0){
		if(distance != minDist)
			distance --;
	}
	
}

static function ClampAngle (angle : float, min : float, max : float) {
	if (angle < -360)
		angle += 360;
	if (angle > 360)
		angle -= 360;
	return Mathf.Clamp (angle, min, max);
}