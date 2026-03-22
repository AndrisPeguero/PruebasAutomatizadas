const containerMessage = document.getElementById("container-message");
const titleMessageError = document.getElementById("error-confirm-account");
const errorMessage = document.getElementById("error-message");



const params = new URLSearchParams(window.location.search);
const userId = params.get("userId");
const token = params.get("token");


async function confirmarCuenta() {
    if (!userId || !token) return;

    try {
        const response = await axios.post("https://localhost:7039/Api/v1/LoginUser/confirm-account", {
            userId,
            token
        });
        
        json = await response.data;
         
        if(json.message && json.message.length > 0 && json.hasError){
              containerMessage.classList.remove("bg-success");
              containerMessage.classList.add("bg-danger");
              titleMessageError.innerText = "Ocurrio un error al confirmar la cuenta";
              errorMessage.innerText = json.message;
              return;
        }else{
           containerMessage.classList.remove("bg-danger"); 
           containerMessage.classList.add("bg-success");

        }
     
    } catch (error) {
         let message = error.response?.data || error.message;
         containerMessage.classList.remove("bg-success");
         containerMessage.classList.add("bg-danger");
         titleMessageError.innerText = "Ocurrio un error al confirmar la cuenta";
         errorMessage.innerText = `No puedes iniciar session, verifica otra vez ${message}`;
    }
}


window.addEventListener("DOMContentLoaded", () => {
    confirmarCuenta();
});
