var scene;
var camera;
var renderer;
var controls;
var viewModel;
var itemMaterial;

//async function PackContainers(request) {
//	return $.ajax({
//		url: '/api/containerpacking',
//		type: 'POST',
//		data: request,
//		contentType: 'application/json; charset=utf-8'
//	});
//};

function InitializeDrawing() {
	var drawingContainer = $('#drawing-container');

	scene = new THREE.Scene();
	camera = new THREE.PerspectiveCamera(50, window.innerWidth / window.innerHeight, 0.1, 1000);
	camera.lookAt(scene.position);

	//var axisHelper = new THREE.AxisHelper( 5 );
	//scene.add( axisHelper );

	// LIGHT
	var light = new THREE.PointLight(0xffffff);
	light.position.set(0, 150, 100);
	scene.add(light);

	// Get the item stuff ready.
	itemMaterial = new THREE.MeshBasicMaterial({ color: 0xff0000, opacity: 0.1, wireframe: true });

	renderer = new THREE.WebGLRenderer({ antialias: true }); // WebGLRenderer CanvasRenderer
	renderer.setClearColor(0xf0f0f0);
	renderer.setPixelRatio(window.devicePixelRatio);
	renderer.setSize(window.innerWidth / 2, window.innerHeight / 2);
	drawingContainer.append(renderer.domElement);

	controls = new THREE.OrbitControls(camera, renderer.domElement);
	window.addEventListener('resize', onWindowResize, false);

	animate();
};

function onWindowResize() {
	camera.aspect = window.innerWidth / window.innerHeight;
	camera.updateProjectionMatrix();
	renderer.setSize(window.innerWidth / 2, window.innerHeight / 2);
}
//
function animate() {
	requestAnimationFrame(animate);
	controls.update();
	render();
}
function render() {
	renderer.render(scene, camera);
}

function ShowPacking(packedItems) {
	var self = this;

	self.ItemCounter = 0;

	self.ItemsToRender = packedItems;//Un-index this if there are multiple algos
	self.LastItemRenderedIndex = -1;

	self.ContainerOriginOffset = {
		x: 0,
		y: 0,
		z: 0
	};

	//InitializeDrawing();

	//this.Containers = [];
	//self.Containers.push({ ID: id, Name: "JLS PALLET", Length: 40, Width: 48, Height: 78, AlgorithmPackingResults: [] });
	var container = new Container();
	container.ID = "1000";
	container.Name = "Pallet";
	container.Length = 40;
	container.Width = 48;
	container.Height = 78;
	container.AlgorithmPackingResults = [];

	var selectedObject = scene.getObjectByName('container');
	scene.remove(selectedObject);

	for (var i = 0; i < 1000; i++) {
	    var selectedObject2 = scene.getObjectByName('cube' + i);
	    scene.remove(selectedObject2);
	}

	camera.position.set(container.Height, container.Height, container.Height);

	//self.ItemsToRender(container.AlgorithmPackingResults.PackedItems);
	//self.LastItemRenderedIndex(-1);

	self.ContainerOriginOffset.x = -1 * container.Length / 2;
	self.ContainerOriginOffset.y = -1 * container.Height / 2;
	self.ContainerOriginOffset.z = -1 * container.Width / 2;

	var geometry = new THREE.BoxGeometry(container.Length, container.Height, container.Width);
	var geo = new THREE.EdgesGeometry(geometry); // or WireframeGeometry( geometry )
	var mat = new THREE.LineBasicMaterial({ color: 0x000000, linewidth: 2 });
	var wireframe = new THREE.LineSegments(geo, mat);
	wireframe.position.set(0, 0, 0);
	wireframe.name = 'container';
	scene.add(wireframe);
}

function AreItemsPacked() {
	var self = this;
	if (self.LastItemRenderedIndex() > -1) {
		return true;
	}

	return false;
}

function AreAllItemsPacked() {
    var self = this;
	if (self.ItemsToRender.length === self.LastItemRenderedIndex() + 1) {
		return true;
	}

	return false;
}

function PackItemInRender() {
    var self = this;
	var itemIndex = self.LastItemRenderedIndex + 1;

	var itemOriginOffset = {
		x: self.ItemsToRender[itemIndex].PackDimX / 2,
		y: self.ItemsToRender[itemIndex].PackDimY / 2,
		z: self.ItemsToRender[itemIndex].PackDimZ / 2
	};

	var itemGeometry = new THREE.BoxGeometry(self.ItemsToRender[itemIndex].PackDimX, self.ItemsToRender[itemIndex].PackDimY, self.ItemsToRender[itemIndex].PackDimZ);

	//TODO: Set the color of the cube differently if it is a layer https://stackoverflow.com/questions/14181631/changing-color-of-cube-in-three-js

	console.log(self.ItemsToRender[itemIndex].CanBeFlagpole);
	console.log(self.ItemsToRender[itemIndex].WeightDef);

	

	var cube = new THREE.Mesh(itemGeometry, new THREE.MeshBasicMaterial({ color: 0xff0000 }));
	cube.name = 'cube' + itemIndex;
	cube.material.transparent = true;
	cube.material.opacity = .2;

	if (self.ItemsToRender[itemIndex].WeightDef == 'A') {
		cube.material.color.setHex(0xff0000);
	}
	else if (self.ItemsToRender[itemIndex].WeightDef == 'B') {
		cube.material.color.setHex(0x0000ff);
	}
	else if (self.ItemsToRender[itemIndex].WeightDef == 'C') {
		cube.material.color.setHex(0x00ff00);
	}

	const wireframeGeometry = new THREE.WireframeGeometry(itemGeometry);
	const wireframeMaterial = new THREE.LineBasicMaterial({ color: 0x000000 });
	wireframeMaterial.transparent = true;
	wireframeMaterial.opacity = .2;

	if (!self.ItemsToRender[itemIndex].CanBeFlagpole) {
		wireframeMaterial.color.setHex(0xffffff);
		wireframeMaterial.opacity = .5;
    }

	const wireframe = new THREE.LineSegments(wireframeGeometry, wireframeMaterial);
	cube.add(wireframe);

	cube.position.set(self.ContainerOriginOffset.x + itemOriginOffset.x + self.ItemsToRender[itemIndex].CoordX, self.ContainerOriginOffset.y + itemOriginOffset.y + self.ItemsToRender[itemIndex].CoordY, self.ContainerOriginOffset.z + itemOriginOffset.z + self.ItemsToRender[itemIndex].CoordZ);
	
	scene.add(cube);

	self.LastItemRenderedIndex = itemIndex;
}

function UnpackItemInRender() {
    var self = this;
	var selectedObject = scene.getObjectByName('cube' + self.LastItemRenderedIndex);
	scene.remove(selectedObject);
	self.LastItemRenderedIndex = self.LastItemRenderedIndex - 1;
}


var ItemToPack = function () {
	this.ID = '';
	this.Name = '';
	this.Length = '';
	this.Width = '';
	this.Height = '',
		this.Quantity = '';
}

var Container = function () {
	this.ID = '';
	this.Name = '';
	this.Length = '';
	this.Width = '';
	this.Height = '';
	this.AlgorithmPackingResults = [];
}

function SetData(json) {
	var packedItems = JSON.parse(json);
	ShowPacking(packedItems);
	//response.forEach(containerPackingResult => {

	//    viewModel.AddNewContainer(containerPackingResult.ContainerId, containerPackingResult.AlgorithmPackingResults);

	//viewModel.Containers().forEach(container => {
	//	if (container.ID() === containerPackingResult.ContainerId) {
	//		container.AlgorithmPackingResults(containerPackingResult.AlgorithmPackingResults);
	//	}
	//});
	//});
}