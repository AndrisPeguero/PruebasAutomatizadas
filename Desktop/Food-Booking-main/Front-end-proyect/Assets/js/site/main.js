const inputUserName = document.getElementById("UserName");
const inputPassword = document.getElementById("Password");
const errorPassword = document.getElementById("error-password");
const errorUserName = document.getElementById("error-userName");
const errorNoEspecifico = document.getElementById("error-no-especifico");
const form = document.getElementById("login-form");
const message = localStorage.getItem("message");
const messagesuccessResset = localStorage.getItem("resset-success");
const messageSuccessChange = localStorage.getItem("message-success-change");
const containerMessageSuccess = document.getElementById("message-success");

if(message){
    containerMessageSuccess.innerText = message;
    localStorage.removeItem("message");
}

if(messagesuccessResset){
    containerMessageSuccess.innerText = messagesuccessResset;
     localStorage.removeItem("resset-success");
}


if(messageSuccessChange){
    containerMessageSuccess.innerText = messageSuccessChange;
    localStorage.removeItem("message-success-change");
}



function validateInputLogin(){
isValid = true;

  validateInput(inputUserName,"userName",isValid);
  validateInput(inputPassword,"password",isValid); 
  
   return isValid;
}


function validateInput(input,type,IsValid){

 if(input.value == undefined || input.value == "" || input.value == null){
    input.classList.add("input-error");
    input.classList.remove("input-success");
    if(type == "password"){
        errorPassword.innerText = "debes indicar una contraseña";
    }else{
        errorUserName.innerText = "debes indicar un nombre de usuario"
    }  
    return false; 
 }else{
    input.classList.remove("input-error");
    input.classList.add("input-success");
   
 }  


  if(!IsValid){
      return false;
  }else{
      return true;
  }

}


function validatePassword(password, input) {

let isValid = true;
let message = "";

if(password.length < 8 || password.length > 8){

errorPassword.innerText = "La contraseña debe tener  8 caracteres";
isValid = false;

}

else if(!/[0-9]/.test(password)){

errorPassword.innerText = "La contraseña debe tener al menos un número";
isValid = false;

}

else if(!/[!@#$%^&*(),.?":{}|<>]/.test(password)){

errorPassword.innerHTML = "La contraseña debe tener al menos un carácter especial";
isValid = false;

}

else if(!/[A-Z]/.test(password)){

errorPassword.innerText = "La contraseña debe tener una mayúscula";
isValid = false;

}

else if(!/[a-z]/.test(password)){

errorPassword.innerText = "La contraseña debe tener una minúscula";
isValid = false;

}

if(!isValid){

input.classList.add("error-input");
input.classList.remove("error-success");

console.log(message); 

}else{

input.classList.remove("error-input");
input.classList.add("error-success");

}

return isValid;

}









form.addEventListener("submit",function(e){
e.preventDefault();

if(!validateInputLogin()){
    return;
}

const passwordValue = inputPassword.value;
const userNameValue = inputUserName.value;

LoginEnpoint(userNameValue,passwordValue);

});



async function LoginEnpoint(userName,Password)
{
  try{

    if(!validatePassword(Password,inputPassword))
    { return;
    }
   
     let res = await axios.post("https://localhost:7039/Api/v1/LoginUser/login",{userName,Password}),
     json = await res.data;

    errorNoEspecifico.innerText = json.errors; 

    if(json.errors.length > 0){
        return;
    } 

    localStorage.setItem("token",json.accessToken)
    localStorage.setItem("rol",json.rol)
    
    redirectForUserRol(json.rol);
  }catch(err){
    errorNoEspecifico.innerText = `Error ${err.response.statusText}`;
  }
}






function redirectForUserRol(role)
{

    switch(role){
      case "Cliente": 
          window.location.href = "/Assets/view/paneles/Cliente.html";
          break;
      case "Propietario":
           window.location.href = "/Assets/view/paneles/Propietario.html";
           break;
       case "Admin":
            window.location.href = "/Assets/view/paneles/Admin.html";
             break;
         default:
            window.location.href = "/Assets/view/Login.html";
         break;   
    }

}



