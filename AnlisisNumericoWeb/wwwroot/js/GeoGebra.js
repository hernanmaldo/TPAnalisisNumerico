
let ggbApp = null;

window.addEventListener("load", function () {
    ggbApp = new GGBApplet({
        appName: "graphing",
        width: 600,
        height: 400,
        showToolBar: false,
        showAlgebraInput: true,
        showMenuBar: false
    }, true);

    ggbApp.inject("applet_container");
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

window.addEventListener('DOMContentLoaded', function () {
    const input = document.getElementById('funcion-biseccion');
    if (input) {
        input.addEventListener('blur', function () {
            var fun = input.value;
            console.log(fun);
            console.log(typeof fun);
            graficarFuncion(fun);
            console.log('El usuario salió del campo de texto');
        });
    }
});