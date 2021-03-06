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
#line 1 "C:\Users\ers007\Source\Repos\PackingEngine\Packing Engine Project\PackingService\PackingService.Blazor\_Imports.razor"
using System.Net.Http;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\ers007\Source\Repos\PackingEngine\Packing Engine Project\PackingService\PackingService.Blazor\_Imports.razor"
using Microsoft.AspNetCore.Authorization;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "C:\Users\ers007\Source\Repos\PackingEngine\Packing Engine Project\PackingService\PackingService.Blazor\_Imports.razor"
using Microsoft.AspNetCore.Components.Authorization;

#line default
#line hidden
#nullable disable
#nullable restore
#line 4 "C:\Users\ers007\Source\Repos\PackingEngine\Packing Engine Project\PackingService\PackingService.Blazor\_Imports.razor"
using Microsoft.AspNetCore.Components.Forms;

#line default
#line hidden
#nullable disable
#nullable restore
#line 5 "C:\Users\ers007\Source\Repos\PackingEngine\Packing Engine Project\PackingService\PackingService.Blazor\_Imports.razor"
using Microsoft.AspNetCore.Components.Routing;

#line default
#line hidden
#nullable disable
#nullable restore
#line 6 "C:\Users\ers007\Source\Repos\PackingEngine\Packing Engine Project\PackingService\PackingService.Blazor\_Imports.razor"
using Microsoft.AspNetCore.Components.Web;

#line default
#line hidden
#nullable disable
#nullable restore
#line 7 "C:\Users\ers007\Source\Repos\PackingEngine\Packing Engine Project\PackingService\PackingService.Blazor\_Imports.razor"
using Microsoft.JSInterop;

#line default
#line hidden
#nullable disable
#nullable restore
#line 8 "C:\Users\ers007\Source\Repos\PackingEngine\Packing Engine Project\PackingService\PackingService.Blazor\_Imports.razor"
using PackingService.Blazor;

#line default
#line hidden
#nullable disable
#nullable restore
#line 9 "C:\Users\ers007\Source\Repos\PackingEngine\Packing Engine Project\PackingService\PackingService.Blazor\_Imports.razor"
using PackingService.Blazor.Shared;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "C:\Users\ers007\Source\Repos\PackingEngine\Packing Engine Project\PackingService\PackingService.Blazor\Pages\Packing.razor"
using CromulentBisgetti.ContainerPacking.Entities;

#line default
#line hidden
#nullable disable
#nullable restore
#line 4 "C:\Users\ers007\Source\Repos\PackingEngine\Packing Engine Project\PackingService\PackingService.Blazor\Pages\Packing.razor"
using PackingService.Blazor.Data;

#line default
#line hidden
#nullable disable
    [Microsoft.AspNetCore.Components.RouteAttribute("/")]
    public partial class Packing : Microsoft.AspNetCore.Components.ComponentBase
    {
        #pragma warning disable 1998
        protected override void BuildRenderTree(Microsoft.AspNetCore.Components.Rendering.RenderTreeBuilder __builder)
        {
        }
        #pragma warning restore 1998
#nullable restore
#line 271 "C:\Users\ers007\Source\Repos\PackingEngine\Packing Engine Project\PackingService\PackingService.Blazor\Pages\Packing.razor"
       
    //private List<int> _algorithms = new List<int>();
    private List<Item> _items = new List<Item>();
    private List<Container> _boxes = new List<Container>();
    private Container _container = new Container("Pallet 72in", 40, 48, 72);
    private List<AlgorithmPackingResult> _packingResults;

    private int _selectedPackingResult = -1;

    private string _custOrderNo;
    private string _containerType;
    private string _flagpoleText;

    private int itemCounter = 0;

    protected override async Task OnInitializedAsync()
    {
        _packingResults = new List<AlgorithmPackingResult>();
        _custOrderNo = "R4888";
        //await GetAlgorithms();
        await GetItems();
        //await GetPallets();
        await GetSmallPackageBoxes();
    }
    //Todo add call to pack what is in the DOM and render it

    private void Pack()
    {
        _packingResults = CromulentBisgetti.ContainerPacking.PackingService.PackContainer(_container, _items);


        JSRuntime.InvokeAsync<string>("InitializeDrawing");

    

        _selectedPackingResult = 0;
        Visualize();

    }

    private void Visualize()
    {
        JSRuntime.InvokeAsync<string>("SetData", System.Text.Json.JsonSerializer.Serialize(_packingResults[_selectedPackingResult].PackedItems));
    }


    private async Task GetSmallPackageBoxes()
    {
        _containerType = "Small Package";
        _boxes = await BoxService.GetSmallPackageBoxesAsync();
    }

    private async Task GetItems()
    {
        _items = await BoxService.GetItemsAsync(_custOrderNo);
    }

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
