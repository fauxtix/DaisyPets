

var dotnetInstance;

function detail(dotnet) {
    dotnetInstance = dotnet; // dotnet instance to invoke C# method from JS 
}

document.addEventListener('click', function (args) {
    if (args.target.classList.contains("e-dtdiagonaldown") || args.target.classList.contains("e-detailrowexpand")) {
        dotnetInstance.invokeMethodAsync('DetailCollapse', args.target.closest("tr").getAttribute("aria-rowindex")); // call C# method from javascript function
    }
})
