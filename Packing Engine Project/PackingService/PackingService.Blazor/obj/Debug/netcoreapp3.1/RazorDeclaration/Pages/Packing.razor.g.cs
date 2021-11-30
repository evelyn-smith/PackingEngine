// <auto-generated/>
#pragma warning disable 1591
#pragma warning disable 0414
#pragma warning disable 0649
#pragma warning disable 0169

namespace PackingService.Blazor.Pages
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Components;
#nullable restore
#line 1 "C:\Users\ers007\source\repos\PackingEngine\Packing Engine Project\PackingService\PackingService.Blazor\_Imports.razor"
using System.Net.Http;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\ers007\source\repos\PackingEngine\Packing Engine Project\PackingService\PackingService.Blazor\_Imports.razor"
using Microsoft.AspNetCore.Authorization;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "C:\Users\ers007\source\repos\PackingEngine\Packing Engine Project\PackingService\PackingService.Blazor\_Imports.razor"
using Microsoft.AspNetCore.Components.Authorization;

#line default
#line hidden
#nullable disable
#nullable restore
#line 4 "C:\Users\ers007\source\repos\PackingEngine\Packing Engine Project\PackingService\PackingService.Blazor\_Imports.razor"
using Microsoft.AspNetCore.Components.Forms;

#line default
#line hidden
#nullable disable
#nullable restore
#line 5 "C:\Users\ers007\source\repos\PackingEngine\Packing Engine Project\PackingService\PackingService.Blazor\_Imports.razor"
using Microsoft.AspNetCore.Components.Routing;

#line default
#line hidden
#nullable disable
#nullable restore
#line 6 "C:\Users\ers007\source\repos\PackingEngine\Packing Engine Project\PackingService\PackingService.Blazor\_Imports.razor"
using Microsoft.AspNetCore.Components.Web;

#line default
#line hidden
#nullable disable
#nullable restore
#line 7 "C:\Users\ers007\source\repos\PackingEngine\Packing Engine Project\PackingService\PackingService.Blazor\_Imports.razor"
using Microsoft.JSInterop;

#line default
#line hidden
#nullable disable
#nullable restore
#line 8 "C:\Users\ers007\source\repos\PackingEngine\Packing Engine Project\PackingService\PackingService.Blazor\_Imports.razor"
using PackingService.Blazor;

#line default
#line hidden
#nullable disable
#nullable restore
#line 9 "C:\Users\ers007\source\repos\PackingEngine\Packing Engine Project\PackingService\PackingService.Blazor\_Imports.razor"
using PackingService.Blazor.Shared;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "C:\Users\ers007\source\repos\PackingEngine\Packing Engine Project\PackingService\PackingService.Blazor\Pages\Packing.razor"
using CromulentBisgetti.ContainerPacking.Entities;

#line default
#line hidden
#nullable disable
#nullable restore
#line 4 "C:\Users\ers007\source\repos\PackingEngine\Packing Engine Project\PackingService\PackingService.Blazor\Pages\Packing.razor"
using PackingService.Blazor.Data;

#line default
#line hidden
#nullable disable
    [Microsoft.AspNetCore.Components.RouteAttribute("/packing")]
    public partial class Packing : Microsoft.AspNetCore.Components.ComponentBase
    {
        #pragma warning disable 1998
        protected override void BuildRenderTree(Microsoft.AspNetCore.Components.Rendering.RenderTreeBuilder __builder)
        {
        }
        #pragma warning restore 1998
#nullable restore
#line 281 "C:\Users\ers007\source\repos\PackingEngine\Packing Engine Project\PackingService\PackingService.Blazor\Pages\Packing.razor"
       
    //private List<int> _algorithms = new List<int>();
    private List<Item> _items = new List<Item>();
    private List<Container> _boxes = new List<Container>();
    private Container _container = new Container("Pallet 72in", 40, 48, 72);
    private List<AlgorithmPackingResult> _packingResults;

    private int _selectedPackingResult = -1;

    private string _custOrderNo;
    private string _containerType;

    private int itemCounter = 0;

    protected override async Task OnInitializedAsync()
    {
        _packingResults = new List<AlgorithmPackingResult>();
        _custOrderNo = "R44999";
        //await GetAlgorithms();
        await GetItems();
        //await GetPallets();
        await GetSmallPackageBoxes();
    }
    //Todo add call to pack what is in the DOM and render it

    private void Pack()
    {

        //var indexId = 0;

        // var unPackedItems = _items;
        //while (unPackedItems.Any())
        // {
        _packingResults = CromulentBisgetti.ContainerPacking.PackingService.PackContainer(_container, _items);
        // packingResult.IndexId = indexId;
        //  indexId++;
        //  _packingResults.Add(packingResult);

        //  foreach (var item in packingResult.PackedItems)
        //  {
        //      unPackedItems.Remove(item);
        //  }

        JSRuntime.InvokeAsync<string>("InitializeDrawing");

        //InitializeDrawing(indexId);

        //var commands = new List<string>();

        //commands.Add($"camera.position.set({_container.MaxDimension}, {_container.MaxDimension}, {_container.MaxDimension});");
        //commands.Add($"var geometry = new THREE.BoxGeometry({_container.Length}, {_container.Height}, {_container.Width});");
        //commands.Add("var geo = new THREE.EdgesGeometry(geometry);");
        //commands.Add("var mat = new THREE.LineBasicMaterial({ color: 0x000000, linewidth: 2 });");
        //commands.Add("var wireframe = new THREE.LineSegments(geo, mat);");
        //commands.Add("wireframe.position.set(0, 0, 0);");
        //commands.Add("wireframe.name = 'container';");
        //commands.Add("wireframe.name = 'container';");

        //RunJsCommands(commands);
        //}

        //var packResult = _packResults.FirstOrDefault();
        //var container = _containers.FirstOrDefault(x => x.Id == packResult.ContainerId);

        //JSRuntime.InvokeAsync<string>("InitializeDrawing");

        //var commands = new List<string>();

        //commands.Add($"camera.position.set({container.MaxDimension}, {container.MaxDimension}, {container.MaxDimension});");
        //commands.Add($"var geometry = new THREE.BoxGeometry({container.Length}, {container.Height}, {container.Width});");
        //commands.Add("var geo = new THREE.EdgesGeometry(geometry);");
        //commands.Add("var mat = new THREE.LineBasicMaterial({ color: 0x000000, linewidth: 2 });");
        //commands.Add("var wireframe = new THREE.LineSegments(geo, mat);");
        //commands.Add("wireframe.position.set(0, 0, 0);");
        //commands.Add("wireframe.name = 'container';");
        //commands.Add("wireframe.name = 'container';");

        //RunJsCommands(commands);

        _selectedPackingResult = 0;
        Visualize();

    }

    private void Visualize()
    {
        JSRuntime.InvokeAsync<string>("SetData", System.Text.Json.JsonSerializer.Serialize(_packingResults[_selectedPackingResult].PackedItems));
    }

    //private void InitializeDrawing(int indexId)
    //{
    //    var commands = new List<string>();

    //    commands.Add($"var scenes-{indexId};");
    //    commands.Add($"var camera-{indexId};");
    //    commands.Add($"var renderer-{indexId};");
    //    commands.Add($"var controls-{indexId};");
    //    commands.Add($"var viewModel-{indexId};");
    //    commands.Add($"var itemMaterial-{indexId};");


    //    commands.Add($"var drawingContainer-{indexId} = $('#drawing-container-{indexId}');");

    //    commands.Add($"scene-{indexId} = new THREE.Scene();");
    //    commands.Add($"camera-{indexId} = new THREE.PerspectiveCamera(50, window.innerWidth / window.innerHeight, 0.1, 1000);");
    //    commands.Add($"camera-{indexId}.lookAt(scene-{indexId}.position);");

    //    //var axisHelper = new THREE.AxisHelper( 5 );
    //    //scene.add( axisHelper );

    //    // LIGHT
    //    commands.Add($"var light-{indexId} = new THREE.PointLight(0xffffff);");
    //    commands.Add($"light-{indexId}.position.set(0, 150, 100);");
    //    commands.Add($"scene-{indexId}.add(light-{indexId});");

    //    // Get the item stuff ready.
    //    commands.Add($"itemMaterial-{indexId} = new THREE.MeshNormalMaterial({{ transparent: true, opacity: 0.6 }});");


    //    commands.Add($"renderer-{indexId} = new THREE.WebGLRenderer({{ antialias: true }});");
    //    commands.Add($"renderer-{indexId}.setClearColor(0xf0f0f0);");
    //    commands.Add($"renderer-{indexId}.setPixelRatio(window.devicePixelRatio);");
    //    commands.Add($"renderer-{indexId}.setSize(window.innerWidth / 2, window.innerHeight / 2);");
    //    commands.Add($"drawingContainer-{indexId}.append(renderer-{indexId}.domElement);");

    //    commands.Add($"controls-{indexId} = new THREE.OrbitControls(camera-{indexId}, renderer.domElement);");
    //    commands.Add($"window.addEventListener('resize', onWindowResize, false);");

    //    commands.Add($"animate();");
    //    RunJsCommands(commands);
    //}

    //private void ShowNext(int indexId)
    //{
    //    var packResult = _packingResults[indexId];
    //    var containerOriginOffsetX = -1 * _container.Length / 2;
    //    var containerOriginOffsetY = -1 * _container.Height / 2;
    //    var containerOriginOffsetZ = -1 * _container.Width / 2;


    //    var itemToRender = packResult.PackedItems[itemCounter];
    //    itemCounter++;
    //    var commands = new List<string>();
    //    commands.Add($"var itemGeometry = new THREE.BoxGeometry({itemToRender.PackDimX}, {itemToRender.PackDimY}, {itemToRender.PackDimZ});");
    //    commands.Add($"var cube = new THREE.Mesh(itemGeometry, itemMaterial);");
    //    commands.Add($"cube.position.set({containerOriginOffsetX} + {itemToRender.PackDimX / 2} + {itemToRender.CoordX}, {containerOriginOffsetY} + {itemToRender.PackDimY / 2} + {itemToRender.CoordY}, {containerOriginOffsetZ} + {itemToRender.PackDimZ / 2} + {itemToRender.CoordZ});");
    //    commands.Add($"cube.name = 'cube' + {itemCounter};");
    //    commands.Add($"scene.add(cube);");
    //    RunJsCommands(commands);

    //}

    //private void RunJsCommands(List<string> commands)
    //{
    //    foreach (var command in commands)
    //    {
    //        JSRuntime.InvokeAsync<string>(command);
    //    }
    //}

    //private async Task GetPallets()
    //{
    //    _containerType = "Pallets";
    //    _containers = await BoxService.GetPalletsAsync();
    //}

    private async Task GetSmallPackageBoxes()
    {
        _containerType = "Small Package";
        _boxes = await BoxService.GetSmallPackageBoxesAsync();
    }

    private async Task GetItems()
    {
        _items = await BoxService.GetItemsAsync(_custOrderNo);
    }

    //private async Task GetAlgorithms()
    //{
    //    _algorithms = await BoxService.GetAlgorithmsAsync();
    //}

    private void ShowNext()
    {
        JSRuntime.InvokeAsync<string>("PackItemInRender");
    }

    private void ShowPrevious()
    {
        JSRuntime.InvokeAsync<string>("UnpackItemInRender");
    }



#line default
#line hidden
#nullable disable
        [global::Microsoft.AspNetCore.Components.InjectAttribute] private IJSRuntime JSRuntime { get; set; }
        [global::Microsoft.AspNetCore.Components.InjectAttribute] private BoxService BoxService { get; set; }
    }
}
#pragma warning restore 1591
