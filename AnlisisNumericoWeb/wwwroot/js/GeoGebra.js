
let ggbApp = null;

let fun = "";

window.addEventListener("load", function () {

    const input = document.getElementById('funcion-biseccion');
    if (input) {
        fun = input.value;
    }
    window.ggbOnInit = function () {
        if (fun) {
            graficarFuncion(fun);
            
            console.log("entro");
        }
    };

    ggbApp = new GGBApplet({

        width: 600,
        height: 400,
        showToolBar: false,
        showSidebar: false,
        showAlgebraInput: false,
        showAlgebraView: false,
        showMenuBar: false,
        perspective: "G",
        ggbOnInit: ggbApplet.setMode(1)

           
  
    }, true);

    


    ggbApp.inject("applet_container");
});

window.addEventListener('DOMContentLoaded', function () {
    const input = document.getElementById('funcion-biseccion');
    if (input) {
        input.addEventListener('blur', function () {
            var fun = input.value;
            graficarFuncion(fun);
        });
    }
});


function graficarFuncion(fun) {
    const ggb = ggbApp.getAppletObject(); // Obtener el objeto applet

    // Verificar que el objeto ggb esté disponible
    if (ggb && ggb.evalCommand) {
        try {     
            ggb.evalCommand(fun); // Evaluar el comando
            console.log("Función graficada con éxito.");
        } catch (error) {
            console.error("Error al graficar la función:", error);
        }
    } else {
        alert("GeoGebra aún no está listo.");
    }
}

