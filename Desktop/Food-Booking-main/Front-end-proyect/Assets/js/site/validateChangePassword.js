
document.addEventListener("DOMContentLoaded", () => {
    const erroPassword = document.getElementById("error-password");
    const formChangePassword = document.getElementById("change-password-form");
    const newPassword = document.getElementById("password");
    const errorNoEspecificPassword = document.getElementById("change-password-NotEspecific");

    const params = new URLSearchParams(window.location.search);
    const userId = params.get("userId");
    const token = params.get("token");

    
      function validatePassword(password, input) {

       let isValid = true;
      

        if(password.length < 8 || password.length > 8){

          erroPassword.innerText = "La contraseña debe tener  8 caracteres";
          isValid = false;

        } 

        else if(!/[0-9]/.test(password)){

        erroPassword.innerText = "La contraseña debe tener al menos un número";
        isValid = false;

       }

        else if(!/[!@#$%^&*(),.?":{}|<>]/.test(password)){

        erroPassword.innerHTML = "La contraseña debe tener al menos un carácter especial";
        isValid = false;

       }

       else if(!/[A-Z]/.test(password)){

       erroPassword.innerText = "La contraseña debe tener una mayúscula";
       isValid = false;

       }

        else if(!/[a-z]/.test(password)){

          erroPassword.innerText = "La contraseña debe tener una minúscula";
          isValid = false;

         } 

        if(!isValid){

          input.classList.add("error-input");
          input.classList.remove("error-success");
        }else{

         input.classList.remove("error-input");
         input.classList.add("error-success");

        }

         return isValid;
}


 if (!formChangePassword) return;
    formChangePassword.addEventListener("submit", async function(e){

        e.preventDefault();

        if (!userId || !token) return;

        try {

            const password = newPassword.value;

            if(!validatePassword(password,newPassword)){
                return;
            }

            await axios.post("https://localhost:7039/Api/v1/LoginUser/change-password",
                {
                    id: userId,
                    password: password,
                    token: token
                }
            );

            localStorage.setItem("message-success-change","Contraseña cambiada, puedes iniciar sesión");
            window.location.href = "/Assets/view/Login.html";

        } catch (error) {

            errorNoEspecificPassword.innerText = "Ocurrio un error con el servidor";

        }

    });

});


